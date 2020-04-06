using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    public class PhysicaladdressBO
    {
        [XmlElement("memberPhysicalAddress")]
        public MemberphysicaladdressBO MemberPhysicalAddress { get; set; }
    }

    public class MemberphysicaladdressBO
    {
        [XmlElement("emailAddress")]
        public string EmailAddress { get; set; }

        [XmlElement("addressInfo")]
        public AddressinfoBO AddressInfo { get; set; }

        [XmlElement("addressTypeCode")]
        public AddresstypecodeBO AddressTypeCode { get; set; }
    }

    public class AddressinfoBO
    {
        [XmlElement("postalAddress")]
        public PostaladdressBO PostalAddress { get; set; }

        [XmlElement("addressPhoneList")]
        public AddressphonelistBO AddressPhoneList { get; set; }
    }

    public class PostaladdressBO
    {
        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("stateCode")]
        public string StateCode { get; set; }

        [XmlElement("zipCode")]
        public string ZipCode { get; set; }

        [XmlElement("cityName")]
        public string CityName { get; set; }

        [XmlElement("ignoreAddressCheck")]
        public bool IgnoreAddressCheck { get; set; }
    }

    public class AddressphonelistBO
    {
        [XmlElement("telephoneNumber")]
        public TelephonenumberBO TelephoneNumber { get; set; }
    }

    public class TelephonenumberBO
    {
        [XmlElement("phoneCountryCode")]
        public int PhoneCountryCode { get; set; }

        [XmlElement("phoneAreaCode")]
        public int PhoneAreaCode { get; set; }

        [XmlElement("phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class AddresstypecodeBO
    {
        [XmlElement("codeSetName")]
        public string CodeSetnNme { get; set; }

        [XmlElement("codeEntry")]
        public int CodeEntry { get; set; }
    }

    public class CorrespondenceaddressBO
    {
        [XmlElement("postalAddress")]
        public PostaladdressBO PostalAddress { get; set; }
    }

    public class OtheridnumberlistBO
    {
        [XmlElement("listMode")]
        public string ListMode { get; set; }

        [XmlElement("identificationNumber")]
        public List<IdentificationnumberBO> IdentificationNumber { get; set; } // 12345
        //3 properties required with same name. Need to check.
        //public IdentificationnumberBO identificationnumber { get; set; } // RHToken-777777
        //public IdentificationnumberBO identificationnumber { get; set; } // 342567
    }
}
