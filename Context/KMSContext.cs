using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KMS.Context
{
    public partial class KMSContext : DbContext
    {
        public KMSContext()
            : base("name=Default")
        {
        }

        public virtual DbSet<CARD> CARDs { get; set; }
        public virtual DbSet<CARD_CARDUID> CARD_CARDUID { get; set; }
        public virtual DbSet<CARD_KAB> CARD_KAB { get; set; }
        public virtual DbSet<CARD_NIK> CARD_NIK { get; set; }
        public virtual DbSet<CARD_PROV> CARD_PROV { get; set; }
        public virtual DbSet<CARD_SUMMARY> CARD_SUMMARY { get; set; }
        public virtual DbSet<LOG_APP> LOG_APP { get; set; }
        public virtual DbSet<LOG_IIS> LOG_IIS { get; set; }
        public virtual DbSet<LOG_PERSO> LOG_PERSO { get; set; }
        public virtual DbSet<LOG_PERSO_COUNT> LOG_PERSO_COUNT { get; set; }
        public virtual DbSet<LOG_USER> LOG_USER { get; set; }
        public virtual DbSet<LOG_YEAR> LOG_YEAR { get; set; }
        public virtual DbSet<M_ACCESS> M_ACCESS { get; set; }
        public virtual DbSet<M_COMPANY> M_COMPANY { get; set; }
        public virtual DbSet<M_CONFIGURATION> M_CONFIGURATION { get; set; }
        public virtual DbSet<M_CONTRACT> M_CONTRACT { get; set; }
        public virtual DbSet<M_GROUP> M_GROUP { get; set; }
        public virtual DbSet<M_ICON> M_ICON { get; set; }
        public virtual DbSet<M_MODULE> M_MODULE { get; set; }
        public virtual DbSet<M_ROLE> M_ROLE { get; set; }
        public virtual DbSet<M_SERVER> M_SERVER { get; set; }
        public virtual DbSet<M_SETTING> M_SETTING { get; set; }
        public virtual DbSet<M_THEME> M_THEME { get; set; }
        public virtual DbSet<M_TOKEN> M_TOKEN { get; set; }
        public virtual DbSet<M_USER> M_USER { get; set; }
        public virtual DbSet<M_VENDOR> M_VENDOR { get; set; }
        public virtual DbSet<MESSAGE> MESSAGEs { get; set; }
        public virtual DbSet<NOTIFICATION> NOTIFICATIONs { get; set; }
        public virtual DbSet<TEST> TESTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CARD>()
                .Property(e => e.LOG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD>()
                .Property(e => e.NIK)
                .IsUnicode(false);

            modelBuilder.Entity<CARD>()
                .Property(e => e.CARDUID)
                .IsUnicode(false);

            modelBuilder.Entity<CARD>()
                .Property(e => e.NAMA)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_CARDUID>()
                .Property(e => e.CARDUID)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_CARDUID>()
                .Property(e => e.TOTAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_CARDUID>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_KAB>()
                .Property(e => e.KAB)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_KAB>()
                .Property(e => e.VALUE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_KAB>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_NIK>()
                .Property(e => e.NIK)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_NIK>()
                .Property(e => e.TOTAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_NIK>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_PROV>()
                .Property(e => e.PROV)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_PROV>()
                .Property(e => e.VALUE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_PROV>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_SUMMARY>()
                .Property(e => e.ITEM)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_SUMMARY>()
                .Property(e => e.VALUE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARD_SUMMARY>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<CARD_SUMMARY>()
                .Property(e => e.TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_APP>()
                .Property(e => e.LOG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_APP>()
                .Property(e => e.LOG_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_APP>()
                .Property(e => e.LOG_CONTENT)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_APP>()
                .Property(e => e.BROWSER)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.LOG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SCWIN32STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SCSUBSTATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SCSTATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSHOST)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSREFERER)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSCOOKIE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSUSERAGENT)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSVERSION)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CIP)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSUSERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SPORT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSURISTEM)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSURIQUERY)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SIP)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SCOMPUTERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.SSITENAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSBYTES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.TIMETAKEN)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.TOTALTAKEN)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.CSMETHOD)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.LOG_IPSERVER)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.LOG_YEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_IIS>()
                .Property(e => e.LOG_MONTH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.LOG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.CARDUID)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.CONTROLNUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.MANUFACTURERCODE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.PERSOSITE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.ERROR_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.VENDOR_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.CONTRACT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.IS_COMMIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.IP_REQUEST)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO>()
                .Property(e => e.IP_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_PERSO_COUNT>()
                .Property(e => e.VENDOR_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO_COUNT>()
                .Property(e => e.CONTRACT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO_COUNT>()
                .Property(e => e.CURRENT_HIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO_COUNT>()
                .Property(e => e.SUCCESS_HIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_PERSO_COUNT>()
                .Property(e => e.FAIL_HIT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOG_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOG_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.MODULE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOCAL_IP)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.REMOTE_IP)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOCATION)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.BROWSER)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_USER>()
                .Property(e => e.LOG_CONTENT)
                .IsUnicode(false);

            modelBuilder.Entity<LOG_YEAR>()
                .Property(e => e.FULLYEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ACCESS>()
                .Property(e => e.MODULE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ACCESS>()
                .Property(e => e.GROUP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.COMPANY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.COMPANY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.COMPANY_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_COMPANY>()
                .HasOptional(e => e.M_CONFIGURATION)
                .WithRequired(e => e.M_COMPANY);

            modelBuilder.Entity<M_COMPANY>()
                .HasMany(e => e.M_USER)
                .WithRequired(e => e.M_COMPANY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.COMPANY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.TELEGRAM_API)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.SMTP_MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.SMTP_SERVER)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.SMTP_PORT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONFIGURATION>()
                .Property(e => e.SMTP_PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.CONTRACT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.CONTRACT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.CONTRACT_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.ATTACHMENT)
                .IsUnicode(false);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.QUOTA)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.VENDOR_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_CONTRACT>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.GROUP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.GROUP_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.GROUP_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.ROLE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_GROUP>()
                .HasMany(e => e.M_ACCESS)
                .WithRequired(e => e.M_GROUP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_ICON>()
                .Property(e => e.ICON_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<M_ICON>()
                .Property(e => e.ICON_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_ICON>()
                .HasMany(e => e.M_MODULE)
                .WithOptional(e => e.M_ICON)
                .HasForeignKey(e => e.MODULE_ICON);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.TYPE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_ROOT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_ICON)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_URL)
                .IsUnicode(false);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.MODULE_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.ISVISIBLE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.ORDER_NO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_MODULE>()
                .HasMany(e => e.M_ACCESS)
                .WithRequired(e => e.M_MODULE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_MODULE>()
                .HasMany(e => e.M_MODULE1)
                .WithOptional(e => e.M_MODULE2)
                .HasForeignKey(e => e.MODULE_ROOT);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ROLE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ROLE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ROLE_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_CREATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_UPDATE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_DELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_EXPORT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_IMPORT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.ALLOW_ENABLEDISABLE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_ROLE>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.SERVER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.SERVER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.SERVER_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.IP_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SERVER>()
                .Property(e => e.LOG_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_PAGESIZE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_THEME)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_ZEBRACOLOR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_WRAP_COLUMN)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_WRAP_CELL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_SHOWFILTERROW)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_SHOWFILTERBAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_SELECTBYROW)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_FOCUSEROW)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_ELLIPSIS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_SHOWFOOTER)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_SETTING>()
                .Property(e => e.GRID_RESPONSIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.THEME_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.THEME_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.THEME_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.THEME_LOCATION)
                .IsUnicode(false);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_THEME>()
                .HasMany(e => e.M_USER)
                .WithRequired(e => e.M_THEME)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<M_TOKEN>()
                .Property(e => e.TOKEN_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_TOKEN>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_TOKEN>()
                .Property(e => e.TOKEN)
                .IsUnicode(false);

            modelBuilder.Entity<M_TOKEN>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.FULLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.TELEGRAM_ID)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.PHOTO)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.GENDER)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.GROUP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.ISREQUIRED_TOKEN)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.THEME_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.COMPANY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.NOTES)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<M_USER>()
                .Property(e => e.VENDOR_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_USER>()
                .HasOptional(e => e.M_SETTING)
                .WithRequired(e => e.M_USER);

            modelBuilder.Entity<M_USER>()
                .HasMany(e => e.MESSAGEs)
                .WithOptional(e => e.M_USER)
                .HasForeignKey(e => e.USER_FROM);

            modelBuilder.Entity<M_USER>()
                .HasMany(e => e.MESSAGEs1)
                .WithOptional(e => e.M_USER1)
                .HasForeignKey(e => e.USER_TO);

            modelBuilder.Entity<M_USER>()
                .HasMany(e => e.NOTIFICATIONs)
                .WithOptional(e => e.M_USER)
                .HasForeignKey(e => e.TO_USER_ID);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.VENDOR_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.VENDOR_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.VENDOR_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.ISACTIVE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.ISDELETE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.CREATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.UPDATEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.DELETEBY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.VENDOR_COLOR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.IP_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.PERSOSITE)
                .IsUnicode(false);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.SERVER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<M_VENDOR>()
                .Property(e => e.COLOR)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.USER_FROM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.USER_TO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.ISREAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.MESSAGE_CONTENT)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.MESSAGE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.NOTIF_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.NOTIF_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.NOTIF_MESSAGE)
                .IsUnicode(false);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.NOTIF_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.TO_USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.ISREAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<NOTIFICATION>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<TEST>()
                .Property(e => e.YEAR)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST>()
                .Property(e => e.MONTH)
                .HasPrecision(38, 0);
        }
    }
}
