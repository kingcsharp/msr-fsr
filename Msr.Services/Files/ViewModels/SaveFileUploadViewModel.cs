using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Files;

namespace Msr.Services.Files.ViewModels
{
    public class SaveFileUploadViewModel
    {
        public string Id { get; set; }
        public string DocId { get; set; }
        public string OldDocId { get; set; }
        public string Name { get; set; }
        [DisplayName("Keyworkds :")]
        public string Desc { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string SrcId { get; set; }
        [DisplayName("Old Source File Name :")]
        public string SrcName { get; set; }
        [DisplayName("Keyworkds :")]
        public string SrcDesc { get; set; }
        public string SrcPath { get; set; }
        public string SrcContentType { get; set; }
        public string SrcChanged { get; set; }
        public string DocChanged { get; set; }
        public string DropSrc { get; set; }
        public string NTLogin { get; set; }
        public string FileUrl { get; set; }
        public string FileKey { get; set; }

        public SaveFileUploadViewModel MapToDto(FileView model)
        {
            return new SaveFileUploadViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Desc = model.Description,
                SrcName = model.Name,
                SrcDesc = model.Description

            };
        }
    }

}
