using System;
using System.Collections;
using System.Collections.Generic;



namespace Ivan.Models
{
    interface IPatrimonio
    {
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
        Patrimonio SelectSingle(int ID);


        /// <summary>
        /// Seleciona todos os objetos do tipo <see cref="Patrimonio"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Patrimonio"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </returns>
        IList<Patrimonio> Select();


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
        IList<Patrimonio> SelectByMarca(int MarcaID);


        /// <summary>
        /// Insere um objeto do tipo <see cref="Patrimonio"/> no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser inserido no banco de dados.
        /// </param>
        void Insert(Patrimonio entity);


        /// <summary>
        /// Altera um objeto do tipo <see cref="Patrimonio"/> no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Patrimonio"/> a ser alterado no banco de dados.
        /// </param>
        void Update(Patrimonio entity);


        /// <summary>
        /// Remove um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser removido no banco de dados.
        /// </param>
        void Delete(Patrimonio entity);
    }
}
