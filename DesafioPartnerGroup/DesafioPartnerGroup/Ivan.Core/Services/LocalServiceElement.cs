using System;
using System.ComponentModel;
using System.Configuration;

namespace Ivan.Services
{

    public sealed class LocalServiceElement : ConfigurationElement
    {

        private static ConfigurationPropertyCollection _properties;
        private static ConfigurationProperty _service;
        private static ConfigurationProperty _type;

        [TypeConverter(typeof(System.Configuration.TypeNameConverter))]
        [ConfigurationProperty("service", IsRequired = true)]
        public Type Service
        {
            get
            {
                return (Type)base[LocalServiceElement._service];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                base[LocalServiceElement._service] = value;
            }
        }

        [TypeConverter(typeof(System.Configuration.TypeNameConverter))]
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public Type Type
        {
            get
            {
                return (Type)base[LocalServiceElement._type];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                base[LocalServiceElement._type] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return LocalServiceElement._properties;
            }
        }

        static LocalServiceElement()
        {
            TypeNameConverter typeNameConverter = new TypeNameConverter();
            LocalServiceElement._type = new ConfigurationProperty("type", typeof(Type), null, typeNameConverter, null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
            LocalServiceElement._service = new ConfigurationProperty("service", typeof(Type), null, typeNameConverter, null, ConfigurationPropertyOptions.IsRequired);
            LocalServiceElement._properties = new ConfigurationPropertyCollection();
            LocalServiceElement._properties.Add(LocalServiceElement._type);
            LocalServiceElement._properties.Add(LocalServiceElement._service);
        }

        internal LocalServiceElement()
        {
        }

        public LocalServiceElement(Type type, Type serviceType)
        {
            Type = type;
            Service = serviceType;
        }

    } // class LocalServiceElement

}

