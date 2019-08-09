namespace Ivan.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Ivan.Services;


    public class Patrimonio
    {
        #region Properties
        /// <summary>
        /// ID do objeto Patrimonio
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome - obrigatório
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// MarcaId - obrigatório
        /// </summary>
        public int MarcaID { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Nº do tombo
        /// </summary>
        public int NumeroDoTombo { get; set; }
        #endregion


        #region Client API    

        /// <summary>
        /// Seleciona um objeto do tipo <see cref="Patrimonio"/> do banco de dados caso 
        /// o objeto com este ID exista, caso contrario retorna nulo. 
        /// </summary>
        /// <param name="ID">
        /// Identificação do objeto do tipo <see cref="Patrimonio"/>
        /// </param>
        /// <returns>
        /// Um objeto do tipo <see cref="Patrimonio"/>
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Patrimonio SelectSingle(int ID)
        {
            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            return service.SelectSingle(ID);
        }


        /// <summary>
        /// Seleciona todos os objetos do tipo <see cref="Patrimonio"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Patrimonio"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IList<Patrimonio> Select()
        {
            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            return service.Select();
        }



        /// <summary>
        /// Seleciona objetos do tipo <see cref="Patrimonio"/> com uma marca especifica no banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </summary>
        /// <param name="MarcaID">
        /// ID da <see cref="Marca"/> a ser pesquisada no banco de dados.
        /// </param>
        /// <returns>
        /// Objetos do tipo <see cref="Patrimonio"/> com uma marca especifica no banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static IList<Patrimonio> SelectByMarca(int MarcaID)
        {
            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            return service.SelectByMarca(MarcaID);
        }


        /// <summary>
        /// Insere um objeto do tipo <see cref="Patrimonio"/> no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser inserido no banco de dados.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void Insert(Patrimonio entity)
        {
            if (entity == null) throw new ArgumentNullException("Patrimonio");

            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            service.Insert(entity);
        }


        /// <summary>
        /// Altera um objeto do tipo <see cref="Patrimonio"/> no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser alterado no banco de dados.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(Patrimonio entity)
        {
            if (entity == null) throw new ArgumentNullException("Patrimonio");

            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            service.Update(entity);
        }


        /// <summary>
        /// Remove um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser removido no banco de dados.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(Patrimonio entity)
        {
            if (entity == null) throw new ArgumentNullException("Patrimonio");

            //IPatrimonio service = Resolver.GetImplementationOf<IPatrimonio>();
            IPatrimonio service = LocalServiceActivator.CreateInstance<IPatrimonio>();
            service.Delete(entity);
        }
        #endregion

    }
}