using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Configuration;



namespace Ivan.SQL
{
    public delegate void DatabaseMapRowDelegate<T>(T item, IDataRecord record, int rowNum);

    public class Database : IDisposable
    {
        private DbProviderFactory factory;
        private string connectionString;
        private DbConnection connectionMain;


        public static ConnectionStringSettings settings = null;


        public static void SetSettings(string name, string connectionString, string providerName) {
            settings = new System.Configuration.ConnectionStringSettings(
                name,
                connectionString,
                providerName
            );
        }


        public Database()
        {
            if (settings == null)
                throw new ConfigurationErrorsException("Connection not found.");


            this.factory = DbProviderFactories.GetFactory(settings.ProviderName);
            this.connectionString = settings.ConnectionString;
        }




        public Database(string connectionStringName)
        {
            if (connectionStringName == null)
                throw new ArgumentNullException("connectionStringName");


            settings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
                throw new ConfigurationErrorsException(string.Format("Connection \"{0}\" not found.", connectionStringName));



            this.factory = DbProviderFactories.GetFactory(settings.ProviderName);
            this.connectionString = settings.ConnectionString;
        }






        public DbParameter AddInParameter(DbCommand command, string name, DbType type, object value)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Input, type, 0, 0, 0, value);
            return parameter;
        }

        public DbParameter AddInParameter(DbCommand command, string name, DbType type, int size, object value)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Input, type, size, 0, 0, value);
            return parameter;
        }

        public DbParameter AddInParameter(DbCommand command, string name, DbType type, byte scale, byte precision, object value)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Input, type, 0, scale, precision, value);
            return parameter;
        }

        public DbParameter AddOutParameter(DbCommand command, string name, DbType type)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Output, type, 0, 0, 0, DBNull.Value);
            return parameter;
        }

        public DbParameter AddOutParameter(DbCommand command, string name, DbType type, int size)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Output, type, size, 0, 0, DBNull.Value);
            return parameter;
        }

        public DbParameter AddOutParameter(DbCommand command, string name, DbType type, byte scale, byte precision)
        {
            DbParameter parameter = AddParameter(command, name, ParameterDirection.Output, type, 0, scale, precision, DBNull.Value);
            return parameter;
        }

        public DbParameter AddParameter(DbCommand command, string name, ParameterDirection direction, DbType type, int size, byte scale, byte precision, object value)
        {
            DbParameter parameter = factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Direction = direction;
            parameter.DbType = type;
            parameter.Size = size;
            if (scale != 0 || precision != 0)
            {
                IDbDataParameter data = parameter;
                data.Scale = scale;
                data.Precision = precision;
            }
            if (direction != ParameterDirection.Output)
                parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
            return parameter;
        }

        public DbCommand CreateCommand()
        {
            return factory.CreateCommand();
        }

        private DbConnection CreateConnection()
        {
            if (connectionMain == null || connectionMain.State == ConnectionState.Closed)
            {
                connectionMain = factory.CreateConnection();
                connectionMain.ConnectionString = connectionString;
            }

            return connectionMain;
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            int result = 0;
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                result = command.ExecuteNonQuery();
                command.Connection = null;
            }
            return result;
        }

        public DbDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = CreateConnection();
            try
            {
                connection.Open();
                command.Connection = connection;
                DbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public DbDataReader ExecuteReader(DbCommand command, CommandBehavior behavior)
        {
            DbConnection connection = CreateConnection();
            try
            {
                connection.Open();
                command.Connection = connection;
                DbDataReader reader = command.ExecuteReader(behavior | CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public T MapRow<T>(DbCommand command, DatabaseMapRowDelegate<T> action) where T : class, new()
        {
            T item = default(T);
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (reader.Read())
                    {
                        item = new T();
                        action(item, reader, 0);
                    }
                }
            }
            return item;
        }

        public IEnumerable<T> MapRows<T>(DbCommand command, DatabaseMapRowDelegate<T> action) where T : class, new()
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                int rowNum = 0;
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T item = new T();
                        action(item, reader, rowNum);
                        yield return item;
                        rowNum++;
                    }
                }
            }
        }

        public IEnumerable<T> MapRows<T>(DbCommand command, CommandBehavior behavior, DatabaseMapRowDelegate<T> action) where T : class, new()
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                int rowNum = 0;
                using (DbDataReader reader = command.ExecuteReader(behavior))
                {
                    while (reader.Read())
                    {
                        T item = new T();
                        action(item, reader, rowNum);
                        yield return item;
                        rowNum++;
                    }
                }
            }
        }

        public Byte[] MapBlob(DbCommand command, string column)
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                Byte[] result;
                DbDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                if (reader.Read())
                {
                    result = reader.GetValue(reader.GetOrdinal(column)) as byte[];
                    return result;
                }
            }
            return null;
        }



        public object ExecuteScalar(DbCommand command)
        {
            object result = null;
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                result = command.ExecuteScalar();
                command.Connection = null;
            }
            return result;
        }


        #region IDisposable Members
        public void Dispose()
        {
            if (connectionMain != null)
            {
                connectionMain.Close();
                connectionMain.Dispose();
            }
        }
        #endregion
    }
}