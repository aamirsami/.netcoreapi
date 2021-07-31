USE [master]
GO

/****** Object:  Table [dbo].[EmployeeRecords]    Script Date: 31-07-2021 20:28:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeRecords]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeRecords]
GO

/****** Object:  Table [dbo].[EmployeeRecords]    Script Date: 31-07-2021 20:28:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



-- =============================================
-- Author:		Aamir Shaikh
-- Create date: 31-07-2021 at 17:30 hrs
-- Description:	Insert Employee record in database and return employee id, code and msg
-- =============================================
IF OBJECT_ID ('usp_InsertEmployee', 'P') IS NOT NULL  
    DROP PROCEDURE usp_InsertEmployee;  
GO
CREATE PROCEDURE usp_InsertEmployee
(
@firstname nvarchar(50) ,
@middlename nvarchar(50) ,
@lastname nvarchar(50) 
)	
AS

BEGIN
	DECLARE @EMP_ID TABLE (ID INT)
	BEGIN TRANSACTION INS_EMPLOYEE
	BEGIN TRY
		IF @firstname IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@firstname') -- with log
		ELSE IF @middlename IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@middlename') -- with log
		ELSE IF @lastname IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@lastname') -- with log

		INSERT INTO EmployeeRecords (FirstName,MiddleName,LastName)
			OUTPUT INSERTED.Id INTO @EMP_ID  
			VALUES (@firstname,@middlename,@lastname)

		COMMIT TRANSACTION INS_EMPLOYEE

		SELECT ID, 1 as CODE, 'Inserted' AS MSG from @EMP_ID

	END TRY
	BEGIN CATCH
		 ROLLBACK TRANSACTION INS_EMPLOYEE
		  SELECT 0 AS ID, ERROR_NUMBER() AS CODE, ERROR_MESSAGE() AS MSG;
	END CATCH

END
GO


-- =============================================
-- Author:		Aamir Shaikh
-- Create date: 31-07-2021 at 17:30 hrs
-- Description:	Update Employee record in database and return employee id, code & msg
-- =============================================
IF OBJECT_ID ('usp_UpdateEmployee', 'P') IS NOT NULL  
    DROP PROCEDURE usp_UpdateEmployee;  
GO
CREATE PROCEDURE usp_UpdateEmployee
(
@id int,
@firstname nvarchar(50) NULL,
@middlename nvarchar(50) NULL,
@lastname nvarchar(50) NULL
)	
AS

BEGIN
	
	BEGIN TRANSACTION UPD_EMPLOYEE
	BEGIN TRY
		IF @id IS NULL 
			RAISERROR('The value for %s should not be null', 16, 1, '@id') -- with log
		ELSE IF @firstname IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@firstname') -- with log
		ELSE IF @middlename IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@middlename') -- with log
		ELSE IF @lastname IS NULL
			RAISERROR('The value for %s should not be null', 16, 1, '@lastname') -- with log

		IF EXISTS(SELECT 1 FROM EmployeeRecords WHERE ID = @id)
			BEGIN
			
			UPDATE EmployeeRecords 
				SET FirstName = @firstname, MiddleName = @middlename, LastName = @lastname
				WHERE ID= @id

			COMMIT TRANSACTION UPD_EMPLOYEE

			SELECT @id AS ID, 1 as CODE, 'Updated' AS MSG 

			END
		ELSE
			BEGIN
				SELECT 0 AS ID, 2 as CODE, 'Employee does not exists' AS MSG
			END


	END TRY
	BEGIN CATCH
		 ROLLBACK TRANSACTION UPD_EMPLOYEE
		  SELECT 0 AS ID, ERROR_NUMBER() AS CODE, ERROR_MESSAGE() AS MSG;
	END CATCH

END
GO


-- =============================================
-- Author:		Aamir Shaikh
-- Create date: 31-07-2021 at 17:30 hrs
-- Description:	Delete Employee record in database and return employee id, code & msg
-- =============================================
IF OBJECT_ID ('usp_DeleteEmployee', 'P') IS NOT NULL  
    DROP PROCEDURE usp_DeleteEmployee;  
GO
CREATE PROCEDURE usp_DeleteEmployee
(
@id int
)	
AS

BEGIN
	
	BEGIN TRANSACTION DLT_EMPLOYEE
	BEGIN TRY
		IF @id IS NULL 
			RAISERROR('The value for %s should not be null', 16, 1, '@id') -- with log

		IF EXISTS(SELECT 1 FROM EmployeeRecords WHERE ID = @id)
			BEGIN
			
			DELETE FROM EmployeeRecords
				WHERE ID= @id

			COMMIT TRANSACTION DLT_EMPLOYEE

			SELECT @id AS ID, 1 as CODE, 'Deleted' AS MSG 

			END
		ELSE
			BEGIN
				SELECT 0 AS ID, 2 as CODE, 'Employee does not exists' AS MSG
			END


	END TRY
	BEGIN CATCH
		 ROLLBACK TRANSACTION DLT_EMPLOYEE
		  SELECT 0 AS ID, ERROR_NUMBER() AS CODE, ERROR_MESSAGE() AS MSG;
	END CATCH

END
GO



truncate table EmployeeRecords;

INSERT INTO EmployeeRecords(FirstName,  MiddleName, LastName) VALUES ('John', 'JS', 'Smith');
INSERT INTO EmployeeRecords(FirstName,  MiddleName, LastName) VALUES ('Jane', 'JA',  'Anderson');
INSERT INTO EmployeeRecords(FirstName,  MiddleName, LastName) VALUES ('Brad', 'BE', 'Everest');
INSERT INTO EmployeeRecords(FirstName,  MiddleName, LastName) VALUES ('Jack', 'JH', 'Horvath');

select
*
from EmployeeRecords;


