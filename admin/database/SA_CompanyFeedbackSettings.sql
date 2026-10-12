-- Self-contained "Quarterly feedback email / Next feedback email / Last
-- feedback" feature behind the Overview tab panel on CustomerDetail.cshtml.
-- Does NOT depend on SA_CompanyRenewalSettings or any renewal table/proc —
-- both tables and all 3 procedures needed by this feature are created here.
--
-- SA_CompanyFeedbackSettings = single "current" row per company (what the
-- panel displays: toggle + next feedback date + last feedback date/rating).
-- SA_CompanyFeedbackHistory  = one row per saved feedback entry (log,
-- shown in the "View feedback history" popup).

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SA_CompanyFeedbackSettings')
BEGIN
    CREATE TABLE SA_CompanyFeedbackSettings (
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

    CREATE INDEX IX_SA_CompanyFeedbackSettings_CompanyId ON SA_CompanyFeedbackSettings(CompanyId);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SA_CompanyFeedbackHistory')
BEGIN
    CREATE TABLE SA_CompanyFeedbackHistory (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId    INT NOT NULL,
        FeedbackDate DATE NULL,
        Rating       INT NULL,
        Notes        NVARCHAR(MAX) NULL,
        CreatedBy    NVARCHAR(200) NULL,
        CreatedDate  DATETIME NULL
    );

    CREATE INDEX IX_SA_CompanyFeedbackHistory_CompanyId ON SA_CompanyFeedbackHistory(CompanyId);
END
GO

IF OBJECT_ID('USP_GetCompanyFeedbackSettings', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetCompanyFeedbackSettings
GO

CREATE PROCEDURE USP_GetCompanyFeedbackSettings
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id, CompanyId, QuarterlyFeedbackEnabled, NextFeedbackEmail,
        LastFeedbackDate, LastFeedbackRating, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate
    FROM SA_CompanyFeedbackSettings
    WHERE CompanyId = @CompanyId
    ORDER BY Id DESC
END
GO

-- Upserts SA_CompanyFeedbackSettings (Quarterly feedback toggle + Next
-- feedback email + Last feedback date/rating) and, only when a
-- @LastFeedbackDate is supplied, also appends a row to
-- SA_CompanyFeedbackHistory so it shows up in "View feedback history".
IF OBJECT_ID('USP_SaveCompanyFeedbackEntry', 'P') IS NOT NULL
    DROP PROCEDURE USP_SaveCompanyFeedbackEntry
GO

CREATE PROCEDURE USP_SaveCompanyFeedbackEntry
    @CompanyId          INT,
    @Enabled            BIT,
    @NextFeedbackEmail  DATE = NULL,
    @LastFeedbackDate   DATE = NULL,
    @LastFeedbackRating INT = NULL,
    @Notes              NVARCHAR(MAX) = NULL,
    @UserName           NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingId INT;
    SELECT TOP 1 @ExistingId = Id FROM SA_CompanyFeedbackSettings WHERE CompanyId = @CompanyId ORDER BY Id DESC;

    IF @ExistingId IS NOT NULL
    BEGIN
        UPDATE SA_CompanyFeedbackSettings
        SET QuarterlyFeedbackEnabled = @Enabled,
            NextFeedbackEmail        = @NextFeedbackEmail,
            LastFeedbackDate         = @LastFeedbackDate,
            LastFeedbackRating       = @LastFeedbackRating,
            UpdatedBy                = @UserName,
            UpdatedDate              = GETDATE()
        WHERE Id = @ExistingId;
    END
    ELSE
    BEGIN
        INSERT INTO SA_CompanyFeedbackSettings
            (CompanyId, QuarterlyFeedbackEnabled, NextFeedbackEmail, LastFeedbackDate, LastFeedbackRating, CreatedBy, CreatedDate)
        VALUES
            (@CompanyId, @Enabled, @NextFeedbackEmail, @LastFeedbackDate, @LastFeedbackRating, @UserName, GETDATE());
    END

    IF @LastFeedbackDate IS NOT NULL
    BEGIN
        INSERT INTO SA_CompanyFeedbackHistory (CompanyId, FeedbackDate, Rating, Notes, CreatedBy, CreatedDate)
        VALUES (@CompanyId, @LastFeedbackDate, @LastFeedbackRating, @Notes, @UserName, GETDATE());
    END
END
GO

IF OBJECT_ID('USP_GetCompanyFeedbackHistory', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetCompanyFeedbackHistory
GO

CREATE PROCEDURE USP_GetCompanyFeedbackHistory
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, CompanyId, FeedbackDate, Rating, Notes, CreatedBy, CreatedDate
    FROM SA_CompanyFeedbackHistory
    WHERE CompanyId = @CompanyId
    ORDER BY FeedbackDate DESC, Id DESC
END
GO
