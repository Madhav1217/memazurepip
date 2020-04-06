using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class MasterBO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class StateBO
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string TimeZone { get; set; }
        public string[] ZipCodeRange { get; set; }
        public int MasterDataId { get; set; }
    }

    public class GenderBO
    {
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public string GenderCode { get; set; }
    }

    public class RelationshipBO
    {
        public int RelationshipId { get; set; }
        public string Description { get; set; }
    }

    public class LanguageBO
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}
