-- =============================================
-- DEBUG: Check TaxInvoiceUpload page permissions
-- =============================================

PRINT '========================================';
PRINT 'DIAGNOSTIC CHECK FOR TAXINVOICEUPLOAD';
PRINT '========================================';
PRINT '';

-- 1. Check what roles exist in the system
PRINT '1. ALL ROLES IN THE SYSTEM:';
PRINT '----------------------------';
SELECT DISTINCT RoleName 
FROM RoleForm 
ORDER BY RoleName;

PRINT '';
PRINT '========================================';

-- 2. Check if the page exists in RoleForm
PRINT '2. PERMISSIONS FOR TAXINVOICEUPLOAD PAGE:';
PRINT '------------------------------------------';
SELECT RoleName, FormName, Enter_By, Enter_Date
FROM RoleForm 
WHERE FormName LIKE '%TaxInvoiceUpload%'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';

-- 3. Check if menu item exists
PRINT '3. MENU ITEM FOR TAXINVOICEUPLOAD:';
PRINT '-----------------------------------';
SELECT MenuItemCode, MenuDescription, MenuLevel, ParentMenuItem, FormName
FROM MenuMaster 
WHERE FormName LIKE '%TaxInvoiceUpload%';

PRINT '';
PRINT '========================================';

-- 4. Check what other pages PAYEE role can access
PRINT '4. ALL PAGES THAT PAYEE ROLE CAN ACCESS:';
PRINT '-----------------------------------------';
SELECT FormName, Enter_Date
FROM RoleForm 
WHERE RoleName LIKE '%PAYEE%'
ORDER BY FormName;

PRINT '';
PRINT '========================================';

-- 5. Check aspnet_Roles table (if using ASP.NET membership)
PRINT '5. CHECKING ASP.NET ROLES TABLE:';
PRINT '---------------------------------';
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'aspnet_Roles')
BEGIN
	SELECT RoleName, LoweredRoleName 
	FROM aspnet_Roles
	ORDER BY RoleName;
END
ELSE
BEGIN
	PRINT '⚠ aspnet_Roles table does not exist';
END

PRINT '';
PRINT '========================================';

-- 6. Check if there's a different PAYEE role name
PRINT '6. SEARCHING FOR ANY PAYEE-RELATED ROLES:';
PRINT '------------------------------------------';
SELECT DISTINCT RoleName 
FROM RoleForm 
WHERE RoleName LIKE '%PAY%'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';

-- 7. Check parent menu item 38 exists
PRINT '7. CHECK PARENT MENU ITEM (38):';
PRINT '--------------------------------';
SELECT MenuItemCode, MenuDescription, FormName
FROM MenuMaster 
WHERE MenuItemCode = 38;

PRINT '';
PRINT '========================================';
PRINT 'NEXT STEPS:';
PRINT '========================================';
PRINT '1. Review the output above';
PRINT '2. Check the exact role name for PAYEE users';
PRINT '3. If role name is different, you need to add that specific role';
PRINT '4. If parent menu 38 does not exist, change ParentMenuItem value';
PRINT '========================================';
