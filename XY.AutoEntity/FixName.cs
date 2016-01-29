using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.AutoEntity
{
    public class FixName
    {
        //目前就发现一个类名不一致的 WfsSub.cs 文件中的
        /*
         * [Table(Name="dbo.ErpProduct")]
         * [Database(Name ="ComBeziWfs" )]
         * [Serializable] 
         * public partial class ErpProduct
         * --------------------------------------------
         * 替换成
         * [Table(Name="dbo.WfsProduct")]
         * [Database(Name ="ComBeziWfs" )]
         * [Serializable] 
         * public partial class WfsProduct
         */



        public static string file = ConfigurationManager.AppSettings["fixName"];

        public static string Fix(string databaseName, string tableName)
        {
            if (!string.IsNullOrWhiteSpace(file))
            {
                string[] f = file.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string f1 in f)
                {
                    string[] text = f1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (text != null && text.Length == 3)
                    {
                        if (text[0] == databaseName && text[1] == tableName)
                        {
                            tableName = text[2];
                        }
                    }
                }
            }
            return tableName;
        }
    }
}
