-- Lightweight toggle used by the inline switch next to "Quarterly feedback
-- email" on the Overview tab panel (CustomerDetail.cshtml). Flips only
-- QuarterlyFeedbackEnabled on SA_CompanyFeedbackSettings — leaves Next
-- feedback email / Last feedback date / rating untouched, and does NOT
-- touch SA_CompanyFeedbackHistory (unlike USP_SaveCompanyFeedbackEntry,
-- which is the full "Edit" form save). Run this after
-- /database/SA_CompanyFeedbackSettings.sql (needs that table to exist).

IF OBJECT_ID('USP_ToggleQuarterlyFeedback', 'P') IS NOT NULL
    DROP PROCEDURE USP_ToggleQuarterlyFeedback
GO

CREATE PROCEDURE USP_ToggleQuarterlyFeedback
    @CompanyId INT,
    @Enabled   BIT,
    @UserName  NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingId INT;
    SELECT TOP 1 @ExistingId = Id FROM SA_CompanyFeedbackSettings WHERE CompanyId = @CompanyId ORDER BY Id DESC;

    IF @ExistingId IS NOT NULL
    BEGIN
        UPDATE SA_CompanyFeedbackSettings
        SET QuarterlyFeedbackEnabled = @Enabled,
            UpdatedBy = @UserName,
            UpdatedDate = GETDATE()
        WHERE Id = @ExistingId;
    END
    ELSE
    BEGIN
        INSERT INTO SA_CompanyFeedbackSettings (CompanyId, QuarterlyFeedbackEnabled, CreatedBy, CreatedDate)
        VALUES (@CompanyId, @Enabled, @UserName, GETDATE());
    END
END
GO
