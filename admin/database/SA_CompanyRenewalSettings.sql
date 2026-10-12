-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to back the "Renewal & automated
-- emails" panel on the Overview tab of CustomerDetail.cshtml.
--
-- Part 1: Expiry notifications toggle — reuses the existing
-- SalesPackageSubscriptions.ExpiryNotification column, no new table.
-- Toggling flips it for every row belonging to the company (matches the
-- user's given query pattern — no ParentPrimaryId filter).

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
    INNER JOIN SA_Customer sc      ON sc.id = sps.UserId
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
    INNER JOIN SA_Customer sc      ON sc.id = sps.UserId
    INNER JOIN SA_CompanyMaster sm ON sm.ID = sc.CompanyPrimaryId
    WHERE sm.ID = @CompanyId
END
GO

-- Part 2: Quarterly feedback email toggle + next/last feedback tracking.
-- New table since none of this exists anywhere yet.

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SA_CompanyRenewalSettings')
BEGIN
    CREATE TABLE SA_CompanyRenewalSettings (
        Id                       INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId                INT NOT NULL,
        QuarterlyFeedbackEnabled BIT NOT NULL DEFAULT (0),
        NextFeedbackEmail        DATE NULL,
        LastFeedbackDate         DATE NULL,
        LastFeedbackRating       INT NULL,
        CreatedBy                NVARCHAR(200) NULL,
        CreatedDate              DATETIME NULL,
        UpdatedBy                NVARCHAR(200) NULL,
        UpdatedDate              DATETIME NULL
    );

    CREATE INDEX IX_SA_CompanyRenewalSettings_CompanyId ON SA_CompanyRenewalSettings(CompanyId);
END
GO

IF OBJECT_ID('USP_GetCompanyRenewalSettings', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetCompanyRenewalSettings
GO

CREATE PROCEDURE USP_GetCompanyRenewalSettings
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id, CompanyId, QuarterlyFeedbackEnabled, NextFeedbackEmail,
        LastFeedbackDate, LastFeedbackRating, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate
    FROM SA_CompanyRenewalSettings
    WHERE CompanyId = @CompanyId
    ORDER BY Id DESC
END
GO

IF OBJECT_ID('USP_SaveQuarterlyFeedbackSetting', 'P') IS NOT NULL
    DROP PROCEDURE USP_SaveQuarterlyFeedbackSetting
GO

-- Upsert: only touches QuarterlyFeedbackEnabled — NextFeedbackEmail /
-- LastFeedbackDate / LastFeedbackRating have no input mechanism yet, so
-- they're left as-is (or NULL on first insert) until that's built.
CREATE PROCEDURE USP_SaveQuarterlyFeedbackSetting
    @CompanyId INT,
    @Enabled   BIT,
    @UserName  NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingId INT;
    SELECT TOP 1 @ExistingId = Id FROM SA_CompanyRenewalSettings WHERE CompanyId = @CompanyId ORDER BY Id DESC;

    IF @ExistingId IS NOT NULL
    BEGIN
        UPDATE SA_CompanyRenewalSettings
        SET QuarterlyFeedbackEnabled = @Enabled,
            UpdatedBy = @UserName,
            UpdatedDate = GETDATE()
        WHERE Id = @ExistingId;
    END
    ELSE
    BEGIN
        INSERT INTO SA_CompanyRenewalSettings (CompanyId, QuarterlyFeedbackEnabled, CreatedBy, CreatedDate)
        VALUES (@CompanyId, @Enabled, @UserName, GETDATE());
    END
END
GO
