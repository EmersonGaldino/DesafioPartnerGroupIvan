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
    /// Web API REST para o gerenciamento de marcas de patrimônios
    /// </summary>
    [RoutePrefix("api/marcas")]
    public class MarcasController : ApiController
    {

        /// <summary>
        /// Obtém todas as marcas disponíveis no banco de dados.
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Marca"/> disponíveis no banco de dados.
        /// </returns>
        /// <remarks>
        /// GET: api/marcas
        /// </remarks>
        public IEnumerable<Marca> Get()
        {
            return Marca.Select();
        }


        /// <summary>
        /// Obtém uma marca por ID.
        /// </summary>
        /// <param name="id">
        /// ID da marca a ser recuperada.
        /// </param>
        /// <returns>
        /// Um objeto do tipo <see cref="Marca"/> disponível no banco de dados.
        /// </returns>
        /// <remarks>
        /// GET: api/marcas
        /// </remarks>
        [HttpGet]
        [ActionName("marcas")]
        [Route("api/marcas/{id}")]
        public Marca Get(int id)
        {
            return Marca.SelectSingle(id);
        }


        /// <summary>
        /// Obtém todos os patrimônios de uma marcas disponíveis no banco de dados.
        /// </summary>
        /// <param name="id">
        /// ID do patrimônio a ser alterado.
        /// </param>
        /// <param name="patrimonio">
        /// ID do patrimônio a ser alterado.
        /// </param>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Marca"/> disponíveis no banco de dados.
        /// </returns>
        /// <remarks>
        /// GET: api/marcas/7/3
        /// </remarks>
        [HttpGet]
        [ActionName("marcas")]
        [Route("api/marcas/{id}/{patrimonio}")]
        public IEnumerable<Patrimonio> Get(int id, int patrimonio)
        {
            return Patrimonio.SelectByMarca(id);
        }


        /// <summary>
        /// Insere um novo objeto do tipo <see cref="Marca"/> no banco de dados caso 
        /// as regras de negócio sejam respeitadas.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser inserido no banco de dados.
        /// </param>
        /// <exception cref="NomeDaMarcaRepetidaException">
        /// Devido às regras de negócio caso uma marca com o nome repetido tente ser cadastrada uma excessão será lançada.
        /// </exception>
        /// <remarks>
        /// POST: api/marcas
        /// </remarks>
        public void Post([FromBody]Marca value)
        {
            Marca.Insert(value);
        }


        /// <summary>
        /// Altera um objeto do tipo <see cref="Marca"/> no banco de dados caso 
        /// as regras de negócio sejam respeitadas.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser alterado no banco de dados.
        /// </param>
        /// <exception cref="NomeDaMarcaRepetidaException">
        /// Devido às regras de negócio não podem existir marcas com nomes repetidos
        /// Caso tente alterar para uma marca com o mesmo nome uma excessão será lançada.
        /// </exception>
        /// <remarks>
        /// PUT: api/marcas/5
        /// </remarks>
        public void Put(int id, [FromBody]Marca value)
        {
            value.MarcaID = id;
            Marca.Update(value);
        }


        /// <summary>
        /// Remove um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser removido no banco de dados.
        /// </param>
        /// <remarks>
        /// DELETE : api/marcas
        /// </remarks>
        public void Delete(int id)
        {
            Marca.Delete(new Marca() { MarcaID = id });
        }
    }
}