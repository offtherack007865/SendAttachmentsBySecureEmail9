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











        // GET /api/Ops/qy_GetSendAttachmentsBySecureEmailAttachmentsConfig
        [HttpGet]
        public qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput
                    qy_GetSendAttachmentsBySecureEmailAttachmentsConfig
                    (
                    )
        {
            qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput
                returnOutput =
                    new qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutput();


            string sql = $"opsemail.qy_GetSendAttachmentsBySecureEmailAttachmentsConfig";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList =
                    MyContext
                    .qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumnsList
                    .FromSqlRaw<qy_GetSendAttachmentsBySecureEmailAttachmentsConfigOutputColumns>
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












    }
}
