using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Ivan.Services
{

    public abstract class LocalServicesConfiguration
    {

        private static Dictionary<Type, KnownServiceTypeEntry> _services;
        private static object _servicesMonitor;

        static LocalServicesConfiguration()
        {
            LocalServicesConfiguration._servicesMonitor = new Object();
            LocalServicesConfiguration._services = new Dictionary<Type, KnownServiceTypeEntry>();
        }

        public static void Configure()
        {
            throw new NotImplementedException("Configure is not implemented yet");
        }


        public static void Configure(string fileName)
        {
            lock (LocalServicesConfiguration._servicesMonitor)
            {
                Dictionary<Type, KnownServiceTypeEntry> dictionary = new Dictionary<Type, KnownServiceTypeEntry>(LocalServicesConfiguration._services);
                ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
                exeConfigurationFileMap.ExeConfigFilename = fileName;
                System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
                ServicesSectionGroup servicesSectionGroup = ServicesSectionGroup.GetSectionGroup(configuration);
                if (servicesSectionGroup != null)
                {
                    LocalServicesSection localServicesSection = servicesSectionGroup.LocalSettings;
                    if (localServicesSection != null)
                    {
                        foreach (LocalServiceElement localServiceElement in localServicesSection.Services)
                        {
                            dictionary.Add(localServiceElement.Type, new KnownServiceTypeEntry(localServiceElement.Type, localServiceElement.Service));
                        }
                    }
                    LocalServicesConfiguration._services = dictionary;
                }
            }
        }

        public static KnownServiceTypeEntry[] GetRegisteredServiceTypes()
        {
            Dictionary<Type, KnownServiceTypeEntry> dictionary = LocalServicesConfiguration._services;
            Dictionary<Type, KnownServiceTypeEntry>.ValueCollection valueCollection = dictionary.Values; // .get_Values();
            KnownServiceTypeEntry[] knownServiceTypeEntryArr = new KnownServiceTypeEntry[valueCollection.Count];
            valueCollection.CopyTo(knownServiceTypeEntryArr, 0);
            return knownServiceTypeEntryArr;
        }

        public static KnownServiceTypeEntry IsKnownServiceType(Type type)
        {
            KnownServiceTypeEntry knownServiceTypeEntry;

            Dictionary<Type, KnownServiceTypeEntry> dictionary = LocalServicesConfiguration._services;
            dictionary.TryGetValue(type, out knownServiceTypeEntry);
            return knownServiceTypeEntry;
        }

        public static void RegisterKnownServiceType(KnownServiceTypeEntry entry)
        {
            lock (LocalServicesConfiguration._servicesMonitor)
            {
                Dictionary<Type, KnownServiceTypeEntry> dictionary = new Dictionary<Type, KnownServiceTypeEntry>(LocalServicesConfiguration._services);
                dictionary.Add(entry.Type, entry);
                LocalServicesConfiguration._services = dictionary;
            }
        }

        public static void RegisterKnownServiceType(Type type, Type service)
        {
            LocalServicesConfiguration.RegisterKnownServiceType(new KnownServiceTypeEntry(type, service));
        }

    } // class LocalServicesConfiguration

}

