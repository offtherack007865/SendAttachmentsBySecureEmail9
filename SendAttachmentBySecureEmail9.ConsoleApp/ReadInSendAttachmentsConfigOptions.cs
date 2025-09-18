using Microsoft.Extensions.Configuration;
using SendAttachmentsBySecureEmail9.Data;
using SendAttachmentsBySecureEmail9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentBySecureEmail9.ConsoleApp
{
    public class ReadInSendAttachmentsConfigOptions
    {
        public ReadInSendAttachmentsConfigOptions(IConfiguration myConfig)
        {
            MyConfig = myConfig;
        }

        public Microsoft.Extensions.Configuration.IConfiguration MyConfig { get; set; }

        public SendAttachmentsConfigOptions ReadIn()
        {
            SendAttachmentsConfigOptions
                returnConfigOptions =
                new SendAttachmentsConfigOptions();

            returnConfigOptions.ConfigOptionsBaseWebUrl =
                MyConfig.GetValue<string>(MyConstants.ConfigOptionsBaseWebUrl);

            return returnConfigOptions;
        }

    }
}
