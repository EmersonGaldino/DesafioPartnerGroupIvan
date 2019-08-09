using System.Configuration;

namespace Ivan.Services
{

    public sealed class LocalServicesSection : ConfigurationSection
    {

        private static ConfigurationPropertyCollection _properties;
        private static ConfigurationProperty _services;

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LocalServiceElementCollection Services
        {
            get
            {
                return (LocalServiceElementCollection)base[LocalServicesSection._services];
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return LocalServicesSection._properties;
            }
        }

        static LocalServicesSection()
        {
            LocalServicesSection._services = new ConfigurationProperty(null, typeof(LocalServiceElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
            LocalServicesSection._properties = new ConfigurationPropertyCollection();
            LocalServicesSection._properties.Add(LocalServicesSection._services);
        }

        public LocalServicesSection()
        {
        }

    } // class LocalServicesSection

}

