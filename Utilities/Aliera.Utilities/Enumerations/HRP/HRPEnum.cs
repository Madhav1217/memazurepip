using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.Utilities.Enumerations.HRP
{
    public class HRPEnum
    {
        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/membershipsparse")]
        public enum MaintenanceCodeType
        {

            /// <remarks/>
            CREATE,

            /// <remarks/>
            CHANGE,

            /// <remarks/>
            TERMINATE,

            /// <remarks/>
            REINSTATEMENT,

            /// <remarks/>
            AUDIT,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/membershipsparse")]
        public enum OverrideResultType
        {

            /// <remarks/>
            ADD,

            /// <remarks/>
            AMEND,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum SmokingStatusType
        {

            /// <remarks/>
            Smoker,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Non Smoker")]
            NonSmoker,

            /// <remarks/>
            Unknown,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum GenderCodeType
        {

            /// <remarks/>
            Male,

            /// <remarks/>
            Female,

            /// <remarks/>
            Unknown,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum MaritalStatusCodeType
        {

            /// <remarks/>
            Single,

            /// <remarks/>
            Married,

            /// <remarks/>
            Divorced,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Legally Separated")]
            LegallySeparated,

            /// <remarks/>
            Separated,

            /// <remarks/>
            Widowed,

            /// <remarks/>
            Unmarried,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Registered Domestic Partner")]
            RegisteredDomesticPartner,

            /// <remarks/>
            Unreported,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum DisabilityTypeDomainType
        {

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Awaiting Verification")]
            AwaitingVerification,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("No Verification Received")]
            NoVerificationReceived,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Verification Under Review")]
            VerificationUnderReview,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Indefinitely Approved")]
            IndefinitelyApproved,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Temporarily Approved")]
            TemporarilyApproved,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Awaiting Re-verification")]
            AwaitingReverification,

            /// <remarks/>
            Denied,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum SalaryIntervalDomainType
        {

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Semi-Monthly")]
            SemiMonthly,

            /// <remarks/>
            Quarterly,

            /// <remarks/>
            Weekly,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("Bi-Weekly")]
            BiWeekly,

            /// <remarks/>
            Monthly,

            /// <remarks/>
            Annually,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum TransactionExceptionStatusType
        {

            /// <remarks/>
            Repair,

            /// <remarks/>
            Review,

            /// <remarks/>
            Reject,

            /// <remarks/>
            Warn,
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        //[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.healthedge.com/connector/schema/basetypes")]
        public enum ListActionType
        {

            /// <remarks/>
            DEFAULT,

            /// <remarks/>
            REPLACE,
        }

    }
}
