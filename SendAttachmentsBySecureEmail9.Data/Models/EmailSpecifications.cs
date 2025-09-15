using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class EmailSpecifications
    {
        public EmailSpecifications
        (
            string inputSendingMailboxAddress
            , string inputFromAddress
            , string inputToAddressListString
            ,string inputSubjectText
            ,MyConstants.BodyType inputBodyType
            , string inputBodyLinesText
            ,List<string> inputAttachmentsList 
        )
        {
            MySendingMailboxAddress = inputSendingMailboxAddress;
            MyFromAddress = inputFromAddress;
            MyToAddressListString = inputToAddressListString;
            MyBodyType = inputBodyType;
            MySubjectText = inputSubjectText;
            MyBodyLinesText = inputBodyLinesText;
            MyAttachmentsList = inputAttachmentsList;
        }
        public string MySendingMailboxAddress { get; set; }
        public string MyFromAddress { get; set; }
        public string MyToAddressListString { get; set; }
        public string MySubjectText { get; set; }
        public MyConstants.BodyType MyBodyType { get; set; }
        public string MyBodyLinesText { get; set; }
        public List<string> MyAttachmentsList { get; set; }
    }
}
