using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Ivan.SQL;
using Ivan.Models;

namespace Ivan.Service
{
    public class MarcaService : MarshalByRefObject, IMarca
    {
        
        #region MapRecord
        public static void MapRecord(Marca entity, IDataRecord record, int rowNum)
        {
            entity.MarcaID = record.GetInt32(record.GetOrdinal("MarcaID"));
            entity.Nome = record.GetValue(record.GetOrdinal("Nome")) as string;
        }
        #endregion


        #region Members
        public Marca SelectSingle(int MarcaID)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].[Marca] WHERE MarcaID = @MarcaID;";
                database.AddInParameter(command, "@MarcaID", DbType.Int32, MarcaID);
                return database.MapRow<Marca>(command, MapRecord);
            }
        }


        public IList<Marca> Select()
        {
            IList<Marca> result;

            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].[Marca];";
                result = new List<Marca>(database.MapRows<Marca>(command, MapRecord));
            }

            return result;
        }


        public void Insert(Marca entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO [dbo].[Marca]");
                sb.Append(" ([Nome])");
                sb.Append(" SELECT @Nome; ");
                sb.Append("SELECT SCOPE_IDENTITY() AS IDENTITY_ID;");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.AddInParameter(command, "@Nome", DbType.String, entity.Nome);

                object ret = database.ExecuteScalar(command);
                entity.MarcaID = Convert.ToInt32(ret);
            }
        }


        public void Update(Marca entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE [dbo].[Marca] ");
                sb.Append(" SET [Nome] = @Nome ");
                sb.Append(" WHERE @MarcaID = [MarcaID];");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.AddInParameter(command, "@Nome", DbType.String, entity.Nome);

                object ret = database.ExecuteScalar(command);
            }
        }


        public void Delete(Marca entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "DELETE FROM [dbo].[Marca] WHERE @MarcaID = [MarcaID];";
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.ExecuteNonQuery(command);
            }
        }


        public bool Exists(Marca entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("SELECT [Nome] FROM [dbo].[Marca] ");
                sb.Append(" WHERE UPPER([Nome]) LIKE UPPER(@Nome) ");
                sb.Append(" AND ");
                sb.Append(" @MarcaID <> [MarcaID];");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.AddInParameter(command, "@Nome", DbType.String, entity.Nome);

                object ret = database.ExecuteScalar(command);

                return !(ret == null || ret == DBNull.Value);
            }
        }

        #endregion

    }
}
