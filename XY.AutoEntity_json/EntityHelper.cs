using NVelocity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.AutoEntity
{
    public class EntityHelper : Helper
    {
        private string shemeProcedure = "EXEC  sp_help '{0}'";
        public EntityInfo GetSheme(string connnectionString, string ns)
        {
            var info = new EntityInfo();
            info.Date = DateTime.Now.ToString();
            info.Namespace = ns;
            var tables = this.GetDatabaseSheme(connnectionString);
            var entityTables = new List<EntityTable>();
            foreach (var table in tables)
            {
                var entityTable = new EntityTable { TableName = table.TableName, Columns = new List<EntityColumn>(), DatabaseName = table.DatabaseName };
                //if (string.IsNullOrEmpty(table.PK))
                //    continue;
                #region
                using (var connection = new SqlConnection(connnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(string.Format(shemeProcedure, table.TableName), connection))
                    {
                        var reader = command.ExecuteReader();
                        // column
                        reader.NextResult();
                        while (reader.Read())
                        {
                            var entityColumn = new EntityColumn();
                            entityColumn.CanBeNull = reader[6].ToString() == "no" ? bool.FalseString.ToLower() : bool.TrueString.ToLower();
                            entityColumn.FullColumnDbType = reader[1] + "(" + reader[3] + ")";
                            entityColumn.ColumnDbType = reader[1].ToString();
                            entityColumn.ColumnName = reader[0].ToString();
                            entityColumn.ColumnNameX = entityColumn.ColumnName.Substring(0, 1) + entityColumn.ColumnName.Substring(1);
                            entityColumn.ColumnType = ConvertToCLRType(reader[1].ToString());
                            entityColumn.IsDbGenerated = Boolean.FalseString.ToLower(); // 稍后重新计算
                            if (table.PK != null)
                            {
                                entityColumn.IsPrimaryKey = table.PK.Equals(entityColumn.ColumnName)
                                                ? Boolean.TrueString.ToLower()
                                                : Boolean.FalseString.ToLower();
                            }
                            entityTable.Columns.Add(entityColumn);
                        }
                        // pk
                        reader.NextResult();
                        string v = string.Empty;
                        while (reader.Read())
                        {
                            v = reader[0].ToString();
                        }
                        foreach (var c in entityTable.Columns)
                        {
                            if (c.IsPrimaryKey == Boolean.TrueString.ToLower() && v.Equals(c.ColumnName))
                            {
                                c.IsDbGenerated = Boolean.TrueString.ToLower();
                            }
                        }
                    }
                }
                #endregion
                entityTables.Add(entityTable);
            }
            info.Tables = entityTables;
            return info;
        }

        private string ConvertToCLRType(string p)
        {
            if (string.IsNullOrEmpty(p))
                return string.Empty;
            string result;
            switch (p.ToLower())
            {
                case "bigint":
                    result = "Integer";
                    break;
                case "binary":
                    result = "Binary data";
                    break;
                case "timestamp":
                    result = "Binary data";
                    break;
                case "bit":
                    result = "Boolean";
                    break;
                case "char":
                    result = "String";
                    break;
                case "money":
                    result = "Number";
                    break;
                case "smallmoney":
                    result = "Number";
                    break;
                case "datetime":
                    result = "Date";
                    break;
                case "smalldatetime":
                    result = "Date";
                    break;
                case "float":
                    result = "Number";
                    break;
                case "uniqueidentifier":
                    result = "String";
                    break;
                case "identity":
                    result = "Number";
                    break;
                case "int":
                    result = "Number";
                    break;
                case "image":
                    result = "Binary data";
                    break;
                case "text":
                    result = "String";
                    break;
                case "ntext":
                    result = "String";
                    break;
                case "decimal":
                    result = "Number";
                    break;
                case "numeric":
                    result = "Number";
                    break;
                case "real":
                    result = "Number";
                    break;
                case "smallint":
                    result = "Number";
                    break;
                case "tinyint":
                    result = "String";
                    break;
                case "varbinary":
                    result = "String";
                    break;
                case "varchar":
                    result = "String";
                    break;
                case "sql_variant":
                    result = "String";
                    break;
                case "nvarchar":
                    result = "String";
                    break;
                case "nchar":
                    result = "String";
                    break;
                default:
                    throw new ApplicationException("no matched!");
            }
            return result;
        }

        private string ConvertToCLRType_s(string p)
        {
            if (string.IsNullOrEmpty(p))
                return string.Empty;
            string result;
            switch (p.ToLower())
            {
                case "bigint":
                    result = "Number";
                    break;
                case "binary":
                    result = "Byte[]";
                    break;
                case "timestamp":
                    result = "Byte[]";
                    break;
                case "bit":
                    result = "Boolean";
                    break;
                case "char":
                    result = "String";
                    break;
                case "money":
                    result = "Decimal";
                    break;
                case "smallmoney":
                    result = "Decimal";
                    break;
                case "datetime":
                    result = "DateTime";
                    break;
                case "smalldatetime":
                    result = "DateTime";
                    break;
                case "float":
                    result = "Double";
                    break;
                case "uniqueidentifier":
                    result = "Guid";
                    break;
                case "identity":
                    result = "Number";
                    break;
                case "int":
                    result = "Number";
                    break;
                case "image":
                    result = "Byte[]";
                    break;
                case "text":
                    result = "String";
                    break;
                case "ntext":
                    result = "String";
                    break;
                case "decimal":
                    result = "Decimal";
                    break;
                case "numeric":
                    result = "Decimal";
                    break;
                case "real":
                    result = "Single";
                    break;
                case "smallint":
                    result = "Int16";
                    break;
                case "tinyint":
                    result = "Byte";
                    break;
                case "varbinary":
                    result = "Byte[]";
                    break;
                case "varchar":
                    result = "String";
                    break;
                case "sql_variant":
                    result = "Object";
                    break;
                case "nvarchar":
                    result = "String";
                    break;
                case "nchar":
                    result = "String";
                    break;
                default:
                    throw new ApplicationException("no matched!");
            }
            return result;
        }
        public void GenerateFile(EntityInfo info, string filename)
        {
            if (info == null)
                throw new Exception("no job!");
            if (string.IsNullOrEmpty(filename))
                throw new Exception("no job!");
            var vltContext = new VelocityContext();
            vltContext.Put("EntityInfo", info);
            Template vltTemplate = vltEngine.GetTemplate("entity.vm");
            var vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);
            File.WriteAllText(string.Format("{0}{1}.json", AppDomain.CurrentDomain.BaseDirectory, filename), vltWriter.GetStringBuilder().ToString());
        }
    }

    public class EntityInfo
    {
        public string Date { get; set; }
        public string Namespace { get; set; }
        public List<EntityTable> Tables { get; set; }
    }

    public class EntityTable
    {
        public string TableName { get; set; }
        public List<EntityColumn> Columns { get; set; }
        public string DatabaseName { get; set; }
    }

    public class EntityColumn
    {
        public string ColumnName { get; set; }
        public string ColumnNameX { get; set; }
        public string ColumnDbType { get; set; }
        public string CanBeNull { get; set; }

        private string _IsPrimaryKey;

        public string IsPrimaryKey
        {
            get {
                if (string.IsNullOrEmpty(_IsPrimaryKey))
                    return "false";
                return _IsPrimaryKey;
            }
            set { _IsPrimaryKey = value; }
        }
        public string IsDbGenerated { get; set; }
        public string ColumnType { get; set; }
        public string FullColumnDbType { get; set; }
    }
}
