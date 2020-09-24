using IronPdf;
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
        private readonly string IronPDF_LicenseKey = "IRONPDF-4902910849-363729-BAAF04-97D98E2A51-F1625104-UEx206F01DF48979D8-CMHWORKSLLC.IRO200914.4583.55139.PROJECT.1DEV.SUB.SUPPORTED.UNTIL.17.SEP.2021";

        private readonly IUploadFiles _fileUploader;
        private readonly IDownloadFiles _fileDownloader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IFileHandlerFactory fileHanderFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;

            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
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
                dv.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), dv.Id);

                dv.RoleIds = documents.Where(d => d.Id == dv.Id).FirstOrDefault().Roles.Select(r => r.Id).Cast<int?>().ToList();
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

                var fileModel = await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Document), retDocument.Id, fileId.Value);

                fileReferences.Add(fileModel);
            }

            retDocument.ReferenceFiles = fileReferences;

            //await AddUpdateDocumentHeaderFooterAsync(retDocument, document);

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
                var currentFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), command.Id);

                var currentFileIds = currentFiles.Select(i => i.FileId).ToList();
                var fileIdsToAdd = command.ReferenceFileIds.Where(i => !currentFileIds.Contains(i.Value)).ToList();
                var fileIdsToRemove = currentFileIds.Where(i => !command.ReferenceFileIds.Contains(i)).ToList();

                foreach (var addFileId in fileIdsToAdd)
                {
                    if (addFileId is null)
                    {
                        continue;
                    }

                    await _fileService.MapUploadedFileAsync(nameof(EntityFramework.Entities.Document), command.Id, addFileId.Value);
                }

                foreach (var removeFileId in fileIdsToRemove)
                {
                    await _fileService.DetachFilesAsync(nameof(EntityFramework.Entities.Document), command.Id, removeFileId);
                }

                retDocument.ReferenceFiles = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), command.Id);

                //await AddUpdateDocumentHeaderFooterAsync(retDocument, currentDocument);
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

        public async Task DeleteDocumentAsync(DeleteDocument command)
        {
            var document = await _unitOfWork.Documents.Query()
                                .Include(d => d.DocumentEntityMaps)
                                .Include(d => d.Roles)
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            var documentApproval = await _unitOfWork.DocumentApprovals.FirstOrDefaultAsync(false, i => i.DocumentId == command.Id);

            if (document is null && documentApproval is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Document)} not found", DomainError.BadRequest);
            }

            if (document != null)
            {
                foreach (var role in document.Roles)
                {
                    _unitOfWork.DocumentRoles.Delete(false, role);
                }

                foreach (var map in document.DocumentEntityMaps)
                {
                    _unitOfWork.DocumentEntityMap.Delete(false, map);
                }
                
                _unitOfWork.Documents.Delete(false, document);

            }

            if (documentApproval != null)
            {
                _unitOfWork.DocumentApprovals.Delete(false, documentApproval);
            }

            await _unitOfWork.SaveChangesAsync();
        }


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

        public async Task<bool> AddUpdateDocumentHeaderFooterAsync(DocumentView dv, EntityFramework.Entities.Document doc)
        {
            if (!License.IsValidLicense(IronPDF_LicenseKey))
            {
                IronPdf.License.LicenseKey = IronPDF_LicenseKey;
            }

            var currentUserId = CurrentUser.GetId();

            //Get the Document Data: 
            //var approvals = _dbContext.Database.SqlQuery<Approval>(@"SELECT 
            //                                                         o.APPROVAL_DATE AS ApprovalDate
            //                                                        ,	o.STATUS AS Status
            //                                                        ,	p.FULL_NAME AS FullName
            //                                                        ,	wfgs.APPROVE_DATE AS ApproveDate
            //                                                        ,   p.POSITION_NAME AS Position
            //                                                        FROM A_OBJECTS o
            //                                                        LEFT JOIN A_WORKFLOWS_STARTED ws
            //                                                         ON o.WFS_ID = ws.ID
            //                                                        LEFT JOIN A_WORKFLOW_GROUP_STARTED wfgs
            //                                                         ON ws.ID = wfgs.WFS_ID
            //                                                        LEFT JOIN A_APPROVED_PEOPLE p
            //                                                         ON wfgs.APPROVER = p.ID
            //                                                        WHERE OBJ_TABLE = 'A_Theory_History'
            //                                                          AND o.OBJ_ID = @ObjId
            //                                                        ORDER BY wfgs.DRCM ", new SqlParameter("@ObjId", doc.Id)).ToList();
            //var fileIds = new List<string>();
            //if (doc.ReferenceFiles != null)
            //{
            //    if (doc.ReferenceFiles.Contains(","))
            //    {
            //        var fileGroups = doc.ReferenceFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //        foreach (var filegroup in fileGroups)
            //        {
            //            var split = filegroup.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //            if (split.Count() > 1)
            //            {
            //                fileIds.Add(split[1]);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var split = doc.ReferenceFiles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (split.Count() > 1)
            //        {
            //            fileIds.Add(split[1]);
            //        }
            //    }
            //}

            var files = _fileService.ListFiles(nameof(EntityFramework.Entities.Document), dv.Id);

            //foreach (var id in fileIds)
            //{
            //    files.Add(_fileService.GetSelectedRefFile(id));
            //}
            foreach (var file in files.Where(f => string.Equals(f.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    var renderer = new HtmlToPdf();

                    PdfDocument pdfDoc;

                    using (var memStream = new MemoryStream())
                    {
                        var stream = await _fileDownloader.DownloadFile(file.FileURL);
                        stream.CopyTo(memStream);
                        pdfDoc = new PdfDocument(memStream.ToArray());
                    }

                    pdfDoc.AppendPdf(renderer.RenderHtmlAsPdf(GetRevisionTable(dv)));

                    var creator = doc.Created;

                    //TODO
                    var fragment = @"<style>
			                        .data > div {
				                        outline: 2px solid black;
				                        outline-offset: -1px;
			                        }
			                        .row {
				                        margin-left:1px;
				                        margin-right: 1px;
			                        }
			                        .container {
			                          width: 95%;
			                          margin-left: 15px;
			                          border-collapse: collapse;
			                        }
			
			                        td {
				                        border:1px solid black;
			                        }
		                        </style>
		                        <div style=""margin-top:5px;"">
			                        <div style=""margin-left: 15px;margin-bottom:5px;"">
			                            <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
			                        </div>
			                        <table class=""container"">
				                        <tr class=""data"">
					                        <td style=""font-size:12px;font-weight:bold;"">" + file.Name.Substring(0, file.Name.IndexOf(".")) + @"</td>
					                        <td style=""font-size:12px;font-weight:bold;"" colspan=5>" + dv.Name + @"</td>
				                        </tr>
				                        <tr class=""data"">
					                        <td colspan=3 style=""font-size:10px;"">Issued by: " + creator.GetFullName() + ". " + creator.Title + @"</td>
					                        <td style=""font-size:10px;font-weight:bold;"">Effective Date: " + dv.LastUpdatedOn.GetValueOrDefault(default(DateTime)).ToString("MM/dd/yyyy") + @"</td>
                                            <td style=""font-size:10px;font-weight:bold;"">Rev. " + dv.Revision + @"</td>
					                        <td style=""font-size:10px;font-weight:bold;""> pg. {page} of {total-pages}</td>
				                        </tr><tr class=""data"">";
                    //int count = 1;

                    // TODO

                    var approval = await _unitOfWork.DocumentApprovals.Query().Where(d => d.Status.Id == (int)ApprovalStatusEnum.Complete).SingleOrDefaultAsync(d => d.DocumentId == dv.Id);

                    if (approval != null)
                    {
                        //    if (count % 6 == 0)
                        //    {
                        fragment += "</tr><tr>";
                        //    }
                        fragment += @"<td style=""font-size:8px;font-weight:bold;"">
                             Approved: " + approval.LastUpdatedOn + @"<br/>
                             " + approval.LastUpdated.GetFullName() + ", " + "?Position?" + @"
                         </td>";
                        //    count++;
                    }

                    fragment += "</tr></table></div>";

                    pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
                    {
                        Height = 30,
                        HtmlFragment = fragment
                    }, false, new int[] { 0 });

                    pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
                    {
                        Height = 27,
                        HtmlFragment = @"<style>
		                            table > td {
			                            outline: 2px solid black;
			                            outline-offset: -1px;
		                            }
		                            .row {
			                            margin-left:1px;
			                            margin-right: 1px;
		                            }
		                            .container {
			                              width: 95%;
			                              margin-left: 15px;
			                              margin-bottom: 10px;
			                              font-weight:bold;
			                              border-collapse: collapse;
		                            }
		
		                            td {
			                            border:1px solid black;
		                            }
	                            </style>
	                            <div style=""border: .25px solid red;margin-bottom:2px; "">
		                            <div style=""margin-left: 15px; margin-top:10px;margin-bottom:5px;"">
			                            <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
		                            </div>
		                            <table class=""container"">
			                            <tr>
				                            <td></td>
				                            <td style=""text-align:center;"">" + dv.Name + @"</td>
				                            <td style=""font-size:10px;"">Rev. " + dv.Revision + @"</td>
				                            <td style=""font-size:10px;""> pg. {page} of {total-pages}</td>
			                            </tr>
		                            </table>
	                            </div>"
                    }, false, Enumerable.Range(1, pdfDoc.PageCount - 1).ToList());

                    pdfDoc = pdfDoc.AddHTMLFooters(new HtmlHeaderFooter()
                    {
                        HtmlFragment = $@"<div style=""border: .25px solid red;padding-bottom:10px;"" >
                                    <hr style=""background-color: blue;height:2px;"" />
                                    <font color=""red"" style=""font-weight:bold;margin-left:50px;"">
                                        Printed copies of this document are not controlled.
                                    </font>
                                    <font color=""red"" style=""font-weight:bold;margin-right:50px;float:right;"">
                                        MSR-FSR Confidential
                                    </font>
                                </div>"
                    }, false, Enumerable.Range(0, pdfDoc.PageCount).ToList());

                    //file.Base64String = pdfDoc.Stream;

                    await _fileUploader.UploadStream(pdfDoc.Stream, file.Name, file.ContentType, nameof(EntityFramework.Entities.Document), dv.Id);

                    //_fileHandler.UploadToCloud(pdfDoc.Stream, ConfigurationManager.AppSettings.Get("AWSBuketName"), file.FileKey);
                }
                catch (Exception ex)
                {
                    var message = "Error occured:" + ex.Message;
                }
            }

            return true;
        }

        private string GetRevisionTable(DocumentView dv)
        {
            //var historyList = _dbContext.Database.SqlQuery<DocHistory>(@"SELECT 
            //                                                              REV
            //                                                            , ISNULL(REV_INFO, 'No Information Provided') AS Rev_Info
            //                                                            , APPROVAL_DATE
            //                                                            FROM A_OBJECTS o
            //                                                            WHERE[ROOT] = @Root
            //                                                            ORDER BY REV, o.DRCM DESC", new SqlParameter("@Root", root)).ToList();

            var historyTable = @"<style> table { border-collapse: collapse; } td, th { border: 1px solid black; } th { background: lightgrey;}</style><h1 style=""margin-top:100px"">Revision History</h1><table><thead><th width=""10%"">Rev. ID</th><th width=""10%"">Date</th><th width=""80%"">Changes</th></thead><tbody>";

            // TODO: List of HISTORY
            //foreach (var history in historyList)
            //{
            //    historyTable += @"<tr><td align=""center""><strong>" + history.Rev.ToString() + @"</strong></td><td align=""center"">" + (history.Approval_Date.HasValue ? history.Approval_Date.Value.ToString("MM/dd/yyyy") : "&nbsp;") + "</td><td>" + history.Rev_Info + "</td></tr>";
            //}

            historyTable += @"<tr><td align=""center""><strong>" + dv.Revision + @"</strong></td><td align=""center"">" + "Approval Date in MM/dd/yyyy" + "</td><td>" + "Rev_Info" + "</td></tr>";

            historyTable += "</tbody></table>";

            return historyTable;
        }
    }
}
