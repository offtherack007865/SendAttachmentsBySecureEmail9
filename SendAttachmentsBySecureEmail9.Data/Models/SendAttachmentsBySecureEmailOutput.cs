using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class SendAttachmentsBySecureEmailOutput
    {
        public SendAttachmentsBySecureEmailOutput()
        {
            IsOk = true;

        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<SendAttachmentsBySecureEmailOutputColumns>
            SendAttachmentsBySecureEmailOutputColumnsList { get; set; }
    }
}
