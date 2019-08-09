namespace Ivan.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Ivan.Services;
    using Ivan.Business.Rule;

    public class Marca
    {
        #region Properties

        /// <summary>
        /// MarcaId - obrigatório
        /// Identificador único do(s) objeto(s) criados desta classe.
        /// </summary>
        public int MarcaID { get; set; }


        /// <summary>
        /// Nome - obrigatório
        /// Nome único do(s) objeto(s) criados desta classe.
        /// Não podem existir duas marcas com o mesmo nome 
        /// </summary>
        public string Nome { get; set; }

        #endregion


        #region Client API    

        /// <summary>
        /// Seleciona um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// o objeto com este ID exista, caso contrario retorna nulo. 
        /// </summary>
        /// <param name="MarcaID">
        /// Identificação do objeto do tipo <see cref="Marca"/>
        /// </param>
        /// <returns>
        /// Um objeto do tipo <see cref="Marca"/>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Marca SelectSingle(int MarcaID)
        {
            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            return service.SelectSingle(MarcaID);
        }


        /// <summary>
        /// Seleciona todos os objetos do tipo <see cref="Marca"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Marca"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IList<Marca> Select()
        {
            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            return service.Select();
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
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void Insert(Marca entity)
        {
            if (entity == null) throw new ArgumentNullException();
            if (Marca.Exists(entity)) throw new NomeDaMarcaRepetidaException();

            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            service.Insert(entity);
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
        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(Marca entity)
        {
            if (entity == null) throw new ArgumentNullException();
            if (Marca.Exists(entity)) throw new NomeDaMarcaRepetidaException();

            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            service.Update(entity);
        }

        
        /// <summary>
        /// Remove um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser removido no banco de dados.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(Marca entity)
        {
            if (entity == null) throw new ArgumentNullException();

            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            service.Delete(entity);
        }


        /// <summary>
        /// Verifica se existe uma marca com este nome no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser verificado no banco de dados.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static bool Exists(Marca entity)
        {
            //IMarca service = Resolver.GetImplementationOf<IMarca>();
            IMarca service = LocalServiceActivator.CreateInstance<IMarca>();
            return service.Exists(entity);
        }        

        #endregion

    }
}