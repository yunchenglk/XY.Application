using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity;

namespace XY.Services
{
    public class MenuService
    {

        private USER user;
        private string _html;

        public string Html
        {
            get { return _html; }
            set { _html = value; }
        }
        public Boolean IsManager
        {
            get { return user.Type == 3; }
        }
        public MenuService()
        {

        }
        public MenuService(Guid uid)
        {
            //权限菜单          
            user = UserService.instance().GetEntityByID(uid);
            IEnumerable<Role> roles = RoleService.instance().GetEnumByUID(uid);
            List<Authority> Authoritys = new List<Authority>();
            List<Menu> menus = new List<Menu>();
            foreach (var item in roles)
            {
                Authoritys.AddRange(AuthorityService.instance().GetAuthorityListByRole(item.ID));
            }
            foreach (var item in Authoritys.GroupBy(m => new { m.PID }))
            {
                Menu menu = new Menu();
                menu.Name = item.First().ParentAuth.Name;
                menu.Icon = "";
                menu.URL = "";
                menu.Type = 1;
                menu.Childs = new List<Menu>();
                var xx = item.OrderBy(m => m.Sort);
                foreach (var auth in xx)
                {
                    menu.Childs.Add(new Menu()
                    {
                        Name = auth.Name,
                        URL = auth.Description,
                        Icon = ""
                    });
                }
                menus.Add(menu);
            }



            //功能菜单
            IEnumerable<Class> classs = ClassService.instance().GetChildByID(Guid.Empty, user.CompanyID).OrderBy(m => m.Sort);
            foreach (var cl in classs)
            {
                Menu menu = new Menu();
                menu.Name = cl.Title;
                menu.Type = cl.Type;
                menu.Childs = new List<Menu>();
                menu.ID = cl.ID;
                if (cl.Ishaschild)
                {
                    menu.Type = 1;
                    menu.URL = "#";
                    cl.Childs.Each(m =>
                    {
                        menu.Childs.Add(new Menu()
                        {
                            Name = m.Title,
                            Icon = "",
                            Type = m.Type,
                            ID = m.ID
                        });
                    });
                }
                menus.Add(menu);
            }






            VelocityEngine vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, AppDomain.CurrentDomain.BaseDirectory);
            vltEngine.Init();

            var vltContext = new VelocityContext();
            vltContext.Put("MENU", menus);
            Template vltTemplate = vltEngine.GetTemplate("_menu.vm");
            var vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);
            this._html = vltWriter.GetStringBuilder().ToString();
        }
        private string GetClassHtml(Class cl)
        {
            switch (cl.Type)
            {
                case 0:
                    return
                        "<li><a href=\"/General/About/" + cl.ID + "\"> <i class=\"fa fa-tint\"></i> " + cl.Title + " </a> </li>";
                case 1:
                    StringBuilder str = new StringBuilder();
                    str.Append("<li class=\"dropdown\">");
                    str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
                    str.Append("<i class=\"fa fa-list\"></i> " + cl.Title + "管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
                    str.Append("</a>");
                    str.Append("<ul class=\"dropdown-menu\">");
                    str.Append("<li>");
                    str.Append("<a href=\"/General/NewList/" + cl.ID + "\">");
                    str.Append("<i class=\"fa fa-caret-right\"></i>" + cl.Title + "列表");
                    str.Append("</a>");
                    str.Append("<a href=\"/General/NewEdit/" + cl.ID + "\">");
                    str.Append("<i class=\"fa fa-caret-right\"></i>添加" + cl.Title + "");
                    str.Append("</a></li></ul></li>");
                    return str.ToString();
                default:
                    return "";
            }
        }
        private string GetClassAndChildHtml(Class cl)
        {


            StringBuilder str = new StringBuilder();
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> " + cl.Title + "管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li><a href=\"/General/About/" + cl.ID + "\"> <i class=\"fa fa-tint\"></i> " + cl.Title + "介绍 </a> </li>");
            foreach (var item in cl.Childs.OrderBy(m => m.Type))
            {
                switch (item.Type)
                {
                    case 0:
                        str.Append("<li><a href=\"/General/About/" + item.ID + "\"> <i class=\"fa fa-tint\"></i> " + item.Title + " </a> </li>");
                        break;
                    case 1:
                        str.Append("<li>");
                        str.Append("<a href=\"/General/NewList/" + item.ID + "\">");
                        str.Append("<i class=\"fa fa-caret-right\"></i>" + item.Title + "列表");
                        str.Append("</a>");
                        str.Append("<a href=\"/General/NewEdit/" + item.ID + "\">");
                        str.Append("<i class=\"fa fa-caret-right\"></i>添加" + item.Title + "");
                        str.Append("</a></li>");
                        break;
                    default:
                        break;
                }

            }
            str.Append("</ul></li>");
            return str.ToString();
        }
        private string GetManagerHTML()
        {
            StringBuilder str = new StringBuilder();
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> 用户管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li>");
            str.Append("<a href=\"/User/Index\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>用户管理");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/User/Create\">");
            str.Append("<i class=\"fa fa-caret-right\"></i> 编辑用户");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/User/Recycle\">");
            str.Append("<i class=\"fa fa-caret-right\"></i> 信息回收站");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("</ul>");
            str.Append("</li>");
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> 角色管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li>");
            str.Append("<a href=\"/Role/Index\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>角色列表");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Role/Create\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>编辑角色");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Role/Recycle\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>信息回收站");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("</ul>");
            str.Append("</li>");
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> 权限管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li>");
            str.Append("<a href=\"/Authority/Index\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>权限列表");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Authority/Create\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>编辑权限");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Authority/Recycle\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>信息回收站");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("</ul>");
            str.Append("</li>");
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> 公司管理 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li>");
            str.Append("<a href=\"/Company/Index\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>公司列表");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Company/Create\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>编辑公司");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Company/Recycle\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>信息回收站");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("</ul>");
            str.Append("</li>");
            str.Append("<li class=\"dropdown\">");
            str.Append("<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">");
            str.Append("<i class=\"fa fa-list\"></i> 功能配置 <b class=\"fa fa-plus dropdown-plus\"></b>");
            str.Append("</a>");
            str.Append("<ul class=\"dropdown-menu\">");
            str.Append("<li>");
            str.Append("<a href=\"/Class/Index\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>功能列表");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Class/Create\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>编辑功能");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("<li>");
            str.Append("<a href=\"/Class/Recycle\">");
            str.Append("<i class=\"fa fa-caret-right\"></i>信息回收站");
            str.Append("</a>");
            str.Append("</li>");
            str.Append("</ul>");
            str.Append("</li>");
            return str.ToString();
        }

    }
}
