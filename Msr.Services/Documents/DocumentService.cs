using EntityFrameworkExtras.EF6;
using IronPdf;
using Msr.Models.Common;
using Msr.Models.Documents;
using Msr.Models.Workflows;
using Msr.Repositories;
using Msr.Services.Documents.Procedures;
using Msr.Services.Documents.ViewModels;
using Msr.Services.Files.ViewModels;
using Msr.Services.Parts.Procedures;
using Msr.Services.S3;
using Msr.Services.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Msr.Services.Documents
{
    public class DocumentService
    {
        private readonly MsrDbContext _dbContext;
        private readonly DocumentFilesService _fileService;
        private readonly AWSFileHandler _fileHandler;
        private readonly UserService _userService;

        public DocumentService()
        {
            _dbContext = new MsrDbContext();
            _fileService = new DocumentFilesService();
            _fileHandler = new AWSFileHandler();
            _userService = new UserService();
        }

        public IQueryable<DocumentView> GetDocumentsQueryable()
        {
            return _dbContext.DocumentViews;
        }

        public DocumentView GetById(string id)
        {
            return GetDocumentsQueryable().Where(x => x.ObjectId == id).SingleOrDefault();
        }

        public List<SelectFile> GetSelectedObjects(string id, string ntlogin)
        {
            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC Portal_GetTheoryObjects  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }

        public List<SelectRole> GetSelectedRoles(string id, string ntlogin)
        {
            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectRole>("EXEC Portal_GetTheoryRoles  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;
        }

        public List<SelectFile> GetSelectedTheories(string id, string ntlogin)
        {

            var objID = new SqlParameter("@ID", id == null ? "0" : id);

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_THEORY_GET_REF_THEORY  @ID, @strNTLogin", objID, NTLogin).ToList();

            return result;

        }

        public bool Save(SaveDocumentViewModel model)
        {
            try
            {
                var saveDocumentProcedure = new SaveDocumentProcedure
                {
                    Id = model.Id,
                    ObjID = model.ObjectId,
                    Company = model.Company,
                    Name = model.Name,
                    Comments = model.Comments,
                    SecurityLevel = model.ApprovalStatus,
                    RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
                    ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
                    ReferenceTheory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null,
                    NTLogin = model.NTLogin

                };

                _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }

        public bool Create(SaveDocumentViewModel model)
        {
            var theory = "";
            try
            {
                if (model.ReferenceTheory.Count == 0)

                {
                    theory = null;
                }
                else
                {
                    theory = model.ReferenceTheory != null ? string.Join(", ", model.ReferenceTheory) : null;

                }

                var saveDocumentProcedure = new SaveDocumentProcedure
                {
                    Company = model.Company,
                    Name = model.Name,
                    Comments = model.Comments,
                    SecurityLevel = model.ApprovalStatus,
                    RolesToView = model.Roles != null ? string.Join(", ", model.Roles) : null,
                    ReferenceObjects = model.ReferenceObject != null ? string.Join(", ", model.ReferenceObject) : null,
                    ReferenceTheory = theory,
                    NTLogin = model.NTLogin
                };

                _dbContext.Database.ExecuteStoredProcedure(saveDocumentProcedure);

                var createdDocument = GetById(saveDocumentProcedure.NewObjID);

                var deleteReferenceFileProcedure = new DeleteFileProcedure() { ObjID = createdDocument.ObjectId, Type = null, NTLogin = model.NTLogin };

                _dbContext.Database.ExecuteStoredProcedure(deleteReferenceFileProcedure);

                if (!string.IsNullOrWhiteSpace(model.ReferenceFiles))
                {
                    foreach (var file in model.ReferenceFiles.Split(','))
                    {
                        var saveFileProcedure = new SaveFileProcedure() { ObjID = createdDocument.ObjectId, DocID = file, Type = null, NTLogin = model.NTLogin };

                        _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }
        }
        public bool SaveSingleFileReference(string objectId, string file, string currentUserId)
        {
            try
            {
                var saveFileProcedure = new SaveFileProcedure() { ObjID = objectId, DocID = file, Type = null, NTLogin = currentUserId };

                _dbContext.Database.ExecuteStoredProcedure(saveFileProcedure);
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }

        }
        public bool RemoveSingleFileReference(string file)
        {
            try
            {
                _dbContext.Database.ExecuteSqlCommand($"DELETE FROM A_DOCUMENT_LINK WHERE LINKED_DOC_ID = '{file}' AND TYPE is NULL");
                return true;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return false;
            }


        }
        public DocLink GetListFileReferences(string id)
        {
            try
            {
                var doc = _dbContext.Database.SqlQuery<DocLink>($"select ID AS LINKED_DOC_ID, NAME ,SERVER_PATH ,CONTENTTYPE,DOC_TYPE,DELETED from  dbo.A_DOCUMENTS where id ='{id}'").SingleOrDefault();

                return doc;
            }
            catch (Exception ex)
            {
                var message = "Error occured:" + ex.Message;

                return new DocLink();
            }
        }

        public bool AddUpdateDocumentHeaderFooter(DocumentView doc, string currentUserId)
        {
            if (doc == null || doc.Status != "APPROVED") return false;

            //Get the Document Data: 
            var approvals = _dbContext.Database.SqlQuery<Approval>(@"SELECT 
	                                                                    o.APPROVAL_DATE AS ApprovalDate
                                                                    ,	o.STATUS AS Status
                                                                    ,	p.FULL_NAME AS FullName
                                                                    ,	wfgs.APPROVE_DATE AS ApproveDate
                                                                    ,   p.POSITION_NAME AS Position
                                                                    FROM A_OBJECTS o
                                                                    LEFT JOIN A_WORKFLOWS_STARTED ws
	                                                                    ON o.WFS_ID = ws.ID
                                                                    LEFT JOIN A_WORKFLOW_GROUP_STARTED wfgs
	                                                                    ON ws.ID = wfgs.WFS_ID
                                                                    LEFT JOIN A_APPROVED_PEOPLE p
	                                                                    ON wfgs.APPROVER = p.ID
                                                                    WHERE OBJ_TABLE = 'A_Theory_History'
		                                                                    AND o.OBJ_ID = @ObjId
                                                                    ORDER BY wfgs.DRCM ", new SqlParameter("@ObjId", doc.Id)).ToList();
            var fileIds = new List<string>();
            if (doc.ReferenceFiles != null)
            {
                if (doc.ReferenceFiles.Contains(","))
                {
                    var fileGroups = doc.ReferenceFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach(var filegroup in fileGroups)
                    {
                        var split = filegroup.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (split.Count() > 1)
                        {
                            fileIds.Add(split[1]);
                        }
                    }
                }
                else
                {
                    var split = doc.ReferenceFiles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Count() > 1)
                    {
                        fileIds.Add(split[1]);
                    }
                }
            }

            var files = new List<DocFile>();

            foreach(var id in fileIds)
            {
                files.Add(_fileService.GetSelectedRefFile(id));
            }

            foreach (var file in files) {
                if (file.ContentType.ToLower() != "application/pdf") continue;
                var renderer = new HtmlToPdf();
                //TODO: Get the file Path
                var stream = _fileHandler.DownloadFromCloud(ConfigurationManager.AppSettings.Get("AWSBuketName"), file.FileKey);
                MemoryStream memStream = new MemoryStream();
                stream.CopyTo(memStream);
                var pdfDoc = new PdfDocument(memStream.ToArray());
                pdfDoc.AppendPdf(renderer.RenderHtmlAsPdf(GetRevisionTable(doc.Root)));

                var creator = _userService.GetUserByObjectId(doc.CreatedBy);
                //TODO: Save document back to 
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
					                        <td style=""font-size:12px;font-weight:bold;"" colspan=5>" + doc.Name + @"</td>
				                        </tr>
				                        <tr class=""data"">
					                        <td colspan=3 style=""font-size:10px;"">Issued by: " + creator.FullName +". "+creator.Title+@"</td>
					                        <td style=""font-size:10px;font-weight:bold;"">Effective Date: "+doc.ApprovalDate.GetValueOrDefault(default(DateTime)).ToString("MM/dd/yyyy")+@"</td>
                                            <td style=""font-size:10px;font-weight:bold;"">Rev. "+doc.Rev+@"</td>
					                        <td style=""font-size:10px;font-weight:bold;""> pg. {page} of {total-pages}</td>
				                        </tr><tr class=""data"">";
                int count = 1;
                foreach (var approval in approvals)
                {
                    if(count % 6 == 0)
                    {
                        fragment += "</tr><tr>";
                    }
                    fragment += @"<td style=""font-size:8px;font-weight:bold;"">
					                    Approved: "+approval.ApproveDate+@"<br/>
					                    "+approval.FullName+", "+approval.Position+@"
					                </td>";
                    count++;
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
				                            <td>WI-0083</td>
				                            <td style=""text-align:center;"">" + doc.Name+@"</td>
				                            <td style=""font-size:10px;"">Rev. "+doc.Rev+@"</td>
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

                _fileHandler.UploadToCloud(pdfDoc.Stream, ConfigurationManager.AppSettings.Get("AWSBuketName"), file.FileKey);

            }

            return true;
        }

        private string GetRevisionTable(string root)
        {
            var historyList = _dbContext.Database.SqlQuery<DocHistory>(@"SELECT 
                                                                          REV
                                                                        , ISNULL(REV_INFO, 'No Information Provided') AS Rev_Info
                                                                        , APPROVAL_DATE
                                                                        FROM A_OBJECTS o
                                                                        WHERE[ROOT] = @Root
                                                                        ORDER BY REV, o.DRCM DESC", new SqlParameter("@Root", root)).ToList();
            var historyTable = "<style> table { border-collapse: collapse; } td, th { border: 1px solid black; } th { background: lightgrey;}</style><table><thead><th>Rev. ID</th><th>Date</th><th>Changes</th></thead><tbody>";
            foreach (var history in historyList)
            {
                historyTable += "<tr><td>" + history.Rev.ToString() + "</td><td>" + (history.Approval_Date.HasValue ? history.Approval_Date.Value.ToString("MMMM dd, yyyy") : "&nbsp;") + "</td><td>" + history.Rev_Info + "</td></tr>";
            }
            
            historyTable += "</tbody></table>";

            return historyTable;
        }
    }
}
