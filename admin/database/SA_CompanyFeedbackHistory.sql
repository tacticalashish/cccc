-- Adds the feedback-history log behind the "View feedback history" popup on
-- CustomerDetail.cshtml's Renewal & automated emails panel, plus a combined
-- save procedure for its Quarterly feedback email / Next feedback email /
-- Last feedback fields (all edited together via one "Edit" form).
--
-- Reuses the existing SA_CompanyRenewalSettings table (single row per
-- company = the "current" values shown in the panel — see
-- /database/SA_CompanyRenewalSettings.sql). This script adds a new
-- SA_CompanyFeedbackHistory table (one row per recorded feedback entry,
-- appended every time a Last feedback date is saved) plus two procedures.

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

-- Upserts SA_CompanyRenewalSettings (Quarterly feedback toggle + Next
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
    SELECT TOP 1 @ExistingId = Id FROM SA_CompanyRenewalSettings WHERE CompanyId = @CompanyId ORDER BY Id DESC;

    IF @ExistingId IS NOT NULL
    BEGIN
        UPDATE SA_CompanyRenewalSettings
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
        INSERT INTO SA_CompanyRenewalSettings
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
