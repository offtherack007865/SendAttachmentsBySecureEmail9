using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput
    {
        public qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList =
                new List<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns>
            qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList
            { get; set; }
    }
}
