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

        public static string Path = "/Images/";

        public static string thumbPath = "/Images/";

        public List<UploadedImageView> MapToDto(List<WorkOrderImageView> orderItemImages)
        {
            List<UploadedImageView> imagesList = new List<UploadedImageView>();

            foreach (var item in orderItemImages)
            {
                UploadedImageView image = new UploadedImageView();
                image.name = item.FILE_NAME;
                image.url = Path + item.FILE_NAME;
                image.thumbnailUrl = thumbPath + item.FILE_NAME;
               // image.deleteUrl = "/doc/DeleteImageById?Id=" + item.FILE_LINK_ID + "&FillId=" + item.ACTUAL_PART_ID;
                image.deleteType = "GET";

                imagesList.Add(image);
            }
            return imagesList;
        }
    }
}

