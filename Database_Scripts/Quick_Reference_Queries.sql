-- =============================================
-- Quick Reference SQL Queries
-- For: Tax Invoice Upload Menu Configuration
-- =============================================

-- =============================================
-- 1. DISCOVERY QUERIES
-- =============================================

-- Find all top-level menu items (to identify Payment section)
SELECT 
	MENUITEMCODE,
	MENUDESCRIPTION,
	MENULEVEL,
	WHETHERLEAFMENU,
	FORMNAME
FROM TYP_MENUMASTER
WHERE PARENTMENUITEM IS NULL
ORDER BY MENUITEMCODE;

-- Find Payment section and its sub-items
SELECT 
	M1.MENUITEMCODE AS ParentCode,
	M1.MENUDESCRIPTION AS ParentMenu,
	M2.MENUITEMCODE AS SubMenuCode,
	M2.MENUDESCRIPTION AS SubMenu,
	M2.FORMNAME
FROM TYP_MENUMASTER M1
LEFT JOIN TYP_MENUMASTER M2 ON M1.MENUITEMCODE = M2.PARENTMENUITEM
WHERE M1.MENUDESCRIPTION LIKE '%Payment%'
ORDER BY M1.MENUITEMCODE, M2.MENUITEMCODE;

-- Find next available menu item code
SELECT MAX(MENUITEMCODE) + 1 AS NextAvailableCode
FROM TYP_MENUMASTER;

-- =============================================
-- 2. ROLE QUERIES
-- =============================================

-- List all roles
SELECT * FROM TYP_ROLES
ORDER BY ROLE_NAME;

-- Find Finance/Admin roles
SELECT * FROM TYP_ROLES
WHERE ROLE_NAME LIKE '%Finance%' 
   OR ROLE_NAME LIKE '%Admin%'
   OR ROLE_NAME LIKE '%Financial%';

-- Check what menus a specific role has access to
DECLARE @RoleID INT = 1; -- Change to target role ID
SELECT 
	R.ROLE_NAME,
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.FORMNAME
FROM TYP_ROLES R
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE R.ROLE_ID = @RoleID
ORDER BY M.MENUITEMCODE;

-- =============================================
-- 3. USER ACCESS QUERIES
-- =============================================

-- Find users with specific role
DECLARE @RoleName VARCHAR(50) = 'Finance'; -- Change as needed
SELECT 
	U.USER_ID,
	U.USER_NAME,
	R.ROLE_NAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
WHERE R.ROLE_NAME LIKE '%' + @RoleName + '%';

-- Check what menus a specific user can access
DECLARE @UserName VARCHAR(50) = 'test_user'; -- Change to target username
SELECT 
	U.USER_NAME,
	R.ROLE_NAME,
	M.MENUDESCRIPTION,
	M.FORMNAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE U.USER_NAME = @UserName
ORDER BY M.MENULEVEL, M.MENUITEMCODE;

-- =============================================
-- 4. VERIFICATION QUERIES (After Adding Menu)
-- =============================================

-- Check if Tax Invoice Upload menu was added
SELECT * FROM TYP_MENUMASTER
WHERE FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx'
   OR MENUDESCRIPTION = 'Tax Invoice Upload';

-- Verify menu hierarchy for Tax Invoice Upload
SELECT 
	P.MENUDESCRIPTION AS ParentMenu,
	C.MENUDESCRIPTION AS ChildMenu,
	C.FORMNAME,
	C.MENULEVEL,
	C.WHETHERLEAFMENU
FROM TYP_MENUMASTER C
LEFT JOIN TYP_MENUMASTER P ON C.PARENTMENUITEM = P.MENUITEMCODE
WHERE C.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- Check which roles have access to Tax Invoice Upload
SELECT 
	R.ROLE_ID,
	R.ROLE_NAME,
	M.MENUDESCRIPTION,
	M.FORMNAME
