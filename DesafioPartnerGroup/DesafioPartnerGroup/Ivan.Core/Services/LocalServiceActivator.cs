using System;
using System.Collections.Generic;

namespace Ivan.Services
{

    public class LocalServiceActivator
    {

        public LocalServiceActivator()
        {
        }


        /// <summary>
        /// Will load the settings from an embedded configuration file "LocalServices.config"
        /// </summary>
        public static void Configure()
        {
            LocalServicesConfiguration.Configure();
        }

        public static void Configure(string fileName)
        {
            LocalServicesConfiguration.Configure(fileName);
        }

        public static T CreateInstance<T>()
        {
            KnownServiceTypeEntry knownServiceTypeEntry = LocalServicesConfiguration.IsKnownServiceType(typeof(T));
            if (knownServiceTypeEntry == null)
                throw new LocalServicesException(String.Format("Unknown service {0}", typeof(T).FullName));
            return (T)Activator.CreateInstance(knownServiceTypeEntry.Service);
        }

    } // class LocalServiceActivator

}

