using System;
using System.Configuration;

namespace Ivan.Services
{

    public sealed class ServicesSectionGroup : ConfigurationSectionGroup
    {

        [ConfigurationProperty("local")]
        public LocalServicesSection LocalSettings
        {
            get
            {
                return (LocalServicesSection)Sections["local"];
            }
        }

        public ServicesSectionGroup()
        {
        }

        public static ServicesSectionGroup GetSectionGroup(System.Configuration.Configuration config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            return config.GetSectionGroup("Ivan.Services") as ServicesSectionGroup;
        }

    } // class ServicesSectionGroup

}

