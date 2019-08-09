using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Ivan.Models;


namespace DesafioPartnerGroup.Controllers
{
    /// <summary>
    /// Web API REST para o gerenciamento de patrimônios.
    /// </summary>
    [RoutePrefix("api/patrimonios")]
    public class PatrimoniosController : ApiController
    {
        /// <summary>
        /// Obtém todos os patrimônios disponíveis no banco de dados.
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Patrimonio"/> disponíveis no banco de dados.
        /// </returns>
        /// <remarks>
        /// GET: api/patrimonios
        /// </remarks>
        public IEnumerable<Patrimonio> Get()
        {
            return Patrimonio.Select();
        }


        /// <summary>
        /// Obtém o patrimônio com o ID passado via parametro se disponível no banco de dados.
        /// </summary>
        /// <param name="id">
        /// ID do Objeto do tipo <see cref="Patrimonio"/> desejado.
        /// </param>
        /// <returns>
        /// O patrimônio com o ID passado via parametro.
        /// </returns>
        /// <remarks>
        /// Exemplo: GET: api/patrimonios/5
        /// </remarks>
        [HttpGet]
        [ActionName("patrimonios")]
        [Route("api/patrimonios/{id}")]
        public Patrimonio Get(int id)
        {
            return Patrimonio.SelectSingle(id);
        }


        /// <summary>
        /// Insere um novo patrimônio no banco de dados.
        /// </summary>
        /// <param name="value">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser inserido no banco de dados.
        /// </param>
        /// <remarks>
        /// Exemplo: POST: api/patrimonios
        /// </remarks>
        public void Post([FromBody]Patrimonio value)
        {
            Patrimonio.Insert(value);
        }


        /// <summary>
        /// Altera os dados de um patrimônio no banco de dados.
        /// </summary>
        /// <param name="id">
        /// ID do patrimônio a ser alterado.
        /// </param>
        /// <param name="value">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser alterado.
        /// </param>
        /// <remarks>
        /// Exemplo: PUT: api/patrimonio/5
        /// </remarks>
        public void Put(int id, [FromBody]Patrimonio value)
        {
            value.ID = id;
            Patrimonio.Update(value);
        }


        /// <summary>
        /// Excluir um objeto do tipo <see cref="Patrimonio"/> do banco de dados.
        /// </summary>
        /// <param name="id">
        /// ID do patrimônio a ser removido.
        /// </param>
        /// <remarks>
        /// Exemplo: DELETE: api/patrimonios/5
        /// </remarks>
        public void Delete(int id)
        {
            Patrimonio.Delete(new Patrimonio() { ID = id });
        }
    }
}
