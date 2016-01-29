using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Services
{
    public class CheckRoleService
    {
        private static CheckRoleService _instance;
        public static CheckRoleService instance()
        {
            if (_instance == null)
            {
                _instance = new CheckRoleService();
            }
            return _instance;
        }
        /// <summary>
        /// 判断基本功能是否有权限
        /// </summary>
        /// <param name="cid">栏目ID</param>
        /// <param name="uid">用户ID</param>
        /// <returns></returns>
        public bool CheckRole_ClassID(Guid cid, Guid uid)
        {
            return UserService.instance().GetEntityByID(uid).CompanyID.Equals(ClassService.instance().GetEnumByID(cid).FirstOrDefault().CompanyID);
        }
    }
}