FROM TYP_ROLES R
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- Check which users can access Tax Invoice Upload
SELECT 
	U.USER_ID,
	U.USER_NAME,
	R.ROLE_NAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx'
ORDER BY U.USER_NAME;

-- =============================================
-- 5. COMPLETE MENU TREE VIEW
-- =============================================

-- View entire menu structure (3 levels deep)
WITH MenuHierarchy AS (
	-- Level 1 (Root)
	SELECT 
		MENUITEMCODE,
		MENUDESCRIPTION,
		FORMNAME,
		MENULEVEL,
		PARENTMENUITEM,
		CAST(MENUDESCRIPTION AS VARCHAR(500)) AS MenuPath,
		CAST(MENUITEMCODE AS VARCHAR(100)) AS CodePath
	FROM TYP_MENUMASTER
	WHERE PARENTMENUITEM IS NULL

	UNION ALL

	-- Recursive: Child items
	SELECT 
		M.MENUITEMCODE,
		M.MENUDESCRIPTION,
		M.FORMNAME,
		M.MENULEVEL,
		M.PARENTMENUITEM,
		CAST(H.MenuPath + ' > ' + M.MENUDESCRIPTION AS VARCHAR(500)),
		CAST(H.CodePath + '.' + CAST(M.MENUITEMCODE AS VARCHAR(10)) AS VARCHAR(100))
	FROM TYP_MENUMASTER M
	INNER JOIN MenuHierarchy H ON M.PARENTMENUITEM = H.MENUITEMCODE
)
SELECT 
	MENUITEMCODE,
	MENULEVEL,
	MenuPath,
	FORMNAME,
	CodePath
FROM MenuHierarchy
ORDER BY CodePath
OPTION (MAXRECURSION 10);

-- =============================================
-- 6. STORED PROCEDURE REFERENCE
-- =============================================

-- View GetMenuData stored procedure definition
EXEC sp_helptext 'GetMenuData';

-- Test GetMenuData for specific user
DECLARE @TestUserName VARCHAR(50) = 'test_user'; -- Change as needed
EXEC GetMenuData @USERNAME = @TestUserName;

-- =============================================
-- 7. MAINTENANCE QUERIES
-- =============================================

-- Find duplicate menu items
SELECT 
	FORMNAME,
	COUNT(*) AS DuplicateCount
FROM TYP_MENUMASTER
WHERE FORMNAME IS NOT NULL
GROUP BY FORMNAME
HAVING COUNT(*) > 1;

-- Find orphaned menu items (parent doesn't exist)
SELECT 
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.PARENTMENUITEM
FROM TYP_MENUMASTER M
LEFT JOIN TYP_MENUMASTER P ON M.PARENTMENUITEM = P.MENUITEMCODE
WHERE M.PARENTMENUITEM IS NOT NULL
  AND P.MENUITEMCODE IS NULL;

-- Find menu items with no role access
SELECT 
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.FORMNAME
FROM TYP_MENUMASTER M
LEFT JOIN TYP_USER_ROLE_MENU_MASTER RM ON M.MENUITEMCODE = RM.MENUITEMCODE
WHERE RM.MENUITEMCODE IS NULL
  AND M.FORMNAME IS NOT NULL;

-- =============================================
-- 8. CLEANUP QUERIES (CAUTION!)
-- =============================================

-- Remove a menu item (CAUTION: Verify before uncommenting)
/*
DECLARE @MenuCodeToDelete INT = 999; -- Change to actual code

-- First remove role access
DELETE FROM TYP_USER_ROLE_MENU_MASTER
WHERE MENUITEMCODE = @MenuCodeToDelete;

-- Then remove menu item
DELETE FROM TYP_MENUMASTER
WHERE MENUITEMCODE = @MenuCodeToDelete;

PRINT 'Menu item removed: ' + CAST(@MenuCodeToDelete AS VARCHAR(10));
*/
