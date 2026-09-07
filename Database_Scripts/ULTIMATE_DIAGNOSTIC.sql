-- =============================================
-- ULTIMATE DIAGNOSTIC - Find Everything
-- =============================================

-- Part 1: What database are we in?
PRINT '========================================';
PRINT 'Current Database:';
PRINT '========================================';
SELECT DB_NAME() AS CurrentDatabase;

PRINT '';
PRINT '========================================';
PRINT 'Part 2: Find Users table (all schemas)';
PRINT '========================================';
SELECT 
	TABLE_SCHEMA,
	TABLE_NAME,
	TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%user%'
ORDER BY TABLE_SCHEMA, TABLE_NAME;

-- Part 3: Try to query the tables we know exist
PRINT '';
PRINT '========================================';
PRINT 'Part 3: Check MenuMaster (we know this exists)';
PRINT '========================================';
SELECT COUNT(*) AS MenuMasterCount FROM MenuMaster;
SELECT COUNT(*) AS MenuItem99Count FROM MenuMaster WHERE MenuItemCode = 99;

PRINT '';
PRINT '========================================';
PRINT 'Part 4: Check RoleForm (we know this exists)';
PRINT '========================================';
SELECT COUNT(*) AS RoleFormCount FROM RoleForm;
SELECT COUNT(*) AS TaxInvoiceCount 
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'Part 5: Check if TYP_USERS exists';
PRINT '========================================';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TYP_USERS')
BEGIN
	SELECT TOP 5 USER_ID, USER_NAME FROM TYP_USERS;
	PRINT 'TYP_USERS exists!';
END
ELSE
BEGIN
	PRINT 'TYP_USERS does NOT exist. Trying aspnet_Users...';
	IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'aspnet_Users')
	BEGIN
		SELECT TOP 5 * FROM aspnet_Users;
		PRINT 'Using aspnet_Users';
	END
	ELSE
	BEGIN
		PRINT 'Neither TYP_USERS nor aspnet_Users found.';
		PRINT 'Available tables:';
		SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME;
	END
END

PRINT '';
PRINT '========================================';
PRINT 'Part 6: Check vw_formname view';
PRINT '========================================';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_formname')
BEGIN
	PRINT 'vw_formname exists. Checking contents...';
	SELECT COUNT(*) AS TotalRecords FROM vw_formname;
	SELECT TOP 5 * FROM vw_formname ORDER BY username, formname;
END
ELSE
BEGIN
	PRINT 'vw_formname does NOT exist!';
END

PRINT '';
PRINT '========================================';
PRINT 'Part 7: Check TYP_ROLES and TYP_USER_ROLES';
PRINT '========================================';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TYP_ROLES')
BEGIN
	SELECT TOP 5 * FROM TYP_ROLES;
END
ELSE
BEGIN
	PRINT 'TYP_ROLES does not exist';
END

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TYP_USER_ROLES')
BEGIN
	SELECT TOP 5 * FROM TYP_USER_ROLES;
END
ELSE
BEGIN
	PRINT 'TYP_USER_ROLES does not exist';
END
