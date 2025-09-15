using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
//using Office = Microsoft.Office.Core;
using SendAttachmentsBySecureEmail9.Data.Models;

using System.Runtime.InteropServices.JavaScript;
using static SendAttachmentsBySecureEmail9.Data.MyConstants;

namespace SendAttachmentsBySecureEmail9.WebApiLand.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpsController));

        public OpsController(SimplifyVbcAdt8Context inputNrcContext)
        {
            MyContext = inputNrcContext;

            log.Info($"Start of OpsController Connection String:  {MyContext.MyConnectionString}");

        }
        public SimplifyVbcAdt8Context MyContext { get; set; }


        // GET /api/Ops/qy_GetSendAttachmentsBySecureEmailConfig
        [HttpGet]
        public qy_GetSendAttachmentsBySecureEmailConfigOutput
                    qy_GetSendAttachmentsBySecureEmailConfig
                    (
                    )
        {
            qy_GetSendAttachmentsBySecureEmailConfigOutput
                returnOutput =
                    new qy_GetSendAttachmentsBySecureEmailConfigOutput();


            string sql = $"opsemail.qy_GetSendAttachmentsBySecureEmailConfig";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList =
                    MyContext
                    .qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList
                    .FromSqlRaw<qy_GetSendAttachmentsBySecureEmailConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }

            return returnOutput;
        }

        // GET /api/Ops/SendAttachmentsBySecureEmail
        [HttpGet]
        public SendAttachmentsBySecureEmailOutput
                    SendAttachmentsBySecureEmail
                    (
                       [FromBody] EmailSpecifications inputEmailSpecifications
                    )
        {
            SendAttachmentsBySecureEmailOutput
                 returnOutput =
                    new SendAttachmentsBySecureEmailOutput();

            try
            {
                //Get Outlook COM objects
                Outlook.Application app = new Outlook.Application();
                Outlook.MailItem newMail = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);

                //Parse 'sToAddress'
                if (!string.IsNullOrWhiteSpace(inputEmailSpecifications.MyToAddressListString))
                {
                    string[] arrAddTos = inputEmailSpecifications.MyToAddressListString.Split(new char[] { ';', ',' });
                    foreach (string strAddr in arrAddTos)
                    {
                        if (!string.IsNullOrWhiteSpace(strAddr) &&
                            strAddr.IndexOf('@') != -1)
                        {
                            newMail.Recipients.Add(strAddr.Trim());
                        }
                        else
                            throw new Exception("Bad to-address: " + inputEmailSpecifications.MyToAddressListString);
                    }
                }
                else
                    throw new Exception("Must specify to-address");

                //Set type of message
                switch (inputEmailSpecifications.MyBodyType)
                {
                    case BodyType.HTML:
                        newMail.HTMLBody = inputEmailSpecifications.MyBodyLinesText;
                        break;
                    case BodyType.RTF:
                        newMail.RTFBody = inputEmailSpecifications.MyBodyLinesText;
                        break;
                    case BodyType.PlainText:
                        newMail.Body = inputEmailSpecifications.MyBodyLinesText;
                        break;
                    default:
                        throw new Exception("Bad email body type: " + inputEmailSpecifications.MyBodyType);
                }


                if (inputEmailSpecifications.MyAttachmentsList != null)
                {
                    //Add attachments
                    foreach (string strPath in inputEmailSpecifications.MyAttachmentsList)
                    {
                        if (System.IO.File.Exists(strPath))
                        {
                            newMail.Attachments.Add(strPath);
                        }
                        else
                        {
                            returnOutput.IsOk = false;

                            string myErrorMessage = $"Attachment file is not found: {strPath}";
                            returnOutput.ErrorMessage = myErrorMessage;
                            return returnOutput;
                        }
                    }
                }

                //Add subject
                if (!string.IsNullOrWhiteSpace(inputEmailSpecifications.MySubjectText))
                    newMail.Subject = inputEmailSpecifications.MySubjectText;

                Outlook.Accounts accounts = app.Session.Accounts;
                Outlook.Account acc = null;

                //Look for our account in the Outlook
                foreach (Outlook.Account account in accounts)
                {
                    if (account.SmtpAddress.Equals(inputEmailSpecifications.MySendingMailboxAddress, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //Use it
                        acc = account;
                        break;
                    }
                }

                //Did we get the account
                if (acc != null)
                {
                    //Use this account to send the e-mail. 
                    newMail.SendUsingAccount = acc;

                    //And send it
                    ((Outlook._MailItem)newMail).Send();

                    //Done

                }
                else
                {
                    returnOutput.IsOk = false;

                    string myErrorMessage = $"Account {inputEmailSpecifications.MySendingMailboxAddress} does not exist in Outlook.";
                    returnOutput.ErrorMessage = myErrorMessage;
                    return returnOutput;
                }

            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }

            return returnOutput;
        }
    }
}
