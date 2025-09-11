using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class qy_GetSendAttachmentsBySecureEmailConfigOutput
    {
        public qy_GetSendAttachmentsBySecureEmailConfigOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList =
                new List<qy_GetSendAttachmentsBySecureEmailConfigOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<qy_GetSendAttachmentsBySecureEmailConfigOutputColumns>
            qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList
            { get; set; }
    }
}
