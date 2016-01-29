using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.DataAccess.Data;
using YunChengLK.Framework.Data.Configuration;

namespace XY.DataAccess
{
    public class DbHelper
    {
        #region Member variables
        private DbTransaction trans;
        DataSet dataSet = new DataSet();
        private DbProviderFactory dbProviderFactory;

        private string connectionString;
        /// <summary>
        /// sql connection str
        /// </summary>
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        #endregion

        #region Constructor
        public DbHelper(string connectionName)
            : this(connectionName, System.Data.SqlClient.SqlClientFactory.Instance)
        {

        }

        public DbHelper(string connectionName, DbProviderFactory dbProviderFactory)
        {
            string ReturnErrorMsg = string.Empty;
            //循环节点检查是否存在该连接名称，如果 不存在，取默认值
            if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                if (string.IsNullOrEmpty(connectionName)) connectionName = "Default";
                //做容错处理以下
                ConnectionStringSettings connStrSettings = new ConnectionStringSettings(connectionName, ConnectionConfig.Connections[connectionName]);// ConfigurationManager.ConnectionStrings[connectionName];
                if (connStrSettings != null)
                {
                    this.dbProviderFactory = dbProviderFactory;

                    connectionString = connStrSettings.ConnectionString;
                }
                else
                {
                    ReturnErrorMsg = "请在web.config文件里添加名为“" + connectionName + "”的数据库连接串";
                }
            }
            else
            {
                ReturnErrorMsg = "请在web.config文件里添加数据库连接";
            }
            if (ReturnErrorMsg == null) throw new Exception(ReturnErrorMsg);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 判断数据库连接是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsConnectionDB()
        {
            bool connState = true;

            DbConnection dc = dbProviderFactory.CreateConnection();
            dc.ConnectionString = connectionString;

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    dc.Open();
                    if (dc.State.ToString() == "Open")
                    {
                        connState = true;
                        dc.Close();

                        break;
                    }
                }
                catch
                {
                    connState = false;
                    System.Threading.Thread.Sleep(2);
                }
            }

