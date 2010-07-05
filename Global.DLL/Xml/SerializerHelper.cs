﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Global.Xml
{
    ///<summary>
    /// Class to be used when performing serialization tasks.
    ///</summary>
    ///<typeparam name="TType">The object type to be serialized/deserialized.</typeparam>
    public class SerializerHelper<TType>
    {
        /// <summary>
        /// Deserializes a file into an object.
        /// </summary>
        /// <param name="xmlFilePath">The content to be serialized.</param>
        /// <returns>The object serialized from the file.</returns>
        public TType DeserializeFromFile(string xmlFilePath)
        {
            TType entity;
            using (TextReader reader = new StreamReader(xmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(TType));
                entity = (TType)serializer.Deserialize(reader);
            }

            return entity;
        }
        /// <summary>
        /// Deserializes a string into an object.
        /// </summary>
        /// <param name="xmlString">The content to be serialized.</param>
        /// <returns>The object serialized from the file.</returns>
        public TType DeserializeFromString(string xmlString)
        {
            TType entity;
            using (var reader = new StringReader(xmlString))
            {
                var serializer = new XmlSerializer(typeof(TType));
                entity = (TType)serializer.Deserialize(reader);
            }

            return entity;
        }
        /// <summary>
        /// Serializes ans object to a file.
        /// </summary>
        /// <param name="xmlFilePath">The file where the serialized object should go to.</param>
        /// <param name="entity">The objkect to be serialized.</param>
        public void SerializeToFile(string xmlFilePath, TType entity)
        {
            using (XmlWriter writer = new XmlTextWriter(xmlFilePath, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                var serializer = new XmlSerializer(typeof(TType));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
            }
        }
        /// <summary>
        /// Serializes an object to a file.
        /// </summary>
        /// <param name="xmlFilePath">The file where the serialized object should go to.</param>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="docTypeName"></param>
        /// <param name="docTypePubId"></param>
        /// <param name="docTypeSysId"></param>
        /// <param name="docTypeSubSet"></param>
        public void SerializeToFile(string xmlFilePath, TType entity, string docTypeName, string docTypePubId,
            string docTypeSysId, string docTypeSubSet)
        {
            using (XmlWriter writer = new XmlTextWriter(xmlFilePath, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                writer.WriteDocType(docTypeName, docTypePubId, docTypeSysId, docTypeSubSet);
                var serializer = new XmlSerializer(typeof(TType));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
            }
        }
        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <returns>The serialized object string.</returns>
        public string SerializeToString(TType entity)
        {
            string xml;
            var ms = new MemoryStream();
            using (XmlWriter writer = new XmlTextWriter(ms, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                var serializer = new XmlSerializer(typeof(TType));
                serializer.Serialize(writer, entity, GetEmptyNamespace());
                xml = ByteArrayToString(Encoding.UTF8, ms.ToArray());
            }

            return xml;
        }
        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="entity">The object to be serialized.</param>
        /// <param name="docTypeName"></param>
        /// <param name="docTypePubId"></param>
        /// <param name="docTypeSysId"></param>
        /// <param name="docTypeSubSet"></param>
        /// <returns>The serialized object string.</returns>
        public string SerializeToString(TType entity, string docTypeName, string docTypePubId,
            string docTypeSysId, string docTypeSubSet)
        {
            string xml;
            var ms = new MemoryStream();
            using (XmlWriter writer = new XmlTextWriter(ms, Encoding.UTF8))
            {
                writer.WriteStartDocument();
                writer.WriteDocType(docTypeName, docTypePubId, docTypeSysId, docTypeSubSet);
                var serializer = new XmlSerializer(typeof(TType));
                serializer.Serialize(ms, entity, GetEmptyNamespace());
                xml = ByteArrayToString(Encoding.UTF8, ms.ToArray());
            }

            return xml;
        }
        ///<summary>
        /// Gets the namespaces from a dictionary.
        ///</summary>
        ///<param name="prefixNs">A collection of namespace prefixes to be added.</param>
        ///<returns>Xml serializer namespaces.</returns>
        public XmlSerializerNamespaces GetNamespace(Dictionary<string, string> prefixNs)
        {
            var namespaces = new XmlSerializerNamespaces();
            foreach (KeyValuePair<string, string> keyValue in prefixNs)
            {
                namespaces.Add(keyValue.Key, keyValue.Value);
            }

            return namespaces;
        }
        ///<summary>
        /// Gets a collection of empty namespaces.
        ///</summary>
        ///<returns>Xml serializer namespaces.</returns>
        public XmlSerializerNamespaces GetEmptyNamespace()
        {
            var dic = new Dictionary<string, string>
                                                 {
                                                     {
                                                         String.Empty,
                                                         String.Empty
                                                     }
                                                 };
            return GetNamespace(dic);
        }
        /// <summary>
        /// To convert a Byte Array of Unicode values to a complete String.
        /// </summary>
        /// <param name="encoding">The encoding to be applied in the convertion.</param>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String ByteArrayToString(Encoding encoding, Byte[] characters)
        {
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }
    }
}
