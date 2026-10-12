-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to back the Subscriptions list
-- page (SalesManagement/Index.cshtml).
--
-- One row here = one "Transfer to CSM" submission from the New Subscription
-- wizard, regardless of how many Lead_Master rows that submission fanned out
-- into (Lead_Master gets one row per covered country/region, per the
-- existing table structure). Package/Coverage summary text is computed by
-- the wizard itself at save time (it already knows exactly what was picked)
-- and stored here as plain text, rather than re-derived later by joining
-- back to Lead_Master.
--
-- Each Lead_Master row created by a submission gets this row's Id written
-- into its (previously unused) InterestArea column, so the two can still be
-- traced back to each other later if needed.

IF OBJECT_ID('SA_SubscriptionRequest', 'U') IS NULL
BEGIN
    CREATE TABLE SA_SubscriptionRequest
    (
        Id                  INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId           INT NULL,
        CompanyName         NVARCHAR(255) NULL,
        PrimaryContactName  NVARCHAR(255) NULL,
        PackageType         NVARCHAR(20) NULL,     -- 'Full' or 'ALaCarte'
        PackageSummary      NVARCHAR(255) NULL,    -- e.g. 'Full package', 'Chemical Pricing + Market Analysis'
        CoverageSummary     NVARCHAR(255) NULL,    -- e.g. 'All tracked countries', '4 countries'
        CSM                 NVARCHAR(255) NULL,    -- assigned CRM/CSM display name
        StepCounter         INT NOT NULL DEFAULT 1, -- same semantics as Lead_Master.StepCounter:
                                                      -- 1-4 = pending at various stages, 5 = approved/live,
                                                      -- 20/30/40 = returned by Operations/Finance/Sales Manager
        StatusRemark        NVARCHAR(1000) NULL,   -- shown under the status badge when returned
        CreatedDate         DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy           NVARCHAR(255) NULL
    )
END
GO