            return connState;
        }

        /// <summary>
        /// GetResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public delegate T GetResult<T>(DbCommand dbCommand);

        /// <summary>
        /// GetResultList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public delegate IList<T> GetResultList<T>(DbCommand dbCommand);


        #region Execute Result
        /// <summary>
        /// get dataReader data
        /// </summary>
        /// <typeparam name="T">T t</typeparam>
        /// <param name="dbCommand">DbCommand</param>
        /// <returns>IList</returns>
        public IList<T> GetDataReader<T>(DbCommand dbCommand) where T : new()
        {
            IList<T> iList = null;
            if (dbCommand != null)
            {
                try
                {
                    iList = new List<T>();
                    using (IDataReader dr = dbCommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            iList.Add(ReflectHelper.PopulateChangeFromIDataReader<T>(dr));
                        }
                    }
                }
                catch (Exception ex)
                {
                    string str = GetCommandString(dbCommand);
                    //LogHelper.WriterError(EnumLogCategory.Data, str, typeof(DbHelper), ex);
                    throw new Exception(str, ex);
                }
            }
            return iList;
        }

        /// <summary>
        /// get single dataReader data
        /// </summary>
        /// <typeparam name="T">T t</typeparam>
        /// <param name="dbCommand">DbCommand</param>
        /// <returns>T t</returns>
        public T GetSingleDataReader<T>(DbCommand dbCommand) where T : new()
        {
            T t = default(T);
            if (dbCommand != null)
            {
                using (IDataReader dr = dbCommand.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        t = default(T);
                        t = ReflectHelper.PopulateChangeFromIDataReader<T>(dr);
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// GetScalar
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public object GetScalar(DbCommand dbCommand)
        {
            return (dbCommand != null) ? dbCommand.ExecuteScalar() : null;
        }

        /// <summary>
        /// GetNonQuery
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public int GetNonQuery(DbCommand dbCommand)
        {
            return (dbCommand != null) ? dbCommand.ExecuteNonQuery() : 0;
        }

        /// <summary>
        /// GetBool
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public bool GetExecuteBool(DbCommand dbCommand)
        {
            return (GetNonQuery(dbCommand) != 0) ? true : false;
        }

        /// <summary>
        /// GetValueConvertBool
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public bool GetValueConvertBool(DbCommand dbCommand)
        {
            int ReturnIntValue = 0;
            Int32.TryParse(GetScalar(dbCommand).ToString(), out ReturnIntValue);
            return (ReturnIntValue != 0) ? true : false;
        }

        /// <summary>
        /// GetDataSet
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public DataSet GetDataSet(DbCommand dbCommand)
        {
            dataSet = new DataSet();
            if (dbCommand != null)
            {
                using (DbDataAdapter adapter = this.dbProviderFactory.CreateDataAdapter())
                {
                    ((IDbDataAdapter)adapter).SelectCommand = dbCommand;
                    adapter.Fill(dataSet);
                }
            }
            return dataSet;
        }
        #endregion

        #region Common Execute Method

        #endregion

        #region Public Execute Method

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult) where T : new()
        {
            IList<DbParameter> ListDataParameter = null;
            return GetData<T>(SqlStr, commandType, getResult, ListDataParameter);
        }

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult, TransactionStateType transactionStateType) where T : new()
        {
            IList<DbParameter> ListDataParameter = null;
            return GetData<T>(SqlStr, commandType, getResult, ListDataParameter, transactionStateType);
        }

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult, IList<DbParameter> ListDataParameter) where T : new()
        {
            return GetData<T>(SqlStr, commandType, getResult, ListDataParameter, TransactionStateType.Close);
        }

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult, IList<DbParameter> ListDataParameter, TransactionStateType transactionStateType) where T : new()
        {
            trans = null;
            T t = default(T);
            using (DbConnection dbConnection = this.CreateConnection())
            {
                dbConnection.Open();
                DbCommand dbCommand = GetDbCommand(dbConnection, SqlStr, commandType);

                DbExecute<T>(getResult, ListDataParameter, transactionStateType, ref trans, ref t, dbConnection, dbCommand);

                dbConnection.Close();
            }
            return t;
        }

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult, IList<IList<DbParameter>> ListDataParameter) where T : new()
        {
            return GetData<T>(SqlStr, commandType, getResult, ListDataParameter, TransactionStateType.Close);
        }

        public T GetData<T>(string SqlStr, CommandType commandType, GetResult<T> getResult, IList<IList<DbParameter>> ListDataParameter, TransactionStateType transactionStateType) where T : new()
        {
            trans = null;
            T t = default(T);
            using (DbConnection dbConnection = this.CreateConnection())
            {
                dbConnection.Open();
                DbCommand dbCommand = GetDbCommand(dbConnection, SqlStr, commandType);

                DbExecute<T>(getResult, ListDataParameter, transactionStateType, ref trans, ref t, dbConnection, dbCommand);

                dbConnection.Close();
            }
            return t;
        }


        public T GetData<T>(IList<string> SqlStrList, CommandType commandType, GetResult<T> getResult, IList<IList<DbParameter>> ListDataParameter, TransactionStateType transactionStateType) where T : new()
        {
            trans = null;
            T t = default(T);
            using (DbConnection dbConnection = this.CreateConnection())
            {
                dbConnection.Open();
                DbCommand dbCommand = null;
                dbCommand = GetDbCommand(dbConnection, null, commandType);

                DbExecute<T>(getResult, ListDataParameter, transactionStateType, ref trans, ref t, dbConnection, dbCommand, SqlStrList);

                dbConnection.Close();
            }
            return t;
        }

        public IList<T> GetDataList<T>(string SqlStr, CommandType commandType, GetResultList<T> getResultList)
        {
            return GetDataList<T>(SqlStr, commandType, getResultList, null);
        }

        public IList<T> GetDataList<T>(string SqlStr, CommandType commandType, GetResultList<T> getResultList, TransactionStateType transactionStateType)
        {
            return GetDataList<T>(SqlStr, commandType, getResultList, null, transactionStateType);
        }

        public IList<T> GetDataList<T>(string SqlStr, CommandType commandType, GetResultList<T> getResultList, IList<DbParameter> ListDataParameter)
        {
            return GetDataList<T>(SqlStr, commandType, getResultList, ListDataParameter, TransactionStateType.Close);
        }

        public IList<T> GetDataList<T>(string SqlStr, CommandType commandType, GetResultList<T> getResultList, IList<DbParameter> ListDataParameter, TransactionStateType transactionStateType)
        {
            trans = null;
            IList<T> iList = new List<T>();
            using (DbConnection dbConnection = this.CreateConnection())
            {
                dbConnection.Open();

                DbCommand dbCommand = GetDbCommand(dbConnection, SqlStr, commandType);
                dbCommand.CommandTimeout = 180;
                SetParameter(ListDataParameter, dbCommand);

                DbExecute<T>(getResultList, transactionStateType, ref trans, ref iList, dbConnection, dbCommand);

                dbConnection.Close();
            }
            return iList;
        }

        #endregion

        private static void DbExecute<T>(GetResult<T> getResult, IList<DbParameter> ListDataParameter, TransactionStateType transactionStateType, ref DbTransaction trans, ref T t, DbConnection dbConnection, DbCommand dbCommand)
        {
            IList<IList<DbParameter>> TempListDbParm = new List<IList<DbParameter>>();
            TempListDbParm.Add(ListDataParameter);
            DbExecute<T>(getResult, TempListDbParm, transactionStateType, ref trans, ref t, dbConnection, dbCommand);
        }

        private static void DbExecute<T>(GetResult<T> getResult, IList<IList<DbParameter>> ListDataParameter, TransactionStateType transactionStateType, ref DbTransaction trans, ref T t, DbConnection dbConnection, DbCommand dbCommand, IList<string> SqlStrList)
        {
            bool tranBoolValue = false;
            try
            {
                if (transactionStateType == TransactionStateType.Open) tranBoolValue = true;
                if (tranBoolValue) trans = dbConnection.BeginTransaction();
                if (tranBoolValue) dbCommand.Transaction = trans;

                if (SqlStrList != null && ListDataParameter != null && SqlStrList.Count == ListDataParameter.Count)
                {
                    DbCommand SingleDbCommand = null;
                    for (int i = 0; i < SqlStrList.Count; i++)
                    {
                        SingleDbCommand = dbCommand;
                        SingleDbCommand.CommandText = SqlStrList[i];
                        SingleDbCommand.Parameters.Clear();

                        if (ListDataParameter[i] != null)
                        {
                            for (int j = 0; j < ListDataParameter[i].Count; j++)
                            {
                                SingleDbCommand.Parameters.Add(ListDataParameter[i][j]);
                            }
                        }
                        if (getResult != null) t = getResult(SingleDbCommand);

                    }
                }

                if (tranBoolValue) trans.Commit();
            }
            catch (Exception ex)
            {
                if (tranBoolValue) trans.Rollback();
                t = default(T);
                string str = GetCommandString(dbCommand);
                //LogHelper.WriterError(EnumLogCategory.Data, str, typeof(DbHelper), ex);
                throw new Exception(str, ex);
            }
        }

        private static void DbExecute<T>(GetResult<T> getResult, IList<IList<DbParameter>> ListDataParameter, TransactionStateType transactionStateType, ref DbTransaction trans, ref T t, DbConnection dbConnection, DbCommand dbCommand)
        {
            bool tranBoolValue = false;
            try
            {
                if (transactionStateType == TransactionStateType.Open) tranBoolValue = true;
                if (tranBoolValue) trans = dbConnection.BeginTransaction();
                if (tranBoolValue) dbCommand.Transaction = trans;

                if (ListDataParameter != null)
                {
                    DbCommand SingleDbCommand = null;
                    for (int i = 0; i < ListDataParameter.Count; i++)
                    {
                        SingleDbCommand = dbCommand;
                        SingleDbCommand.Parameters.Clear();
                        if (ListDataParameter[i] != null)
                        {
                            for (int j = 0; j < ListDataParameter[i].Count; j++)
                            {
                                SingleDbCommand.Parameters.Add(ListDataParameter[i][j]);
                            }
                        }
                        if (getResult != null) t = getResult(SingleDbCommand);
                    }
                }

                if (tranBoolValue) trans.Commit();
            }
            catch (Exception ex)
            {
                if (tranBoolValue) trans.Rollback();
                t = default(T);
                string str = GetCommandString(dbCommand);
                // LogHelper.WriterError(EnumLogCategory.Data, str, typeof(DbHelper), ex);
                throw new Exception(str, ex);
            }
        }

        #region DbExecute<T>
        private static void DbExecute<T>(GetResultList<T> getResultList, TransactionStateType transactionStateType, ref DbTransaction trans, ref IList<T> iList, DbConnection dbConnection, DbCommand dbCommand)
        {
            bool tranBoolValue = false;
            try
            {
                if (transactionStateType == TransactionStateType.Open) tranBoolValue = true;
                if (tranBoolValue) trans = dbConnection.BeginTransaction();
                if (tranBoolValue) dbCommand.Transaction = trans;

                if (getResultList != null) iList = getResultList(dbCommand);
                if (tranBoolValue) trans.Commit();
            }
            catch (Exception ex)
            {
                if (tranBoolValue) trans.Rollback();
                throw new Exception(GetCommandString(dbCommand), ex);
            }
        }

        private void SetParameter(IList<DbParameter> ListDataParameter, DbCommand dbCommand)
        {
            if (ListDataParameter != null)
            {
                try
                {
                    foreach (DbParameter dbParameter in ListDataParameter)
                    {
                        dbCommand.Parameters.Add(dbParameter);
                    }
                }
                catch (Exception ex)
                {
                    string str = GetCommandString(dbCommand);
                    // LogHelper.WriterError(EnumLogCategory.Data, str, typeof(DbHelper), ex);
                    throw new Exception(str, ex);
                }
            }

        }

        private DbCommand GetDbCommand(DbConnection dbConnection, string commandText, CommandType commandType)
        {
            DbCommand dbCommand = this.dbProviderFactory.CreateCommand();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            dbCommand.Connection = dbConnection;
            return dbCommand;
        }
        #endregion

        #region Parameter

        #region AddParameter

        public void AddParameter(IList<DbParameter> ListDataParameter, string ParameterName, DbType dbType, object value)
        {
            AddParameter(ListDataParameter, null, ParameterName, null, LocationType.Null, dbType, value);
        }

        public void AddParameter(IList<DbParameter> ListDataParameter, IList<ParamInfo> ListParamInfo, string ParameterName, string condition, DbType dbType, object value)
        {
            AddParameter(ListDataParameter, null, ParameterName, condition, LocationType.Null, dbType, value);
        }

        public void AddParameter(IList<DbParameter> ListDataParameter, IList<ParamInfo> ListParamInfo, string ParameterName, string condition, LocationType location, DbType dbType, object value)
        {
            ListDataParameter.Add(CreateParameter(ParameterName, dbType, 0, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, value));
            if (ListParamInfo != null) ListParamInfo.Add(new ParamInfo(ParameterName.TrimStart('@'), condition, location));
        }
        #endregion

        protected DbParameter CreateParameter(string ParameterName, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = this.dbProviderFactory.CreateParameter();
            ConfigureParameter(param, ParameterName, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        public void ConfigureParameter(DbParameter param, string ParameterName, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.ParameterName = ParameterName;
            param.DbType = dbType;
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }
        #endregion

        #endregion

        #region
        /// <summary>
        /// <para>Creates a connection for this database.</para>
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns>
        /// <para>The <see cref="DbConnection"/> for this database.</para>
        /// </returns>
        /// <seealso cref="DbConnection"/>  
        public virtual DbConnection CreateConnection()
        {
            DbConnection newConnection = this.dbProviderFactory.CreateConnection();
            newConnection.ConnectionString = this.connectionString;

            return newConnection;
        }
        #endregion

        /// <summary>
        /// 将Sql及参数信息拼接出字符串
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string GetCommandString(DbCommand cmd)
        {
            StringBuilder sb = new StringBuilder(512);
            sb.Append(cmd.CommandText);
            if (cmd.Parameters != null && cmd.Parameters.Count > 0)
            {
                sb.Append(" ");
                foreach (DbParameter p in cmd.Parameters)
                {
                    sb.Append("[Name]:").Append(p.ParameterName).Append("[Value]:").Append(p.Value).Append(",");
                }
                sb.Remove(sb.Length - 1, 1);

            }
            return sb.ToString();
        }
    }
}
