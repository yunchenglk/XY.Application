using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class Menu
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsURL
        {
            get
            {
                return !string.IsNullOrEmpty(URL);
            }
        }
        public List<Menu> Childs { get; set; }
        public bool IsChild
        {
            get
            {
                return Childs.Count() > 0;
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        public string Icon { get; set; }
        public Guid ID { get; set; }
        public bool IsSystem { get; set; }
    }
}
