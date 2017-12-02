using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.AutoEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            var help = new Helper();
            var list = help.GetConnections();
            #region 生成模板
            //foreach (var item in list)
            //{
            //    help.GenerateFetchStatement(help.GetDatabaseSheme(item));
            //    help.GenerateFetchStatementWithNolock(help.GetDatabaseSheme(item));
            //}

            #endregion
            #region 生成实体类

            var help2 = new EntityHelper();

            int c = 0;
            foreach (var item in list)
            {
                help2.GenerateFile(help2.GetSheme(item, ConfigurationManager.AppSettings["ns"].Split('|')[c]), ConfigurationManager.AppSettings["fn"].Split('|')[c]);
                c++;
            }


            #endregion

            Console.WriteLine("生成完毕！");
            Console.Read();
        }
    }
}
