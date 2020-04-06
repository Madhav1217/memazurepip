using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerBO
    {
        public long BrokerId { get; set; }
        public long? UserId { get; set; }
        public string Admin123Id { get; set; }
        public string ExternalId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalNpn { get; set; }
        public string Username { get; set; }
        public string BrokerType { get; set; }
        public string Company { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsWebsiteActive { get; set; }
        public BrokerStatus Status { get; set; }
        public DateTimeOffset? TrainingDate { get; set; }
        public DateTime? TermDate { get; set; }
        public string LastLoggedIn { get; set; }
        public BrokerAddressBO BrokerAddress { get; set; }
        public BrokerContactBO BrokerContact { get; set; }
        public bool IsBrokerLicenseActive { get; set; }
        public int BrokerStatus { get; set; }
        public string EnrollmentNotAllowedStates { get; set; }
        public string EnrollmentURI { get; set; }
        public string Website { get; set; }
        public List<string> EnrollmentAllowedStates { get; set; }
        public int SessionIdleTime { get; set; }
        public DateTime ServerDateTime { get; set; }
    }
}