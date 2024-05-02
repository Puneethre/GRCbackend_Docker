using System;
using System.Collections.Generic;
using GRCServices.Models;
using Microsoft.EntityFrameworkCore;

namespace GRCServices.Data;

public partial class GRCDbContext : DbContext
{
    public GRCDbContext()
    {
    }

    public GRCDbContext(DbContextOptions<GRCDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitiyNameMaster> ActivitiyNameMasters { get; set; }

    public virtual DbSet<ActivityMaster> ActivityMasters { get; set; }

    public virtual DbSet<AssignmentMaster> AssignmentMasters { get; set; }

    public virtual DbSet<CategoryListMaster> CategoryListMasters { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<ClientRoleMaster> ClientRoleMasters { get; set; }

    public virtual DbSet<ClientUserInfo> ClientUserInfos { get; set; }

    public virtual DbSet<ComplianceMaster> ComplianceMasters { get; set; }

    public virtual DbSet<DocumentMaster> DocumentMasters { get; set; }

    public virtual DbSet<DomainMaster> DomainMasters { get; set; }

    public virtual DbSet<EntitlementMaster> EntitlementMasters { get; set; }

    public virtual DbSet<FrequencyMaster> FrequencyMasters { get; set; }

    public virtual DbSet<GovernanceMaster> GovernanceMasters { get; set; }

    public virtual DbSet<ProcessMaster> ProcessMasters { get; set; }

    public virtual DbSet<RoleType> RoleTypes { get; set; }

    public virtual DbSet<StandardMaster> StandardMasters { get; set; }

    public virtual DbSet<StatusMaster> StatusMasters { get; set; }

    public virtual DbSet<SysCustomerInfo> SysCustomerInfos { get; set; }

    public virtual DbSet<SysCustomerLink> SysCustomerLinks { get; set; }

    public virtual DbSet<SysLicenseMaster> SysLicenseMasters { get; set; }

    public virtual DbSet<SysRoleMaster> SysRoleMasters { get; set; }

    public virtual DbSet<SysRoleType> SysRoleTypes { get; set; }

    public virtual DbSet<SysUserLogin> SysUserLogins { get; set; }

    public virtual DbSet<TechnologiesMaster> TechnologiesMasters { get; set; }

    public virtual DbSet<UserActivityEmail> UserActivityEmails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=192.168.29.128;Port=5432;Database=grc_master;Username=GRC;Password=Welcome@0668;Include Error Detail=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivitiyNameMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ACTIVITIY_NAME_MASTER_pkey");

            entity.ToTable("ACTIVITIY_NAME_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(50)
                .HasColumnName("ACTIVITY_NAME");
        });

        modelBuilder.Entity<ActivityMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ACTIVITY_MASTER_pkey");

            entity.ToTable("ACTIVITY_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityDescr).HasColumnName("ACTIVITY_DESCR");
            entity.Property(e => e.ActivityNameId).HasColumnName("ACTIVITY_NAME_ID");
            entity.Property(e => e.ApproverRole).HasColumnName("APPROVER_ROLE");
            entity.Property(e => e.Auditable).HasColumnName("AUDITABLE");
            entity.Property(e => e.DoerRole).HasColumnName("DOER_ROLE");
            entity.Property(e => e.Duration).HasColumnName("DURATION");
            entity.Property(e => e.FrequencyId).HasColumnName("FREQUENCY_ID");
            entity.Property(e => e.HelpRef).HasColumnName("HELP_REF");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.OutputDocumentPath).HasColumnName("OUTPUT_DOCUMENT_PATH");
            entity.Property(e => e.PracticeId).HasColumnName("PRACTICE_ID");
            entity.Property(e => e.RefDocumentId).HasColumnName("REF_DOCUMENT_ID");
            entity.Property(e => e.TriggeringActivityNameId).HasColumnName("TRIGGERING_ACTIVITY_NAME_ID");

            entity.HasOne(d => d.ActivityName).WithMany(p => p.ActivityMasterActivityNames)
                .HasForeignKey(d => d.ActivityNameId)
                .HasConstraintName("ACTIVITY_NAME_ID_FK");

            entity.HasOne(d => d.ApproverRoleNavigation).WithMany(p => p.ActivityMasterApproverRoleNavigations)
                .HasForeignKey(d => d.ApproverRole)
                .HasConstraintName("APPROVER_ROLE_FK");

            entity.HasOne(d => d.DoerRoleNavigation).WithMany(p => p.ActivityMasterDoerRoleNavigations)
                .HasForeignKey(d => d.DoerRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DOER_ROLE_FK");

            entity.HasOne(d => d.Frequency).WithMany(p => p.ActivityMasters)
                .HasForeignKey(d => d.FrequencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FREQUENCY_FK");

            entity.HasOne(d => d.OutputDocumentPathNavigation).WithMany(p => p.ActivityMasterOutputDocumentPathNavigations)
                .HasForeignKey(d => d.OutputDocumentPath)
                .HasConstraintName("OUTPUT_DOCUMENT_FK");

            entity.HasOne(d => d.RefDocument).WithMany(p => p.ActivityMasterRefDocuments)
                .HasForeignKey(d => d.RefDocumentId)
                .HasConstraintName("REF_DOCUMENT_FK");

            entity.HasOne(d => d.TriggeringActivityName).WithMany(p => p.ActivityMasterTriggeringActivityNames)
                .HasForeignKey(d => d.TriggeringActivityNameId)
                .HasConstraintName("TRIGGERING_ACTIVITY_FK");
        });

        modelBuilder.Entity<AssignmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ASSIGNMENT_MASTER_pkey");

            entity.ToTable("ASSIGNMENT_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityMasterId).HasColumnName("ACTIVITY_MASTER_ID");
            entity.Property(e => e.ApprovalDate).HasColumnName("APPROVAL_DATE");
            entity.Property(e => e.ApprovalStatus).HasColumnName("APPROVAL_STATUS");
            entity.Property(e => e.ApproverCliUserId).HasColumnName("APPROVER_CLI_USER_ID");
            entity.Property(e => e.ApproverComments)
                .HasMaxLength(255)
                .HasColumnName("APPROVER_COMMENTS");
            entity.Property(e => e.AuditCheck)
                .HasDefaultValue(false)
                .HasColumnName("AUDIT_CHECK");
            entity.Property(e => e.DoerCliUserId).HasColumnName("DOER_CLI_USER_ID");
            entity.Property(e => e.DoerComments)
                .HasMaxLength(255)
                .HasColumnName("DOER_COMMENTS");
            entity.Property(e => e.DoerStatus)
                .HasDefaultValue(0)
                .HasColumnName("DOER_STATUS");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.EvidenceDetails)
                .HasMaxLength(255)
                .HasColumnName("EVIDENCE_DETAILS");
            entity.Property(e => e.StartDate).HasColumnName("START_DATE");

            entity.HasOne(d => d.ActivityMaster).WithMany(p => p.AssignmentMasters)
                .HasForeignKey(d => d.ActivityMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ACTIVITY_ID_FK");

            entity.HasOne(d => d.ApproverCliUser).WithMany(p => p.AssignmentMasterApproverCliUsers)
                .HasForeignKey(d => d.ApproverCliUserId)
                .HasConstraintName("APPROVER_CLI_USER_ID_FK");

            entity.HasOne(d => d.DoerCliUser).WithMany(p => p.AssignmentMasterDoerCliUsers)
                .HasForeignKey(d => d.DoerCliUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DOER_CLI_USER_ID_FK");
        });

        modelBuilder.Entity<CategoryListMaster>(entity =>
        {
            entity.HasKey(e => e.ListId).HasName("CATEGORY_LIST_MASTER_pkey");

            entity.ToTable("CATEGORY_LIST_MASTER", "master_config");

            entity.Property(e => e.ListId)
                .ValueGeneratedNever()
                .HasColumnName("LIST_ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryListMasters)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CATEGORY_ID_FK");
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("CATEGORY_MASTER_pkey");

            entity.ToTable("CATEGORY_MASTER", "master_config");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
        });

        modelBuilder.Entity<ClientRoleMaster>(entity =>
        {
            entity.HasKey(e => e.CliRoleId).HasName("CLIENT_ROLE_MASTER_pkey");

            entity.ToTable("CLIENT_ROLE_MASTER", "master_config");

            entity.Property(e => e.CliRoleId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDateTime).HasColumnName("CREATED_DATE_TIME");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_ACTIVE");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleTypeId).HasColumnName("ROLE_TYPE_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClientRoleMasters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.RoleType).WithMany(p => p.ClientRoleMasters)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("ROLE_TYPE_ID_FK");
        });

