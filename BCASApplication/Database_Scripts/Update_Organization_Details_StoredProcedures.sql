-- ================================================
-- UPDATE STORED PROCEDURES FOR ORGANIZATION DETAILS
-- ================================================
-- These stored procedures need to be updated in your database
-- to filter by the logged-in user's organization code
-- ================================================

-- ================================================
-- 1. UPDATE GetPayeeOrganizationDetails
-- ================================================
-- This stored procedure now accepts a parameter to filter by Payee Organization Code
-- ================================================

USE [BCAS]
GO

-- Drop the existing procedure first
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPayeeOrganizationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPayeeOrganizationDetails]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create the updated stored procedure with parameter
CREATE PROCEDURE [dbo].[GetPayeeOrganizationDetails]
	@Payee_Organisation_Code VARCHAR(50) = NULL  -- Parameter to filter by organization code
AS
BEGIN
	SET NOCOUNT ON;

	-- Select payee organization details
	-- If @Payee_Organisation_Code is provided, filter by it
	-- Otherwise, return all records (for admin users)
	SELECT 
		Payee_Organisation_Name,
		Payee_Organisation_Code,
		STATES AS Payee_Organisation_State,
		Payee_address AS Payee_Address,
		Payee_Location_Pincode AS Location_Pincode,
		GSTIN,
		SCODE,
		Enter_By AS Entered_By,
		Enter_Date AS Entered_Date
	FROM [BCAS].[dbo].[Payee_Organisation_Master]
	WHERE 
		(@Payee_Organisation_Code IS NULL OR Payee_Organisation_Code = @Payee_Organisation_Code)
	ORDER BY Payee_Organisation_Name;

END
GO


-- ================================================
-- 2. UPDATE GetSponsoringOrganizationDetails
-- ================================================
-- This stored procedure now accepts a parameter to filter by Sponsoring Organization Code
-- ================================================

USE [BCAS]
GO

-- Drop the existing procedure first
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSponsoringOrganizationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSponsoringOrganizationDetails]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create the updated stored procedure with parameter
CREATE PROCEDURE [dbo].[GetSponsoringOrganizationDetails]
	@Organisation_Code VARCHAR(50) = NULL  -- Parameter to filter by organization code
AS
BEGIN
	SET NOCOUNT ON;

	-- Select sponsoring organization details
	-- If @Organisation_Code is provided, filter by it
	-- Otherwise, return all records (for admin users)
	SELECT 
		Organisation_Name AS Sponsoring_Organisation_Name,
		Organisation_Code AS Sponsoring_Organisation_Code,
		STATES AS Sponsoring_Organisation_State,
		Organisation_Address AS Sponsoring_Address,
		GSTIN,
		SCODE,
		Enter_by AS Entered_By,
		Enter_Date AS Entered_Date
	FROM [BCAS].[dbo].[Sponsoring_Organisation_Master]
	WHERE 
		(@Organisation_Code IS NULL OR Organisation_Code = @Organisation_Code)
	ORDER BY Organisation_Name;

END
GO


-- ================================================
-- TESTING THE STORED PROCEDURES
-- ================================================

-- Test 1: Get all payee organizations (pass NULL)
EXEC [dbo].[GetPayeeOrganizationDetails] @Payee_Organisation_Code = NULL;

-- Test 2: Get specific payee organization (replace 'YOUR_ORG_CODE' with actual code)
-- EXEC [dbo].[GetPayeeOrganizationDetails] @Payee_Organisation_Code = 'YOUR_ORG_CODE';

-- Test 3: Get all sponsoring organizations (pass NULL)
EXEC [dbo].[GetSponsoringOrganizationDetails] @Organisation_Code = NULL;

-- Test 4: Get specific sponsoring organization (replace 'YOUR_ORG_CODE' with actual code)
-- EXEC [dbo].[GetSponsoringOrganizationDetails] @Organisation_Code = 'YOUR_ORG_CODE';
