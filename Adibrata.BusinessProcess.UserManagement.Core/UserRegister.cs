﻿using Adibrata.BusinessProcess.UserManagement.Entities;
using Adibrata.Configuration;
using Adibrata.Framework.DataAccess;
using Adibrata.Framework.Logging;
using Adibrata.Framework.Security;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Adibrata.BusinessProcess.UserManagement.Core
{
    public class UserRegister
    {
        static string ConnectionString = AppConfig.Config("ConnectionString");
        static string _coyName = AppConfig.Config("CoyName");

       
    }
}
