using EmailWebApiLand9.CallEmailWebApiLand;
using EmailWebApiLand9.Data.Models;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using SendAttachmentsBySecureEmail9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace SendAttachmentBySecureEmail9.CallEmailWebApiConsoleApp
{
    public class SendAttachmentsMainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SendAttachmentsMainOps));
        public SendAttachmentsMainOps
                (
                    SendAttachmentsConfigOptions inputSendAttachmentsConfigOptions
                    , qy_GetSendAttachmentsBySecureEmailConfigOutputColumns inputqy_GetSendAttachmentsBySecureEmailConfigOutputColumns
                    , List<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns> inputqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList
                )
        {
            MySendAttachmentsConfigOptions =
                inputSendAttachmentsConfigOptions;
            Myqy_GetSendAttachmentsBySecureEmailConfigOutputColumns =
                inputqy_GetSendAttachmentsBySecureEmailConfigOutputColumns;
            Myqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList =
                inputqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList;
        }

        // File Configuration Options
        public SendAttachmentsConfigOptions MySendAttachmentsConfigOptions { get; set; }

        // Application Configuration (from database) Options
        public qy_GetSendAttachmentsBySecureEmailConfigOutputColumns Myqy_GetSendAttachmentsBySecureEmailConfigOutputColumns { get; set; }

        // Attachment System Configuration (from database) Options
        public List<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns> Myqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList { get; set; }

        public SendAttachmentsMainOpsOutput DoIt()
        {
            SendAttachmentsMainOpsOutput returnOutput =
                new SendAttachmentsMainOpsOutput();
            foreach (qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                        loopSystem in Myqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList)
            {
                List<string>
                    myListOfAttachmentFullFilenames
                        = GetBlanklessFullFilenamesForSystem(loopSystem);
                log.Debug($"myListOfAttachmentFullFilenames.Count = {myListOfAttachmentFullFilenames.Count.ToString()}");
                if (myListOfAttachmentFullFilenames.Count > 0)
                {
                    ProcessAttachmentsSystem
                    (
                        loopSystem
                        , myListOfAttachmentFullFilenames
                    );
                }
            }
            return returnOutput;
        }
        public List<string> GetBlanklessFullFilenamesForSystem(qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns inputSystem)
        {
            List<string> 
                returnOutput =
                    new List<string>();
            List<string>
                myListOfAttachmentFullFilenames
                    = Directory.GetFiles(inputSystem.AttachmentReadFolder, $"*.*").ToList();

            foreach (string loopFullFilename in myListOfAttachmentFullFilenames)
            {
                FileInfo loopFi = new FileInfo(loopFullFilename);
                if (loopFi.Name.Contains(" "))
                {
                    string blanklessFilename = loopFi.Name.Replace(" ", "_");
                    string blanklessFullFilename =
                        Path.Combine(loopFi.DirectoryName, blanklessFilename);
                    if (File.Exists(blanklessFullFilename))
                    {
                        File.Delete(blanklessFullFilename);
                    }
                    File.Copy(loopFi.FullName, blanklessFullFilename);
                    if (File.Exists(loopFi.FullName))
                    {
                        File.Delete(loopFi.FullName);
                    }
                    returnOutput.Add(blanklessFullFilename);
                }
                else
                {
                    returnOutput.Add(loopFi.FullName);
                }
            }
            return returnOutput;
        }

        public
            ProcessAttachmentsSystemOutput
                ProcessAttachmentsSystem
                (
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                        inputSystem
                    , List<string>
                        inputListOfAttachmentFullFilenames
                )
        {
            ProcessAttachmentsSystemOutput
                returnOutput =
                    new ProcessAttachmentsSystemOutput();

            string myFromAddress = string.Empty;
            string myToAddressListString = string.Empty;
            string mySubject = string.Empty;
            string myBodyLineListString = string.Empty;
            BodyType myBodyType = BodyType.HTML;
            if (inputSystem.EmailFormatPlainTextOrHtml.ToUpper().CompareTo("PLAINTEXT") == 0)
            {
                myBodyType = BodyType.PlainText;
            }

            // From Address
            myFromAddress =
                inputSystem
                .EmailFromAddress;

            log.Debug($"Before email send, myFromAddress = {myFromAddress}");

            // To Address List String
            myToAddressListString =
                inputSystem
                .EmailToAddresses;

            log.Debug($"Before email send, myToAddressListString = {myToAddressListString}");

            string[]
                myToAddressArray =
                    myToAddressListString.Split(";");

            List<string>
                myToAddressList =
                    new List<string>();
            foreach (string loopToEmailAddress in myToAddressArray)
            {
                if (loopToEmailAddress.Trim().Length > 0)
                {
                    myToAddressList.Add(loopToEmailAddress.Trim());
                }
            }

            // Subject
            mySubject =
                BuildSubject
                (
                    inputSystem
                );

            log.Debug($"Before email send, mySubject = {mySubject}");

            // BodyLineList
            myBodyLineListString =
                BuildBodyLineListString
                (
                    inputSystem
                    , myBodyType
                    , inputListOfAttachmentFullFilenames
                );

            log.Debug($"Before email send, myBodyLineListString = {myBodyLineListString}");


            // Email the notifyees.
            CallEmailWebApiLand myCallEmailWebApi =
                new CallEmailWebApiLand
                (
                    mySubject // string inputEemailSubject
                    , myBodyLineListString // string inputEmailBodyLineListString
                    , myToAddressList // List<string> inputEmailAddressList
                    , inputSystem.EmailFromAddress // string inputFromEmailAddress
                    , inputSystem.EmailWebApiUrl // string inputEmailWebApiBaseUrl
                    , inputListOfAttachmentFullFilenames //List<string> inputAttachmentList
                );
            EmailSendWithHtmlStringOutput
                myEmailSendWithHtmlStringOutput =
                    myCallEmailWebApi.CallIHtmlStringBody();
            if (!myEmailSendWithHtmlStringOutput.IsOk)
            {
                log.Error($"Error upon trying to invoke the Email Web Api.  Error Message:  {myEmailSendWithHtmlStringOutput}");
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = myEmailSendWithHtmlStringOutput.ErrorMessage;
                return returnOutput;
            }

            // Delete Attachment Files for System.
            DeleteAttachmentFilesOutput
                myDeleteAttachmentFilesOutput =
                    DeleteAttachmentFiles
                    (
                        inputListOfAttachmentFullFilenames
                        , inputSystem
                    );
            if (!myDeleteAttachmentFilesOutput.IsOk)
            {
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage =
                    myDeleteAttachmentFilesOutput.ErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }
        public
            string
                BuildSubject
                (
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns inputSystem
                )
        {
            string returnOutput = string.Empty;
            returnOutput = inputSystem.EmailSubject;
            if (!returnOutput.ToUpper().StartsWith("[SECURE]"))
            {
                returnOutput = $"[SECURE] {returnOutput}";
            }
            return returnOutput;
        }

        public string
                BuildBodyLineListString
                (
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                        inputSystem
                    , BodyType inputBodyType
                    , List<string>
                        inputListOfAttachmentFullFilenames
                )
        {
            string returnOutput = string.Empty;
            string startOfBody =
                inputSystem
                .EmailBodyStart;
            string
                attachmentListPortionOfString =
                    BuildEmailBodyAttachmentList
                    (
                        inputSystem
                        , inputBodyType
                        , inputListOfAttachmentFullFilenames
                    );
            string endOfBody =
                inputSystem
                .EmailBodyEnd;
            returnOutput = $"{inputSystem.EmailBodyStart}{attachmentListPortionOfString}{inputSystem.EmailBodyEnd}";

            return returnOutput;
        }
        public
            string
                BuildEmailBodyAttachmentList
                (
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                        inputSystem
                    , BodyType
                        inputBodyType
                    , List<string>
                        inputListOfAttachmentFullFilenames
                )
        {
            List<FileInfo>
                myFileInfoList =
                    new List<FileInfo>();
            foreach (string loopString in inputListOfAttachmentFullFilenames)
            {
                FileInfo loopFi = new FileInfo(loopString);
                myFileInfoList.Add(loopFi);
            }

            string returnOutput = string.Empty;
            switch (inputBodyType)
            {
                case BodyType.HTML:
                    returnOutput = $"{returnOutput}<br><br><ol>";
                    foreach (FileInfo loopFi in myFileInfoList)
                    {
                        returnOutput = $"{returnOutput}<li>{loopFi.Name}</li>";
                    }
                    returnOutput = $"{returnOutput}</ol><br>";
                    break;
                case BodyType.PlainText:
                    int attachmentCtr = 1;
                    returnOutput = $"{returnOutput}\r\n\r\n";
                    foreach (FileInfo loopFi in myFileInfoList)
                    {
                        returnOutput = $"{returnOutput}{attachmentCtr.ToString()}. {loopFi.Name}\r\n";
                        attachmentCtr++;
                    }
                    returnOutput = $"{returnOutput}\r\n";
                    break;

            }
            return returnOutput;
        }
        public enum BodyType
        {
            PlainText,
            RTF,
            HTML
        }
        public DeleteAttachmentFilesOutput
            DeleteAttachmentFiles
            (
                List<string> inputAttachmentFullFilenameList
                , qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                    inputqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
            )
        {
            DeleteAttachmentFilesOutput returnOutput = new DeleteAttachmentFilesOutput();
            foreach (string loopAttachmentFilename in inputAttachmentFullFilenameList)
            {
                FileInfo fi = new FileInfo(loopAttachmentFilename);

                string archiveFullFilename =
                    Path.Combine
                    (
                        inputqy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns
                            .AttachmentInputArchiveFolder
                        , fi.Name
                    );
                if (File.Exists(archiveFullFilename))
                {
                    File.Delete(archiveFullFilename);
                }
                File.Copy(loopAttachmentFilename, archiveFullFilename);
                if (
                        File.Exists(archiveFullFilename) &&
                        File.Exists(loopAttachmentFilename)
                    )
                {
                    File.Delete(loopAttachmentFilename);
                }
            }

            return returnOutput;
        }
    }
}
