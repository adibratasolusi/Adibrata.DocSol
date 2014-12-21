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
        public string Form { get; set; }
        public Boolean FlagInsert { get; set; }
        public long UserID { get; set; }
        public int isConnect { get; set; }
        public string MenuID { get; set; }
        public string FormID { get; set; }

        public string FormURL { get; set; }
        public string FormCode { get; set; }
 
        
    }
}
