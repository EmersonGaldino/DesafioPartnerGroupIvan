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
    public class PatrimonioService : MarshalByRefObject, IPatrimonio
    {
        
        #region MapRecord
        public static void MapRecord(Patrimonio entity, IDataRecord record, int rowNum)
        {
            entity.ID = record.GetInt32(record.GetOrdinal("ID"));
            entity.Nome = record.GetValue(record.GetOrdinal("Nome")) as string;
            entity.MarcaID = record.GetInt32(record.GetOrdinal("MarcaID"));
            entity.Descricao = record.GetValue(record.GetOrdinal("Descricao")) as string;
            entity.NumeroDoTombo = record.GetInt32(record.GetOrdinal("NumeroDoTombo"));
        }
        #endregion


        #region Members
        public Patrimonio SelectSingle(int ID)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].[Patrimonio] WHERE ID = @ID;";
                database.AddInParameter(command, "@ID", DbType.Int32, ID);
                return database.MapRow<Patrimonio>(command, MapRecord);
            }
        }


        public IList<Patrimonio> Select()
        {
            IList<Patrimonio> result;

            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].[Patrimonio];";
                result = new List<Patrimonio>(database.MapRows<Patrimonio>(command, MapRecord));
            }

            return result;
        }


        public IList<Patrimonio> SelectByMarca(int MarcaID)
        {
            IList<Patrimonio> result;

            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].[Patrimonio] WHERE [MarcaID] = @MarcaID;";
                database.AddInParameter(command, "@MarcaID", DbType.Int32, MarcaID);
                result = new List<Patrimonio>(database.MapRows<Patrimonio>(command, MapRecord));
            }

            return result;
        }


        public void Insert(Patrimonio entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO [dbo].[Patrimonio]");
                sb.Append("    ([Nome]");
                sb.Append("    ,[MarcaID]");
                sb.Append("    ,[Descricao]");
                sb.Append("    ,[NumeroDoTombo]");
                sb.Append(")");
                sb.Append(" SELECT ");
                sb.Append("@Nome, ");
                sb.Append("@MarcaID, ");
                sb.Append("@Descricao, ");
                sb.Append("@NumeroDoTombo; ");
                sb.Append("SELECT SCOPE_IDENTITY() AS IDENTITY_ID;");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@Nome", DbType.String, entity.Nome);
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.AddInParameter(command, "@Descricao", DbType.String, entity.Descricao);
                database.AddInParameter(command, "@NumeroDoTombo", DbType.Int32, entity.NumeroDoTombo);

                object ret = database.ExecuteScalar(command);
                entity.ID = Convert.ToInt32(ret);
            }
        }


        public void Update(Patrimonio entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE [dbo].[Patrimonio] ");
                sb.Append("SET [Nome] = @Nome,");
                sb.Append("    [MarcaID] = @MarcaID,");
                sb.Append("    [Descricao] = @Descricao ");
                // Atenção!
                // O nº do tombo deve ser gerado automaticamente pelo sistema, e não pode ser alterado pelos usuários.
                // sb.Append("    [NumeroDoTombo] = @NumeroDoTombo ");
                sb.Append(" WHERE @ID = [ID];");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@ID", DbType.Int32, entity.ID);
                database.AddInParameter(command, "@Nome", DbType.String, entity.Nome);
                database.AddInParameter(command, "@MarcaID", DbType.Int32, entity.MarcaID);
                database.AddInParameter(command, "@Descricao", DbType.String, entity.Descricao);
                database.AddInParameter(command, "@NumeroDoTombo", DbType.Int32, entity.NumeroDoTombo);

                object ret = database.ExecuteScalar(command);
            }
        }


        public void Delete(Patrimonio entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "DELETE FROM [dbo].[Patrimonio] WHERE @ID = [ID];";
                database.AddInParameter(command, "@ID", DbType.Int32, entity.ID);
                database.ExecuteNonQuery(command);
            }
        }


        #endregion

    }
}
