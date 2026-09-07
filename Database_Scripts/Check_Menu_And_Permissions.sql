-- =============================================
-- Check Current User Permissions and Menu Access
-- =============================================

PRINT 'Checking menu system and user permissions...';
PRINT '';

-- Check if TYP_MENUMASTER or MenuMaster table exists
PRINT '==============================================';
PRINT '1. Checking which menu table exists';
PRINT '==============================================';

IF OBJECT_ID('TYP_MENUMASTER', 'U') IS NOT NULL
	PRINT '✓ TYP_MENUMASTER exists';
ELSE
	PRINT '✗ TYP_MENUMASTER does NOT exist';

IF OBJECT_ID('MenuMaster', 'U') IS NOT NULL
	PRINT '✓ MenuMaster exists';
ELSE
	PRINT '✗ MenuMaster does NOT exist';

PRINT '';

-- Check existing Payments menu items
PRINT '==============================================';
PRINT '2. Current Payment Menu Items';
PRINT '==============================================';

SELECT 
	MENUITEMCODE AS [Code],
	MENUDESCRIPTION AS [Menu Item],
	MENULEVEL AS [Level],
	WHETHERLEAFMENU AS [IsLeaf],
	PARENTMENUITEM AS [Parent],
	FORMNAME AS [Page]
FROM TYP_MENUMASTER
WHERE MENUDESCRIPTION LIKE '%Payment%' 
   OR PARENTMENUITEM IN (SELECT MENUITEMCODE FROM TYP_MENUMASTER WHERE MENUDESCRIPTION LIKE '%Payment%')
ORDER BY PARENTMENUITEM, MENUITEMCODE;

PRINT '';

-- Check if the Signed Tax Invoice Download page is already in menu
PRINT '==============================================';
PRINT '3. Is Signed Tax Invoice Download in menu?';
PRINT '==============================================';

IF EXISTS (SELECT 1 FROM TYP_MENUMASTER WHERE FORMNAME LIKE '%Signed_TaxInvoice_Download_Payee%')
	SELECT 
		'YES - Already exists' AS Status,
		MENUITEMCODE,
		MENUDESCRIPTION,
		FORMNAME
	FROM TYP_MENUMASTER 
	WHERE FORMNAME LIKE '%Signed_TaxInvoice_Download_Payee%';
ELSE
	SELECT 'NO - Not found in menu system' AS Status;

PRINT '';

-- Check role permissions table
PRINT '==============================================';
PRINT '4. Checking Role Permission Tables';
PRINT '==============================================';

IF OBJECT_ID('RoleForm', 'U') IS NOT NULL
BEGIN
	PRINT '✓ RoleForm table exists';
	PRINT 'Sample permissions:';
	SELECT TOP 5 
		RoleName,
		FormName
	FROM RoleForm
	ORDER BY RoleName;
END
ELSE
	PRINT '✗ RoleForm table does NOT exist';

IF OBJECT_ID('TYP_ROLEMASTER', 'U') IS NOT NULL
	PRINT '✓ TYP_ROLEMASTER table exists';
ELSE
	PRINT '✗ TYP_ROLEMASTER table does NOT exist';

PRINT '';
PRINT 'Diagnostic check complete!';
