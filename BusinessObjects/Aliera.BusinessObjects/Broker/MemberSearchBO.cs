using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSearchBO
    {
        public MemberSearchBO()
        {
            ExcludeBrokerIds = new List<string>();
        }
        public string MemberId { get; set; }
        public string BrokerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime InActiveDate { get; set; }
        public int ZipCode { get; set; }      
        public bool IsTree { get; set; }
        public int LoggedinBrokerId { get; set; }
        public List<long> InternalBrokerIds { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public MemberProductStatus MemberProductStatus { get; set; }
        public string ProductName { get; set; }
        public List<string> ExcludeBrokerIds { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }        
        public List<MemberBO> MemberList { get; set; }
        public int TotalRecords { get; set; }
        public int ViewBy { get; set; }
        public string Admin123Id { get; set; }
        public string GroupName { get; set; }
    }

    public class FilteredMemberDetil
    {
        public long MemberId { get; set; }
        public int Status { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public int AddressTypeId { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
    }

    public class FilteredGroup
    {
        public long GroupId { get; set; }
        public string GroupLabel { get; set; }
    }

    public class FilteredMemberSubscribed
    {
        public long MemberSubscriptionId { get; set; }
        public int Status { get; set; }
    }

    public class FilteredBroker
    {
        public long BrokerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
