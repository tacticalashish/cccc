-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to back the "Subscription history"
-- table on the History tab of CustomerDetail.cshtml.
--
-- Grouped strictly by ToDate — if a company has only 3 distinct ToDate
-- values, this returns exactly 3 rows, each with every product from any
-- subscription sharing that ToDate merged together (regardless of FromDate).
-- FromDate shown per row is the earliest FromDate among the subscriptions
-- sharing that ToDate, just for the "Term" display range.
--
-- Users = COUNT(DISTINCT UserId), joined without the ParentPrimaryId=1
-- restriction (matches the corrected query), grouped by ToDate.
-- AmountPaid still has no source table — left out entirely, shown as "—"
-- in the UI. Status is computed: ToDate in the future = Active, else Renewed.

IF OBJECT_ID('USP_GetSubscriptionHistoryByCompany', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetSubscriptionHistoryByCompany
GO

CREATE PROCEDURE USP_GetSubscriptionHistoryByCompany
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Base AS (
        SELECT
            CONVERT(DATE, sps.FromDate) AS FromDate,
            CONVERT(DATE, sps.ToDate)   AS ToDate,
            sps.ProductId
        FROM SalesPackageSubscriptions sps
        INNER JOIN SA_Customer sc      ON sc.id = sps.UserId AND sc.ParentPrimaryId = 1
        INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
        WHERE sm.ID = @CompanyId
    ),
    Flattened AS (
        SELECT DISTINCT b.ToDate, p.ProductName
        FROM Base b
        CROSS APPLY STRING_SPLIT(b.ProductId, ',') ps
        INNER JOIN SA_Product p ON p.id = TRY_CAST(ps.value AS INT)
    ),
    ToDateRange AS (
        SELECT ToDate, MIN(FromDate) AS FromDate
        FROM Base
        GROUP BY ToDate
    ),
    UserCounts AS (
        SELECT
            CONVERT(DATE, sps.ToDate)  AS ToDate,
            COUNT(DISTINCT sps.UserId) AS Users
        FROM SalesPackageSubscriptions sps
        INNER JOIN SA_Customer sc      ON sc.id = sps.UserId
        INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
        WHERE sm.ID = @CompanyId
        GROUP BY CONVERT(DATE, sps.ToDate)
    )
    SELECT
        r.FromDate,
        f.ToDate,
        uc.Users,
        CASE WHEN f.ToDate > CAST(GETDATE() AS DATE) THEN 'Active' ELSE 'Renewed' END AS Status,
        STRING_AGG(CAST(f.ProductName AS NVARCHAR(MAX)), ', ') AS ProductsSubscribed
    FROM Flattened f
    INNER JOIN ToDateRange r ON r.ToDate = f.ToDate
    LEFT JOIN UserCounts uc  ON uc.ToDate = f.ToDate
    GROUP BY r.FromDate, f.ToDate, uc.Users
    ORDER BY f.ToDate DESC
END
GO
