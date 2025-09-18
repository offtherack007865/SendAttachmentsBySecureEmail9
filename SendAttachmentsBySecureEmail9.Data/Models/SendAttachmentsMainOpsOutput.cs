using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class SendAttachmentsMainOpsOutput
    {
        public SendAttachmentsMainOpsOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            EmailWebApiDotNet8BaseUrl = string.Empty;
            EmailSubject = string.Empty;
            EmailFromAddress = string.Empty;
            EmailSubject = string.Empty;

            EmailAddressList = new List<string>();
            EmailBodyLineList = new List<string>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public int NumberOfFilesProcessed { get; set; }
        public string EmailWebApiDotNet8BaseUrl { get; set; }
        public string EmailSubject { get; set; }
        public string EmailFromAddress { get; set; }
        public List<string> EmailAddressList { get; set; }
        public List<string> EmailBodyLineList { get; set; }

        public string EmailBody
        {
            get
            {
                return (EmailBodyLineList.Count > 0) ? string.Join("\r\n", EmailBodyLineList) : string.Empty;
            }
        }
    }
}
