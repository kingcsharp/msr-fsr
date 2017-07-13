using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Orders
{
    public class UploadedImageView
    {
        public string Id { get; set; }

        public string name { get; set; }

        public long size { get; set; }

        public string url { get; set; }

        public string thumbnailUrl { get; set; }

        public string type { get; set; }

        public string deleteUrl { get; set; }

        public string deleteType { get; set; }

        public string error { get; set; }

        public List<UploadedImageView> MapToDto(List<WorkOrderImageView> orderItemImages)
        {
            List<UploadedImageView> imagesList = new List<UploadedImageView>();

            foreach (var item in orderItemImages)
            {
                var image = new UploadedImageView();
                image.name = item.FILE_NAME;
                image.url = item.Path;
                image.deleteUrl = "/doc/DeleteImageById?Id=" + item.Id + "&taskId=" + item.Task_Id;
                image.deleteType = "GET";
                imagesList.Add(image);
            }
            return imagesList;
        }
    }
}

