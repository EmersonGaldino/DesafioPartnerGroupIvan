using System;
using System.Configuration;

namespace Ivan.Services
{

    [ConfigurationCollection(typeof(Ivan.Services.LocalServiceElement), CollectionType = (System.Configuration.ConfigurationElementCollectionType)1)]
    public sealed class LocalServiceElementCollection : ConfigurationElementCollection
    {

        private static ConfigurationPropertyCollection _properties;

        public LocalServiceElement this[int index]
        {
            get
            {
                return (LocalServiceElement)BaseGet(index);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(value);
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return LocalServiceElementCollection._properties;
            }
        }

        static LocalServiceElementCollection()
        {
            LocalServiceElementCollection._properties = new ConfigurationPropertyCollection();
        }

        public LocalServiceElementCollection()
        {
        }

        public void Add(LocalServiceElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(LocalServiceElement element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(LocalServiceElement element)
        {
            BaseRemove(element.Type);
        }

        public void Remove(Type type)
        {
            BaseRemove(type);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LocalServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LocalServiceElement)element).Type;
        }

    } // class LocalServiceElementCollection

}

