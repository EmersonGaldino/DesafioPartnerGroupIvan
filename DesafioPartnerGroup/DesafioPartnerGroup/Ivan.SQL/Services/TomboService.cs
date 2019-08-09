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
    public class TomboService : MarshalByRefObject, ITombo
    {
        
        #region Members

        public Tombo Create()
        {
            Tombo entity = new Tombo() { DataDeCriacao = DateTime.Now };

            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO [dbo].[Tombo]");
                sb.Append(" ([DataDeCriacao])");
                sb.Append(" SELECT @DataDeCriacao; ");
                sb.Append("SELECT SCOPE_IDENTITY() AS IDENTITY_ID;");

                command.CommandText = sb.ToString();
                database.AddInParameter(command, "@DataDeCriacao", DbType.DateTime, entity.DataDeCriacao);

                object ret = database.ExecuteScalar(command);
                entity.ID = Convert.ToInt32(ret);
            }

            return entity;
        }


        public void Delete(Tombo entity)
        {
            using (Database database = new Database())
            {
                DbCommand command = database.CreateCommand();
                command.CommandText = "DELETE FROM [dbo].[Tombo] WHERE @ID = [ID];";
                database.AddInParameter(command, "@ID", DbType.Int32, entity.ID);
                database.ExecuteNonQuery(command);
            }
        }


        #endregion

    }
}
