-- Fixes: Notes typed in the "Edit feedback settings" modal never bound back
-- into the textarea on reopen, because SA_CompanyFeedbackSettings (the
-- "current" row shown/edited on the panel) never stored Notes at all —
-- Notes was only ever written into SA_CompanyFeedbackHistory (the log).
--
-- Adds a LastFeedbackNotes column to SA_CompanyFeedbackSettings and updates
-- USP_GetCompanyFeedbackSettings / USP_SaveCompanyFeedbackEntry to
-- read/write it, so the modal now round-trips Notes correctly. Run this
-- AFTER /database/SA_CompanyFeedbackSettings.sql.

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('SA_CompanyFeedbackSettings') AND name = 'LastFeedbackNotes')
BEGIN
    ALTER TABLE SA_CompanyFeedbackSettings ADD LastFeedbackNotes NVARCHAR(MAX) NULL;
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
        LastFeedbackDate, LastFeedbackRating, LastFeedbackNotes,
        CreatedBy, CreatedDate, UpdatedBy, UpdatedDate
    FROM SA_CompanyFeedbackSettings
    WHERE CompanyId = @CompanyId
    ORDER BY Id DESC
END
GO

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
            LastFeedbackNotes        = @Notes,
            UpdatedBy                = @UserName,
            UpdatedDate              = GETDATE()
        WHERE Id = @ExistingId;
    END
    ELSE
    BEGIN
        INSERT INTO SA_CompanyFeedbackSettings
            (CompanyId, QuarterlyFeedbackEnabled, NextFeedbackEmail, LastFeedbackDate, LastFeedbackRating, LastFeedbackNotes, CreatedBy, CreatedDate)
        VALUES
            (@CompanyId, @Enabled, @NextFeedbackEmail, @LastFeedbackDate, @LastFeedbackRating, @Notes, @UserName, GETDATE());
    END

    IF @LastFeedbackDate IS NOT NULL
    BEGIN
        INSERT INTO SA_CompanyFeedbackHistory (CompanyId, FeedbackDate, Rating, Notes, CreatedBy, CreatedDate)
        VALUES (@CompanyId, @LastFeedbackDate, @LastFeedbackRating, @Notes, @UserName, GETDATE());
    END
END
GO
