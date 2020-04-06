using Microsoft.EntityFrameworkCore;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public virtual DbSet<BrokerTree> BrokerTree { get; set; }
        public virtual DbSet<BrokerIdTree> BrokerIdTree { get; set; }
        public virtual DbSet<MemberStatusGraph> MemberStatusGraph { get; set; }
        public virtual DbSet<EnrollmentGraph> EnrollmentGraph { get; set; }
        public virtual DbSet<ProductStatusGraph> ProductStatusGraph { get; set; }
        public virtual DbSet<BrokerHierarchyGraph> BrokerHierarchyGraph { get; set; }
        public virtual DbSet<Accumulator> Accumulator { get; set; }
        public virtual DbSet<AddOnPlan> AddOnPlan { get; set; }
        public virtual DbSet<Admin123Log> Admin123Log { get; set; }
        public virtual DbSet<Admin123MasterData> Admin123MasterData { get; set; }
        public virtual DbSet<AgentCommission> AgentCommission { get; set; }
        public virtual DbSet<AgentProductApplicationFee> AgentProductApplicationFee { get; set; }
        public virtual DbSet<AgentUserBridge> AgentUserBridge { get; set; }
        public virtual DbSet<Benefit> Benefit { get; set; }
        public virtual DbSet<Broker> Broker { get; set; }
        public virtual DbSet<BrokerAddress> BrokerAddress { get; set; }
        public virtual DbSet<BrokerContact> BrokerContact { get; set; }
        public virtual DbSet<BrokerDocument> BrokerDocument { get; set; }
        public virtual DbSet<BrokerHierarchy> BrokerHierarchy { get; set; }
        public virtual DbSet<BrokerLicense> BrokerLicense { get; set; }
        public virtual DbSet<BrokerPayPeriod> BrokerPayPeriod { get; set; }
        public virtual DbSet<BrokerProduct> BrokerProduct { get; set; }
        public virtual DbSet<BrokerType> BrokerType { get; set; }
        public virtual DbSet<BulletinBoardNews> BulletinBoardNews { get; set; }
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<ClaimsStatus> ClaimsStatus { get; set; }
        public virtual DbSet<CommissionReport> CommissionReport { get; set; }
        public virtual DbSet<BrokerCommission> BrokerCommission { get; set; }
        public virtual DbSet<CommissionPayPeriodReport> CommissionPayPeriodReport { get; set; }
        public virtual DbSet<CombinedProduct> CombinedProduct { get; set; }
        public virtual DbSet<CommissionAllPayPeriodData> CommissionAllPayPeriodData { get; set; }
        public virtual DbSet<CommissionPayPeriodData> CommissionPayPeriodData { get; set; }
        public virtual DbSet<ConsentMemberQuestionAnswer> ConsentMemberQuestionAnswer { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<DocumentAndForm> DocumentAndForm { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategory { get; set; }
        public virtual DbSet<Eodetails> Eodetails { get; set; }
        public virtual DbSet<Faq> Faq { get; set; }
        public virtual DbSet<Faqcategory> Faqcategory { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupAddress> GroupAddress { get; set; }
        public virtual DbSet<BrokerCommissions> BrokerCommissions { get; set; }
        public virtual DbSet<InvoiceAging> InvoiceAging { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<MasterData> MasterData { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberAccumulatorDetails> MemberAccumulatorDetails { get; set; }
        public virtual DbSet<MemberAddress> MemberAddress { get; set; }
        public virtual DbSet<MemberDependent> MemberDependent { get; set; }
        public virtual DbSet<MemberDependentAddress> MemberDependentAddress { get; set; }
        public virtual DbSet<MemberDependentQuestionAnswer> MemberDependentQuestionAnswer { get; set; }
        public virtual DbSet<MemberDependentStatusHistory> MemberDependentStatusHistory { get; set; }
        public virtual DbSet<MemberDetail> MemberDetail { get; set; }
        public virtual DbSet<MemberFeedback> MemberFeedback { get; set; }
        public virtual DbSet<MemberPaymentDetail> MemberPaymentDetail { get; set; }
        public virtual DbSet<MemberPlanParameter> MemberPlanParameter { get; set; }
        public virtual DbSet<MemberQuestionAnswer> MemberQuestionAnswer { get; set; }
        public virtual DbSet<MemberQuestionnaire> MemberQuestionnaire { get; set; }
        public virtual DbSet<MemberStatusHistory> MemberStatusHistory { get; set; }
        public virtual DbSet<MemberSubscribedPlan> MemberSubscribedPlan { get; set; }
        public virtual DbSet<MemberSubscribedPlanHistory> MemberSubscribedPlanHistory { get; set; }
        public virtual DbSet<MemberSubscription> MemberSubscription { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<MigratedMemberProductDetails> MigratedMemberProductDetails { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<PlanBenefit> PlanBenefit { get; set; }
        public virtual DbSet<PlanPackage> PlanPackage { get; set; }
        public virtual DbSet<PlanPremium> PlanPremium { get; set; }
        public virtual DbSet<Portals> Portals { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductDocumentLink> ProductDocumentLink { get; set; }
        public virtual DbSet<ProductAccumulator> ProductAccumulator { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductFee> ProductFee { get; set; }
        public virtual DbSet<ProductProviderNetwork> ProductProviderNetwork { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public virtual DbSet<ProviderNetwork> ProviderNetwork { get; set; }
        public virtual DbSet<RecipientCategory> RecipientCategory { get; set; }
        public virtual DbSet<SecurityQuestions> SecurityQuestions { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Subgroup> Subgroup { get; set; }
        public virtual DbSet<TemplateDetails> TemplateDetails { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserSecurityQuestionAnswers> UserSecurityQuestionAnswers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accumulator>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AddOnPlan>(entity =>
            {
                entity.Property(e => e.AddOnPlanIds).HasMaxLength(500);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.AddOnPlan)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Admin123Log>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin123MasterData>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<AgentCommission>(entity =>
            {
                entity.HasIndex(e => new { e.AgentId, e.AgentActive, e.PayToAgentActive, e.ProductId });

                entity.HasIndex(e => new { e.AgentId, e.AgentActive, e.ProductId, e.PayToAgentActive });

                entity.Property(e => e.Advancement)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AdvancementMonths)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AgentActive)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.AgentLabel)
                    .HasMaxLength(77)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.AmountType)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.BillingEndDate).HasColumnType("date");

                entity.Property(e => e.BillingStartDate).HasColumnType("date");

                entity.Property(e => e.CommissionEnd).HasColumnType("date");

                entity.Property(e => e.CommissionStart).HasColumnType("date");

                entity.Property(e => e.Commissionable)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(77)
                    .IsUnicode(false);

                entity.Property(e => e.ExcludeAllPriceTypes)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExcludePriceTypes)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExcludeforPaymentNumber)
                    .HasMaxLength(26)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.IncludeAllPriceTypes)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IncludePriceTypes)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IncludeforPaymentNumber)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.MemberActiveEndDate).HasColumnType("date");

                entity.Property(e => e.MemberActiveStartDate).HasColumnType("date");

                entity.Property(e => e.Parent)
                    .HasMaxLength(58)
                    .IsUnicode(false);

                entity.Property(e => e.PayOnPaymentNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PayToAgentLabel)
                    .HasMaxLength(77)
                    .IsUnicode(false);

                entity.Property(e => e.PriceIds)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PriceTypes)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProductLabel)
                    .HasMaxLength(81)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionEndDate).HasColumnType("date");

                entity.Property(e => e.TransactionStartDate).HasColumnType("date");

                entity.Property(e => e.TreeCount)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.WebsiteDisplayActive)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentCommissionAgent)
                    .HasForeignKey(d => d.AgentId);

                entity.HasOne(d => d.PayToAgent)
                    .WithMany(p => p.AgentCommissionPayToAgent)
                    .HasForeignKey(d => d.PayToAgentId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AgentCommission)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<AgentProductApplicationFee>(entity =>
            {
                entity.HasKey(e => new { e.BrokerId, e.ProductId })
                    .HasName("PK_AgentProductApplicationFee_BrokerId_ProductId");

                entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.AgentProductApplicationFee)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AgentProductApplicationFee)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AgentUserBridge>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__AgentUserBridge__UserId");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentUserBridge)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AgentUserBridge__Broker__BrokerId");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AgentUserBridge)
                    .HasForeignKey<AgentUserBridge>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AgentUserBridge__User__UserId");
            });

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.HasIndex(e => e.Admin123Id);

                entity.HasIndex(e => e.ExternalId);

                entity.HasIndex(e => e.PersonalNpn);

                entity.HasIndex(e => new { e.DateOfBirth, e.PersonalNpn, e.Status })
                    .HasName("IX_Broker_Status");

                entity.HasIndex(e => new { e.DateOfBirth, e.Status, e.PersonalNpn })
                    .HasName("IX_Broker_Status_PersonalNPN");

                entity.Property(e => e.Admin123Id).HasMaxLength(50);

                entity.Property(e => e.Alias).HasMaxLength(100);

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PersonalNpn)
                    .HasColumnName("PersonalNPN")
                    .HasMaxLength(10);

                entity.Property(e => e.Salutation).HasMaxLength(10);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.Property(e => e.TaxId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TermDate).HasColumnType("datetime");

                entity.HasOne(d => d.BrokerType)
                    .WithMany(p => p.Broker)
                    .HasForeignKey(d => d.BrokerTypeId);
            });

            modelBuilder.Entity<BrokerAddress>(entity =>
            {
                entity.HasIndex(e => e.BrokerId);

                entity.Property(e => e.AddressLine1).HasMaxLength(500);

                entity.Property(e => e.AddressLine2).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerAddress)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BrokerContact>(entity =>
            {
                entity.HasIndex(e => new { e.PhoneNumber, e.Ext, e.PhoneNumberAlternate, e.BestTimeToCall, e.FaxNumber, e.EmailId, e.EmailIdAlternate, e.Website, e.DoNotCall, e.EmailOptOut, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.BrokerId })
                    .HasName("IX_BrokerContact_BrokerId");

                entity.Property(e => e.BestTimeToCall).HasMaxLength(15);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DoNotCall).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmailId).HasMaxLength(255);

                entity.Property(e => e.EmailIdAlternate).HasMaxLength(255);

                entity.Property(e => e.EmailOptOut).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ext).HasMaxLength(6);

                entity.Property(e => e.FaxNumber).HasMaxLength(10);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PhoneNumberAlternate).HasMaxLength(20);

                entity.Property(e => e.Website).HasMaxLength(500);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerContact)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BrokerDocument>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplayDocumentName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.DocumentId123)
                    .HasColumnName("DocumentId_123")
                    .HasMaxLength(50);

                entity.Property(e => e.DocumentLabel).HasMaxLength(100);

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.DocumentNotes).HasMaxLength(3000);

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerDocument)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.DocumentCategory)
                    .WithMany(p => p.BrokerDocument)
                    .HasForeignKey(d => d.DocumentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrokerDocument_DocumentCategoryId");
            });

            modelBuilder.Entity<BrokerHierarchy>(entity =>
            {
                entity.HasIndex(e => e.BrokerId)
                    .HasName("IX_BrokerHierarchy_brokerid");

                entity.HasIndex(e => e.ParentBrokerId)
                    .HasName("IX_BrokerHierarchy_parentbrokerid");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerHierarchyBroker)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ParentBroker)
                    .WithMany(p => p.BrokerHierarchyParentBroker)
                    .HasForeignKey(d => d.ParentBrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BrokerLicense>(entity =>
            {
                entity.HasKey(e => new { e.LicenseNumber, e.BrokerId, e.StateCode })
                .HasName("PK_Composite_BrokerLicense_LicenseNumber_BrokerId")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.StateCode);

                entity.HasIndex(e => new { e.BrokerId, e.ActiveDate, e.ExpiryDate });

                entity.HasIndex(e => new { e.LicenseNumber, e.BrokerId, e.ActiveDate, e.LicenseType, e.Description, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.StateCode, e.ExpiryDate })
                    .HasName("IX_BrokerLicense_StateCode_ExpiryDate");

                entity.Property(e => e.LicenseNumber).HasMaxLength(50);

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseType).HasMaxLength(60);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<BrokerPayPeriod>(entity =>
            {
                entity.HasKey(e => e.PayPeriodId)
                .HasName("PK_BrokerPayPeriod_PayPeriodId");

                entity.HasIndex(e => new { e.PayPeriod, e.StartPeriod, e.EndPeriod, e.BrokerId })
                    .HasName("IX_BrokerPayPeriod_BrokerId");

                entity.Property(e => e.EndPeriod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartPeriod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerPayPeriod)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PayPeriod_Broker_BrokerId");
            });

            modelBuilder.Entity<BrokerProduct>(entity =>
            {
                entity.HasKey(e => new { e.BrokerId, e.ProductId })
                .HasName("PK_Composite_BrokerProduct_BrokerId_ProductId")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.ApplicationFee).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NoSaleStates).HasMaxLength(2000);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.BrokerProduct)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BrokerProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BrokerType>(entity =>
            {
                entity.Property(e => e.BrokerTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BulletinBoardNews>(entity =>
            {
                entity.Property(e => e.News).IsRequired();

                entity.Property(e => e.NewsCreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Claims>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.Admin123Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BilledAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ClaimsNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimsStatusId).HasColumnName("ClaimsStatusID");

                entity.Property(e => e.ClaimsTypeId).HasColumnName("ClaimsTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.DueAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FacilityName).IsRequired();

                entity.Property(e => e.MemberExternalId)
                    .IsRequired()
                    .HasColumnName("MemberExternalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Msraamount)
                    .HasColumnName("MSRAAmount")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PlanDiscountedRate).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PlanPaidAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProcessingDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceDescription).IsRequired();

                entity.Property(e => e.ServiceEndDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClaimsStatus)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.ClaimsStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<CombinedProduct>(entity =>
            {
                entity.Property(e => e.PlanId)
                    .IsRequired()
                    .HasMaxLength(500);
            });
            modelBuilder.Entity<ClaimsStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CommissionReport>(entity =>
            {
                entity.HasIndex(e => new { e.PostedDate, e.Type, e.PayeeAgentId, e.MemberAgentId })
                    .HasName("IX_CommissionReport_Payee Agent ID_Member Agent ID");

                entity.HasIndex(e => new { e.CreatedDate, e.Type, e.Subtype, e.Notes, e.Credit, e.Debit, e.Total, e.PayeeAgentLabel, e.PayeeAddress1, e.PayeeAddress2, e.PayeeCity, e.PayeeState, e.PayeeZipcode, e.MemberId, e.MemberCompany, e.MemberFirstName, e.MemberLastName, e.MemberAgentId, e.MemberAgentLabel, e.MemberAgentCategory, e.MemberSourceDetail, e.MemberLeadStatus, e.ProductId, e.ProductLabel, e.ProductCategory, e.ProductCategory2, e.ProductCode, e.ProductCreatedDate, e.ProductActiveDate, e.TransactionId, e.TransactionAmount, e.TransactionType, e.TransactionPayment, e.TransactionPaidThroughStart, e.TransactionPaidThroughEnd, e.CommissionableAmount, e.Payout, e.GroupId, e.PayeeAgentId, e.PostedDate })
                    .HasName("IX_CommissionReport_Payee Agent ID_Posted Date");

                entity.HasIndex(e => new { e.PostedDate, e.CreatedDate, e.Type, e.Subtype, e.Notes, e.Credit, e.Debit, e.Total, e.PayeeAgentLabel, e.PayeeAddress1, e.PayeeAddress2, e.PayeeCity, e.PayeeState, e.PayeeZipcode, e.MemberId, e.MemberCompany, e.MemberFirstName, e.MemberLastName, e.MemberAgentId, e.MemberAgentLabel, e.MemberAgentCategory, e.MemberSourceDetail, e.MemberLeadStatus, e.ProductId, e.ProductLabel, e.ProductCategory, e.ProductCategory2, e.ProductCode, e.ProductCreatedDate, e.ProductActiveDate, e.TransactionId, e.TransactionAmount, e.TransactionType, e.TransactionPayment, e.TransactionPaidThroughStart, e.TransactionPaidThroughEnd, e.CommissionableAmount, e.Payout, e.GroupId, e.PayeeAgentId })
                    .HasName("IX_CommissionReport_Payee Agent ID");

                entity.Property(e => e.CommissionableAmount)
                    .HasColumnName("Commissionable Amount")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("Created Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Credit)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.MemberAgentCategory)
                    .HasColumnName("Member Agent Category")
                    .HasMaxLength(28)
                    .IsUnicode(false);

                entity.Property(e => e.MemberAgentId).HasColumnName("Member Agent ID");

                entity.Property(e => e.MemberAgentLabel)
                    .HasColumnName("Member Agent Label")
                    .HasMaxLength(77)
                    .IsUnicode(false);

                entity.Property(e => e.MemberCompany)
                    .HasColumnName("Member Company")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MemberFirstName)
                    .HasColumnName("Member First Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("Member ID");

                entity.Property(e => e.MemberLastName)
                    .HasColumnName("Member Last Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemberLeadStatus)
                    .HasColumnName("Member Lead Status")
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.MemberSourceDetail)
                    .HasColumnName("Member Source Detail")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(82)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeAddress1)
                    .HasColumnName("Payee Address 1")
                    .HasMaxLength(43)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeAddress2)
                    .HasColumnName("Payee Address 2")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeAgentId).HasColumnName("Payee Agent ID");

                entity.Property(e => e.PayeeAgentLabel)
                    .HasColumnName("Payee Agent Label")
                    .HasMaxLength(77)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeCity)
                    .HasColumnName("Payee City")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeState)
                    .HasColumnName("Payee State")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeZipcode)
                    .HasColumnName("Payee Zipcode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Payout)
                    .HasColumnName(" Payout")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PostedDate)
                    .HasColumnName("Posted Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductActiveDate)
                    .HasColumnName("Product Active Date")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCategory)
                    .HasColumnName("Product Category")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCategory2)
                    .HasColumnName("Product Category 2")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasColumnName("Product Code")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCreatedDate)
                    .HasColumnName("Product Created Date")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("Product ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProductLabel)
                    .HasColumnName("Product Label")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Subtype)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionAmount)
                    .HasColumnName("Transaction Amount")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("Transaction ID")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionPaidThroughEnd)
                    .HasColumnName("Transaction Paid Through End")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionPaidThroughStart)
                    .HasColumnName("Transaction Paid Through Start")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionPayment)
                    .HasColumnName("Transaction Payment")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasColumnName("Transaction Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CommissionReport)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.MemberAgent)
                    .WithMany(p => p.CommissionReportMemberAgent)
                    .HasForeignKey(d => d.MemberAgentId)
                    .HasConstraintName("FK_CommissionReport_Broker_[Member Agent ID]");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CommissionReport)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_CommissionReport_Member_[Member ID]");

                entity.HasOne(d => d.PayeeAgent)
                    .WithMany(p => p.CommissionReportPayeeAgent)
                    .HasForeignKey(d => d.PayeeAgentId)
                    .HasConstraintName("FK_CommissionReport_Broker_[Payee Agent ID]");
            });

            modelBuilder.Entity<CommissionAllPayPeriodData>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Commission)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Credit)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GroupExternalId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MemberBrokerCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MemberExternalId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("MemberId");

                entity.Property(e => e.PayeeBrokerCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeExternalId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Premium)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Refunds)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Subtype)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CommissionCalculatedReport)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<CommissionPayPeriodData>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Commission)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Credit)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Debit)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GroupExternalId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MemberBrokerCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MemberExternalId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId).HasColumnName("MemberId");

                entity.Property(e => e.PayeeBrokerCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PayeeExternalId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Premium)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Refunds)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Subtype)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CommissionCalculatedReport90Days)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<ConsentMemberQuestionAnswer>(entity =>
            {
                entity.Property(e => e.ChangedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MemberAnswer).IsRequired();

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ConsentMemberQuestionAnswer)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MemberQuestionnaire)
                    .WithMany(p => p.ConsentMemberQuestionAnswer)
                    .HasForeignKey(d => d.MemberQuestionnaireId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Subgroup)
                    .WithMany(p => p.Division)
                    .HasForeignKey(d => d.SubgroupId);
            });

            modelBuilder.Entity<DocumentAndForm>(entity =>
            {
                entity.Property(e => e.Ext)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ext)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DocumentCategoryId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Portal)
                    .WithMany(p => p.DocumentAndForm)
                    .HasForeignKey(d => d.PortalId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DocumentCategory>(entity =>
            {
                entity.Property(e => e.DocumentCategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Eodetails>(entity =>
            {
                entity.HasKey(e => e.Eoid)
                .HasName("PK_EODetails_EOId");

                entity.ToTable("EODetails");

                entity.Property(e => e.Eoid).HasColumnName("EOId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Eoamount)
                    .HasColumnName("EOAmount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Eocarrier)
                    .HasColumnName("EOcarrier")
                    .HasMaxLength(100);

                entity.Property(e => e.EoexpirationDate)
                    .HasColumnName("EOExpirationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.EopolicyNumber)
                    .HasColumnName("EOPolicyNumber")
                    .HasMaxLength(100);

                entity.Property(e => e.IsEocoverage).HasColumnName("IsEOcoverage");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.Eodetails)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.Faqid).HasColumnName("FAQId");

                entity.Property(e => e.Faqanswer)
                    .IsRequired()
                    .HasColumnName("FAQAnswer")
                    .IsUnicode(false);

                entity.Property(e => e.FaqcategoryId).HasColumnName("FAQCategoryId");

                entity.Property(e => e.Faqquestion)
                    .IsRequired()
                    .HasColumnName("FAQQuestion")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Faqcategory)
                    .WithMany(p => p.Faq)
                    .HasForeignKey(d => d.FaqcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Portal)
                    .WithMany(p => p.Faq)
                    .HasForeignKey(d => d.PortalId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Faqcategory>(entity =>
            {
                entity.ToTable("FAQCategory");

                entity.Property(e => e.FaqcategoryId).HasColumnName("FAQCategoryId");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.Admin123Id);

                entity.HasIndex(e => e.BrokerId);

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.Admin123Id).HasMaxLength(10);

                entity.Property(e => e.AlternateEmailId).HasMaxLength(255);

                entity.Property(e => e.AlternatePhone).HasMaxLength(20);

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(255);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GroupLabel).HasMaxLength(100);

                entity.Property(e => e.InactiveDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.RenewalDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<GroupAddress>(entity =>
            {
                entity.Property(e => e.AddressLine1).HasMaxLength(60);

                entity.Property(e => e.AddressLine2).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateCode).HasMaxLength(2);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupAddress)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InvoiceAging>(entity =>
            {
                entity.Property(e => e.DayMoreThan90).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Days30).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Days60).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Days90).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceTo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TotalDue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InvoiceAging)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                .HasName("PK_Invoices_InvoiceId");

                entity.HasIndex(e => new { e.InvoiceNumber, e.InvoiceTo, e.Method, e.Delivery, e.CreatedOn, e.InvoiceDate, e.StartDate, e.EndDate, e.NumberOfProducts, e.NumberOfMembers, e.Total, e.Due, e.Paid, e.PastDue, e.PayDate, e.FileId, e.GroupId })
                    .HasName("IX_Invoices_GroupId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Delivery).HasMaxLength(50);
                entity.Property(e => e.Due).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FileId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.InvoiceTo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Method).HasMaxLength(50);
                entity.Property(e => e.Paid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PastDue).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.PayDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterData>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.CreatedOn);

                entity.HasIndex(e => e.ExternalId);

                entity.HasIndex(e => new { e.Status, e.CreatedOn });

                entity.HasIndex(e => new { e.Status, e.ExternalId });

                entity.HasIndex(e => new { e.ExternalId, e.CreatedOn, e.Status })
                    .HasName("IX_Member_Status");

                entity.HasIndex(e => new { e.ExternalId, e.Admin123Id, e.IsTobaccoUser, e.IsFamilyRiderOn, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.UserId, e.DigitalIdcardId, e.MemberDetailId, e.Status })
                    .HasName("IX_Member_MemberDetailId_Status");

                entity.HasIndex(e => new { e.Status, e.ExternalId, e.Admin123Id, e.IsTobaccoUser, e.IsFamilyRiderOn, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.UserId, e.DigitalIdcardId, e.MemberDetailId })
                    .HasName("IX_Member_MemberDetailId");

                entity.Property(e => e.Admin123Id).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DigitalIdcardId).HasColumnName("DigitalIDCardId");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.Member)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberAccumulatorDetails>(entity =>
            {
                entity.Property(e => e.AccumulatorType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ConsumedAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.NetworkTier)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProvisionText).IsRequired();

                entity.HasOne(d => d.Accumulator)
                    .WithMany(p => p.MemberAccumulatorDetails)
                    .HasForeignKey(d => d.AccumulatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberAccumulator_Accumulator_AccumulatorId");

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MemberAccumulatorDetails)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberAccumulatorDetails_MemberDetailId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberAccumulatorDetails)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberAccumulatorDetails_MemberId");
            });

            modelBuilder.Entity<MemberAddress>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.AddressTypeId })
                .HasName("PK_MemberAddress_MemberId");

                entity.Property(e => e.AddressLine1).HasMaxLength(500);

                entity.Property(e => e.AddressLine2).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberAddress)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberDependent>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.MemberDetailId })
                .HasName("PK_MemberDependent_MemberId");

                entity.HasIndex(e => e.MemberDetailId);

                entity.Property(e => e.Admin123Id).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeactiveDate).HasColumnType("datetime");

                entity.Property(e => e.DigitalIdcardId).HasColumnName("DigitalIDCardId");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MemberDependent)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberDependent)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberDependentAddress>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.MemberDetailId, e.AddressTypeId })
                .HasName("PK_MemberDependentAddress_MemberId");

                entity.HasIndex(e => e.MemberDetailId);

                entity.Property(e => e.AddressLine1).HasMaxLength(1000);

                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

                entity.Property(e => e.City).HasMaxLength(60);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MemberDependentAddress)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberDependentAddress)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberDependentQuestionAnswer>(entity =>
            {
                entity.Property(e => e.DependentAnswer).HasMaxLength(150);

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MemberDependentQuestionAnswer)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberDependentQuestionAnswer)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberDependentStatusHistory>(entity =>
            {
                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.ActiveDateComment).HasMaxLength(1000);

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HoldDate).HasColumnType("datetime");

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.InActiveDateComment).HasMaxLength(1000);

                entity.Property(e => e.OnHoldDateComment).HasMaxLength(1000);

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MemberDependentStatusHistory)
                    .HasForeignKey(d => d.MemberDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberDependentStatusHistory)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MemberSubscribedPlan)
                    .WithMany(p => p.MemberDependentStatusHistory)
                    .HasForeignKey(d => d.MemberSubscribedPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberDetail>(entity =>
            {
                entity.HasIndex(e => e.DateOfBirth);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ext).HasMaxLength(6);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MemberDependentId123)
                    .HasColumnName("MemberDependentId_123")
                    .HasMaxLength(50);

                entity.Property(e => e.MemberId123)
                    .HasColumnName("MemberId_123")
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Salutation).HasMaxLength(10);

                entity.Property(e => e.Ssn)
                    .IsRequired()
                    .HasColumnName("SSN")
                    .HasMaxLength(11);

                entity.Property(e => e.Suffix).HasMaxLength(10);

                entity.HasOne(d => d.PreferredLanguage)
                    .WithMany(p => p.MemberDetail)
                    .HasForeignKey(d => d.PreferredLanguageId)
                    .HasConstraintName("FK_MemberDetail_Language_LanguageId");
            });

            modelBuilder.Entity<MemberFeedback>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<MemberPaymentDetail>(entity =>
            {
                entity.HasIndex(e => new { e.InvoiceId, e.TransactionId, e.BillDate, e.PaidDate, e.InvoicedAmount, e.PaidAmount, e.Processor, e.Method, e.Status, e.Information, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.ProductId, e.MemberId })
                    .HasName("IX_MemberPaymentDetail_MemberId");

                entity.Property(e => e.BillDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Information).HasMaxLength(1000);

                entity.Property(e => e.InvoiceId).HasMaxLength(50);

                entity.Property(e => e.InvoicedAmount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Method).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.Processor).HasMaxLength(300);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransactionId).HasMaxLength(50);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberPaymentDetail)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MemberPaymentDetail)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<MemberPlanParameter>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                .HasName("PK_MemberPlanParameter_MemberId");

                entity.Property(e => e.MemberId).ValueGeneratedNever();
            });

            modelBuilder.Entity<MemberQuestionAnswer>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.MemberQuestionnaireId })
                .HasName("PK_Composite_MemberQuestionAnswer_MemberId_MemberQuestionnaireId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberQuestionAnswer)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MemberQuestionnaire)
                    .WithMany(p => p.MemberQuestionAnswerNavigation)
                    .HasForeignKey(d => d.MemberQuestionnaireId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberQuestionnaire>(entity =>
            {
                entity.Property(e => e.MemberQuestionnaire1)
                    .IsRequired()
                    .HasColumnName("MemberQuestionnaire");
            });

            modelBuilder.Entity<MemberStatusHistory>(entity =>
            {
                entity.HasIndex(e => e.MemberId);

                entity.HasIndex(e => e.MemberSubscribedPlanId);

                entity.HasIndex(e => new { e.MemberSubscribedPlanId, e.ActiveDate, e.InActiveDate })
                    .HasName("IX_MemberStatusHistory_ActiveDate_InActiveDate");

                entity.HasIndex(e => new { e.MemberSubscribedPlanId, e.InActiveDate, e.ActiveDate })
                    .HasName("IX_MemberStatusHistory_InActiveDate_ActiveDate");

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.ActiveDateComment).HasMaxLength(1000);

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HoldDate).HasColumnType("datetime");

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.InActiveDateComment).HasMaxLength(1000);

                entity.Property(e => e.OnHoldDateComment).HasMaxLength(1000);

                entity.HasOne(d => d.MemberSubscribedPlan)
                    .WithMany(p => p.MemberStatusHistory)
                    .HasForeignKey(d => d.MemberSubscribedPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberSubscribedPlan>(entity =>
            {
                entity.HasIndex(e => e.MemberSubscriptionId);

                entity.HasIndex(e => new { e.MemberSubscriptionId, e.PolicyNumber, e.PlanType, e.Premium, e.ApplicationFee, e.TobaccoRider, e.FamilyRider, e.Status, e.CancelDate, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.FamilyIndicator, e.PlanId })
                    .HasName("IX_MemberSubscribedPlan_PlanId");

                entity.HasIndex(e => new { e.PolicyNumber, e.PlanId, e.PlanType, e.Premium, e.ApplicationFee, e.TobaccoRider, e.FamilyRider, e.Status, e.CancelDate, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.FamilyIndicator, e.MemberSubscriptionId })
                    .HasName("IX_MemberSubscribedPlan_MemberSubscriptionId1");

                entity.Property(e => e.ActiveDate).HasColumnType("datetime");

                entity.Property(e => e.ApplicationFee).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FamilyRider).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FullfillmentDate).HasColumnType("datetime");

                entity.Property(e => e.InactiveDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NextBillingDate).HasColumnType("datetime");

                entity.Property(e => e.PolicyNumber).HasMaxLength(50);

                entity.Property(e => e.Premium).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.TobaccoRider).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.MemberSubscription)
                    .WithMany(p => p.MemberSubscribedPlan)
                    .HasForeignKey(d => d.MemberSubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.MemberSubscribedPlan)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberSubscribedPlanHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                .HasName("PK_MemberSubscribedPlanHistory_HistoryId");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Effective).HasColumnType("datetime");

                entity.HasOne(d => d.MemberSubscribedPlan)
                    .WithMany(p => p.MemberSubscribedPlanHistory)
                    .HasForeignKey(d => d.MemberSubscribedPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberSubscription>(entity =>
            {
                entity.HasIndex(e => e.BrokerId);

                entity.HasIndex(e => e.MemberId);

                entity.Property(e => e.Browser).HasMaxLength(100);

                entity.Property(e => e.CardOrAccountNumber).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FirstPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NotificationDate).HasColumnType("datetime");

                entity.Property(e => e.NotificationEmail).HasMaxLength(510);

                entity.Property(e => e.NotificationPhone).HasMaxLength(40);

                entity.Property(e => e.RoutingNumber).HasMaxLength(50);

                entity.Property(e => e.SignedDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPremium).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.VerificationCode).HasMaxLength(50);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.MemberSubscription)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.MemberSubscription)
                    .HasForeignKey(d => d.DivisionId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MemberSubscription)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberSubscription)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Subgroup)
                    .WithMany(p => p.MemberSubscription)
                    .HasForeignKey(d => d.SubgroupId);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                .HasName("PK_Message_BrokerMessageId");

                entity.HasIndex(e => new { e.RecipientId, e.PortalId });

                entity.HasIndex(e => new { e.RecipientId, e.IsRead, e.PortalId, e.MessageSentTime });

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.MessageSentTime).HasColumnType("datetime");

                entity.HasOne(d => d.Portal)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.PortalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Portals");
            });

            modelBuilder.Entity<MigratedMemberProductDetails>(entity =>
            {
                entity.HasKey(e => e.MemberSubscribedPlanId)
                .HasName("PK_MemberSubscribedPlanId");

                entity.Property(e => e.MemberSubscribedPlanId).ValueGeneratedNever();

                entity.Property(e => e.CarePlusMsra)
                    .HasColumnName("CarePlusMSRA")
                    .HasMaxLength(1000);

                entity.Property(e => e.CareVanMsra)
                    .HasColumnName("CareVanMSRA")
                    .HasMaxLength(1000);

                entity.Property(e => e.CatastrophicMsra)
                    .HasColumnName("CatastrophicMSRA")
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EvermedMsra)
                    .HasColumnName("EvermedMSRA")
                    .HasMaxLength(1000);

                entity.Property(e => e.HealthShareMsra)
                    .HasColumnName("HealthShareMSRA")
                    .HasMaxLength(1000);

                entity.Property(e => e.MaximumTermSharingCp)
                    .HasColumnName("MaximumTermSharingCP")
                    .HasMaxLength(1000);

                entity.Property(e => e.MaximumTermSharingCpnt)
                    .HasColumnName("MaximumTermSharingCPNT")
                    .HasMaxLength(1000);

                entity.Property(e => e.MaximumTermSharingCv)
                    .HasColumnName("MaximumTermSharingCV")
                    .HasMaxLength(1000);

                entity.Property(e => e.MaximumTermSharingEcp)
                    .HasColumnName("MaximumTermSharingECP")
                    .HasMaxLength(1000);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.MsraalieraCare)
                    .HasColumnName("MSRAAlieraCare")
                    .HasMaxLength(1000);

                entity.Property(e => e.MsraalieraCareSpa)
                    .HasColumnName("MSRAAlieraCareSPA")
                    .HasMaxLength(1000);

                entity.Property(e => e.Msrastm)
                    .HasColumnName("MSRASTM")
                    .HasMaxLength(1000);

                entity.Property(e => e.PolicyTerm).HasMaxLength(1000);

                entity.Property(e => e.Program_SRA)
                    .HasColumnName("Program_SRA")
                    .HasMaxLength(1000);

                entity.Property(e => e._500kaddOn)
                    .HasColumnName("$500KAdd-on")
                    .HasMaxLength(1000);

                entity.Property(e => e._500kaddOnSpa)
                    .HasColumnName("$500KAdd-onSPA")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.MemberSubscribedPlan)
                    .WithOne(p => p.MigratedMemberProductDetails)
                    .HasForeignKey<MigratedMemberProductDetails>(d => d.MemberSubscribedPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Type)
                .HasName("PK_Notification_Type");

                entity.Property(e => e.Type).ValueGeneratedNever();

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.From).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(100);
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.CoverageDuration).HasMaxLength(50);

                entity.Property(e => e.CoverageLimit).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Msra)
                    .HasColumnName("MSRA")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.RelatedPlanIds).HasMaxLength(1000);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Plan)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PlanBenefit>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.BenefitId })
                .HasName("PK_Composite_PlanBenefit_PlanId_BenefitId");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<PlanPackage>(entity =>
            {
                entity.Property(e => e.PackagedPlanIds).HasMaxLength(500);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanPackage)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PlanPremium>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.MaxAge, e.FamilyIndicator })
                .HasName("PK_Composite_PlanPremium_PlanId_MaxAge_FamilyIndicator");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Msra)
                    .HasColumnName("MSRA")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Premium).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanPremium)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Portals>(entity =>
            {
                entity.HasKey(e => e.PortalId)
                .HasName("PK_Portals_PortalId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ApplicationFee).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.Er)
                    .HasColumnName("ER")
                    .HasMaxLength(500);

                entity.Property(e => e.ExcludedSaleStates).HasMaxLength(500);

                entity.Property(e => e.FamilyRiderFee).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Pcp)
                    .HasColumnName("PCP")
                    .HasMaxLength(500);

                entity.Property(e => e.ProductLabel).HasMaxLength(50);

                entity.Property(e => e.ProductLogo).HasMaxLength(1000);

                entity.Property(e => e.ShellSheetName).HasMaxLength(100);

                entity.Property(e => e.Speciality).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TermDate).HasColumnType("datetime");

                entity.Property(e => e.TobaccoUserFee).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.UrgentCare).HasMaxLength(500);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId);

                entity.HasOne(d => d.ProductSubCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductSubCategoryId);
            });

            modelBuilder.Entity<ProductDocumentLink>(entity =>
            {

                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.DocumentAndFormId).HasColumnName("DocumentAndFormId");
            });

            modelBuilder.Entity<ProductAccumulator>(entity =>
            {
                entity.HasOne(d => d.Accumulator)
                    .WithMany(p => p.ProductAccumulator)
                    .HasForeignKey(d => d.AccumulatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAccumulator_AccumulatorId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAccumulator)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAccumulator_ProductId");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PlanCategory).HasMaxLength(8);

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProductFee>(entity =>
            {
                entity.HasIndex(e => new { e.Fee, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.BenefitLevel, e.BrokerId, e.ProductId })
                    .HasName("IX_ProductFee_ProductId");

                entity.HasIndex(e => new { e.ProductId, e.Fee, e.CreatedBy, e.CreatedOn, e.ModifiedBy, e.ModifiedOn, e.BenefitLevel, e.BrokerId })
                    .HasName("IX_ProductFee_BrokerId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.ProductFee)
                    .HasForeignKey(d => d.BrokerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductFee)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductProviderNetwork>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProviderNetwork)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__ProductId");

                entity.HasOne(d => d.ProviderNetwork)
                    .WithMany(p => p.ProductProviderNetwork)
                    .HasForeignKey(d => d.ProviderNetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProviderNetwork__ProviderNetworkId");
            });

            modelBuilder.Entity<ProductSubCategory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProductSubCategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProviderNetwork>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<RecipientCategory>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<SecurityQuestions>(entity =>
            {
                entity.HasKey(e => e.SecurityQuestionId)
                .HasName("PK_SecurityQuestion_SecurityQuestionId");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateCode)
                .HasName("PK_State_StateCode");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(2)
                    .ValueGeneratedNever();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Timezone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCodeRange)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subgroup>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Subgroup)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<TemplateDetails>(entity =>
            {
                entity.HasKey(e => e.TemplateDetailId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasMaxLength(500);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.TemplateDetails)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_TemplateDetails_Language");

                entity.HasOne(d => d.ProviderNetwork)
                    .WithMany(p => p.TemplateDetails)
                    .HasForeignKey(d => d.ProviderNetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateDetails_ProviderNetwork");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.TemplateDetails)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateDetails_Templates");
            });

            modelBuilder.Entity<Templates>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                .HasName("PK_Users_UserId");

                entity.HasIndex(e => e.NormalizedUserName);

                entity.HasIndex(e => e.UserName)
                    .HasName("UK_Users_Username")
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<UserSecurityQuestionAnswers>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.SecurityQuestion)
                    .WithMany(p => p.UserSecurityQuestionAnswers)
                    .HasForeignKey(d => d.SecurityQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSecurityQuestionAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}