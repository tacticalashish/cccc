-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to set up the Billing & invoices
-- > Invoicing setup form on CustomerDetail.cshtml.
--
-- Creates: table SA_Invoicingsetup, plus USP_GetInvoicingSetupByCompany and
-- USP_SaveInvoicingSetup, called from DAL/InvoicingSetupDAL.cs.

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SA_Invoicingsetup')
BEGIN
    CREATE TABLE SA_Invoicingsetup (
        Id                        INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId                 INT NOT NULL,
        HowToSendInvoice          NVARCHAR(200)  NULL,
        InvoiceEmail              NVARCHAR(200)  NULL,
        VendorRegistrationNo      NVARCHAR(100)  NULL,
        PORequiredBeforeInvoice   BIT NOT NULL DEFAULT (0),
        UploadPortal              NVARCHAR(200)  NULL,
        HardCopyNeeded            BIT NOT NULL DEFAULT (0),
        HardCopyAddress           NVARCHAR(500)  NULL,
        SpecialInstructions       NVARCHAR(MAX)  NULL,
        CreatedBy                 NVARCHAR(200)  NULL,
        CreatedDate               DATETIME       NULL,
        UpdatedBy                 NVARCHAR(200)  NULL,
        UpdatedDate               DATETIME       NULL
    );

    CREATE INDEX IX_SA_Invoicingsetup_CompanyId ON SA_Invoicingsetup(CompanyId);
END
GO

IF OBJECT_ID('USP_GetInvoicingSetupByCompany', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetInvoicingSetupByCompany
GO

CREATE PROCEDURE USP_GetInvoicingSetupByCompany
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        Id, CompanyId, HowToSendInvoice, InvoiceEmail, VendorRegistrationNo,
        PORequiredBeforeInvoice, UploadPortal, HardCopyNeeded, HardCopyAddress,
        SpecialInstructions, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate
    FROM SA_Invoicingsetup
    WHERE CompanyId = @CompanyId
    ORDER BY Id DESC
END
GO

IF OBJECT_ID('USP_SaveInvoicingSetup', 'P') IS NOT NULL
    DROP PROCEDURE USP_SaveInvoicingSetup
GO

-- Upsert: updates the existing row for @CompanyId if one exists, otherwise inserts.
CREATE PROCEDURE USP_SaveInvoicingSetup
    @CompanyId               INT,
    @HowToSendInvoice        NVARCHAR(200),
    @InvoiceEmail            NVARCHAR(200),
    @VendorRegistrationNo    NVARCHAR(100),
    @PORequiredBeforeInvoice BIT,
    @UploadPortal            NVARCHAR(200),
    @HardCopyNeeded          BIT,
    @HardCopyAddress         NVARCHAR(500),
    @SpecialInstructions     NVARCHAR(MAX),
    @UserName                NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExistingId INT;
    SELECT TOP 1 @ExistingId = Id FROM SA_Invoicingsetup WHERE CompanyId = @CompanyId ORDER BY Id DESC;

    IF @ExistingId IS NOT NULL
    BEGIN
        UPDATE SA_Invoicingsetup SET
            HowToSendInvoice        = @HowToSendInvoice,
            InvoiceEmail            = @InvoiceEmail,
            VendorRegistrationNo    = @VendorRegistrationNo,
            PORequiredBeforeInvoice = @PORequiredBeforeInvoice,
            UploadPortal            = @UploadPortal,
            HardCopyNeeded          = @HardCopyNeeded,
            HardCopyAddress         = @HardCopyAddress,
            SpecialInstructions     = @SpecialInstructions,
            UpdatedBy               = @UserName,
            UpdatedDate             = GETDATE()
        WHERE Id = @ExistingId;
    END
    ELSE
    BEGIN
        INSERT INTO SA_Invoicingsetup
            (CompanyId, HowToSendInvoice, InvoiceEmail, VendorRegistrationNo, PORequiredBeforeInvoice,
             UploadPortal, HardCopyNeeded, HardCopyAddress, SpecialInstructions, CreatedBy, CreatedDate)
        VALUES
            (@CompanyId, @HowToSendInvoice, @InvoiceEmail, @VendorRegistrationNo, @PORequiredBeforeInvoice,
             @UploadPortal, @HardCopyNeeded, @HardCopyAddress, @SpecialInstructions, @UserName, GETDATE());
    END
END
GO