        modelBuilder.Entity<ClientUserInfo>(entity =>
        {
            entity.HasKey(e => e.CliUserId).HasName("CLIENT_USER_INFO_pkey");

            entity.ToTable("CLIENT_USER_INFO", "master_config");

            entity.Property(e => e.CliUserId)
                .ValueGeneratedNever()
                .HasColumnName("CLI_USER_ID");
            entity.Property(e => e.CliRoleId).HasColumnName("CLI_ROLE_ID");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDateTime).HasColumnName("CREATED_DATE_TIME");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.SysUserId).HasColumnName("SYS_USER_ID");

            entity.HasOne(d => d.CliRole).WithMany(p => p.ClientUserInfos)
                .HasForeignKey(d => d.CliRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CLI_ROLE_ID_FK");
        });

        modelBuilder.Entity<ComplianceMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("COMPLIANCE_MASTER_pkey");

            entity.ToTable("COMPLIANCE_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ComplEndDate).HasColumnName("COMPL_END_DATE");
            entity.Property(e => e.ComplStartDate).HasColumnName("COMPL_START_DATE");
            entity.Property(e => e.GovernanceId).HasColumnName("GOVERNANCE_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.MetCompliance)
                .HasDefaultValue(true)
                .HasColumnName("MET_COMPLIANCE");
            entity.Property(e => e.StandardId).HasColumnName("STANDARD_ID");

            entity.HasOne(d => d.Governance).WithMany(p => p.ComplianceMasters)
                .HasForeignKey(d => d.GovernanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GOVERNANCE_ID_FK");

            entity.HasOne(d => d.Standard).WithMany(p => p.ComplianceMasters)
                .HasForeignKey(d => d.StandardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("STANDARD_ID_fk");
        });

        modelBuilder.Entity<DocumentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DOCUMENT_MASTER_pkey");

            entity.ToTable("DOCUMENT_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(100)
                .HasColumnName("DOCUMENT_NAME");
        });

        modelBuilder.Entity<DomainMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DOMAIN_MASTER_pkey");

            entity.ToTable("DOMAIN_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DomainName)
                .HasMaxLength(50)
                .HasColumnName("DOMAIN_NAME");
        });

        modelBuilder.Entity<EntitlementMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("ENTITLEMENT_MASTER_pkey");

            entity.ToTable("ENTITLEMENT_MASTER", "master_config");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.MenuItem)
                .HasMaxLength(50)
                .HasColumnName("MENU_ITEM");
        });

        modelBuilder.Entity<FrequencyMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FREQUENCY_MASTER_pkey");

            entity.ToTable("FREQUENCY_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .HasColumnName("FREQUENCY");
        });

        modelBuilder.Entity<GovernanceMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GOVERNANCE_MASTER_pkey");

            entity.ToTable("GOVERNANCE_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .HasColumnName("SHORT_NAME");
        });

        modelBuilder.Entity<ProcessMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PROCESS_MASTER_pkey");

            entity.ToTable("PROCESS_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .HasColumnName("PROCESS_NAME");
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_TYPE_pkey");

            entity.ToTable("ROLE_TYPE", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.RoleTypeDesc)
                .HasMaxLength(30)
                .HasColumnName("ROLE_TYPE_DESC");
        });

        modelBuilder.Entity<StandardMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("STANDARD_MASTER_pkey");

            entity.ToTable("STANDARD_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GovrId).HasColumnName("GOVR_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.LevelNames)
                .HasMaxLength(100)
                .HasColumnName("LEVEL_NAMES");
            entity.Property(e => e.Levels).HasColumnName("LEVELS");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.NoOfControls).HasColumnName("NO_OF_CONTROLS");
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .HasColumnName("SHORT_NAME");

            entity.HasOne(d => d.Govr).WithMany(p => p.StandardMasters)
                .HasForeignKey(d => d.GovrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GOVR_ID_FK");
        });

        modelBuilder.Entity<StatusMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("STATUS_MASTER_pkey");

            entity.ToTable("STATUS_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<SysCustomerInfo>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("SYS_CUSTOMER_INFO_pkey");

            entity.ToTable("SYS_CUSTOMER_INFO", "system_info");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("CITY");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(120)
                .HasColumnName("CONTACT_EMAIL");
            entity.Property(e => e.ContactName)
                .HasMaxLength(130)
                .HasColumnName("CONTACT_NAME");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(15)
                .HasColumnName("CONTACT_PHONE");
            entity.Property(e => e.Country)
                .HasMaxLength(60)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDateTime).HasColumnName("CREATED_DATE_TIME");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("STATE");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("STREET");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("ZIP_CODE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysCustomerInfos)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_USER_LOGIN_CREATED_BY_fkey");
        });

        modelBuilder.Entity<SysCustomerLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ID_PK");

            entity.ToTable("SYS_CUSTOMER_LINK", "system_info");

            entity.HasIndex(e => e.CustomerId, "CUSTOMER_ID_UNIQ").IsUnique();

            entity.HasIndex(e => e.DbConStr, "DB_CON_STR_UNIQ").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDateTime).HasColumnName("CREATED_DATE_TIME");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DbConStr)
                .HasMaxLength(512)
                .HasColumnName("DB_CON_STR");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysCustomerLinks)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_USER_LOGIN_CREATED_BY_FK");

            entity.HasOne(d => d.Customer).WithOne(p => p.SysCustomerLink)
                .HasForeignKey<SysCustomerLink>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ID_FK");
        });

        modelBuilder.Entity<SysLicenseMaster>(entity =>
        {
            entity.HasKey(e => e.LicnId).HasName("SYS_LICENSE_MASTER_pkey");

            entity.ToTable("SYS_LICENSE_MASTER", "system_info");

            entity.Property(e => e.LicnId)
                .ValueGeneratedNever()
                .HasColumnName("LICN_ID");
            entity.Property(e => e.Approved)
                .HasDefaultValue(false)
                .HasColumnName("APPROVED");
            entity.Property(e => e.ApprovedBy).HasColumnName("APPROVED_BY");
            entity.Property(e => e.ApprovedDate).HasColumnName("APPROVED_DATE");
            entity.Property(e => e.ContractDocuments)
                .HasMaxLength(2048)
                .HasColumnName("CONTRACT_DOCUMENTS");
            entity.Property(e => e.ContractPeriodInMonths).HasColumnName("CONTRACT_PERIOD_IN_MONTHS");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.EditedBy).HasColumnName("EDITED_BY");
            entity.Property(e => e.EditedDate).HasColumnName("EDITED_DATE");
            entity.Property(e => e.EndDate).HasColumnName("END_DATE");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("REMARKS");
            entity.Property(e => e.StandardId).HasColumnName("STANDARD_ID");
            entity.Property(e => e.StartOrRenewalDate).HasColumnName("START_OR_RENEWAL_DATE");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.SysLicenseMasterApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("APPROVED_BY_FK");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysLicenseMasterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.Customer).WithMany(p => p.SysLicenseMasters)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ID_FK");

            entity.HasOne(d => d.EditedByNavigation).WithMany(p => p.SysLicenseMasterEditedByNavigations)
                .HasForeignKey(d => d.EditedBy)
                .HasConstraintName("EDITED_BY_FK");

            entity.HasOne(d => d.Standard).WithMany(p => p.SysLicenseMasters)
                .HasForeignKey(d => d.StandardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("STANDARD_ID_FK");
        });

        modelBuilder.Entity<SysRoleMaster>(entity =>
        {
            entity.HasKey(e => e.SysRoleId).HasName("SYS_ROLE_MASTER_pkey");

            entity.ToTable("SYS_ROLE_MASTER", "system_info");

            entity.Property(e => e.SysRoleId)
                .ValueGeneratedNever()
                .HasColumnName("SYS_ROLE_ID");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.CreateDateTime).HasColumnName("CREATE_DATE_TIME");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("ROLE_NAME");
            entity.Property(e => e.RoleTypeId).HasColumnName("ROLE_TYPE_ID");
            entity.Property(e => e.SysRoleTypeId).HasColumnName("SYS_ROLE_TYPE_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SysRoleMasters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.RoleType).WithMany(p => p.SysRoleMasters)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("ROLE_TYPE_ID_FK");

            entity.HasOne(d => d.SysRoleType).WithMany(p => p.SysRoleMasters)
                .HasForeignKey(d => d.SysRoleTypeId)
                .HasConstraintName("SYS_ROLE_TYPE_ID_FK");
        });

        modelBuilder.Entity<SysRoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_TYPE_pkey");

            entity.ToTable("SYS_ROLE_TYPE", "system_info");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.RoleTypeDesc)
                .HasColumnType("character varying")
                .HasColumnName("ROLE_TYPE_DESC");
        });

        modelBuilder.Entity<SysUserLogin>(entity =>
        {
            entity.HasKey(e => e.SysUserId).HasName("SYS_USER_LOGIN_pkey");

            entity.ToTable("SYS_USER_LOGIN", "system_info");

            entity.HasIndex(e => e.LoginEmailId, "LOGIN_EMAIL_ID_UNIQ").IsUnique();

            entity.Property(e => e.SysUserId)
                .ValueGeneratedNever()
                .HasColumnName("SYS_USER_ID");
            entity.Property(e => e.CreatedBy)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDatetime).HasColumnName("CREATED_DATETIME");
            entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.LastloginDatetime).HasColumnName("LASTLOGIN_DATETIME");
            entity.Property(e => e.LoginAttemptsAllowed).HasColumnName("LOGIN_ATTEMPTS_ALLOWED");
            entity.Property(e => e.LoginEmailId)
                .HasMaxLength(120)
                .HasColumnName("LOGIN_EMAIL_ID");
            entity.Property(e => e.Mfacode)
                .HasMaxLength(6)
                .HasColumnName("MFACODE");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("NAME");
            entity.Property(e => e.NewUser).HasColumnName("NEW_USER");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(60)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.SysRoleId).HasColumnName("SYS_ROLE_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CREATED_BY_FK");

            entity.HasOne(d => d.Customer).WithMany(p => p.SysUserLogins)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CUSTOMER_ID_FK");

            entity.HasOne(d => d.SysRole).WithMany(p => p.SysUserLogins)
                .HasForeignKey(d => d.SysRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SYS_ROLE_ID_FK");

            entity.HasMany(d => d.Customers).WithMany(p => p.SysUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "SysCustomerUser",
                    r => r.HasOne<SysCustomerInfo>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CUSTOMER_ID_FK"),
                    l => l.HasOne<SysUserLogin>().WithMany()
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SYS_USER_ID_FK"),
                    j =>
                    {
                        j.HasKey("SysUserId", "CustomerId").HasName("SYS_CUSTOMER_USER_pkey");
                        j.ToTable("SYS_CUSTOMER_USER", "system_info");
                        j.IndexerProperty<int>("SysUserId").HasColumnName("SYS_USER_ID");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("CUSTOMER_ID");
                    });
        });

        modelBuilder.Entity<TechnologiesMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TECHNOLOGIES_MASTER_pkey");

            entity.ToTable("TECHNOLOGIES_MASTER", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.TechnologyName)
                .HasMaxLength(50)
                .HasColumnName("TECHNOLOGY_NAME");
        });

        modelBuilder.Entity<UserActivityEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_ACTIVITY_EMAIL_pkey");

            entity.ToTable("USER_ACTIVITY_EMAIL", "master_config");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityId).HasColumnName("ACTIVITY_ID");
            entity.Property(e => e.EmailCodeToActivity)
                .HasMaxLength(30)
                .HasColumnName("EMAIL_CODE_TO_ACTIVITY");

            entity.HasOne(d => d.Activity).WithMany(p => p.UserActivityEmails)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ACTIVITY_ID_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
