using System;
using System.Collections;
using System.Collections.Generic;



namespace Ivan.Models
{
    interface IMarca
    {
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
        Marca SelectSingle(int MarcaID);


        /// <summary>
        /// Seleciona todos os objetos do tipo <see cref="Marca"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </summary>
        /// <returns>
        /// Todos os objetos do tipo <see cref="Marca"/> do banco de dados caso 
        /// não exista nenhum retorna uma lista vazia não nula. 
        /// </returns>
        IList<Marca> Select();


        /// <summary>
        /// Insere um objeto do tipo <see cref="Marca"/> no banco de dados caso 
        /// as regras de negócio sejam respeitadas.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser inserido no banco de dados.
        /// </param>
        /// <exception cref="NomeDaMarcaRepetidaException">
        /// Devido às regras de negócio caso uma marca com o nome repetido tente ser cadastrada uma excessão será lançada.
        /// </exception>
        void Insert(Marca entity);


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
        void Update(Marca entity);


        /// <summary>
        /// Remove um objeto do tipo <see cref="Marca"/> do banco de dados caso 
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser removido no banco de dados.
        /// </param>
        void Delete(Marca entity);


        /// <summary>
        /// Verifica se existe uma marca com este nome no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Marca"/> a ser verificado no banco de dados.
        /// </param>
        bool Exists(Marca entity);
    }
}
