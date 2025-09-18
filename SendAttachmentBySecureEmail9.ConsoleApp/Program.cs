
using SendAttachmentsBySecureEmail9.CallWebApiLand;
using SendAttachmentsBySecureEmail9.Data.Models;

using log4net;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SendAttachmentBySecureEmail9.ConsoleApp
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // Limit Program to run one instance only.
        public static Process PriorProcess()
        // Returns a System.Diagnostics.Process pointing to
        // a pre-existing process with the same name as the
        // current one, if any; or null if the current process
        // is unique.
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }
        public static void Main(string[] args)
        {
            if (PriorProcess() != null)
            {

                log.Error("Another instance of the app is already running.");
                return;
            }

            // configure logging via log4net
            string log4netConfigFullFilename =
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "log4net.config");
            var fileInfo = new FileInfo(log4netConfigFullFilename);
            if (fileInfo.Exists)
                log4net.Config.XmlConfigurator.Configure(fileInfo);
            else
                throw new InvalidOperationException("No log config file found");


            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(SendAttachmentsBySecureEmail9.Data.MyConstants.AppSettingsFile)
                .Build();

            // Read in Configuration Options for this Console Application
            ReadInSendAttachmentsConfigOptions
                myReadInSendAttachmentsConfigOptions =
                    new ReadInSendAttachmentsConfigOptions(config);
            SendAttachmentsConfigOptions
                myConfigOptions =
                    myReadInSendAttachmentsConfigOptions.ReadIn();

            // Get Config Options from the database.
            CallWebApiLandClass
                myCallForGetOptions =
                    new CallWebApiLandClass
                        (
                            myConfigOptions.ConfigOptionsBaseWebUrl
                        );

            qy_GetSendAttachmentsBySecureEmailConfigOutput
                myqy_GetSendAttachmentsBySecureEmailConfigOutput =
                    myCallForGetOptions.qy_GetSendAttachmentsBySecureEmailConfig();

            if (!myqy_GetSendAttachmentsBySecureEmailConfigOutput.IsOk ||
                myqy_GetSendAttachmentsBySecureEmailConfigOutput
                .qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList
                .Count != 1)
            {
                log.Error($"We had an error in trying to get the configuration file from the database:  {myqy_GetSendAttachmentsBySecureEmailConfigOutput.ErrorMessage}");
                return;
            }


            // Main Operations.
            SendAttachmentsMainOps
                mySendAttachmentsMainOps =
                    new SendAttachmentsMainOps
                        (
                            myConfigOptions
                            , myqy_GetSendAttachmentsBySecureEmailConfigOutput
                            .qy_GetSendAttachmentsBySecureEmailConfigOutputColumnsList[0]
                        );

            SendAttachmentsMainOpsOutput
                mySendAttachmentsMainOpsOutput =
                    mySendAttachmentsMainOps.DoIt();
            if (!mySendAttachmentsMainOpsOutput.IsOk)
            {
                log.Error(mySendAttachmentsMainOpsOutput.ErrorMessage);
                return;
            }
        }
    }
}

