using System;
using System.Collections;
using System.Collections.Generic;



namespace Ivan.Models
{
    interface ITombo
    {
        /// <summary>
        /// Cria um novo objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo
        /// </summary>
        /// <returns>
        /// Um novo objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo
        /// </returns>
        Tombo Create();


        /// <summary>
        /// Remove um objeto do tipo <see cref="Tombo"/> caso exista no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo a ser removido.
        /// </param>
        void Delete(Tombo entity);
    }
}
