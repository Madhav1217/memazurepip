using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class Admin123BO
    {
        public long CORPID { get; set; }
        public long AGENT { get; set; }
        public string PAYMENTPROCESS { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; } = string.Empty;
        public string LASTNAME { get; set; }
        public string DOB { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIPCODE { get; set; }
        public string EMAIL { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; } = string.Empty;
        public string PHONE3 { get; set; } = string.Empty;
        public string FAX { get; set; } = string.Empty;
        public string DLNUMBER { get; set; } = string.Empty;
        public string SSN { get; set; }
        public string TOBACCO { get; set; }
        public PAYMENT PAYMENT { get; set; }
        public string LEAD { get; set; }
        public string CREATEDDATE { get; set; }
        public string SOURCE { get; set; } = string.Empty;
        public string SOURCEDETAIL { get; set; } = string.Empty;
        public string TPVDATETIME { get; set; } = string.Empty;
        public string TPVCODE { get; set; } = string.Empty;
        public string ATTRIBUTE1 { get; set; } = string.Empty;
        public string ATTRIBUTE2 { get; set; } = string.Empty;
        public string ATTRIBUTE3 { get; set; } = string.Empty;
        public string ATTRIBUTE4 { get; set; } = string.Empty;
        public string ATTRIBUTE5 { get; set; } = string.Empty;
        public string ATTRIBUTE6 { get; set; } = string.Empty;
        public CUSTOMFIELDS CUSTOMFIELDS { get; set; }
        public List<DEPENDENTS> DEPENDENTS { get; set; }
        public List<PRODUCTS> PRODUCTS { get; set; }
    }

    public class CUSTOMFIELDS
    {
        public string cq_code_1 { get; set; } = string.Empty;
        public string cq_code_2 { get; set; } = string.Empty;
    }

    public class PRODUCTCUSTOMFIELDS
    {
        public string Key { get; set; }
        public string Answer { get; set; }
    }

    public class DEPENDENTS
    {
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIPCODE { get; set; }
        public string DOB { get; set; }
        public string GENDER { get; set; }
        public string SSN { get; set; }
        public string RELATIONSHIP { get; set; }
    }

    public class PRODUCTS
    {
        public long? PDID { get; set; }
        public string DTEFFECTIVE { get; set; }
        public Dictionary<string, string> CUSTOMFIELDS { get; set; }
        public decimal? PRODUCTFEE { get; set; }
        public int PERIODID { get; set; }
        public string DTBILLING { get; set; }
        public string DTCREATED { get; set; }
        public int BENEFITID { get; set; }
    }

    public class PAYMENT
    {
        public string PAYMENTTYPE { get; set; }
        public string TOKEN { get; set; }
        public string ACHTYPE { get; set; }
        public string ACHROUTING { get; set; }
        public string ACHACCOUNT { get; set; }
        public string ACHBANK { get; set; }
        public string CCTYPE { get; set; }
        public string CCNUMBER { get; set; }
        public int CCEXPMONTH { get; set; } = 0;
        public int CCEXPYEAR { get; set; } = 0;
        public int CCSECURITYCODE { get; set; } = 0;
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string ZIPCODE { get; set; }
    }

    public class RESPONSEPRODUCT
    {
        public string BILLING { get; set; }
        public string POLICYNUMBER { get; set; }
        public string EFFECTIVE { get; set; }
        public int PAID { get; set; }
        public int PDID { get; set; }
        public string RECURRING { get; set; }
    }

    public class RESPONSEDEPENDENT
    {
        public string DTCREATED { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string DOB { get; set; }
        public string FIRSTNAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string UUID { get; set; }
    }

    public class RESPONSEMEMBER
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }

    public class RESPONSERootObject
    {
        public bool SUCCESS { get; set; }
        public List<RESPONSEPRODUCT> PRODUCT { get; set; }
        public List<RESPONSEDEPENDENT> DEPENDENTS { get; set; }
        public RESPONSEMEMBER MEMBER { get; set; }
    }
}
