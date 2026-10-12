-- Expiry notifications toggle (Overview tab panel, CustomerDetail.cshtml).
-- Reuses the existing SalesPackageSubscriptions.ExpiryNotification column —
-- no new table needed. Toggling flips it for every row belonging to the
-- company (matches the exact query the user gave).

IF OBJECT_ID('USP_GetExpiryNotificationStatusByCompany', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetExpiryNotificationStatusByCompany
GO

CREATE PROCEDURE USP_GetExpiryNotificationStatusByCompany
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(MAX(CAST(sps.ExpiryNotification AS INT)), 0) AS IsEnabled
    FROM SalesPackageSubscriptions sps
    INNER JOIN SA_Customer sc      ON sc.ID = sps.UserId
    INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
    WHERE sm.ID = @CompanyId
END
GO

IF OBJECT_ID('USP_UpdateExpiryNotificationByCompany', 'P') IS NOT NULL
    DROP PROCEDURE USP_UpdateExpiryNotificationByCompany
GO

CREATE PROCEDURE USP_UpdateExpiryNotificationByCompany
    @CompanyId INT,
    @IsEnabled BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE sps
    SET sps.ExpiryNotification = @IsEnabled
    FROM SalesPackageSubscriptions sps
    INNER JOIN SA_Customer sc      ON sc.ID = sps.UserId
    INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
    WHERE sm.ID = @CompanyId
END
GO
