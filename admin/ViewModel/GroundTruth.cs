using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemAnalyst.ViewModel
{
    public class GroundTruth
    {


        public class Rootobject
        {
            public string GroundTruthID { get; set; }
            public Variant[] variant { get; set; }
            public Narrative narrative { get; set; }
            public string[] exports_to { get; set; }
            public Consumption consumption { get; set; }
            public string[] imports_from { get; set; }
            public Demand_Dynamics demand_dynamics { get; set; }
            public string[] major_producers { get; set; }
            public Supply_Dynamics supply_dynamics { get; set; }
            public Import_Vs_Export import_vs_export { get; set; }
            public Manufacturing_Dynamics manufacturing_dynamics { get; set; }
            public Key_Influencing_Factors[] key_influencing_factors { get; set; }
            public Market_External_Factors market_external_factors { get; set; }
        }

        public class Narrative
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Consumption
        {
            public IM IM { get; set; }
            public PR PR { get; set; }
        }

        public class IM
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class PR
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Demand_Dynamics
        {
            public Seasonality seasonality { get; set; }
            public Substitution_Effects substitution_effects { get; set; }
            public Green_Energy_Transition green_energy_transition { get; set; }
            public Global_Economic_Activity global_economic_activity { get; set; }
            public Inventory_Purchasing_Behavior inventory_purchasing_behavior { get; set; }
            public Policy_And_Regulatory_Changes policy_and_regulatory_changes { get; set; }
            public Consumer_Behavior_And_Preferences consumer_behavior_and_preferences { get; set; }
            public Emerging_Market_Industrialization emerging_market_industrialization { get; set; }
            public End_Use_Industry_Growth_And_Demand end_use_industry_growth_and_demand { get; set; }
            public Urbanization_And_Population_Growth urbanization_and_population_growth { get; set; }
            public Consumer_Electronics_And_Ecommerce_Growth consumer_electronics_and_ecommerce_growth { get; set; }
            public Technological_Advancements_In_Downstream_Sectors technological_advancements_in_downstream_sectors { get; set; }
        }

        public class Seasonality
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Substitution_Effects
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Green_Energy_Transition
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Global_Economic_Activity
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Inventory_Purchasing_Behavior
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Policy_And_Regulatory_Changes
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Consumer_Behavior_And_Preferences
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Emerging_Market_Industrialization
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class End_Use_Industry_Growth_And_Demand
        {
            public Description description { get; set; }
            public Industries_Sectors[] industries_sectors { get; set; }
            public Other_Applications[] other_applications { get; set; }
            public Chemical_Intermediates_Products[] chemical_intermediates_products { get; set; }
        }

        public class Description
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Industries_Sectors
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Other_Applications
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Chemical_Intermediates_Products
        {
            public Name name { get; set; }
            public Weight weight { get; set; }
        }

        public class Name
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Weight
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Urbanization_And_Population_Growth
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Consumer_Electronics_And_Ecommerce_Growth
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Technological_Advancements_In_Downstream_Sectors
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Supply_Dynamics
        {
            public Inventory_Levels inventory_levels { get; set; }
            public Energy_Supply_And_Prices energy_supply_and_prices { get; set; }
            public Raw_Material_Availability raw_material_availability { get; set; }
            public Trade_Policies_And_Tariffs trade_policies_and_tariffs { get; set; }
            public Geopolitical_Risks_And_Sanctions geopolitical_risks_and_sanctions { get; set; }
            public Technological_Barriers_Or_Delays technological_barriers_or_delays { get; set; }
            public Environmental_And_Safety_Regulations environmental_and_safety_regulations { get; set; }
            public Labor_Strikes_Or_Workforce_Shortages labor_strikes_or_workforce_shortages { get; set; }
            public Industry_Consolidation_Or_Restructuring industry_consolidation_or_restructuring { get; set; }
        }

        public class Inventory_Levels
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Energy_Supply_And_Prices
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Raw_Material_Availability
        {
            public Description1 description { get; set; }
            public Feedstock[] feedstocks { get; set; }
        }

        public class Description1
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Feedstock
        {
            public Name1 name { get; set; }
            public Weight1 weight { get; set; }
        }

        public class Name1
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Weight1
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Trade_Policies_And_Tariffs
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Geopolitical_Risks_And_Sanctions
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Technological_Barriers_Or_Delays
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Environmental_And_Safety_Regulations
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Labor_Strikes_Or_Workforce_Shortages
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Industry_Consolidation_Or_Restructuring
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Import_Vs_Export
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Manufacturing_Dynamics
        {
            public Plant_Utilization plant_utilization { get; set; }
            public Production_Capacity production_capacity { get; set; }
            public Plant_Outages_And_Maintenance plant_outages_and_maintenance { get; set; }
        }

        public class Plant_Utilization
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Production_Capacity
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Plant_Outages_And_Maintenance
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Market_External_Factors
        {
            public Currency_Exchange_Rates currency_exchange_rates { get; set; }
            public Pandemic_And_Health_Crises pandemic_and_health_crises { get; set; }
            public Inflation_And_Interest_Rates inflation_and_interest_rates { get; set; }
            public Natural_Disasters_And_Climate_Events natural_disasters_and_climate_events { get; set; }
            public Market_Speculation_And_Futures_Trading market_speculation_and_futures_trading { get; set; }
            public Logistics_And_Transportation_Constraints logistics_and_transportation_constraints { get; set; }
            public Carbon_Pricing_And_Sustainability_Mandates carbon_pricing_and_sustainability_mandates { get; set; }
        }

        public class Currency_Exchange_Rates
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Pandemic_And_Health_Crises
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Inflation_And_Interest_Rates
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Natural_Disasters_And_Climate_Events
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Market_Speculation_And_Futures_Trading
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Logistics_And_Transportation_Constraints
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Carbon_Pricing_And_Sustainability_Mandates
        {
            public Environmental_Regulations environmental_regulations { get; set; }
            public Shift_To_Bio_Based_Production shift_to_bio_based_production { get; set; }
        }

        public class Environmental_Regulations
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Shift_To_Bio_Based_Production
        {
            public string text { get; set; }
            public string link { get; set; }
        }

        public class Variant
        {
            public string name { get; set; }
            public string rank { get; set; }
        }

        public class Key_Influencing_Factors
        {
            public string text { get; set; }
            public string link { get; set; }
        }


    }
}