using System;

namespace Msr.Models.PrePro
{
    public class PreProDockLink
    {
        private string SetType { get; set; }
        public string Step_Id { get; set; }
        public string Id { get; set; }
        public string Doc_Id { get; set; }
        public string Name { get; set; }
        public string Source_Id { get; set; }
        public DateTime? Drcm { get; set; }
        public string ModBy { get; set; }
        public string Deleted { get; set; }
        public string Doc_Type { get; set; }
        public string Server_Path { get; set; }
        public string Approved { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public string Contenttype { get; set; }
        public string Object_Id { get; set; }
        public string Creator_Id { get; set; }
        public string Show { get; set; }
        public string Value { get; set; }

        public string TYPE
        {
            get => SetType;
            set => GetType(Contenttype);
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
