-- =============================================
-- DEFINITIVE FIX - Add Tax Invoice Upload Menu
-- Based on actual table structure: MenuMaster and RoleForm
-- =============================================

-- Step 1: Check if menu item 99 already exists
IF EXISTS (SELECT 1 FROM MenuMaster WHERE MenuItemCode = 99)
BEGIN
	PRINT '✓ Menu item 99 already exists in MenuMaster';
	SELECT MenuItemCode, MenuDescription, FormName FROM MenuMaster WHERE MenuItemCode = 99;
END
ELSE
BEGIN
	PRINT 'Adding menu item 99 to MenuMaster...';

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
		99,
		'Tax Invoice Upload',
		2,
		'Y',
		38,
		'~/Forms/DetailsForms/TaxInvoiceUpload.aspx',
		'admin',
		GETDATE()
	);

	SET IDENTITY_INSERT [dbo].[MenuMaster] OFF;

	PRINT '✓ Menu item 99 added successfully!';
END

PRINT '';
PRINT '========================================';

-- Step 2: Add ADMINISTRATOR role permission
IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'ADMINISTRATOR' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT '✓ ADMINISTRATOR role permission already exists';
END
ELSE
BEGIN
	PRINT 'Adding ADMINISTRATOR role permission...';

	INSERT INTO [dbo].[RoleForm] 
	([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES 
	('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

	PRINT '✓ ADMINISTRATOR role permission added!';
END

PRINT '';
PRINT '========================================';

-- Step 3: Add Admin role permission (if different)
IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'Admin' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT '✓ Admin role permission already exists';
END
ELSE
BEGIN
	PRINT 'Adding Admin role permission...';

	INSERT INTO [dbo].[RoleForm] 
	([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES 
	('Admin', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

	PRINT '✓ Admin role permission added!';
END

PRINT '';
PRINT '========================================';

-- Step 4: Add FINANCE role permission
IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'FINANCE' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT '✓ FINANCE role permission already exists';
END
ELSE
BEGIN
	PRINT 'Adding FINANCE role permission...';

	INSERT INTO [dbo].[RoleForm] 
	([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES 
	('FINANCE', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

	PRINT '✓ FINANCE role permission added!';
END

PRINT '';
PRINT '========================================';

-- Step 5: Refresh the vw_formname view
BEGIN TRY
	EXEC sp_refreshview 'vw_formname';
	PRINT '✓ View vw_formname refreshed successfully!';
END TRY
BEGIN CATCH
	PRINT '⚠ Could not refresh vw_formname view';
	PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

PRINT '';
PRINT '========================================';

-- Step 6: Recompile GetMenuData stored procedure
BEGIN TRY
	EXEC sp_recompile 'GetMenuData';
	PRINT '✓ Stored procedure GetMenuData recompiled successfully!';
END TRY
BEGIN CATCH
	PRINT '⚠ Could not recompile GetMenuData';
	PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

PRINT '';
PRINT '========================================';
PRINT 'VERIFICATION';
PRINT '========================================';

-- Verify menu item
PRINT 'Menu item in MenuMaster:';
SELECT MenuItemCode, MenuDescription, ParentMenuItem, FormName 
FROM MenuMaster 
WHERE MenuItemCode = 99;

PRINT '';
PRINT 'Role permissions:';
SELECT RoleName, FormName 
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'NEXT STEPS:';
PRINT '========================================';
PRINT '1. Run: EXEC GetMenuData @USERNAME = ''admin''';
PRINT '   (Look for MenuItemCode = 99)';
PRINT '';
PRINT '2. If you see it: RESTART IIS and clear browser cache';
PRINT '3. If you DON''T see it: Run FINAL_DIAGNOSTIC_Check_Views.sql';
PRINT '========================================';
