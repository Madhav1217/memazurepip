namespace Aliera.Utilities.Enumerations
{
    public enum QuestionnaireType
    {
        StatementOfBelief = 1,
        HealthBasedQuestions = 2,
        DependentHealthBasedQuestions = 3
    }

    public enum Relationship
    {
        Self = 1,
        Spouse = 2,
        Child = 3,
        Others = 4
    }

    public enum BrokerStatus
    {
        Active = 1,
        Pending = 2,
        Terminated = 3
    }

    public enum ProductStatus
    {
        All = 1,
        Active = 2,
        InActive = 3
    }

    public enum MemberStatus
    {
        Active = 1,
        OnHold = 2,
        Inactive = 4,
        Pending = 6
    }

    public enum MemberProductStatus
    {
        Active = 1,
        OnHold = 2,
        UnderReview = 3,
        Inactive = 4,
        Pending = 6
    }

    public enum InvoicePeriod
    {
        All = 1,
        LastMonth = 2,
        Last3Months = 3,
        Last6Months = 4,
        Last12Months = 5
    }

    public enum FamilyIndicator
    {
        Individual = 1,
        IndividualPlusSpouse = 2,
        Family = 3,
        IndividualPlusChild = 4,
        IndividualPlus1Dependent = 5,
        NotApplicable = 6
    }

    public enum ComputationType
    {
        Individual = 1,
        Family = 2,
    }

    public enum PlanType
    {
        Primary = 1,
        Supplemental = 2,
        Addons = 3,
        Packaged = 4
    }

    public enum BrokerNavigation
    {
        Demographics = 1,
        Plan = 2,
        Payment = 3,
        StateOfBelief = 4,
        HealthBasedQuestions = 5
    }

    public enum AddressType
    {
        Primary = 1,
        Secondary = 2
    }

    public enum NotificationType
    {
        Email = 1,
        Sms = 2
    }

    public enum ViewBy
    {
        All,
        OnlyMe,
        TreeOnly,
        BrokerId
    }

    public enum MemberType
    {
        Individual = 1,
        Group = 2
    }

    public enum MemberRegistrationVerification
    {
        ExistingMember = 6000,
        ValidMemberId = 6001,
        InvalidMemberId = 6002,
        InvalidDetails = 6003,
        Verified = 6004,
        SentOtpSuccessfully = 6005,
        InvalidPhoneNumber = 8010,
        InvalidEmailId = 8011,
        InvalidEmailIdAndPhoneNumber = 8012,
        PhoneNumberEmptyAndInvalidEmailId = 8013,
        EmailIdEmptyAndInvalidPhoneNumber = 8014,
        PhoneNumberEmpty = 8015,
        EmailIdEmpty = 8016,
        EmailIdAndPhoneNumberAreEmpty = 8017
    }

    public enum NextButton
    {
        Demographics = 1,
        PlanDetails = 2,
        PlanPricing = 3,
        PlanPayment = 4,
        Payment = 5,
        StateofBelief = 6,
        HealthBasedQuestions = 7
    }

    public enum EmailType
    {
        MemberSummary = 1,
        MemberSms = 2,
        BrokerResetRequest = 3,
        BrokerResetSuccess = 4,
        BrokerAccountAccess = 5,
        SetSuccess = 6,
        MemberRegistrationOtp = 7,
        MemberRegistrationSuccess = 8,
        MemberStoryTestimonial = 12,
        MemberStory = 13,
        MemberContactSubmission = 14,
        MemberDependentInactivation = 15,
        MemberDependentActivation = 16,
        MemberResetRequest = 9,
        MemberResetSuccess = 10,
        MemberAccountAccess = 11,
        AdminAccountAccess = 17,
        AdminResetRequest = 18,
        AdminResetSuccess = 19,
        BrokerResendUserName = 21,
        MemberResendUserName = 22,
        AdminResendUserName = 23,
        WelcomeUser = 24,
        AdminUnlockAccount = 25,
        MemberRequestForPhysicalIdCard = 26,
        BrokerVerify = 27,
        ResendBrokerVerify = 28,
        ResendWelcomeUser = 29
    }

    public enum PaymentType
    {
        CC = 1,
        ACH = 2,
    }

    public enum CCType
    {
        Visa = 1,
        Mastercard = 2,
        AmericanExpress = 3,
        Discover = 4
    }

    public enum ACHType
    {
        Checking = 'C',
        Savings = 'S'
    }

    public enum PayPeriod
    {
        Weekly = 1,
        BiMonthly = 2
    }

    public enum CommissionStatus
    {
        Earned = 1,
        Paid = 2
    }

    public enum Days
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public enum AuditAction
    {
        Select = 1,
        Insert = 2,
        Update = 3,
        Delete = 4
    }

    public enum ClaimType
    {
        All = 0,
        Medical = 1,
        Dental = 2,
        Vision = 3
    }

    public enum Portals
    {
        BrokerPortal = 1,
        MemberPortal = 2,
        ProviderPortal = 3,
        EmployerPortal = 4,
        AdminPortal = 5
    }

    public enum EntityType
    {
        Broker = 1,
        Member = 2,
        InternalUser = 3
    }

    public enum EnrollmentPeriod
    {
        Last3Months = 1,
        Today = 2,
        Yesterday = 3,
        Last7Days = 4,
        LastWeek = 5,
        LastMonth = 6,
        CurrentMonthToDate = 7
    }

    public enum MemberFeedbackType
    {
        ShareStory = 1,
        ShareContactInfo = 2
    }

    public enum DependentAddRemove
    {
        IsAdded = 7000,
        IsNotAdded = 7001,
        IsDeleted = 7002,
        IsNotDeleted = 7003,
        IsExisting = 7004
    }

    public enum PaymentUpdate
    {
        IsUpdated = 7005,
        IsNotUpdated = 7006
    }

    public enum DependentStatus
    {
        PendingActivation = 7007,
        Active = 7008,
        PendingInactivation = 7009,
        Inactive = 7010,
        NoStatus = 7011
    }

    public enum ValidateState
    {
        StateInvalid = 7013
    }

    public enum EditInformationStatus
    {
        Success = 7014,
        Fail = 7015
    }

    public enum Permission
    {
        No = 0,
        Yes = 1,
        NoAccess = 2
    }

    public enum Login
    {
        InvalidPasswordHash = 6005,
        ResetPasswordSuccess = 6006,
        PasswordAlreadyExists = 6007,
        BadRequest = 6008,
        AccountLocked = 6009,
        AccountAlreadyLocked = 6004,
        InvalidLoginUserNameAndPassword = 6010,
        ChangePasswordSuccess = 6011,
        InvalidCurrentPassword = 6012,
        LoginSuccess = 6013,
        PasswordShouldNotSameAsUserName = 6014,
        PasswordExpired = 6015,
        ResetPasswordLinkExpired = 6016,
        UserNotFoundErrorCode = 5001,
        NoSecurityQuestionsErrorCode = 5005,
        AnswersDoNotMatchErrorCode = 5006,
        InvalidPasswordResetTokenErrorCode = 5007,
        ModelValidationErrorCode = 5000,
        InvalidInput = 6018
    }

    public enum JobStatus
    {
        Pending = 1,
        InProgress = 2,
        Success = 3,
        Failed = 4,
        Retry = 5
    }

    public enum DeliveryTypes
    {
        Email = 1,
        Manual = 2,
        Both = 3,
        None = 4
    }

    public enum RuleType
    {
        Recurring = 1,
        Adhoc = 2
    }

    public enum DataFormat
    {
        XML = 1,
        JSON = 2,
        INVALID = 3
    }

    //Role Management Enums
    public enum RoleType
    {
        Internal = 1,
        External = 2,
        All = 0
    }

    public enum SearchStatus
    {
        InActive = 0,
        Active = 1,
        Locked = 2,
        All = 3,
        Pending = 4,
        LockedAndPending = 5
    }

    public enum SearchBy
    {
        All = 0,
        UserName = 1,
        FullName = 2,
        ID = 3,
        Email = 4,
        Status = 5,
        Role = 6
    }

    //Role Management Enums
    public enum Status
    {
        Inactive = 0,
        Active = 1,
        All = 2
    }

    //Role Management Enums
    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1
    }

    public enum Environment
    {
        Development = 1,
        QA = 2,
        UAT = 3,
        Production = 4
    }

    public enum Admin123Type
    {
        Enrollment = 1,
        BrokerDemographics = 2,
        MemberDemographics = 3
    }

    public enum UserStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum PortalRoleStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum PortalRole
    {
        Member = 4
    }

    public enum DocumentCategoryType
    {
        BrokerDocument = 1,
        BrokerDocumentAndForm = 2,
        MemberDocumentAndForm = 3
    }

    public enum ProviderNetworkId
    {
        Dental = 3,
        Vision = 4
    }

    public enum Supplemental
    {
        TrinityDentalVision = 25594,
        TrinityDental = 25595,
        TrinityVision = 25596,
        UnityDental = 17182,
        UnityVision = 17183,
        UnityDentalVision = 17181
    }

    public enum DocumentType
    {
        Guide = 1,
        SellSheet = 2,
        General = 3
    }


}