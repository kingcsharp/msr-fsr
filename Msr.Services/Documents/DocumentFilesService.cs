using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using IronPdf;
using Msr.Commons.Files;
using Msr.Models.Common;
using Msr.Models.PrePro;
using Msr.Repositories;
using Msr.Services.Documents.ViewModels;

namespace Msr.Services.Documents
{
    public class DocumentFilesService
    {
        private readonly MsrDbContext _dbContext;

        public DocumentFilesService()
        {
            _dbContext = new MsrDbContext();
        }
        public List<SelectFile> GetSelectedFiles(string id, string type, string ntlogin)
        {
            var objId = new SqlParameter("@objID", id ?? "0");
            var selecttype = type == null ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);
            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objId, selecttype, ntLogin).ToList();

            return result;
        }

        public List<DocFile> GetSelectedRefFiles(string id, string type, string ntlogin)
        {
            var objId = new SqlParameter("@objID", id ?? "0");

            var selecttype = string.IsNullOrWhiteSpace(type) ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_SpFilesShowForObject  @objID, @type, @strNTLogin", objId, selecttype, ntLogin).ToList();

            return result;
        }
        public DocFile GetSelectedRefFile(string id)
        {
            var result = _dbContext.Database.SqlQuery<DocFile>($"select NAME AS SHOW,DOC_ID AS VALUE,SERVER_PATH as ServerPath, Name, FileUrl, FileKey,ContentType from A_DOCUMENTS where DOC_ID={id}").SingleOrDefault();

            if (result.ServerPath.IndexOf("http:") == 0)
            {
                result.ServerPath = result.ServerPath.Replace("http:", "");
            }
            if (result.ServerPath.IndexOf("https:") == 0)
            {
                result.ServerPath = result.ServerPath.Replace("https:", "");
            }

            return result;
        }
        
        public List<DocFile> GetProcedureSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_ProcedureStepGetRefFilesDialog @procStepID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }
        public List<DocLink> GetDocByObjectId(string id)
        {
            if (id == null) return new List<DocLink>();

            var result = _dbContext.Database.SqlQuery<DocLink>($"SELECT * FROM dbo.A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = '{id}'").ToList();

            return result;
        }

        public List<DocLink> GetDocByObjectId(string id, string type)
        {
            if (id == null) return new List<DocLink>();

            var result = _dbContext.Database.SqlQuery<DocLink>($"SELECT * FROM dbo.A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = '{id}' AND TYPE ='{type}'").ToList();

            return result;
        }
        public List<PreProDockLink> GetPreProRefFileDocLinks(string id, string ntlogin)
        {
            var objectId = id ?? "0";

            var result = _dbContext.Database.SqlQuery<PreProDockLink>($"SELECT *,NAME AS SHOW, DOC_ID AS VALUE,DOC_TYPE AS TYPE  FROM A_V_PROCEDURE_STEP_DOCUMENT_DATA WHERE STEP_ID = {objectId}").ToList();

            return result;
        }

        public string ConvertImageUrlToBase64(string url)
        {
            var sb = new StringBuilder();

            var extension = Path.GetExtension(url);

            var data = "";
            var fileName = MimeTypes.GetTypes(extension.Replace(".", ""));
            if (fileName == "application/pdf")
            {
                data = "data:application/pdf;base64,";
            }
            else if (fileName == "vnd.ms-word")
            {
                data = "data:vnd.ms-word;base64,";
            }
            else if (fileName == "application/zip")
            {
                data = "data:application/zip;base64,";
            }
            else if (fileName == "application/vnd.ms-powerpoint")
            {
                data = url;
                return data;
            }
            else if (fileName == "jpg" || fileName == "png" || fileName == "jpeg" || fileName == "gif")
            {
                data = $"data:image/{extension};base64,";
            }

            bool isUri = url.Contains("http");

            if (!isUri)
            {
                return string.Empty;
            }

            byte[] _byte = GetImage(url);

            sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return data + sb;

        }

        private byte[] GetImage(string url)
        {
            byte[] buf;

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)req.GetResponse();
                var stream = response.GetResponseStream();

                using (var br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception)
            {
                buf = null;
            }

            return (buf);
        }
    }
}
