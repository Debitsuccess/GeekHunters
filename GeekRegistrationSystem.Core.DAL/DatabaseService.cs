using GeekRegistrationSystem.Core.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;

namespace GeekRegistrationSystem.Core.DAL
{
    public enum SqlCommand
    {
        Insert = 1,
        Select = 2,
    }

    public static class DatabaseService
    {
        static string _connectionString;
        public static string ConnectionSting
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #region Private Methods        
        private static string InsertCommand<T>(T entity) where T : IEntity<T>
        {
            string Insert = $"INSERT INTO {((IEntity<T>)entity).EntityName }";
            string columns = "(";
            string values = "VALUES(";
            Dictionary<string, object> infos = GetInfo(entity);
            //Read Column Names
            foreach (var item in infos)
            {
                if (item.Value != null && item.Value != "" && !entity.PrimaryKey.Exists(p => p.Name == item.Key))
                {
                    columns += item.Key + ",";
                    values += Formatting(item.Value.ToString()) + ",";
                }
            }
            columns = $" {columns.Remove(columns.Length - 1, 1) } ) ";
            values = $" {values.Remove(values.Length - 1, 1) } ) ";
            Insert += columns + values;
            return Insert;
        }

        private static string selectCommand<T>(T entity) where T : IEntity<T>
        {
            string select = $"SELECT * FROM  {((IEntity<T>)entity).EntityName } ";
            return select;
        }
        private static string selectCommand<T>(T entity, string where) where T : IEntity<T>
        {
            string select = $"SELECT * FROM { ((IEntity<T>)entity).EntityName } { where } ";
            return select;
        }
        
        private static string Formatting(string item)
        {
            bool r1 = false;
            Int32 r2 = 0;
            if (bool.TryParse(item, out r1) || Int32.TryParse(item, out r2))
                return item;
            else
                return $"'{ item }'";
        }
        #endregion

        #region Public Methods

        public static bool Initialize(string connectionString)
        {
            try
            {
                ConnectionSting = connectionString;
               return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Dictionary<string, object> GetInfo<T>(T entity) where T : IEntity<T>
        {
            try
            {
                Dictionary<string, object> values = new Dictionary<string, object>();
                foreach (var item in ((IEntity<T>)entity).GetType().GetProperties())
                {
                    if (item.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        if (item.CanRead && item.PropertyType.IsSerializable && item.PropertyType.IsPublic)
                        {
                            if (item.Name == "PrimaryKey" || item.Name == "EntityName")
                                continue;
                            else if (item.PropertyType.IsEnum)
                                values.Add(item.Name, item.GetValue(entity, null).GetHashCode());
                            else
                                values.Add(item.Name, item.GetValue(entity, null));
                        }

                    }
                }
                return values;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static SQLiteDataReader ExecuteReader(string CommandText)
        {
            try
            {
                using (SQLiteConnection sqliteDbconnection = new SQLiteConnection(ConnectionSting))
                {
                    if (sqliteDbconnection.State != System.Data.ConnectionState.Open)
                        sqliteDbconnection.Open();
                    SQLiteDataReader myReader = null;
                    SQLiteCommand myCommand = new SQLiteCommand(CommandText, sqliteDbconnection);
                    myReader = myCommand.ExecuteReader();
                    return myReader;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ExecuteReader(string CommandText, out List<Dictionary<string, object>> entityList)
        {
            try
            {
                entityList = new List<Dictionary<string, object>>();
                using (SQLiteConnection sqliteDbconnection = new SQLiteConnection(ConnectionSting))
                {
                    if (sqliteDbconnection.State != System.Data.ConnectionState.Open)
                        sqliteDbconnection.Open();
                    SQLiteDataReader myReader = null;
                    SQLiteCommand myCommand = new SQLiteCommand(CommandText, sqliteDbconnection);
                    myReader = myCommand.ExecuteReader();
                    if (myReader.HasRows)
                    {
                        int i = 0;
                        while (myReader.Read())
                        {
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            for (int j = 0; j < myReader.FieldCount; j++)
                                dictionary.Add(myReader.GetSchemaTable().Rows[j][0].ToString(), myReader[j]);
                            entityList.Add(dictionary);
                            i++;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object ExecuteScaler(string CommandText)
        {
            try
            {
                using (SQLiteConnection sqliteDbconnection = new SQLiteConnection(ConnectionSting))
                {
                    SQLiteCommand Com = sqliteDbconnection.CreateCommand();
                    if (sqliteDbconnection.State != ConnectionState.Open)
                        sqliteDbconnection.Open();
                    sqliteDbconnection.Open();
                    return Com.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ExecuteNoneQuery(string CommandText)
        {
            try
            {
                using (SQLiteConnection sqliteDbconnection = new SQLiteConnection(ConnectionSting))
                {
                    SQLiteCommand Com = sqliteDbconnection.CreateCommand();
                    if (sqliteDbconnection.State == ConnectionState.Open)
                        sqliteDbconnection.Close();
                    sqliteDbconnection.Open();
                    Com.CommandText = CommandText;
                    Com.ExecuteNonQuery();
                }
                return true;
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GenerateCommand<T>(SqlCommand sqlCommand, T entity, string where = "") where T : IEntity<T>
        {
            string commandText = string.Empty;
            switch (sqlCommand)
            {
                case SqlCommand.Insert:
                    commandText = InsertCommand(entity);
                    break;   
                case SqlCommand.Select:
                    if (where == "")
                        commandText = selectCommand(entity);
                    else if (where != "")
                        commandText = selectCommand(entity, where);
                    break;
                default:
                    break;
            }
            return commandText;
        }

        public static bool DoEntity<T>(T entity, SqlCommand sqlCommand, string where = "") where T : IEntity<T>
        {
            bool result = false; ;
            try
            {
                switch (sqlCommand)
                {
                    case SqlCommand.Insert:
                        result = ExecuteNoneQuery(GenerateCommand(SqlCommand.Insert, entity, where));
                        break;
                    case SqlCommand.Select:
                        result = ExecuteNoneQuery(GenerateCommand(SqlCommand.Select, entity, where));
                        break;
                    default:
                        break;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props =
                    TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(values);
                }
                return table;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

}
