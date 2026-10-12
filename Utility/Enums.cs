using System.ComponentModel;

namespace Utility
{
    public enum MenuType
    {
        [Description("Chemical Pricing")]
        CHEMICAL_PRICING = 1,
        [Description("Market Analysis")]
        MARKET_ANALYSIS = 2,
        [Description("Company Profile")]
        COMPANY_PROFILE = 3,
        [Description("News")]
        NEWS = 5,
        [Description("Deals")]
        DEALS = 6,
        [Description("Sectoral")]
        SECTORAL = 7
    }

    public enum MarketType
    {
        [Description("Product-Wise")]
        PRODUCT_WISE = 1,
        [Description("Country-Wise")]
        COUNTRY_WISE = 2,
        [Description("Region-Wise")]
        REGION_WISE = 3
    }

    public enum Type
    {
        [Description("Chemical Pricing")]
        CHEMICAL_PRICING = 1,
        [Description("Market Analysis")]
        MARKET_ANALYSIS = 2
    }

    public enum Status
    {
        [Description("Sucess")]
        SUCESS = 1,
        [Description("Fail")]
        FAIL = 2
    }

    public enum TicketStatus
    {
        [Description("Pending Admin Response")]
        PENDING_ADMIN_RESPONSE = 1,
        [Description("Pending Customer Response")]
        PENDING_CUSTOMER_RESPONSE = 2,
        [Description("Closed")]
        CLOSED = 3
    }

    public enum ActionType
    {
        [Description("Create")]
        CREATE = 1,
        [Description("Update")]
        UPDATE = 2,
        [Description("Delete")]
        DELETE = 3
    }

    public enum ChemicalRangeType
    {
        [Description("Yearly")]
        YEARLY = 1,
        [Description("Monthly")]
        MONTHLY = 2,
        [Description("Quarterly")]
        QUARTERLY = 3,
        [Description("Weekly")]
        WEEKLY = 4,
        [Description("Daily Data Import")]
        DAILY_DATA_IMPORT = 5,
        [Description("Daily Retail")]
        DAILY_RETAIL = 6,
        [Description("Daily Bulk")]
        DAILY_BULK = 7,
        [Description("Chemical 1")]
        CHEMICAL_1 = 8,
        [Description("Chemical 2")]
        CHEMICAL_2 = 9,
        [Description("Chemical 3")]
        CHEMICAL_3 = 10,
        [Description("Chemical 4")]
        CHEMICAL_4 = 11,
        [Description("Chemical 5")]
        CHEMICAL_5 = 12,
        [Description("Chemical 6")]
        CHEMICAL_6 = 13
    }

    public enum MarketRangeType
    {
        [Description("Capacity By Company")]
        CAPACITY_BY_COMPANY = 2,
        [Description("Capacity By Location")]
        CAPACITY_BY_LOCATION = 3,
        [Description("Capacity By Technology")]
        CAPACITY_BY_TECHNOLOGY = 5,
        [Description("Capacity By Process")]
        CAPACITY_BY_PROCESS = 6,
        [Description("Production By Company")]
        PRODUCTION_BY_COMPANY = 7,
        [Description("Operating Efficiency")]
        OPERATING_EFFICIENCY = 8,
        [Description("Demand By End Use")]
        DEMAND_BY_END_USE = 9,
        [Description("Demand By Grade")]
        DEMAND_BY_GRADE = 10,
        [Description("Demand By Type")]
        DEMAND_BY_TYPE = 11,
        [Description("Demand By Sales channel")]
        DEMAND_BY_SALES_CHANNEL = 12,
        [Description("Demand By Region")]
        DEMAND_BY_REGION = 13,
        [Description("Demand By Country Wise Export")]
        DEMAND_BY_COUNTRY_WISE_EXPORT = 19,
        [Description("Demand By Country Wise Import")]
        DEMAND_BY_COUNTRY_WISE_IMPORT = 20,
        [Description("Demand By Company Shares")]
        DEMAND_BY_COMPANY_SHARES = 18,
        [Description("Demand By Supply Gap")]
        DEMAND_BY_SUPPLY_GAP = 16,
        [Description("Demand By Supply Gap Graph")]
        DEMAND_BY_SUPPLY_GAP_Graph = 17,
        [Description("Port Wise Import")]
        PORT_WISE_IMPORT = 21,
        [Description("Port Wise Export")]
        PORT_WISE_EXPORT = 22,
        [Description("Supplier Analysis")]
        SUPPLIER_ANALYSIS = 23,
        [Description("Customer")]
        CUSTOMER = 24,
        [Description("Industry Market Size")]
        Industry_Market_Size = 25,
        [Description("TechnologyLicensor")]
        TechnologyLicensor = 26,
        [Description("Policy and Regulation")]
        PolicyandRegulation = 27,
        [Description("Manufacturing Process")]
        ManufacturingProcess = 28
    }




}
