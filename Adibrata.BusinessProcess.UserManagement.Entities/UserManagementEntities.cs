using System;
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
    }
}
