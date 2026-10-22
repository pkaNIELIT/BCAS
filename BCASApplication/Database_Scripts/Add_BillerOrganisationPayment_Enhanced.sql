/*
===============================================================================
ENHANCED SQL SCRIPT - Add BillerOrganisationPayment for Multiple Roles
===============================================================================
This script adds BillerOrganisationPayment.aspx to the menu for multiple roles
including PAYEE, SPONSORING, and ADMINISTRATOR roles.
===============================================================================
*/

USE [BCAS]
GO

PRINT '==============================================================================='
PRINT 'ENHANCED SCRIPT - Adding BillerOrganisationPayment for Multiple Roles'
PRINT '==============================================================================='

-- First, let's check what the actual role names are in the system
PRINT ''
PRINT 'Current roles in the system:'
SELECT DISTINCT RoleName FROM aspnet_Roles ORDER BY RoleName
PRINT ''

PRINT '==============================================================================='
PRINT 'STEP 1: Checking and Adding to MenuMaster...'
PRINT '==============================================================================='

-- Check if already exists
IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx')
BEGIN
	PRINT 'BillerOrganisationPayment.aspx already exists in MenuMaster.'

	-- Show existing entry
	SELECT 'Existing Entry:' as Status,
		   MenuItemCode, MenuDescription, ParentMenuItem, FormName
	FROM MenuMaster 
	WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx'
END
ELSE
BEGIN
	-- Find next MenuItemCode
	DECLARE @NextMenuCode INT
	SELECT @NextMenuCode = ISNULL(MAX(MenuItemCode), 0) + 1 FROM MenuMaster

	-- Find Payments parent menu
	DECLARE @PaymentMenuCode INT
	SELECT TOP 1 @PaymentMenuCode = MenuItemCode
	FROM MenuMaster
	WHERE (MenuDescription LIKE '%Payment%' OR MenuDescription LIKE '%Finance%')
	  AND MenuLevel = 1
	  AND WhetherLeafMenu = 'N'

	IF @PaymentMenuCode IS NULL
	BEGIN
		SET @PaymentMenuCode = 38  -- Default fallback
		PRINT 'WARNING: Using default parent menu code 38'
	END
	ELSE
	BEGIN
		PRINT 'Using parent menu code: ' + CAST(@PaymentMenuCode AS VARCHAR(10))
	END

	-- Insert into MenuMaster
	SET IDENTITY_INSERT [dbo].[MenuMaster] ON;

	INSERT INTO [dbo].[MenuMaster] 
	(MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName, Enter_By, Enter_Date)
	VALUES 
	(@NextMenuCode, 'Online Fee Payment', 2, 'Y', @PaymentMenuCode, 
	 '~/Forms/DetailsForms/BillerOrganisationPayment.aspx', 'admin', GETDATE());

	SET IDENTITY_INSERT [dbo].[MenuMaster] OFF;

	PRINT 'SUCCESS: Added to MenuMaster with code ' + CAST(@NextMenuCode AS VARCHAR(10))
END

GO

PRINT ''
PRINT '==============================================================================='
PRINT 'STEP 2: Adding Role Permissions...'
PRINT '==============================================================================='

-- Add for PAYEE role
IF NOT EXISTS (SELECT 1 FROM RoleForm 
			   WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' 
			   AND RoleName = 'PAYEE')
BEGIN
	INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
	VALUES ('PAYEE', '~/Forms/DetailsForms/BillerOrganisationPayment.aspx', 'admin', GETDATE());
	PRINT 'Added permission for PAYEE role'
END
ELSE
BEGIN
	PRINT 'PAYEE role permission already exists'
END

-- Add for SPONSORING role
IF NOT EXISTS (SELECT 1 FROM RoleForm 
			   WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' 
			   AND RoleName = 'SPONSORING')
BEGIN
	IF EXISTS (SELECT 1 FROM aspnet_Roles WHERE RoleName = 'SPONSORING')
	BEGIN
		INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
		VALUES ('SPONSORING', '~/Forms/DetailsForms/BillerOrganisationPayment.aspx', 'admin', GETDATE());
		PRINT 'Added permission for SPONSORING role'
	END
	ELSE
	BEGIN
		PRINT 'SPONSORING role does not exist in system'
	END
END
ELSE
BEGIN
	PRINT 'SPONSORING role permission already exists'
END

-- Add for ADMINISTRATOR role
IF NOT EXISTS (SELECT 1 FROM RoleForm 
			   WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' 
			   AND RoleName = 'ADMINISTRATOR')
BEGIN
	IF EXISTS (SELECT 1 FROM aspnet_Roles WHERE RoleName = 'ADMINISTRATOR')
	BEGIN
		INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
		VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/BillerOrganisationPayment.aspx', 'admin', GETDATE());
		PRINT 'Added permission for ADMINISTRATOR role'
	END
	ELSE
	BEGIN
		PRINT 'ADMINISTRATOR role does not exist in system'
	END
END
ELSE
BEGIN
	PRINT 'ADMINISTRATOR role permission already exists'
END

-- Check for other payment-related role variations
DECLARE @RoleName VARCHAR(50)
DECLARE role_cursor CURSOR FOR
SELECT DISTINCT RoleName 
FROM RoleForm 
WHERE FormName LIKE '%CourseWisePayment%' 
  AND RoleName NOT IN ('PAYEE', 'SPONSORING', 'ADMINISTRATOR')

OPEN role_cursor
FETCH NEXT FROM role_cursor INTO @RoleName

WHILE @@FETCH_STATUS = 0
BEGIN
	IF NOT EXISTS (SELECT 1 FROM RoleForm 
				   WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' 
				   AND RoleName = @RoleName)
	BEGIN
		INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
		VALUES (@RoleName, '~/Forms/DetailsForms/BillerOrganisationPayment.aspx', 'admin', GETDATE());
		PRINT 'Added permission for ' + @RoleName + ' role'
	END

	FETCH NEXT FROM role_cursor INTO @RoleName
END

CLOSE role_cursor
DEALLOCATE role_cursor

GO

PRINT ''
PRINT '==============================================================================='
PRINT 'STEP 3: Verification'
PRINT '==============================================================================='

SELECT 'MenuMaster Entry:' as CheckType
SELECT MenuItemCode, MenuDescription, ParentMenuItem, FormName
FROM MenuMaster 
WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx'

PRINT ''
SELECT 'Role Permissions:' as CheckType
SELECT RoleName, FormName
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx'
ORDER BY RoleName

PRINT ''
PRINT '==============================================================================='
PRINT 'SCRIPT COMPLETED!'
PRINT '==============================================================================='
PRINT ''
PRINT 'IMPORTANT: You must now:'
PRINT '1. LOGOUT from the application'
PRINT '2. Close ALL browser windows'
PRINT '3. Clear browser cache (Ctrl+Shift+Delete)'
PRINT '4. Restart IIS or the development server'
PRINT '5. LOGIN again'
PRINT ''
PRINT 'The menu item should now appear under the Payments menu'
PRINT '==============================================================================='

GO
