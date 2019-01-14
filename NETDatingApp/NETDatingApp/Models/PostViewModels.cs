using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETDatingApp.Models
{
    public class SendPostViewModel
    {
        public string Message { get; set; }
        public int SenderID { get; set; }
        public int RecieverID { get; set; }

    }

}