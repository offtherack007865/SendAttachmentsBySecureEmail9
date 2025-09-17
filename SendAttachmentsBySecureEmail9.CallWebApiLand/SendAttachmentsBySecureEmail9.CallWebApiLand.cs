using log4net;
using Newtonsoft.Json;
using SendAttachmentsBySecureEmail9.Data.Models;

namespace SendAttachmentsBySecureEmail9.CallWebApiLand
{
    public class CallWebApiLandClass
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CallWebApiLandClass));

        public CallWebApiLandClass
        (
            string inpuSimplifyVbcAdt8BaseWebApiUrl
        )
        {
            MySimplifyVbcAdt8BaseWebApiUrl = inpuSimplifyVbcAdt8BaseWebApiUrl;
        }
        public string MySimplifyVbcAdt8BaseWebApiUrl { get; set; }

        // GET /api/Ops/qy_GetSendAttachmentsBySecureEmailAttachmentsConfig
        public qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput qy_GetSendAttachmentsBySecureEmailAttachmentsConfig()
        {
            qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput
                returnOutput =
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfigAsync().Result;

            return returnOutput;
        }
        public async Task<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput> qy_GetSendAttachmentsBySecureEmailAttachmentsConfigAsync()
        {
            log.Info($"In CallSpGetSimplifyVbcAdtEthinConfigAsync()");
            qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput
                returnOutput =
                    new qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput();

            string myCompleteUrl = $"{MySimplifyVbcAdt8BaseWebApiUrl}/api/Ops/qy_GetSendAttachmentsBySecureEmailAttachmentsConfig";
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            if (returnOutput == null ||
                returnOutput.qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList.Count == 0)
            {
                returnOutput = new qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput();
                returnOutput.IsOk = false;
                returnOutput.ErrorMessage = $"Url {myCompleteUrl} returned an error.";
                return returnOutput;
            }

            return returnOutput;
        }

    }
}
