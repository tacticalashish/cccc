/*************************************** Query *******************************************/

1. ALTER TABLE SA_Role ADD ForwardingEmail nvarchar(200) NULL;
2. UPDATE SA_Role set ForwardingEmail = 'test@test.com' where [Role] = 'manager'
3. ALTER TABLE SA_Role ALTER COLUMN ForwardingEmail nvarchar(200) NOT NULL;





/******************************* table ************************************************/


CREATE TABLE [dbo].[SA_Ticket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserFID] [int] NOT NULL,
	[RoleName] [nvarchar](200) NOT NULL,
	[Status] [nvarchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SA_Ticket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[SA_TicketComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TicketFID] [int] NOT NULL,
	[UserFID] [int] NULL,
	[ManagerFID] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[FileName] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_SA_TicketComment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



/***************************************** store procedure *************************************/

CREATE PROC [dbo].[usp_tbl_SA_ticket_CREATE]
@UserFID INT,
@RoleName NVARCHAR(100),
@Query NVARCHAR(MAX),
@Status NVARCHAR(200),
@FileName NVARCHAR(500),
@CreatedDate DATETIME
AS
BEGIN	
		--Insert/creat Ticket
		INSERT INTO [dbo].[SA_Ticket]
				([UserFID]
				,[RoleName]
				,[Status]
				,[CreatedDate]
				,[UpdatedDate])
		VALUES
				(@UserFID
				,@RoleName
				,@Status
				,@CreatedDate
				,@CreatedDate)


		--create comment with file attachment name
			INSERT INTO [dbo].[SA_TicketComment]
					   ([TicketFID]
					   ,[UserFID]
					   ,[Comment]
					   ,[FileName]
					   ,[CreatedDate])
				 VALUES
					   (Scope_Identity()
					   ,@UserFID
					   ,@Query
					   ,@FileName
					   ,@CreatedDate)
			

END



-- [dbo].[usp_tbl_SA_Ticket_CustomSearch]  NULL,NULL,NULL,NULL,NULL,1,10
CREATE PROCEDURE [dbo].[usp_tbl_SA_Ticket_CustomSearch] 
(
@UserFID INT = NULL,
@RoleName INT = NULL,
@Status NVARCHAR(200) = NULL,
@FromDate DATE = NULL,
@ToDate DATE = NULL,
@StartRowNumber INT = NULL,
@EndRowNumber INT = NULL
 )    
AS
BEGIN
SET NOCOUNT ON;

        DECLARE @Query NVARCHAR(MAX)
        SET @Query = 'SELECT   join_SA_Ticket.[ID]
							,join_SA_Ticket.[UserFID]
							,join_SA_Ticket.[RoleName]
							,join_SA_Ticket.[Status]
							,join_SA_Ticket.[CreatedDate]
							,join_SA_Ticket.[UpdatedDate]
							,join_SA_Role.[Role]
							,join_SA_Role.[ForwardingEmail]
							,join_SA_Customer.Fname as CustomerName
							,join_SA_Customer.Email as CustomerEmail
							, ROW_NUMBER() OVER (ORDER BY join_SA_Ticket.[ID] DESC) AS RowNumber
							, RecordsCount = COUNT(*) OVER()
						FROM [dbo].[SA_Ticket] join_SA_Ticket
						JOIN SA_Role join_SA_Role ON join_SA_Ticket.RoleName = join_SA_Role.[Role]
						JOIN SA_Customer join_SA_Customer ON  join_SA_Customer.id = join_SA_Ticket.UserFID
					      WHERE 1 = 1 '
		

         IF(@UserFID IS NOT NULL AND  @UserFID > 0) 
                        BEGIN 
                            SET	@query = @query + ' AND join_SA_Ticket.[UserFID] = ''' + CAST(@UserFID AS NVARCHAR(50)) + ''''
                        END

		 IF(@RoleName IS NOT NULL) 
                        BEGIN 
                            SET	@query = @query + ' AND join_SA_Ticket.[RoleName] = ''' + CAST(@RoleName AS NVARCHAR(250)) + ''''
                        END

		 IF(@Status IS NOT NULL) 
                        BEGIN 
                            SET	@query = @query + ' AND join_SA_Ticket.[Status] = ''' + CAST(@Status AS NVARCHAR(250)) + ''''
                        END

        
		IF(@FromDate IS NOT NULL)
            BEGIN
                  IF(@ToDate IS NOT NULL)
                        BEGIN
                              SET @query = @query + ' AND CONVERT(DATE, join_SA_Ticket.[CreatedDate]'+  ') BETWEEN ''' + CAST(@FromDate AS NVARCHAR(250)) + ''' AND ''' + CAST(@ToDate AS NVARCHAR(250)) + ''''
                        END
                  ELSE
                        BEGIN
                              SET   @query = @query + ' AND CONVERT(DATE, join_SA_Ticket.[CreatedDate]' + ') = ''' +  CAST(@FromDate AS NVARCHAR(250)) + ''''
                        END
            END


        SET @Query = @Query + ' ORDER BY join_SA_Ticket.ID DESC OFFSET (' + CAST(@StartRowNumber AS VARCHAR) + ' - 1) ROWS 
                                            FETCH NEXT (' + CAST(@EndRowNumber AS VARCHAR) + ' + 1) - ' + CAST(@StartRowNumber AS VARCHAR) + ' ROWS ONLY'
                
        --PRINT @Query
        EXEC SP_EXECUTESQL @Query;

END


CREATE PROC [dbo].[usp_tbl_SA_Ticket_READ]
@ID INT
AS
BEGIN
	IF(@ID > 0)
		BEGIN
			SELECT   join_SA_Ticket.[ID]
					,join_SA_Ticket.[UserFID]
					,join_SA_Ticket.[RoleName]
					,join_SA_Ticket.[Status]
					,join_SA_Ticket.[CreatedDate]
					,join_SA_Ticket.[UpdatedDate]
					,join_SA_Role.[Role]
					,join_SA_Role.[ForwardingEmail]
					,join_SA_Customer.Fname as CustomerName
					,join_SA_Customer.Email as CustomerEmail
					, ROW_NUMBER() OVER (ORDER BY join_SA_Ticket.[ID] DESC) AS RowNumber
					, RecordsCount = COUNT(*) OVER()
				FROM [dbo].[SA_Ticket] join_SA_Ticket
				JOIN SA_Role join_SA_Role ON join_SA_Ticket.RoleName = join_SA_Role.[Role]
				JOIN SA_Customer join_SA_Customer ON  join_SA_Customer.id = join_SA_Ticket.UserFID
				WHERE join_SA_Ticket.ID = @ID
		END
END



CREATE PROC [dbo].[usp_tbl_SA_ticket_UPDATE]
@ID INT,
@UserFID INT,
@Type NVARCHAR(100),
@Query NVARCHAR(2000),
@Status NVARCHAR(200),
@FileName NVARCHAR(500),
@UpdatedDate DATETIME
AS
BEGIN
	UPDATE [dbo].[SA_Ticket]
	   SET [UserFID] = @UserFID
		  ,[Type] = @Type
		  ,[Query] = @Query
		  ,[Status] = @Status
		  ,[FileName] = @FileName
		  ,[UpdatedDate] = @UpdatedDate
	 WHERE ID = @ID
END




CREATE PROC [dbo].[usp_tbl_SA_TicketComment_CREATE]
@TicketFID INT = NULL,
@UserFID INT = NULL,
@ManagerFID INT = NULL,
@FileName NVARCHAR(200) = NULL,
@Comment NVARCHAR(MAX) = NULL,
@CreatedDate DATETIME = NULL,
@Status NVARCHAR(150)
AS
BEGIN
	INSERT INTO [dbo].[SA_TicketComment]
           ([TicketFID]
           ,[UserFID]
           ,[ManagerFID]
		   ,[FileName]
           ,[Comment]
           ,[CreatedDate])
     VALUES
           (@TicketFID,
			@UserFID,
			@ManagerFID,
			@FileName,
			@Comment,
			@CreatedDate)

	--Update Ticket last updated date
	UPDATE SA_Ticket SET UpdatedDate = @CreatedDate WHERE ID = @TicketFID
	UPDATE SA_Ticket SET [Status] = @Status WHERE ID = @TicketFID

	
	
END



--[dbo].[usp_tbl_SA_TicketComment_READ] 1
CREATE PROC [dbo].[usp_tbl_SA_TicketComment_READ]
@TicketFID INT = NULL
AS
BEGIN
		IF(@TicketFID IS NOT  NULL AND @TicketFID > 0)
			BEGIN
				SELECT [ID]
					  ,[TicketFID]
					  ,[FileName]
					  ,[UserFID]
					  ,(SELECT Fname FROM SA_Customer WHERE id = UserFID) as CustomerName
					  ,[ManagerFID]
					  ,(SELECT Fname FROM SA_User WHERE id = ManagerFID) as ManagerName
					  ,[Comment]
					  ,[CreatedDate]
				  FROM [dbo].[SA_TicketComment]
				  WHERE TicketFID = @TicketFID
				  ORDER BY ID DESC
			END
		
END




CREATE PROC [dbo].[usp_tbl_Ticket_Closed]
@ID INT,
@ManagerFID INT,
@Comment NVARCHAR(500),
@Status NVARCHAR(200),
@UpdatedDate DATETIME
AS
BEGIN
	
	--Udate Status based ticket ID
		UPDATE [dbo].[SA_Ticket]
		   SET [Status] = @Status 
			  ,[UpdatedDate] = @UpdatedDate
		 WHERE ID = @ID

		 --create comment according to manager
		 exec [usp_tbl_SA_TicketComment_CREATE] @ID,NULL,@ManagerFID,NULL,@Comment,@UpdatedDate,@Status
	  
END



--[dbo].[usp_tbl_Ticket_UpdateStatus] 11,1,'transfer to another status report','Pending Admin Response','2021-01-01'
CREATE PROC [dbo].[usp_tbl_Ticket_UpdateStatus]
@ID INT,
@ManagerFID INT,
@Comment NVARCHAR(500),
@Status NVARCHAR(200),
@RoleName NVARCHAR(200),
@UpdatedDate DATETIME
AS
BEGIN
	
	--Udate Status based ticket ID
		UPDATE [dbo].[SA_Ticket]
		   SET [Status] = @Status 
			  ,[RoleName] = @RoleName
			  ,[UpdatedDate] = @UpdatedDate
		 WHERE ID = @ID

		 --create comment according to manager
		 exec [usp_tbl_SA_TicketComment_CREATE] @ID,NULL,@ManagerFID,NULL,@Comment,@UpdatedDate,@Status
	  
END





















