﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Frontend.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; set; }
        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
