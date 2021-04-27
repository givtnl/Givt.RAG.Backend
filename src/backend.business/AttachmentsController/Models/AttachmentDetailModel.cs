using System;

namespace backend.business.AttachmentsController.Models
{
    public class AttachmentDetailModel
    {
        public bool Exists { get; set; }
        public string DownloadUrl { get; set; }
        public DateTime DownloadUrlValidTill { get; set;  }
        public string UploadUrl { get; set; }
        public DateTime UploadUrlValidTill { get; set; }
    }
}