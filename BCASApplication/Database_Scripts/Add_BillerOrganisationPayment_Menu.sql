/*
===============================================================================
SQL SCRIPT TO ADD BillerOrganisationPayment.aspx TO MENU
===============================================================================
This script adds the BillerOrganisationPayment page to the menu system so PAYEE
users can see and access it.

PREREQUISITES:
- Ensure you're connected to the BCAS database
- Check that MenuItemCode and ParentMenuItem values are correct for your system
===============================================================================
*/

USE [BCAS]
GO

PRINT '==============================================================================='
PRINT 'STEP 1: Checking existing menu structure...'
PRINT '==============================================================================='

-- Check if the page already exists in MenuMaster
IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx')
BEGIN
	PRINT 'WARNING: BillerOrganisationPayment.aspx already exists in MenuMaster table.'
	PRINT 'Skipping MenuMaster insert.'
END
ELSE
BEGIN
	PRINT 'BillerOrganisationPayment.aspx not found in MenuMaster. Will add it.'

	-- Find next available MenuItemCode
	DECLARE @NextMenuCode INT
	SELECT @NextMenuCode = ISNULL(MAX(MenuItemCode), 0) + 1 FROM MenuMaster

	PRINT 'Next available MenuItemCode: ' + CAST(@NextMenuCode AS VARCHAR(10))

	-- Check what Payments parent menu code is
	DECLARE @PaymentsMenuCode INT
	SELECT @PaymentsMenuCode = MenuItemCode 
	FROM MenuMaster 
	WHERE MenuDescription LIKE '%Payment%' 
	  AND MenuLevel = 1
	  AND WhetherLeafMenu = 'N'

	IF @PaymentsMenuCode IS NULL
	BEGIN
		-- Try alternative payment menu names
		SELECT TOP 1 @PaymentsMenuCode = MenuItemCode 
		FROM MenuMaster 
		WHERE (MenuDescription LIKE '%Finance%' OR MenuDescription LIKE '%Fee%')
		  AND MenuLevel = 1
		  AND WhetherLeafMenu = 'N'
	END

	IF @PaymentsMenuCode IS NULL
	BEGIN
		PRINT 'WARNING: Could not find Payments parent menu. Using parent code 38 as default.'
		SET @PaymentsMenuCode = 38
	END
	ELSE
	BEGIN
		PRINT 'Found Payments parent menu code: ' + CAST(@PaymentsMenuCode AS VARCHAR(10))
	END

	PRINT ''
	PRINT '==============================================================================='
	PRINT 'STEP 2: Adding BillerOrganisationPayment to MenuMaster...'
	PRINT '==============================================================================='

	SET IDENTITY_INSERT [dbo].[MenuMaster] ON;

	INSERT INTO [dbo].[MenuMaster] 
	(
		[MenuItemCode],
		[MenuDescription],
		[MenuLevel],
		[WhetherLeafMenu],
		[ParentMenuItem],
		[FormName],
		[Enter_By],
		[Enter_Date]
	)
	VALUES 
	(
		@NextMenuCode,
		'Online Fee Payment',
		2,
		'Y',
		@PaymentsMenuCode,
		'~/Forms/DetailsForms/BillerOrganisationPayment.aspx',
		'admin',
		GETDATE()
	);

	SET IDENTITY_INSERT [dbo].[MenuMaster] OFF;

	PRINT 'SUCCESS: Added menu item with code ' + CAST(@NextMenuCode AS VARCHAR(10))
END

GO

PRINT ''
PRINT '==============================================================================='
PRINT 'STEP 3: Adding role permissions to RoleForm...'
PRINT '==============================================================================='

-- Check existing roles in the system
PRINT 'Existing roles that contain PAYEE:'
SELECT DISTINCT RoleName FROM RoleForm WHERE RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%'

-- Add permission for PAYEE role
IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' AND RoleName = 'PAYEE')
BEGIN
	PRINT 'Permission for PAYEE role already exists. Skipping.'
END
ELSE
BEGIN
	INSERT INTO [dbo].[RoleForm] 
	(
		[RoleName], 
		[FormName], 
		[Enter_By], 
		[Enter_Date]
	)
	VALUES 
	(
		'PAYEE',
		'~/Forms/DetailsForms/BillerOrganisationPayment.aspx',
		'admin', 
		GETDATE()
	);

	PRINT 'SUCCESS: Added PAYEE role permission'
END

-- Also add for ADMINISTRATOR role (common in BCAS system)
IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx' AND RoleName = 'ADMINISTRATOR')
BEGIN
	PRINT 'Permission for ADMINISTRATOR role already exists. Skipping.'
END
ELSE IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'ADMINISTRATOR')
BEGIN
	INSERT INTO [dbo].[RoleForm] 
	(
		[RoleName], 
		[FormName], 
		[Enter_By], 
		[Enter_Date]
	)
	VALUES 
	(
		'ADMINISTRATOR',
		'~/Forms/DetailsForms/BillerOrganisationPayment.aspx',
		'admin', 
		GETDATE()
	);

	PRINT 'SUCCESS: Added ADMINISTRATOR role permission'
END

GO

PRINT ''
PRINT '==============================================================================='
PRINT 'STEP 4: Verification'
PRINT '==============================================================================='

-- Verify MenuMaster entry
PRINT 'MenuMaster entry:'
SELECT MenuItemCode, MenuDescription, MenuLevel, ParentMenuItem, FormName, Enter_Date
FROM MenuMaster 
WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx'

PRINT ''
PRINT 'RoleForm entries:'
SELECT RoleName, FormName, Enter_Date
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/BillerOrganisationPayment.aspx'

PRINT ''
PRINT '==============================================================================='
PRINT 'SCRIPT COMPLETED SUCCESSFULLY!'
PRINT '==============================================================================='
PRINT ''
PRINT 'NEXT STEPS:'
PRINT '1. Logout from the BCAS application'
PRINT '2. Login again as a PAYEE user'
PRINT '3. The "Online Fee Payment" menu item should now appear under Payments menu'
PRINT ''
PRINT 'If the menu does not appear:'
PRINT '- Clear browser cache and cookies'
PRINT '- Verify the role name is exactly "PAYEE" (case-sensitive)'
PRINT '- Check IIS application pool has been recycled'
PRINT '==============================================================================='

GO
