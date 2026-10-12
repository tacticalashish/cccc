-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to back the three summary cards
-- on the History tab of CustomerDetail.cshtml (Customer since / Renewals /
-- Lifetime value). LifetimeValue isn't computed here — no source for it yet,
-- left NULL and shown as such in the UI until that's defined.

IF OBJECT_ID('USP_GetCustomerHistorySummary', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetCustomerHistorySummary
GO

CREATE PROCEDURE USP_GetCustomerHistorySummary
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MIN(sps.FromDate)                        AS CustomerSince,
        COUNT(DISTINCT CAST(sps.ToDate AS DATE))  AS Renewals
    FROM SalesPackageSubscriptions sps
    INNER JOIN SA_Customer sc      ON sc.id = sps.UserId AND sc.ParentPrimaryId = 1
    INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
    WHERE sm.ID = @CompanyId
END
GO
