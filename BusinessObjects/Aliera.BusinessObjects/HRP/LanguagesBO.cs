using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    public class LanguagesBO
    {
        [XmlElement("listMode")]
        public string ListMode { get; set; }

        [XmlElement("language")]
        public LanguageBO Language { get; set; }
    }

    public class LanguageBO
    {
        [XmlElement("primaryLanguage")]
        public bool PrimaryLanguage { get; set; }

        [XmlElement("languageDomainCode")]
        public LanguagedomaincodeBO LanguageDomainCode { get; set; }
    }

    public class LanguagedomaincodeBO
    {
        [XmlElement("codeSetName")]
        public string CodeSetName { get; set; }

        [XmlElement("codeEntry")]
        public string CodeEntry { get; set; }
    }
}
