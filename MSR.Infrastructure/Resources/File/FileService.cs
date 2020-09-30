using AutoMapper;
using IronPdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSR.Answer.Domain.Models;
using MSR.Domain.Abstractions;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = MSR.Infrastructure.Resources.EntityFramework.Entities.File;

namespace MSR.Infrastructure.Resources.Services
{
    public class FileService : IFileService
    {
        private readonly IUploadFiles _fileUploader;
        private readonly IDownloadFiles _fileDownloader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FileService(IFileHandlerFactory fileHanderFactory, IUnitOfWork unitOfWork, IMapper mapper, ILogger<FileService> logger)
        {
            _fileUploader = fileHanderFactory.CreateUploader(FileProvider.S3);
            _fileDownloader = fileHanderFactory.CreateDownloader(FileProvider.S3);
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public static string GetURLEncodedBase64(string rawb64, string type)
        {
            return $"data:{type};base64,{rawb64}";
        }

        public Task<bool> CreateDocumentAsync<T>(T entity, int entityId, FileModel file) where T : class
        {
            throw new NotImplementedException();
        }

        public ICollection<FileModel> ListFiles(string entityName, int? entityId = null, int? fileId = null)
        {
            var tableName = mapEntityToTable(entityName ?? "");
            var fileMaps = _unitOfWork.FileEntityMap.Query();
            
            if (fileId.HasValue)
            {
                fileMaps = fileMaps.Where(x => x.FileId == fileId);
            }

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                fileMaps = fileMaps.Where(x => x.EntityTableName == entityName);
            }

            if (entityId.HasValue)
            {
                fileMaps = fileMaps.Where(x => x.EntityId == entityId);
            }

            var ret = new List<FileModel>();
            var files = fileMaps.Select(x => x.FileObject).ToList();

            foreach (var x in files)
            {
                if (ret.Any(i => i.FileId == x.Id))
                {
                    continue;
                }

                var fileURL = _fileDownloader.GetURL(x.FileURL, 6000);
                ret.Add(new FileModel()
                {
                    FileId = x.Id,
                    EntityId = entityId,
                    Base64String = "",
                    ContentType = x.ContentType,
                    Name = x.Name,
                    FileURL = fileURL
                });
            }

            return ret;
        }

        public ICollection<FileModel> ListFilesForEntitySet(string tableName, ICollection<int> entityIds)
        {
            var files = _unitOfWork.FileEntityMap.Query()
                .Include(x => x.FileObject)
                .Where(x => x.EntityTableName == tableName && entityIds.Contains(x.EntityId))
                .Select(x => new FileModel()
                {
                    FileId = x.FileId,
                    Name = x.FileObject.Name,
                    FileURL = _fileDownloader.GetURL(x.FileObject.FileURL, 6000),
                    EntityId = x.EntityId,
                    ContentType = x.FileObject.ContentType
                }).ToList();

            return files;
        }

        public async Task<FileModel> CreateFileAsync(string entityName, int entityId, FileModel file)
        {
            var tableName = mapEntityToTable(entityName);

            var url = await _fileUploader.UploadFile(file, tableName, entityId);

            var efFile = new File()
            {
                ContentType = file.ContentType,
                FileURL = url,
                Name = file.Name
            };

            _unitOfWork.Files.Add(efFile);
            await _unitOfWork.SaveChangesAsync();

            var fileEntityMap = new FileEntityMap()
            {
                EntityId = entityId,
                EntityTableName = tableName,
                FileId = efFile.Id
            };

            _unitOfWork.FileEntityMap.Add(fileEntityMap);
            await _unitOfWork.SaveChangesAsync();

            return new FileModel()
            {
                FileId = efFile.Id,
                EntityId = entityId,
                Name = file.Name,
                Base64String = "",
                ContentType = file.ContentType,
                FileURL = url
            };
        }

