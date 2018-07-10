using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ObjectCollectionConverter
{
    public static class ObjectDictionaryExtension
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        public static IDictionary<string, string> ToStringDictionary(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                object value = property.GetValue(source);
                if (IsPrimitive(value))
                {
                    dictionary.Add(property.Name, value.ToString());
                }
                else
                {
                    ProcessNonPrimitiveObject(dictionary, property, value);
                }
            }
            return dictionary;
        }

        private static void ProcessNonPrimitiveObject(Dictionary<string, string> dictionary, PropertyDescriptor property, object value)
        {
            var innerObjectDictionary = value.ToStringDictionary();
            foreach (var element in innerObjectDictionary)
            {
                var objectKey = property.Name + ":" + element.Key;
                dictionary.Add(objectKey, element.Value);
            }
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsPrimitive(object source)
        {
            return source.GetType().IsPrimitive || IsOfType<string>(source) || IsOfType<String>(source) || IsOfType<decimal>(source);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
