-- =============================================
-- CHECK FOR VIEWS AND SYNONYMS
-- This will tell us how TYP_MENUMASTER maps to MenuMaster
-- =============================================

PRINT '========================================';
PRINT 'Step 1: Check for VIEWS';
PRINT '========================================';

SELECT 
	TABLE_NAME,
	VIEW_DEFINITION
FROM INFORMATION_SCHEMA.VIEWS
WHERE TABLE_NAME IN ('TYP_MENUMASTER', 'TYP_USERS', 'TYP_ROLES', 'TYP_USER_ROLES', 'vw_formname')
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'Step 2: Check for SYNONYMS';
PRINT '========================================';

SELECT 
	name AS SynonymName,
	base_object_name AS PointsTo
FROM sys.synonyms
WHERE name LIKE 'TYP_%'
ORDER BY name;

PRINT '';
PRINT '========================================';
PRINT 'Step 3: Check if menu item 99 exists in MenuMaster';
PRINT '========================================';

SELECT 
	MenuItemCode,
	MenuDescription,
	ParentMenuItem,
	FormName
FROM MenuMaster
WHERE MenuItemCode = 99;

PRINT '';
PRINT '========================================';
PRINT 'Step 4: Check if ADMINISTRATOR role has access';
PRINT '========================================';

SELECT 
	RoleName,
	FormName
FROM RoleForm
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'Step 5: Test GetMenuData stored procedure';
PRINT '========================================';

-- Check what GetMenuData uses
EXEC sp_helptext 'GetMenuData';

PRINT '';
PRINT '========================================';
PRINT 'Step 6: Check vw_formname view definition';
PRINT '========================================';

EXEC sp_helptext 'vw_formname';

PRINT '';
PRINT '========================================';
PRINT 'Step 7: Check what vw_formname contains';
PRINT '========================================';

SELECT TOP 10 * FROM vw_formname
ORDER BY username, formname;

PRINT '';
PRINT '========================================';
PRINT 'Step 8: Check for TaxInvoiceUpload in vw_formname';
PRINT '========================================';

SELECT * FROM vw_formname
WHERE formname LIKE '%TaxInvoice%';
