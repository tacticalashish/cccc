-- Run this once against the ChemAnalyst database (the DB behind the
-- "ChemAnalystContext" connection string) to set up the Documents tab on
-- CustomerDetail.cshtml.
--
-- Creates: table SA_CustomerDocuments, plus USP_GetCustomerDocumentsByCompany
-- and USP_InsertCustomerDocument, called from DAL/CustomerDocumentDAL.cs.
-- Files themselves are saved on disk under ~/CustomerDocs/{CompanyId}/{FileName}
-- (same file name on disk as in the DB — no renaming).

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SA_CustomerDocuments')
BEGIN
    CREATE TABLE SA_CustomerDocuments (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        CompanyId       INT NOT NULL,
        DocumentType    NVARCHAR(100)  NOT NULL,
        ReferenceNote   NVARCHAR(300)  NULL,
        FileName        NVARCHAR(260)  NOT NULL,   -- file name, same on disk and in the DB
        UploadedDate    DATETIME       NULL,
        UploadedBy      NVARCHAR(200)  NULL
    );

    CREATE INDEX IX_SA_CustomerDocuments_CompanyId ON SA_CustomerDocuments(CompanyId);
END
GO

IF OBJECT_ID('USP_GetCustomerDocumentsByCompany', 'P') IS NOT NULL
    DROP PROCEDURE USP_GetCustomerDocumentsByCompany
GO

CREATE PROCEDURE USP_GetCustomerDocumentsByCompany
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, CompanyId, DocumentType, ReferenceNote, FileName, UploadedDate, UploadedBy
    FROM SA_CustomerDocuments
    WHERE CompanyId = @CompanyId
    ORDER BY UploadedDate DESC, Id DESC
END
GO

IF OBJECT_ID('USP_InsertCustomerDocument', 'P') IS NOT NULL
    DROP PROCEDURE USP_InsertCustomerDocument
GO

CREATE PROCEDURE USP_InsertCustomerDocument
    @CompanyId      INT,
    @DocumentType   NVARCHAR(100),
    @ReferenceNote  NVARCHAR(300),
    @FileName       NVARCHAR(260),
    @UploadedBy     NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SA_CustomerDocuments
        (CompanyId, DocumentType, ReferenceNote, FileName, UploadedDate, UploadedBy)
    VALUES
        (@CompanyId, @DocumentType, @ReferenceNote, @FileName, GETDATE(), @UploadedBy)
END
GO
