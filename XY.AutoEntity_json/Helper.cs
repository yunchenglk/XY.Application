using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.AutoEntity
{
    public class Helper
    {
        protected VelocityEngine vltEngine;

        private readonly string _scheme =
            "SELECT distinct TABLE_NAME FROM  INFORMATION_SCHEMA.[COLUMNS] WHERE TABLE_NAME NOT LIKE 'syncobj%'";

        private readonly string _pkSql = "SELECT column_name AS 'primary_key' "
                                         + " FROM   information_schema.table_constraints pk "
                                         + "       INNER JOIN information_schema.key_column_usage c "
                                         + "            ON  c.table_name = pk.table_name "
                                         + "           AND c.constraint_name = pk.constraint_name "
                                         + " WHERE  pk.table_name = '{0}' "
                                         + "      AND constraint_type = 'primary key'";
       // private string _findCom = "SELECT sc.NAME FROM sys.tables st INNER JOIN sys.columns sc ON st.object_id = sc.object_id WHERE st.name = '{0}'";
        private string _findCom = "SELECT name FROM syscolumns WHERE (id = OBJECT_ID('{0}'))";
        public Helper()
        {
            vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, AppDomain.CurrentDomain.BaseDirectory);

            vltEngine.Init();
        }
        public List<Table> GetDatabaseSheme(string connectionString)
        {

            var list = new List<Table>();

            using (var connection = new SqlConnection(connectionString))
            {
                string databaseName = Replace(connection.Database);

                connection.Open();

                using (var command = new SqlCommand(_scheme, connection))
                {


                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {

                            Table table = new Table();
                            table.TableName = sqlDataReader[0].ToString();
                            table.DatabaseName = databaseName;
                            table.DatabaseRealName = connection.Database;
                            table.TemplateName = Replace(databaseName) + "_" + table.TableName + "_FetchEntityByIdentity";
                            //table.PK = GetPk(connection, table.TableName);
                            table.ColumnNames = string.Join(",", GetAllColumns(connectionString, table.TableName).ToArray());
                            if (SkipSomeTables(table))
                            {
                                continue;

                            }
                            list.Add(table);
                        }
                    }
                }


                foreach (var table in list)
                {

                    using (var command = new SqlCommand(string.Format(_pkSql, table.TableName), connection))
                    {
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            Console.WriteLine(table.TemplateName);
                            while (sqlDataReader.Read())
                            {
                                table.PK = sqlDataReader[0].ToString();
                                table.TableName = FixName.Fix(table.DatabaseName, table.TableName);//此处做替换
                            }
                        }
                    }
                }
            }

            return list;
        }
        private List<string> GetAllColumns(string connectionString, string tableName)
        {
            var list = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(string.Format(_findCom, tableName), connection))
                {
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {

                        while (sqlDataReader.Read())
                        {
                            list.Add(sqlDataReader.GetString(0));
                        }
                    }
                }
            }
            return list;
        }
        private bool SkipSomeTables(Table table)
        {
            if (table.TableName.StartsWith("MS"))
            {
                return true;
            }
            if (table.TableName.StartsWith("sys"))
                return true;
            if (table.TableName.Equals("sysarticles"))
                return true;
            return false;
        }
        public static string Replace(string databaseName)
        {
            return databaseName.Replace("ComBeziWfsSub", System.Configuration.ConfigurationManager.AppSettings["ComBeziWfsSub"]);
        }
        public List<string> GetConnections()
        {
            string conns = System.Configuration.ConfigurationManager.AppSettings["conns"];
            if (string.IsNullOrEmpty(conns))
                throw new Exception("conns");

            return conns.Split('|').ToList();
        }
        private string GetPk(SqlConnection connection, string tableName)
        {


            using (SqlConnection connection1 = new SqlConnection(connection.ConnectionString))
            {

                using (var command = new SqlCommand(string.Format(_pkSql, tableName), connection1))
                {
                    connection1.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        return sqlDataReader[0].ToString();
                    }
                }
            }

            throw new Exception("no table pk");

        }
        /// <summary>
        /// 生成按主键查询SQL
        /// </summary>
        public void GenerateFetchStatement(List<Table> tables)
        {
            if (tables == null || tables.Count == 0)
                throw new Exception("no job!");
            var vltContext = new VelocityContext();
            vltContext.Put("Tables", tables);
            Template vltTemplate = vltEngine.GetTemplate("fetch.vm");
            var vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "_f_" + tables.First().DatabaseRealName.ToLower() + ".xml", vltWriter.GetStringBuilder().ToString());
        }
        public void GenerateFetchStatementWithNolock(List<Table> tables)
        {
            if (tables == null || tables.Count == 0)
                throw new Exception("no job!");

            var vltContext = new VelocityContext();

            vltContext.Put("Tables", tables);

            Template vltTemplate = vltEngine.GetTemplate("fetch2.vm");
            var vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            //Console.WriteLine(vltWriter.GetStringBuilder().ToString());

            //Console.Read();



            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "_f_" + tables.First().DatabaseRealName.ToLower() + "_nolock.xml", vltWriter.GetStringBuilder().ToString());


        }
    }
    public class Table
    {
        public string TemplateName { get; set; }
        public string TableName { get; set; }
        public string PK { get; set; }
        public string DatabaseName { get; set; }
        public List<Column> Columns { get; set; }
        public string ColumnNames { get; set; }
        public string DatabaseRealName { get; set; }
    }
    public class Column
    {
        public string Name { get; set; }

        public bool IsAuto { get; set; }
    }
}
