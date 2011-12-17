using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Global.Test.Console
{
    [XmlRoot(Namespace = "", ElementName = "result")]
    [DataContract(Namespace = "", Name = "result")]
    public class Result<T>
    {
        [XmlElement(ElementName = "code")]
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "successful")]
        [DataMember(Name = "successful")]
        public bool Successful { get; set; }

        [XmlElement(ElementName = "message")]
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [XmlElement(ElementName = "detailedmessage")]
        [DataMember(Name = "detailedmessage")]
        public string DetailedMessage { get; set; }

        [XmlElement(ElementName = "parameter")]
        [DataMember(Name = "parameters")]
        public List<Parameter> Parameters { get; set; }

        [XmlElement(ElementName = "value")]
        [DataMember(Name = "value")]
        public T Value { get; set; }
    }
}