namespace Ivan.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Ivan.Services;


    public class Tombo
    {
        #region Properties

        /// <summary>
        /// Id - Identificacao do tombo, criado automaticamente
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// DataDeCriacao - Data de criacao do tombo
        /// </summary>
        public DateTime DataDeCriacao { get; set; }

        #endregion


        #region Client API    
        /// <summary>
        /// Cria um novo objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo
        /// </summary>
        /// <returns>
        /// Um novo objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static Tombo Create()
        {
            //ITombo service = Resolver.GetImplementationOf<ITombo>();
            ITombo service = LocalServiceActivator.CreateInstance<ITombo>();
            return service.Create();
        }


        /// <summary>
        /// Remove um objeto do tipo <see cref="Tombo"/> caso exista no banco de dados.
        /// </summary>
        /// <param name="entity">
        /// Objeto do tipo <see cref="Tombo"/> com Id - Identificacao do tombo a ser removido.
        /// </param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(Tombo entity)
        {
            if (entity == null) throw new ArgumentNullException("Tombo");

            //ITombo service = Resolver.GetImplementationOf<ITombo>();
            ITombo service = LocalServiceActivator.CreateInstance<ITombo>();
            service.Delete(entity);
        }
        #endregion

    }
}