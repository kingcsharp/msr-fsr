//using IronPdf;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace MSR.Infrastructure.Resources.Services.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ICollection<DocumentView>> GetDocuments(int? id)
        {
            var documentsQuery = _unitOfWork.Documents.Query()
                .Include(d => d.DocumentEntityMaps)
                .Include(d => d.Roles)
                .ThenInclude(r => r.Role)
                .AsQueryable();

            if (id.HasValue && id > 0)
            {
                documentsQuery = documentsQuery.Where(d => d.Id == id);
            }

            var documents = await documentsQuery.ToListAsync();

            var retDocumentViews = _mapper.Map<ICollection<DocumentView>>(documents);

            foreach (var dv in retDocumentViews)
            {
                dv.ReferenceFiles = _fileService.ListFiles(nameof(Document), dv.Id);
            }

            //foreach (var customer in customers.ToList())
            //{
            //    var customerApproval = await _unitOfWork.CustomerApprovals.Query().Include(i => i.Status).FirstOrDefaultAsync(i => i.CustomerId == customer.Id);

            //    var retCustomer = _mapper.Map<Domain.Models.Customer>(customer);
            //    if (customerApproval != null && customerApproval.Status != null)
            //    {
            //        _mapper.Map(customerApproval, retCustomer);
            //        retCustomer.Status = customerApproval.Status.Name;
            //    }
            //    customerList.Add(retCustomer);
            //}

            return retDocumentViews;
        }

        public async Task<DocumentView> CreateDocumentAsync(CreateDocument command)
        {
            if (!CurrentUser.CanApproveActivity(EnumApprovalTables.DocumentApproval))
            {
                var documentApproval = _mapper.Map<DocumentApproval>(command);
                documentApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(documentApproval);
                documentApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(documentApproval.Workflow?.Id ?? 0);
                documentApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                documentApproval.ApprovalJSON = JsonConvert.SerializeObject(new { command.RoleIds, command.ReferenceFileIds });

                await _unitOfWork.DocumentApprovals.AddAsync(documentApproval);

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<DocumentView>(documentApproval);
            }

            var document = _mapper.Map<EntityFramework.Entities.Document>(command);

            document.Revision = 1;
            await _unitOfWork.Documents.AddAsync(document);
            await _unitOfWork.LogApprovalTransaction(document, document.Id);

            var retDocument = _mapper.Map<DocumentView>(document);

            var listDocumentRoles = new List<DocumentRoleMap>();

            foreach (var roleId in command.RoleIds)
            {
                var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == roleId);

                if (role is null)
                {
                    continue;
                }

                var documentRoles = new DocumentRoleMap()
                {
                    DocumentId = retDocument.Id,
                    RoleId = role.Id
                };

                await _unitOfWork.DocumentRoles.AddAsync(documentRoles);

                listDocumentRoles.Add(documentRoles);
            }

            await _unitOfWork.SaveChangesAsync();

            retDocument.RoleIds = command.RoleIds;

            var fileReferences = new List<FileModel>();

            foreach (var fileId in command.ReferenceFileIds)
            {
                if (fileId is null || !fileId.HasValue)
                {
                    continue;
                }

                var fileModel = await _fileService.MapUploadedFileAsync(nameof(Document), retDocument.Id, fileId.Value);

                fileReferences.Add(fileModel);
            }

            retDocument.ReferenceFiles = fileReferences;

            return retDocument;
        }

        public async Task<DocumentView> UpdateDocumentAsync(UpdateDocument command)
        {
            var currentDocument = await _unitOfWork.Documents.Query().Include(d => d.Roles).FirstOrDefaultAsync(i => i.Id == command.Id);

            if (currentDocument is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Document)} not found with ID: {command.Id}");
            }

            currentDocument.Revision += 1;

            DocumentView retDocument;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.DocumentApproval))
            {
                _mapper.Map(command, currentDocument);

                _unitOfWork.Documents.Update(currentDocument);
                await _unitOfWork.LogApprovalTransaction(currentDocument, currentDocument.Id);

                retDocument = _mapper.Map<DocumentView>(currentDocument);

                // Update Document roles mapping
                var currentRoles = currentDocument.Roles.Select(i => i.RoleId).ToList();
                var roleIdsToAdd = command.RoleIds.Where(i => !currentRoles.Contains(i.Value)).ToList();
                var roleIdsToRemove = currentRoles.Where(i => !command.RoleIds.Contains(i)).ToList();

                var rolesToRemove = _unitOfWork.DocumentRoles.Query().Where(i => i.Id == command.Id && roleIdsToRemove.Contains(i.RoleId)).ToList();

                foreach (var addRoleId in roleIdsToAdd)
                {
                    var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == addRoleId);

                    if (role is null) continue;

                    _unitOfWork.DocumentRoles.Add(new DocumentRoleMap()
                    {
                        DocumentId = currentDocument.Id,
                        RoleId = role.Id
                    });
                }

                foreach (var removeRole in rolesToRemove)
                {
                    _unitOfWork.DocumentRoles.Delete(false, removeRole);
                }

                retDocument.RoleIds = currentRoles.Except(roleIdsToRemove).Cast<int?>().ToList();
                retDocument.RoleIds.AddRange(roleIdsToAdd);

                // Update Document files mapping
                var currentFiles = _fileService.ListFiles(nameof(Document), command.Id);

                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                var fileIdsToAdd = command.ReferenceFileIds.Where(i => !currentFileIds.Contains(i.Value)).ToList();
                var fileIdsToRemove = currentFileIds.Where(i => !command.ReferenceFileIds.Contains(i)).ToList();

                foreach (var addFileId in fileIdsToAdd)
                {
                    if (addFileId is null)
                    {
                        continue;
                    }

                    await _fileService.MapUploadedFileAsync(nameof(Document), command.Id, addFileId.Value);
                }

                foreach (var removeFileId in fileIdsToRemove)
                {
                    await _fileService.DetachFilesAsync(nameof(Document), command.Id, removeFileId);
                    //_unitOfWork.DocumentRoles.Delete(false, removeRole);
                }

                retDocument.ReferenceFiles = _fileService.ListFiles(nameof(Document), command.Id);
            }
            else
            {
                var documentApproval = _mapper.Map<DocumentApproval>(currentDocument);
                _mapper.Map(command, documentApproval);

                documentApproval.DocumentId = currentDocument.Id;
                documentApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(documentApproval);
                documentApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(documentApproval.Workflow?.Id ?? 0);
                documentApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                documentApproval.ApprovalJSON = JsonConvert.SerializeObject(new { command.RoleIds, command.ReferenceFileIds });

                await _unitOfWork.DocumentApprovals.UpdateAndSaveChangesAsync(documentApproval);

                retDocument = _mapper.Map<DocumentView>(documentApproval);
            }

            return retDocument;
        }

        //public async Task<UploadResponse> UploadHelpFile(UploadFile command)
        //{
        //    var fileModel = _mapper.Map<FileModel>(command);

        //    var ret = await _fileUploader.UploadHelpFile(fileModel);

        //    return new UploadResponse()
        //    {
        //        URL = ret
        //    };
        //}

        //private readonly MsrDbContext _dbContext;
        //private readonly DocumentFilesService _fileService;
        //private readonly AWSFileHandler _fileHandler;
        //private readonly UserService _userService;

        //public DocumentService()
        //{
        //_dbContext = new MsrDbContext();
        //_fileService = new DocumentFilesService();
        //_fileHandler = new AWSFileHandler();
        //_userService = new UserService();
        //}

        //public IQueryable<DocumentView> GetDocumentsQueryable()
        //{
        //    return _dbContext.DocumentViews;
        //}

        //public DocumentView GetById(string id)
        //{
        //    return GetDocumentsQueryable().Where(x => x.ObjectId == id).SingleOrDefault();
        //}

        //public List<SelectFile> GetSelectedObjects(string id, string ntlogin)
        //{
        //    var objID = new SqlParameter("@ID", id == null ? "0" : id);

        //    var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

        //    var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_GetTheoryObjects  @ID, @strNTLogin", objID, NTLogin).ToList();

        //    return result;
        //}

        //public List<SelectRole> GetSelectedRoles(string id, string ntlogin)
        //{
        //    var objID = new SqlParameter("@ID", id == null ? "0" : id);

        //    var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

        //    var result = _dbContext.Database.SqlQuery<SelectRole>("EXEC Portal_GetTheoryRoles  @ID, @strNTLogin", objID, NTLogin).ToList();

        //    return result;
        //}

        //public List<SelectFile> GetSelectedTheories(string id, string ntlogin)
        //{

        //    var objID = new SqlParameter("@ID", id == null ? "0" : id);

        //    var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

        //    var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_THEORY_GET_REF_THEORY  @ID, @strNTLogin", objID, NTLogin).ToList();

        //    return result;

        //}

        //public bool Save(SaveDocumentViewModel model)
        //{
        //    try
        //    {
        //        var saveDocumentProcedure = new SaveDocumentProcedure
        //        {
        //            Id = model.Id,
        //            ObjID = model.ObjectId,
        //            Company = model.Company,
        //            Name = model.Name,
        //            Comments = model.Comments,
        //            SecurityLevel = model.ApprovalStatus,
        //            RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
        //            ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
        //            ReferenceTheory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null,
        //            NTLogin = model.NTLogin

        //        };

        //        _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Error occured:" + ex.Message;

        //        return false;
        //    }
        //}

        //public bool Create(SaveDocumentViewModel model)
        //{
        //    var theory = "";
        //    try
        //    {
        //        if (model.ReferenceTheory.Count == 0)

        //        {
        //            theory = null;
        //        }
        //        else
        //        {
        //            theory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null;

        //        }

        //        var saveDocumentProcedure = new SaveDocumentProcedure
        //        {
        //            Company = model.Company,
        //            Name = model.Name,
        //            Comments = model.Comments,
        //            SecurityLevel = model.ApprovalStatus,
        //            RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
        //            ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
        //            ReferenceTheory = theory,
        //            NTLogin = model.NTLogin
        //        };

        //        _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

        //        var createdDocument = GetById(saveDocumentProcedure.NewObjID);

        //        var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = createdDocument.ObjectId, Type = null, NTLogin = model.NTLogin };

        //        _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

        //        if (!string.IsNullOrWhiteSpace(model.ReferenceFiles))
        //        {
        //            foreach (var file in model.ReferenceFiles.Split(','))
        //            {
        //                var saveFileProcedure = new SaveFileProcedure() { ObjID = createdDocument.ObjectId, DocID = file, Type = null, NTLogin = model.NTLogin };

        //                _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Error occured:" + ex.Message;

        //        return false;
        //    }
        //}
        //public bool SaveSingleFileReference(string objectId, string file, string currentUserId)
        //{
        //    try
        //    {
        //        var saveFileProcedure = new SaveFileProcedure() { ObjID = objectId, DocID = file, Type = null, NTLogin = currentUserId };

        //        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Error occured:" + ex.Message;

        //        return false;
        //    }

        //}
        //public bool RemoveSingleFileReference(string file)
        //{
        //    try
        //    {
        //        _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_DOCUMENT_LINK WHERE LINKED_DOC_ID = '{file}' AND TYPE is NULL");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Error occured:" + ex.Message;

        //        return false;
        //    }


        //}
        //public DocLink GetListFileReferences(string id)
        //{
        //    try
        //    {
        //        var doc = _dbContext.Database.SqlQuery<DocLink>($"select ID AS LINKED_DOC_ID, NAME ,SERVER_PATH ,CONTENTTYPE,DOC_TYPE,DELETED from  dbo.A_DOCUMENTS where id ='{id}'").SingleOrDefault();

        //        return doc;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "Error occured:" + ex.Message;

        //        return new DocLink();
        //    }
        //}

        //public bool AddUpdateDocumentHeaderFooter(DocumentView doc, string currentUserId)
        //{
        //    if (doc == null || doc.Status != "APPROVED") return false;
        //    if (!License.IsValidLicense("IRONPDF-138372CE75-686825-423338-419A44C0B1-F5AA4668-UEx8129778D4BA18D8-CMHWORKSLLC.IRO190627.4855.33211.PRO.1DEV.1YR.SUPPORTED.UNTIL.27.JUN.2020"))
        //    {

        // 


        //        IronPdf.License.LicenseKey = "IRONPDF-4902910849-363729-BAAF04-97D98E2A51-F1625104-UEx206F01DF48979D8-CMHWORKSLLC.IRO200914.4583.55139.PROJECT.1DEV.SUB.SUPPORTED.UNTIL.17.SEP.2021";
        //    }
        //    //Get the Document Data: 
        //    var approvals = _dbContext.Database.SqlQuery<Approval>(@"SELECT 
        //                                                             o.APPROVAL_DATE AS ApprovalDate
        //                                                            ,	o.STATUS AS Status
        //                                                            ,	p.FULL_NAME AS FullName
        //                                                            ,	wfgs.APPROVE_DATE AS ApproveDate
        //                                                            ,   p.POSITION_NAME AS Position
        //                                                            FROM A_OBJECTS o
        //                                                            LEFT JOIN A_WORKFLOWS_STARTED ws
        //                                                             ON o.WFS_ID = ws.ID
        //                                                            LEFT JOIN A_WORKFLOW_GROUP_STARTED wfgs
        //                                                             ON ws.ID = wfgs.WFS_ID
        //                                                            LEFT JOIN A_APPROVED_PEOPLE p
        //                                                             ON wfgs.APPROVER = p.ID
        //                                                            WHERE OBJ_TABLE = 'A_Theory_History'
        //                                                              AND o.OBJ_ID = @ObjId
        //                                                            ORDER BY wfgs.DRCM ", new SqlParameter("@ObjId", doc.Id)).ToList();
        //    var fileIds = new List<string>();
        //    if (doc.ReferenceFiles != null)
        //    {
        //        if (doc.ReferenceFiles.Contains(","))
        //        {
        //            var fileGroups = doc.ReferenceFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        //            foreach (var filegroup in fileGroups)
        //            {
        //                var split = filegroup.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //                if (split.Count() > 1)
        //                {
        //                    fileIds.Add(split[1]);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var split = doc.ReferenceFiles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //            if (split.Count() > 1)
        //            {
        //                fileIds.Add(split[1]);
        //            }
        //        }
        //    }

        //    var files = new List<DocFile>();

        //    foreach (var id in fileIds)
        //    {
        //        files.Add(_fileService.GetSelectedRefFile(id));
        //    }

        //    foreach (var file in files)
        //    {
        //        try
        //        {
        //            if (file.ContentType.ToLower() != "application/pdf") continue;
        //            var renderer = new HtmlToPdf();
        //            //TODO: Get the file Path
        //            var stream = _fileHandler.DownloadFromCloud(ConfigurationManager.AppSettings.Get("AWSBuketName"), file.FileKey);
        //            MemoryStream memStream = new MemoryStream();
        //            stream.CopyTo(memStream);
        //            var pdfDoc = new PdfDocument(memStream.ToArray());
        //            pdfDoc.AppendPdf(renderer.RenderHtmlAsPdf(GetRevisionTable(doc.Root)));

        //            var creator = _userService.GetUserByObjectId(doc.CreatedBy);
        //            //TODO: Save document back to 
        //            var fragment = @"<style>
        //                   .data > div {
        //                    outline: 2px solid black;
        //                    outline-offset: -1px;
        //                   }
        //                   .row {
        //                    margin-left:1px;
        //                    margin-right: 1px;
        //                   }
        //                   .container {
        //                     width: 95%;
        //                     margin-left: 15px;
        //                     border-collapse: collapse;
        //                   }

        //                   td {
        //                    border:1px solid black;
        //                   }
        //                  </style>
        //                  <div style=""margin-top:5px;"">
        //                   <div style=""margin-left: 15px;margin-bottom:5px;"">
        //                       <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
        //                   </div>
        //                   <table class=""container"">
        //                    <tr class=""data"">
        //                     <td style=""font-size:12px;font-weight:bold;"">" + file.Name.Substring(0, file.Name.IndexOf(".")) + @"</td>
        //                     <td style=""font-size:12px;font-weight:bold;"" colspan=5>" + doc.Name + @"</td>
        //                    </tr>
        //                    <tr class=""data"">
        //                     <td colspan=3 style=""font-size:10px;"">Issued by: " + creator.FullName + ". " + creator.Title + @"</td>
        //                     <td style=""font-size:10px;font-weight:bold;"">Effective Date: " + doc.ApprovalDate.GetValueOrDefault(default(DateTime)).ToString("MM/dd/yyyy") + @"</td>
        //                                    <td style=""font-size:10px;font-weight:bold;"">Rev. " + doc.Rev + @"</td>
        //                     <td style=""font-size:10px;font-weight:bold;""> pg. {page} of {total-pages}</td>
        //                    </tr><tr class=""data"">";
        //            int count = 1;
        //            foreach (var approval in approvals)
        //            {
        //                if (count % 6 == 0)
        //                {
        //                    fragment += "</tr><tr>";
        //                }
        //                fragment += @"<td style=""font-size:8px;font-weight:bold;"">
        //                 Approved: " + approval.ApproveDate + @"<br/>
        //                 " + approval.FullName + ", " + approval.Position + @"
        //             </td>";
        //                count++;
        //            }

        //            fragment += "</tr></table></div>";
        //            pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
        //            {
        //                Height = 30,
        //                HtmlFragment = fragment
        //            }, false, new int[] { 0 });

        //            pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
        //            {
        //                Height = 27,
        //                HtmlFragment = @"<style>
        //                      table > td {
        //                       outline: 2px solid black;
        //                       outline-offset: -1px;
        //                      }
        //                      .row {
        //                       margin-left:1px;
        //                       margin-right: 1px;
        //                      }
        //                      .container {
        //                         width: 95%;
        //                         margin-left: 15px;
        //                         margin-bottom: 10px;
        //                         font-weight:bold;
        //                         border-collapse: collapse;
        //                      }

        //                      td {
        //                       border:1px solid black;
        //                      }
        //                     </style>
        //                     <div style=""border: .25px solid red;margin-bottom:2px; "">
        //                      <div style=""margin-left: 15px; margin-top:10px;margin-bottom:5px;"">
        //                       <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
        //                      </div>
        //                      <table class=""container"">
        //                       <tr>
        //                        <td></td>
        //                        <td style=""text-align:center;"">" + doc.Name + @"</td>
        //                        <td style=""font-size:10px;"">Rev. " + doc.Rev + @"</td>
        //                        <td style=""font-size:10px;""> pg. {page} of {total-pages}</td>
        //                       </tr>
        //                      </table>
        //                     </div>"
        //            }, false, Enumerable.Range(1, pdfDoc.PageCount - 1).ToList());

        //            pdfDoc = pdfDoc.AddHTMLFooters(new HtmlHeaderFooter()
        //            {
        //                HtmlFragment = $@"<div style=""border: .25px solid red;padding-bottom:10px;"" >
        //                            <hr style=""background-color: blue;height:2px;"" />
        //                            <font color=""red"" style=""font-weight:bold;margin-left:50px;"">
        //                                Printed copies of this document are not controlled.
        //                            </font>
        //                            <font color=""red"" style=""font-weight:bold;margin-right:50px;float:right;"">
        //                                MSR-FSR Confidential
        //                            </font>
        //                        </div>"
        //            }, false, Enumerable.Range(0, pdfDoc.PageCount).ToList());

        //            _fileHandler.UploadToCloud(pdfDoc.Stream, ConfigurationManager.AppSettings.Get("AWSBuketName"), file.FileKey);
        //        }
        //        catch (Exception ex)
        //        {
        //            var message = "Error occured:" + ex.Message;
        //        }
        //    }

        //    return true;
        //}

        //private string GetRevisionTable(string root)
        //{
        //    var historyList = _dbContext.Database.SqlQuery<DocHistory>(@"SELECT 
        //                                                                  REV
        //                                                                , ISNULL(REV_INFO, 'No Information Provided') AS Rev_Info
        //                                                                , APPROVAL_DATE
        //                                                                FROM A_OBJECTS o
        //                                                                WHERE[ROOT] = @Root
        //                                                                ORDER BY REV, o.DRCM DESC", new SqlParameter("@Root", root)).ToList();
        //    var historyTable = @"<style> table { border-collapse: collapse; } td, th { border: 1px solid black; } th { background: lightgrey;}</style><h1 style=""margin-top:100px"">Revision History</h1><table><thead><th width=""10%"">Rev. ID</th><th width=""10%"">Date</th><th width=""80%"">Changes</th></thead><tbody>";
        //    foreach (var history in historyList)
        //    {
        //        historyTable += @"<tr><td align=""center""><strong>" + history.Rev.ToString() + @"</strong></td><td align=""center"">" + (history.Approval_Date.HasValue ? history.Approval_Date.Value.ToString("MM/dd/yyyy") : "&nbsp;") + "</td><td>" + history.Rev_Info + "</td></tr>";
        //    }

        //    historyTable += "</tbody></table>";

        //    return historyTable;
        //}
    }
}
