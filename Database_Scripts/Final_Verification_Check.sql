-- =============================================
-- FINAL VERIFICATION: Check if everything is configured correctly
-- =============================================

PRINT '========================================';
PRINT 'FINAL VERIFICATION CHECKLIST';
PRINT '========================================';
PRINT '';

-- 1. Check RoleForm permissions
PRINT '1. RoleForm Permissions:';
PRINT '-------------------------';
IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	SELECT RoleName, FormName, Enter_Date
	FROM RoleForm 
	WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
	ORDER BY RoleName;

	DECLARE @RoleCount INT;
	SELECT @RoleCount = COUNT(*) FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';
	PRINT '';
	PRINT 'Total roles with access: ' + CAST(@RoleCount AS VARCHAR(10));

	IF @RoleCount >= 3
		PRINT '✓ GOOD: Multiple roles configured';
	ELSE
		PRINT '⚠ WARNING: Only ' + CAST(@RoleCount AS VARCHAR(10)) + ' role(s) configured - expected at least 3 (PAYEE, ADMIN, FINANCE)';
END
ELSE
BEGIN
	PRINT '✗ CRITICAL ERROR: No permissions found!';
	PRINT '  ACTION: Run Add_TaxInvoiceUpload_Page_Permissions.sql';
END

PRINT '';
PRINT '========================================';

-- 2. Check MenuMaster entry
PRINT '2. MenuMaster Entry:';
PRINT '---------------------';
IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	SELECT MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName
	FROM MenuMaster 
	WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';
	PRINT '✓ Menu item exists';
END
ELSE
BEGIN
	PRINT '⚠ WARNING: No menu item found';
	PRINT '  IMPACT: Page accessible directly but won''t appear in navigation menu';
END

PRINT '';
PRINT '========================================';

-- 3. Compare with known PAYEE pages
PRINT '3. Comparing PAYEE Role Access:';
PRINT '--------------------------------';
PRINT 'PAYEE can access these pages:';
SELECT FormName 
FROM RoleForm 
WHERE RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%'
ORDER BY FormName;

-- Check if TaxInvoiceUpload is in the list
IF EXISTS (
	SELECT 1 FROM RoleForm 
	WHERE (RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%')
	AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
)
	PRINT '✓ TaxInvoiceUpload.aspx is in PAYEE access list';
ELSE
	PRINT '✗ TaxInvoiceUpload.aspx is NOT in PAYEE access list';

PRINT '';
PRINT '========================================';

-- 4. Check if exact role names match
PRINT '4. Role Name Matching Check:';
PRINT '-----------------------------';
PRINT 'Available PAYEE-related roles in RoleForm:';
SELECT DISTINCT RoleName 
FROM RoleForm 
WHERE RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%' OR RoleName LIKE '%payee%'
ORDER BY RoleName;

PRINT '';
PRINT 'TaxInvoiceUpload has permissions for these roles:';
SELECT RoleName 
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
AND (RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%' OR RoleName LIKE '%payee%')
ORDER BY RoleName;

PRINT '';
PRINT '========================================';

-- 5. Test query to simulate what the application does
PRINT '5. Simulating Application Query:';
PRINT '---------------------------------';
PRINT 'If a user has role "PAYEE", can they access TaxInvoiceUpload?';

IF EXISTS (
	SELECT 1 FROM RoleForm 
	WHERE RoleName = 'PAYEE' 
	AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
)
	PRINT '✓ YES - User with "PAYEE" role can access';
ELSE
	PRINT '✗ NO - Need to add "PAYEE" role permission';

PRINT '';
PRINT '========================================';
PRINT 'SUMMARY';
PRINT '========================================';

DECLARE @HasPermission BIT = 0;
DECLARE @HasMenu BIT = 0;
DECLARE @HasPayeeAccess BIT = 0;

IF EXISTS (SELECT 1 FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
	SET @HasPermission = 1;

IF EXISTS (SELECT 1 FROM MenuMaster WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
	SET @HasMenu = 1;

IF EXISTS (
	SELECT 1 FROM RoleForm 
	WHERE (RoleName LIKE '%PAYEE%' OR RoleName LIKE '%Payee%')
	AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
)
	SET @HasPayeeAccess = 1;

PRINT '';
PRINT 'Configuration Status:';
PRINT '  RoleForm configured: ' + CASE WHEN @HasPermission = 1 THEN '✓ YES' ELSE '✗ NO' END;
PRINT '  MenuMaster configured: ' + CASE WHEN @HasMenu = 1 THEN '✓ YES' ELSE  '⚠ NO (optional)' END;
PRINT '  PAYEE has access: ' + CASE WHEN @HasPayeeAccess = 1 THEN '✓ YES' ELSE '✗ NO (PROBLEM!)' END;

PRINT '';
IF @HasPermission = 1 AND @HasPayeeAccess = 1
BEGIN
	PRINT '========================================';
	PRINT '✓✓✓ DATABASE CONFIGURATION IS CORRECT ✓✓✓';
	PRINT '========================================';
	PRINT '';
	PRINT 'If PAYEE still cannot access, the issue is likely:';
	PRINT '1. IIS not restarted after database changes';
	PRINT '2. Browser cache not cleared';
	PRINT '3. User not actually in PAYEE role';
	PRINT '';
	PRINT 'ACTIONS TO TAKE:';
	PRINT '----------------';
	PRINT '1. iisreset (PowerShell as Administrator)';
	PRINT '2. Clear browser cache (Ctrl+Shift+Delete)';
	PRINT '3. Logout and login again';
	PRINT '4. Test direct URL access:';
	PRINT '   https://yoursite/Forms/DetailsForms/TaxInvoiceUpload.aspx';
END
ELSE
BEGIN
	PRINT '========================================';
	PRINT '✗✗✗ CONFIGURATION INCOMPLETE ✗✗✗';
	PRINT '========================================';
	PRINT '';
	IF @HasPermission = 0
		PRINT '⚠ Run: Add_TaxInvoiceUpload_Page_Permissions.sql';
	IF @HasPayeeAccess = 0 AND @HasPermission = 1
		PRINT '⚠ Run: QUICK_FIX_All_Roles.sql';
END

PRINT '';
PRINT '========================================';
