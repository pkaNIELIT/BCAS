-- =============================================
-- Stored Procedures for Updating Organization Details
-- Created: For Edit functionality on Organization Details pages
-- =============================================

-- =============================================
-- Stored Procedure: UpdatePayeeOrganizationDetails
-- Description: Updates payee organization details based on the organization code
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePayeeOrganizationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdatePayeeOrganizationDetails]
GO

CREATE PROCEDURE [dbo].[UpdatePayeeOrganizationDetails]
	@Payee_Organisation_Code VARCHAR(50),
	@Payee_Organisation_Name VARCHAR(200),
	@Payee_Organisation_State VARCHAR(100),
	@Payee_Address VARCHAR(500),
	@Location_Pincode VARCHAR(10),
	@GST_NUMBER VARCHAR(50),
	@State_Code VARCHAR(10),
	@Modified_By VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Update the payee organization details
		UPDATE PayeeOrganisation
		SET 
			Payee_Organisation_Name = @Payee_Organisation_Name,
			Payee_Organisation_State = @Payee_Organisation_State,
			Payee_Address = @Payee_Address,
			Location_Pincode = @Location_Pincode,
			GST_NUMBER = @GST_NUMBER,
			State_Code = @State_Code,
			Modified_Date = GETDATE(),
			Modified_By = @Modified_By
		WHERE 
			Payee_Organisation_Code = @Payee_Organisation_Code;

		-- Return the number of rows affected
		SELECT @@ROWCOUNT AS RowsAffected;
	END TRY
	BEGIN CATCH
		-- Return error information
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
GO

-- =============================================
-- Stored Procedure: UpdateSponsoringOrganizationDetails
-- Description: Updates sponsoring organization details based on the organization code
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateSponsoringOrganizationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateSponsoringOrganizationDetails]
GO

CREATE PROCEDURE [dbo].[UpdateSponsoringOrganizationDetails]
	@Sponsoring_Organisation_Code VARCHAR(50),
	@Sponsoring_Organisation_Name VARCHAR(200),
	@Sponsoring_Organisation_State VARCHAR(100),
	@Sponsoring_Address VARCHAR(500),
	@GST_NUMBER VARCHAR(50),
	@State_Code VARCHAR(10),
	@Modified_By VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		-- Update the sponsoring organization details
		UPDATE Organisation
		SET 
			Sponsoring_Organisation_Name = @Sponsoring_Organisation_Name,
			Sponsoring_Organisation_State = @Sponsoring_Organisation_State,
			Sponsoring_Address = @Sponsoring_Address,
			GST_NUMBER = @GST_NUMBER,
			State_Code = @State_Code,
			Modified_Date = GETDATE(),
			Modified_By = @Modified_By
		WHERE 
			Sponsoring_Organisation_Code = @Sponsoring_Organisation_Code;

		-- Return the number of rows affected
		SELECT @@ROWCOUNT AS RowsAffected;
	END TRY
	BEGIN CATCH
		-- Return error information
		SELECT 
			ERROR_NUMBER() AS ErrorNumber,
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
GO

-- =============================================
-- Test the stored procedures (optional - comment out in production)
-- =============================================
-- Test UpdatePayeeOrganizationDetails
/*
EXEC UpdatePayeeOrganizationDetails 
	@Payee_Organisation_Code = 'TEST001',
	@Payee_Organisation_Name = 'Test Payee Organization',
	@Payee_Organisation_State = 'Delhi',
	@Payee_Address = 'Test Address',
	@Location_Pincode = '110001',
	@GST_NUMBER = '07AAACH1234A1Z5',
	@State_Code = '07',
	@Modified_By = 'ADMIN';
*/

-- Test UpdateSponsoringOrganizationDetails
/*
EXEC UpdateSponsoringOrganizationDetails 
	@Sponsoring_Organisation_Code = 'SPON001',
	@Sponsoring_Organisation_Name = 'Test Sponsoring Organization',
	@Sponsoring_Organisation_State = 'Maharashtra',
	@Sponsoring_Address = 'Test Address',
	@GST_NUMBER = '27AAACH1234A1Z5',
	@State_Code = '27',
	@Modified_By = 'ADMIN';
*/

PRINT 'Update stored procedures created successfully!'
GO
