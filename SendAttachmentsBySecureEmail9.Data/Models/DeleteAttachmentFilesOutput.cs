﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendAttachmentsBySecureEmail9.Data.Models
{
    public class DeleteAttachmentFilesOutput
    {
        public DeleteAttachmentFilesOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
    }
}
