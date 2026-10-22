-- =============================================
-- COMPLETE DIAGNOSTIC FOR PAYEE ACCESS ISSUE
-- Run this to understand what's preventing PAYEE access
-- =============================================

PRINT '========================================';
PRINT 'STEP 1: CHECK ASP.NET MEMBERSHIP ROLES';
PRINT '========================================';

-- Check aspnet_Roles table
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'aspnet_Roles')
BEGIN
	PRINT 'ASP.NET Roles in the system:';
	SELECT RoleId, ApplicationId, RoleName, LoweredRoleName, Description
	FROM aspnet_Roles
	WHERE LoweredRoleName LIKE '%payee%' OR LoweredRoleName LIKE '%admin%' OR LoweredRoleName LIKE '%finance%'
	ORDER BY RoleName;
END
ELSE
BEGIN
	PRINT '⚠ aspnet_Roles table not found - system may use custom role provider';
END

PRINT '';
PRINT '========================================';
PRINT 'STEP 2: CHECK WHAT PAYEE USERS HAVE ACCESS TO';
PRINT '========================================';

-- Find all forms that PAYEE role can access
SELECT RoleName, FormName
FROM RoleForm 
WHERE RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%' OR RoleName LIKE '%payee%'
ORDER BY RoleName, FormName;

PRINT '';
PRINT '========================================';
PRINT 'STEP 3: CHECK OUR TAXINVOICEUPLOAD PAGE

 PERMISSIONS';
PRINT '========================================';

-- Check permissions for TaxInvoiceUpload
IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'TaxInvoiceUpload page permissions found:';
	SELECT RoleName, FormName, Enter_By, Enter_Date
	FROM RoleForm 
	WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
	ORDER BY RoleName;
END
ELSE
BEGIN
	PRINT '⚠ NO PERMISSIONS FOUND for TaxInvoiceUpload.aspx!';
	PRINT '   This means the Add_TaxInvoiceUpload_Page_Permissions.sql script was not run or failed.';
END

PRINT '';
PRINT '========================================';
PRINT 'STEP 4: CHECK MENU ITEM';
PRINT '========================================';

-- Check if menu exists
IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'Menu item found:';
	SELECT MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName
	FROM MenuMaster 
	WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

	-- Check if parent menu exists
	DECLARE @ParentMenuId INT;
	SELECT @ParentMenuId = ParentMenuItem 
	FROM MenuMaster 
	WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

	PRINT '';
	PRINT 'Parent menu item:';
	SELECT MenuItemCode, MenuDescription, FormName
	FROM MenuMaster 
	WHERE MenuItemCode = @ParentMenuId;
END
ELSE
BEGIN
	PRINT '⚠ NO MENU ITEM FOUND for TaxInvoiceUpload.aspx!';
END

PRINT '';
PRINT '========================================';
PRINT 'STEP 5: COMPARE WITH WORKING PAYEE PAGE';
PRINT '========================================';

-- Check PayeeOrganisationPayment.aspx as reference
PRINT 'PayeeOrganisationPayment.aspx permissions (known working PAYEE page):';
SELECT RoleName, FormName
FROM RoleForm 
WHERE FormName LIKE '%PayeeOrganisation%'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'STEP 6: CHECK IF USER CAN ACCESS THE PAGE DIRECTLY';
PRINT '========================================';

-- This needs to be checked manually by the user
PRINT 'MANUAL TEST REQUIRED:';
PRINT '1. Login as PAYEE user';
PRINT '2. Try to access URL directly:';
PRINT '   https://yoursite/Forms/DetailsForms/TaxInvoiceUpload.aspx';
PRINT '3. Note what happens:';
PRINT '   - Redirected to Home? = Permission issue';
PRINT '   - Access Denied error? = Authorization issue';
PRINT '   - Page loads but menu missing? = Menu configuration issue';

PRINT '';
PRINT '========================================';
PRINT 'DIAGNOSIS SUMMARY';
PRINT '========================================';

DECLARE @HasRoleFormEntry BIT = 0;
DECLARE @HasMenuEntry BIT = 0;
DECLARE @RoleCount INT = 0;

-- Check RoleForm
IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
	SET @HasRoleFormEntry = 1;

-- Check MenuMaster
IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
	SET @HasMenuEntry = 1;

-- Count roles with access
SELECT @RoleCount = COUNT(*) 
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT 'Status Check:';
PRINT '-------------';
PRINT 'RoleForm entry exists: ' + CASE WHEN @HasRoleFormEntry = 1 THEN 'YES ✓' ELSE 'NO ✗' END;
PRINT 'MenuMaster entry exists: ' + CASE WHEN @HasMenuEntry = 1 THEN 'YES ✓' ELSE 'NO ✗' END;
PRINT 'Number of roles with access: ' + CAST(@RoleCount AS VARCHAR(10));

PRINT '';
IF @HasRoleFormEntry = 0
BEGIN
	PRINT '⚠ CRITICAL: No RoleForm entries found!';
	PRINT '   ACTION: Run Add_TaxInvoiceUpload_Page_Permissions.sql';
END

IF @HasMenuEntry = 0
BEGIN
	PRINT '⚠ WARNING: No MenuMaster entry found!';
	PRINT '   ACTION: Users can access directly but won''t see menu item';
END

IF @RoleCount < 3
BEGIN
	PRINT '⚠ WARNING: Less than 3 roles configured!';
	PRINT '   ACTION: Run Fix_PAYEE_Role_Variations.sql';
END

PRINT '';
PRINT '========================================';
PRINT 'RECOMMENDED ACTIONS';
PRINT '========================================';
PRINT '1. If RoleForm entries are missing, run:';
PRINT '   >>> Add_TaxInvoiceUpload_Page_Permissions.sql';
PRINT '';
PRINT '2. If RoleForm entries exist but PAYEE still can''t access, run:';
PRINT '   >>> Fix_PAYEE_Role_Variations.sql';
PRINT '';
PRINT '3. After fixing database:';
PRINT '   >>> Restart IIS';
PRINT '   >>> Clear browser cache';
PRINT '   >>> Re-login as PAYEE user';
PRINT '========================================';
