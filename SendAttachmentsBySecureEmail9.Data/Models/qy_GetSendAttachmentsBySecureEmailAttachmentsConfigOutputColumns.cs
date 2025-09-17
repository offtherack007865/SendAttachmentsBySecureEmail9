using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
    {
        public bool Enabled { get; set; }
        public string SystemName { get; set; }
        public string AttachmentReadFolder { get; set; }
        public string AttachmentInputArchiveFolder { get; set; }
        public string EmailToAddresses { get; set; }
        public string EmailSubject { get; set; }
        public string EmailFormatPlainTextOrHtml { get; set; }
        public string EmailBodyStart { get; set; }
        public string EmailBodyEnd { get; set; }
    }
}