        /// <summary>
        /// Map an uploaded file to an entity name + id
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="uploadedFileId"></param>
        /// <returns></returns>
        public async Task<FileModel> MapUploadedFileAsync(string entityName, int entityId, int uploadedFileId)
        {
            var tableName = mapEntityToTable(entityName);

            var fileEntityMap = new FileEntityMap()
            {
                EntityId = entityId,
                EntityTableName = tableName,
                FileId = uploadedFileId
            };

            _unitOfWork.FileEntityMap.Add(fileEntityMap);
            await _unitOfWork.SaveChangesAsync();

            var fileModel = await _unitOfWork.Files
                                    .Query()
                                    .Where(x => x.Id == uploadedFileId)
                                    .Select(x => new FileModel()
                                    {
                                        FileId = x.Id,
                                        EntityId = entityId,
                                        Name = x.Name,
                                        Base64String = "",
                                        ContentType = x.ContentType,
                                        FileURL = x.FileURL
                                    })
                                    .SingleOrDefaultAsync();

            return fileModel;
        }

        public async Task<int> DetachFilesAsync(string entityName, int entityId, int? fileId = null)
        {
            string tableName = mapEntityToTable(entityName);
            int deleteCount = 0;

            if (fileId.HasValue)
            {
                var file = _unitOfWork.FileEntityMap.Query().FirstOrDefault(x =>
                        x.EntityTableName == tableName &&
                        x.EntityId == entityId &&
                        x.FileId == fileId.Value
                    );
                if (file != null)
                {
                    _unitOfWork.FileEntityMap.Delete(false, file);
                    deleteCount++;
                }
            }
            else
            {
                foreach (var e in _unitOfWork.FileEntityMap.Query().Where(x =>
                        x.EntityTableName == tableName &&
                        x.EntityId == entityId
                    ).ToList())
                {
                    _unitOfWork.FileEntityMap.Delete(false, e);
                    deleteCount++;
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return deleteCount;
        }

        public async Task<ICollection<FileModel>> AttachFilesAsync(string entityName, int entityId, ICollection<FileModel> files)
        {
            List<FileModel> ret = new List<FileModel>();
            if (files == null)
            {
                return ret;
            }

            await DetachFilesAsync(entityName, entityId);

            foreach (var file in files)
            {
                ret.Add(await CreateFileAsync(entityName, entityId, file));
            }

            return ret;
        }

        public static string mapEntityToTable(string entityName)
        {
            return entityName.Replace("Model", "");
        }

        public async Task<UploadResponse> UploadHelpFile(UploadFile command)
        {
            var fileModel = _mapper.Map<FileModel>(command);

            var ret = await _fileUploader.UploadHelpFile(fileModel);

            return new UploadResponse()
            {
                URL = ret
            };
        }

        public async Task<UploadResponse> UploadImportFile(UploadFile command)
        {
            var fileModel = _mapper.Map<FileModel>(command);

            var ret = await _fileUploader.UploadImportFile(fileModel);

            return new UploadResponse()
            {
                URL = ret
            };
        }

        public async Task<bool> EditPdfFile(FileModel file, DocumentView document)
        {
            try
            {
                var currentUser = await _unitOfWork.Users.FirstOrDefaultAsync(false, i => i.Id == CurrentUser.GetId());
                var renderer = new HtmlToPdf();
                PdfDocument pdfDoc;

                using (var memStream = new MemoryStream())
                {
                    var stream = await _fileDownloader.DownloadFile(file.FileURL);
                    stream.CopyTo(memStream);
                    pdfDoc = new PdfDocument(memStream.ToArray());
                }

                var pageOneHeader = $@"<style>
			                        .data > div {{
				                        outline: 2px solid black;
				                        outline-offset: -1px;
			                        }}
			                        .row {{
				                        margin-left:1px;
				                        margin-right: 1px;
			                        }}
			                        .container {{
			                            width: 95%;
			                            margin-left: 15px;
			                            border-collapse: collapse;
			                        }}
			
			                        td {{
				                        border:1px solid black;
			                        }}
		                        </style>
		                        <div style=""margin-top:5px;"">
			                        <div style=""margin-left: 15px;margin-bottom:5px;"">
			                            <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
			                        </div>
			                        <table class=""container"">
				                        <tr class=""data"">
					                        <td style=""font-size:12px;font-weight:bold;"">{file.Name.Substring(0, file.Name.IndexOf("."))}</td>
					                        <td style=""font-size:12px;font-weight:bold;"" colspan=5>{document.Name}</td>
				                        </tr>
				                        <tr class=""data"">
					                        <td colspan=3 style=""font-size:10px;"">Issued by: {document.Created.FullName}. {document.Created.Title}</td>
					                        <td style=""font-size:10px;font-weight:bold;"">Effective Date: {DateTime.UtcNow}</td>
                                            <td style=""font-size:10px;font-weight:bold;"">Rev. {document.Revision}</td>
					                        <td style=""font-size:10px;font-weight:bold;""> pg. {{page}} of {{total-pages}}</td>
				                        </tr>
                                        <tr class=""data"">
                                            <td style=""font-size:8px;font-weight:bold;"">Approved: {document.LastUpdatedOn ?? document.CreatedOn}<br/>{currentUser.GetFullName()}, {currentUser.Title}</td>
                                        </tr>
                                </div>";
                pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
                {
                    Height = 30,
                    HtmlFragment = pageOneHeader
                }, false, new int[] { 0 });

                var mainHeader = $@"<style>
		                            table > td {{
			                            outline: 2px solid black;
			                            outline-offset: -1px;
		                            }}
		                            .row {{
			                            margin-left:1px;
			                            margin-right: 1px;
		                            }}
		                            .container {{
			                              width: 95%;
			                              margin-left: 15px;
			                              margin-bottom: 10px;
			                              font-weight:bold;
			                              border-collapse: collapse;
		                            }}
		
		                            td {{
			                            border:1px solid black;
		                            }}
	                            </style>
	                            <div style=""border: .25px solid red;margin-bottom:2px; "">
		                            <div style=""margin-left: 15px; margin-top:10px;margin-bottom:5px;"">
			                            <img height=""50px"" width=""181px"" src=""https://msrfsr.s3-us-west-2.amazonaws.com/DataFiles/Answer2/big-msr-fsr-logo.png""/>
		                            </div>
		                            <table class=""container"">
			                            <tr>
				                            <td></td>
				                            <td style=""text-align:center;"">{document.Name}</td>
				                            <td style=""font-size:10px;"">Rev. {document.Revision}</td>
				                            <td style=""font-size:10px;""> pg. {{page}} of {{total-pages}}</td>
			                            </tr>
		                            </table>
	                            </div>";
                pdfDoc = pdfDoc.AddHTMLHeaders(new HtmlHeaderFooter()
                {
                    Height = 27,
                    HtmlFragment = mainHeader
                }, false, Enumerable.Range(1, pdfDoc.PageCount - 1).ToList());

                var footer = $@"<div style=""border: .25px solid red;padding-bottom:10px;"" >
                                    <hr style=""background-color: blue;height:2px;"" />
                                    <font color=""red"" style=""font-weight:bold;margin-left:50px;"">
                                        Printed copies of this document are not controlled.
                                    </font>
                                    <font color=""red"" style=""font-weight:bold;margin-right:50px;float:right;"">
                                        MSR-FSR Confidential
                                    </font>
                                </div>";
                pdfDoc = pdfDoc.AddHTMLFooters(new HtmlHeaderFooter()
                {
                    HtmlFragment = footer
                }, false, Enumerable.Range(0, pdfDoc.PageCount).ToList());

                pdfDoc.AppendPdf(renderer.RenderHtmlAsPdf(await GetRevisionTable(document)));

                await _fileUploader.UploadFile(pdfDoc.Stream, new FileModel() { Name = file.Name, ContentType = file.ContentType }, nameof(EntityFramework.Entities.Document), document.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }

        private async Task<string> GetRevisionTable(DocumentView document)
        {
            var historyList = await _unitOfWork.ApprovalTransactionLogs.Query().Where(i => i.ApprovalEntity == nameof(EntityFramework.Entities.Document) && i.ApprovalEntityId == document.Id).ToListAsync();

            var historyTable = @"<style> table { border-collapse: collapse; } td, th { border: 1px solid black; } th { background: lightgrey;}</style><h1 style=""margin-top:100px"">Revision History</h1><table><thead><th width=""10%"">Rev. ID</th><th width=""10%"">Date</th><th width=""80%"">Changes</th></thead><tbody>";
            var revision = 0;
            foreach (var history in historyList.OrderBy(i => i.Id))
            {
                historyTable += $@"<tr><td align=""center""><strong>{revision++}</strong></td><td align=""center"">{history.ProcessedOn}</td><td>{history.Comments}</td></tr>";
            }

            historyTable += "</tbody></table>";

            return historyTable;
        }
    }
}
