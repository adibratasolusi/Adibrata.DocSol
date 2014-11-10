﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Adibrata.BusinessProcess.Entities.Base;
namespace Adibrata.BusinessProcess.UserManagement.Entities
{
    [Serializable]
    public class UserManagementEntities : EntitiesBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int MaxWrong { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int IsActive { get; set; }
        public string FullName { get; set; }
        public int IsConnect { get; set; }
        public string SecQuestion { get; set; }
        public string SecAnswer { get; set; }
        public int MenuItemId { get; set; }
        public int MenuParentId { get; set; }
        public int ShortOrder { get; set; }
        public string MenuTxt { get; set; }
        public char IsSeparator { get; set; }

        public string Icon { get; set; }
    }
}
