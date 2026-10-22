-- =============================================
-- CRITICAL CHECK: Are you in the right database?
-- =============================================

-- You showed me these tables exist:
-- MenuMaster (without TYP_ prefix)
-- RoleForm (without TYP_ prefix)

-- But the code uses:
-- TYP_MENUMASTER
-- TYP_USERS
-- TYP_ROLES

-- This means EITHER:
-- 1. There are TWO sets of tables (old and new schema)
-- 2. You're querying the wrong database
-- 3. The tables have synonyms

PRINT '========================================';  
PRINT 'Check 1: What database are you connected to?';
PRINT '========================================';
SELECT DB_NAME() AS [Current Database];

PRINT '';
PRINT '========================================';
PRINT 'Check 2: Do BOTH table sets exist?';
PRINT '========================================';

-- Check MenuMaster (your version)
IF OBJECT_ID('MenuMaster', 'U') IS NOT NULL
	PRINT 'MenuMaster EXISTS';
ELSE
	PRINT 'MenuMaster DOES NOT EXIST';

-- Check TYP_MENUMASTER (code version)
IF OBJECT_ID('TYP_MENUMASTER', 'U') IS NOT NULL
	PRINT 'TYP_MENUMASTER EXISTS';
ELSE
	PRINT 'TYP_MENUMASTER DOES NOT EXIST';

-- Check RoleForm (your version)
IF OBJECT_ID('RoleForm', 'U') IS NOT NULL
	PRINT 'RoleForm EXISTS';
ELSE
	PRINT 'RoleForm DOES NOT EXIST';

PRINT '';
PRINT '========================================';
PRINT 'Check 3: Which table has the data?';
PRINT '========================================';

-- Try both table names for the menu item
DECLARE @MenuMasterCount INT = 0;
DECLARE @TYPMenuMasterCount INT = 0;

IF OBJECT_ID('MenuMaster', 'U') IS NOT NULL
BEGIN
	SELECT @MenuMasterCount = COUNT(*) FROM MenuMaster WHERE MenuItemCode = 99;
	PRINT 'MenuMaster has ' + CAST(@MenuMasterCount AS VARCHAR) + ' records for MenuItem 99';
END

IF OBJECT_ID('TYP_MENUMASTER', 'U') IS NOT NULL
BEGIN
	SELECT @TYPMenuMasterCount = COUNT(*) FROM TYP_MENUMASTER WHERE MENUITEMCODE = 99;
	PRINT 'TYP_MENUMASTER has ' + CAST(@TYPMenuMasterCount AS VARCHAR) + ' records for MenuItem 99';
END

PRINT '';
PRINT '========================================';
PRINT 'CONCLUSION:';
PRINT '========================================';
IF @MenuMasterCount > 0 AND @TYPMenuMasterCount = 0
	PRINT 'You added the menu to MenuMaster, but the code reads from TYP_MENUMASTER!';
ELSE IF @MenuMasterCount = 0 AND @TYPMenuMasterCount > 0
	PRINT 'Menu item is in TYP_MENUMASTER only. This is correct.';
ELSE IF @MenuMasterCount > 0 AND @TYPMenuMasterCount > 0
	PRINT 'Menu item exists in BOTH tables.';
ELSE
	PRINT 'Menu item is missing from BOTH tables!';
