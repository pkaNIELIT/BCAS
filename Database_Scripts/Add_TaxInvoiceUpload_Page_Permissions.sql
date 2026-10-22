-- =============================================
-- Add Tax Invoice Upload Page Permissions
-- Grants access to: PAYEE, ADMIN/ADMINISTRATOR, and FINANCE roles
-- =============================================

PRINT '========================================';
PRINT 'Adding TaxInvoiceUpload page permissions';
PRINT '========================================';
PRINT '';

-- Step 1: Check if menu item 99 already exists (if using menu system)
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
		'Tax Invoice Upload/Download',
		2,
		'Y',
		38,  -- Parent menu item (adjust if needed)
		'~/Forms/DetailsForms/TaxInvoiceUpload.aspx',
		'admin',
		GETDATE()
	);

	SET IDENTITY_INSERT [dbo].[MenuMaster] OFF;

	PRINT '✓ Menu item 99 added successfully!';
END

PRINT '';
PRINT '========================================';

-- Step 2: Add PAYEE role permission
IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'PAYEE' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT '✓ PAYEE role permission already exists';
END
ELSE
BEGIN
	PRINT 'Adding PAYEE role permission...';

	INSERT INTO [dbo].[RoleForm] 
	([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES 
	('PAYEE', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

	PRINT '✓ PAYEE role permission added!';
END

PRINT '';
PRINT '========================================';

-- Step 3: Add ADMINISTRATOR role permission
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

-- Step 4: Add Admin role permission (if using 'Admin' instead of 'ADMINISTRATOR')
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

-- Step 5: Add FINANCE role permission
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

-- Step 6: Add Finance role permission (if using 'Finance' instead of 'FINANCE')
IF EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'Finance' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT '✓ Finance role permission already exists';
END
ELSE
BEGIN
	PRINT 'Adding Finance role permission...';

	INSERT INTO [dbo].[RoleForm] 
	([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES 
	('Finance', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

	PRINT '✓ Finance role permission added!';
END

PRINT '';
PRINT '========================================';

-- Step 7: Refresh the vw_formname view
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

-- Step 8: Recompile GetMenuData stored procedure
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

-- Verify menu item (if using menu system)
PRINT 'Menu item in MenuMaster:';
SELECT MenuItemCode, MenuDescription, ParentMenuItem, FormName 
FROM MenuMaster 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT 'Role permissions in RoleForm:';
SELECT RoleName, FormName, Enter_Date
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'NEXT STEPS:';
PRINT '========================================';
PRINT '1. RESTART IIS or recycle the application pool';
PRINT '2. Clear browser cache / logout and login again';
PRINT '3. Test with users from each role:';
PRINT '   - PAYEE: Should see download option only';
PRINT '   - ADMIN/ADMINISTRATOR: Should see upload and download';
PRINT '   - FINANCE: Should see upload and download';
PRINT '';
PRINT '4. If menu not showing, run:';
PRINT '   EXEC GetMenuData @USERNAME = ''your_username''';
PRINT '   (Check if MenuItemCode 99 appears)';
PRINT '========================================';
