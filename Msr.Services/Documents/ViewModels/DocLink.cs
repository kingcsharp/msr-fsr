using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Documents.ViewModels
{
    public class DocLink
    {
        private string SetType { get; set; }
        public string LINKED_DOC_ID { get; set; }
        public string NAME { get; set; }
        public string DELETED { get; set; }
        public string DOC_TYPE { get; set; }
        public string CONTENTTYPE { get; set; }
        public string SERVER_PATH { get; set; }
        public string OBJECT_ID { get; set; }

        public string TYPE
        {
            get => SetType;
            set => GetType(CONTENTTYPE);
        }

        public void GetType(string value)
        {
            switch (value)
            {
                case "image/jpeg":
                    SetType = "image";
                    break;
                case "application/pdf":
                    SetType = "pdf";
                    break;
                case "text/plain":
                    SetType = "text";
                    break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":

                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":

                case "application/vnd.ms-excel":
                    SetType = "office";
                    break;
                default:
                    SetType = "other";
                    break;
            }
        }
    }
}
