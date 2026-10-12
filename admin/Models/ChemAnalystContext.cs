namespace ChemAnalyst.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SpeedUpCoreAPIExample.Models;

    public partial class ChemAnalystContext : DbContext
    {
        public ChemAnalystContext()
            : base("name=ChemAnalystContext")
        {
        }


        public virtual DbSet<SA_MarketbyPolicyandRegulation> SA_MarketbyPolicyandRegulation { get; set; }
        public virtual DbSet<SA_MarketbyManufacturingProcess> SA_MarketbyManufacturingProcess { get; set; }
        public virtual DbSet<SA_MarketbyTechnologyLicensor> SA_MarketbyTechnologyLicensor { get; set; }
        public virtual DbSet<Productdescription> Productdescriptions { get; set; }
        public virtual DbSet<SA_ContactUs> SA_ContactUs { get; set; }

        public virtual DbSet<SA_Chem1PriceWeekly> SA_Chem1PriceWeekly { get; set; }

        public virtual DbSet<SA_Chem2PriceWeekly> SA_Chem2PriceWeekly { get; set; }

        public virtual DbSet<SA_Chem3PriceWeekly> SA_Chem3PriceWeekly { get; set; }

        public virtual DbSet<SA_Chem4PriceWeekly> SA_Chem4PriceWeekly { get; set; }

        public virtual DbSet<SA_Chem5PriceWeekly> SA_Chem5PriceWeekly { get; set; }

        public virtual DbSet<SA_Chem6PriceWeekly> SA_Chem6PriceWeekly { get; set; }

        public virtual DbSet<SA_States> SA_States { get; set; }

        public DbSet<CompanyProfRecordNew> CompanyProfRecordNew { get; set; }

        public virtual DbSet<SA_Company_SWOT> SA_Company_SWOT { get; set; }

        public virtual DbSet<SA_AdvisoryContent> SA_AdvisoryContent { get; set; }

        public virtual DbSet<SA_HomeHeader> SA_HomeHeader { get; set; }

        public virtual DbSet<SA_ChemPriceWeeklyNew> SA_ChemPriceWeeklyNew { get; set; }

        public virtual DbSet<SA_ChemPriceDailyAverage> SA_ChemPriceDailyAverage { get; set; }

        public virtual DbSet<SA_ChemPriceDailyNew> SA_ChemPriceDailyNew { get; set; }

        public virtual DbSet<SA_Company> SA_Company { get; set; }

        public virtual DbSet<SA_ChemPriceDaily> SA_ChemPriceDaily { get; set; }

        public virtual DbSet<SA_ChemPriceWeekly> SA_ChemPriceWeekly { get; set; }

        public virtual DbSet<CustProduct> CustProduct { get; set; }

        public virtual DbSet<SA_RoleWiseAccess> SA_RoleWiseAccess { get; set; }

        public virtual DbSet<SA_PageList> SA_PageList { get; set; }

        public virtual DbSet<SA_Contact> SA_Contact { get; set; }

        public virtual DbSet<SA_CMS> SA_CMS { get; set; }

        public virtual DbSet<SA_Deals> SA_Deals { get; set; }

        public virtual DbSet<SA_News> SA_News { get; set; }

        public virtual DbSet<SA_Category> SA_Category { get; set; }

        public virtual DbSet<SA_Product> SA_Product { get; set; }
        public DbSet<SA_NewsCategory> SA_NewsCategory { get; set; }
        public DbSet<SA_NewsAndCategoryRelation> SA_NewsAndCategoryRelation { get; set; }

        public virtual DbSet<SA_Role> SA_Role { get; set; }

        public virtual DbSet<SA_User> SA_User { get; set; }

        public virtual DbSet<Lead_Master> Lead_Master { get; set; }

        public virtual DbSet<SA_ChemPriceYearly> SA_ChemPriceYearly { get; set; }

        public virtual DbSet<SA_ChemPriceQuarterly> SA_ChemPriceQuarterly { get; set; }

        public virtual DbSet<SA_ChemPriceMonthly> SA_ChemPriceMonthly { get; set; }

        public virtual DbSet<SA_Quote> SA_Quote { get; set; }

        public virtual DbSet<SA_Advisory> SA_Advisory { get; set; }

        public virtual DbSet<SA_Industry> SA_Industry { get; set; }

        public virtual DbSet<SA_priceoverview> SA_priceoverview { get; set; }

        public virtual DbSet<SA_Mastrategic> SA_Mastrategic { get; set; }

        public virtual DbSet<SA_Commentary> SA_Commentary { get; set; }
        public virtual DbSet<SA_Sectoral> SA_Sectoral { get; set; }

        public virtual DbSet<SA_Job> SA_Job { get; set; }

        public virtual DbSet<SalesSubscription> SalesSubscription { get; set; }

        public virtual DbSet<SA_Slider> SA_Slider { get; set; }

        public virtual DbSet<SalesPackageSubscription> SalesPackageSubscription { get; set; }

        public virtual DbSet<Modules> Module { get; set; }

        public virtual DbSet<SubscriptionType> SubscriptionType { get; set; }

        public virtual DbSet<SA_Customer> SA_Customers { get; set; }

        public virtual DbSet<SA_ChemContent> SA_ChemContent { get; set; }

        public virtual DbSet<CustWiseAccess> CustWiseAccess { get; set; }

        public virtual DbSet<SA_MarketbyComp> SA_MarketbyComp { get; set; }

        public virtual DbSet<SA_MarketbyCustomer> SA_MarketbyCustomer { get; set; }

        public virtual DbSet<SA_MarketbyPortWiseImport> SA_MarketbyPortWiseImport { get; set; }

        public virtual DbSet<SA_MarketbyPortWiseExport> SA_MarketbyPortWiseExport { get; set; }

        public virtual DbSet<SA_MarketbySupplierAnalysis> SA_MarketbySupplierAnalysis { get; set; }

        public virtual DbSet<SA_MarketbyLoc> SA_MarketbyLoc { get; set; }

        public virtual DbSet<SA_MarketbyProcess> SA_MarketbyProcess { get; set; }

        public virtual DbSet<SA_MarketbyTech> SA_MarketbyTech { get; set; }

        public virtual DbSet<SA_MarketbyProducer> SA_MarketbyProducer { get; set; }

        public virtual DbSet<SA_MarketbyEfficiency> SA_MarketbyEfficiency { get; set; }

        public virtual DbSet<SA_MarketbyEndUsepercent> SA_MarketbyEndUsepercent { get; set; }

        public virtual DbSet<SA_MarketbyEndUsetonnes> SA_MarketbyEndUsetonnes { get; set; }
        public virtual DbSet<SA_MarketbyIndustryMarketSize> SA_MarketbyIndustryMarketSize { get; set; }

        public virtual DbSet<SA_MarketbyGradepercent> SA_MarketbyGradepercent { get; set; }

        public virtual DbSet<SA_MarketbyGradetonnes> SA_MarketbyGradetonnes { get; set; }

        public virtual DbSet<SA_MarketbyTypepercent> SA_MarketbyTypepercent { get; set; }

        public virtual DbSet<SA_MarketbyTypetonnes> SA_MarketbyTypetonnes { get; set; }

        public virtual DbSet<SA_MarketbySalespercent> SA_MarketbySalespercent { get; set; }

        public virtual DbSet<SA_MarketbySalestonnes> SA_MarketbySalestonnes { get; set; }

        public virtual DbSet<SA_MarketbyGradepricing> SA_MarketbyGradepricing { get; set; }

        public virtual DbSet<SA_MarketbyRegionpercent> SA_MarketbyRegionpercent { get; set; }

        public virtual DbSet<SA_MarketbyRegiontonnes> SA_MarketbyRegiontonnes { get; set; }

        public virtual DbSet<SA_MarketbyTradeExport> SA_MarketbyTradeExport { get; set; }

        public virtual DbSet<SA_MarketbyTradeImport> SA_MarketbyTradeImport { get; set; }

        public virtual DbSet<SA_MarketbyDemandsupply> SA_MarketbyDemandsupply { get; set; }

        public virtual DbSet<SA_MarketbyDemandsupplyGraph> SA_MarketbyDemandsupplyGraph { get; set; }

        public virtual DbSet<SA_MarketbyCompanySharepercent> SA_MarketbyCompanySharepercent { get; set; }

        public virtual DbSet<SA_MarketbyCompanySharetonnes> SA_MarketbyCompanySharetonnes { get; set; }

        public DbSet<SA_NewsAndProductRelation> SA_NewsAndProductRelation { get; set; }

        public DbSet<SA_DealsAndProductRelation> SA_DealsAndProductRelation { get; set; }

        public virtual DbSet<SA_MaintenanceAndShutdown> SA_MaintenanceAndShutdown { get; set; }

        public virtual DbSet<SA_ExpectedNewCapacity> SA_ExpectedNewCapacity { get; set; }

        public virtual DbSet<SA_RegionalCapacityChange> SA_RegionalCapacityChange { get; set; }

        public virtual DbSet<SA_Logistic> SA_Logistic { get; set; }


        public virtual DbSet<SA_World> SA_World { get; set; }
        public virtual DbSet<SA_Blogs> SA_Blogs { get; set; }

        public virtual DbSet<SA_BlogsAndProductRelation> SA_BlogsAndProductRelation { get; set; }

        public virtual DbSet<SA_ProcurementGuide> SA_ProcurementGuide { get; set; }

        public virtual DbSet<SA_ProcurementGuideAndProductRelation> SA_ProcurementGuideAndProductRelation { get; set; }
        public virtual DbSet<SA_InterviewVideo> SA_InterviewVideo { get; set; }

        //NEW CODE
        public DbSet<CompanyProf> CompanyProfs { get; set; }

        public DbSet<FinancialYear> FinancialYears { get; set; }

        public DbSet<CompanyAndProductRelation> CompanyAndProductRelations { get; set; }

        public DbSet<SA_RegionAndCountryRelation> SA_RegionAndCountryRelation { get; set; }

        public DbSet<SA_CountryAndCategoryRelation> SA_CountryAndCategoryRelation { get; set; }

        public DbSet<CompanyProduct> CompanyProducts { get; set; }

        public DbSet<CompanyProfRecord> CompanyProfRecords { get; set; }

        public DbSet<SA_Country> SA_Country { get; set; }

        public DbSet<SA_Region> SA_Region { get; set; }

        public DbSet<SA_Currency> SA_Currency { get; set; }

        public DbSet<SA_ProductAndFileTypeRelation> SA_ProductAndFileTypeRelation { get; set; }

        public DbSet<SA_ProductAndMarketRangeRelation> SA_ProductAndMarketRangeRelation { get; set; }

        public DbSet<SA_FreightIndex> SA_FreightIndex { get; set; }
        public DbSet<SA_FileType> SA_FileType { get; set; }

        public DbSet<SA_MarketRange> SA_MarketRange { get; set; }
        public DbSet<SA_IndustryLanding> SA_IndustryLanding { get; set; }
        public DbSet<SA_IndustrylandingAndProductRelation> SA_IndustrylandingAndProductRelation { get; set; }
        


        public virtual DbSet<SA_ProductFeedstock> SA_ProductFeedstock { get; set; }

        //public DbSet<SA_CompanyProduct> SA_CompanyProduct { get; set; }
        // public DbSet<SA_CompanyAndProductRelation> SA_CompanyAndProductRelation { get; set; }
        //public DbSet<CustomerNew> CustomerNew { get; set; }
        public DbSet<CustomerAndProducRelation> CustomerAndProducRelation { get; set; }

        public DbSet<SA_Product_IndiaMapData> SA_Product_IndiaMapDatas { get; set; }

        public DbSet<SA_Customer_LoginDetail> SA_Customer_LoginDetails { get; set; }
        //NEW CODE
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SA_CMS>()
             .Property(e => e.CMSName)
             .IsUnicode(false);

            modelBuilder.Entity<SA_CMS>()
                .Property(e => e.CMSDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_CMS>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<SA_CMS>()
                .Property(e => e.MetaDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_CMS>()
                .Property(e => e.CMSImg)
                .IsUnicode(false);
            modelBuilder.Entity<SA_Deals>()
              .Property(e => e.DealsName)
              .IsUnicode(false);

            modelBuilder.Entity<SA_Deals>()
                .Property(e => e.DealsDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Deals>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Deals>()
                .Property(e => e.Keywords)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Deals>()
                .Property(e => e.MetaDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Deals>()
                .Property(e => e.DealsImg)
                .IsUnicode(false);
            modelBuilder.Entity<SA_News>()
               .Property(e => e.NewsName)
               .IsUnicode(false);

            modelBuilder.Entity<SA_News>()
                .Property(e => e.NewsDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_News>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<SA_News>()
               .Property(e => e.Keywords)
               .IsUnicode(false);

            modelBuilder.Entity<SA_News>()
                .Property(e => e.MetaDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_News>()
                .Property(e => e.NewsImg)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Category>()
                .Property(e => e.CategoryDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Category>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Category>()
                .Property(e => e.MetaDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Category>()
                .Property(e => e.CategoryImg)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Product>()
                .Property(e => e.ProductDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Product>()
                .Property(e => e.Meta)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Product>()
                .Property(e => e.MetaDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Product>()
                .Property(e => e.ProductImg)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Role>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<SA_Role>()
                .Property(e => e.RoleDiscription)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Fname)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Lname)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.ProfileImage)
                .IsUnicode(false);

            modelBuilder.Entity<SA_User>()
                .Property(e => e.Role)
                .IsUnicode(false);
        }
    }
}
