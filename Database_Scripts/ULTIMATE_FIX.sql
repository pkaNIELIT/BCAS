-- =============================================
-- ULTIMATE FIX: Tax Invoice Upload Menu Not Showing
-- The stored procedure GetMenuData uses vw_formname view
-- =============================================

-- DIAGNOSIS: Run this first to understand the problem
PRINT '========================================';
PRINT 'DIAGNOSTIC: Checking vw_formname view';
PRINT '========================================';

-- Check if vw_formname has the new form for admin user
SELECT COUNT(*) AS 'Tax Invoice Upload in vw_formname'
FROM vw_formname
WHERE username = 'admin'
  AND formname = 'TaxInvoiceUpload.aspx';
-- If this returns 0, the view is not picking up the new form

PRINT '';
PRINT 'Checking what the view definition looks like...';
EXEC sp_helptext 'vw_formname';

-- =============================================
-- MOST LIKELY FIX: Refresh or rebuild the view
-- =============================================

-- If vw_formname is a materialized/indexed view, refresh it:
-- EXEC sp_refreshview 'vw_formname';

-- OR if the view has schema binding issues, try:
-- EXEC sp_recompile 'vw_formname';

-- =============================================
-- ALTERNATIVE FIX: Check USER-ROLE-FORM chain
-- =============================================

-- Verify the complete chain: User -> Role -> Form
PRINT '';
PRINT '========================================';
PRINT 'Checking User -> Role -> Form chain';
PRINT '========================================';

-- Step 1: What roles does 'admin' user have?
SELECT 
	U.USER_ID,
	U.USER_NAME,
	R.ROLE_ID,
	R.ROLE_NAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
WHERE U.USER_NAME = 'admin';

-- Step 2: What forms does ADMINISTRATOR role have access to?
SELECT FormName
FROM RoleForm
WHERE RoleName = 'ADMINISTRATOR'
  AND FormName LIKE '%TaxInvoice%';

-- Step 3: Test the complete query that vw_formname SHOULD be doing
-- (This is a guess - adjust based on the actual view definition you see above)
SELECT DISTINCT
	U.USER_NAME as username,
	SUBSTRING(RF.FormName,
		LEN(RF.FormName) - 
		CASE WHEN CHARINDEX('/', REVERSE(RF.FormName)) = 0 
			THEN LEN(RF.FormName) - 1 
			ELSE CHARINDEX('/', REVERSE(RF.FormName)) - 2 
		END,
		LEN(RF.FormName)) AS formname
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
INNER JOIN RoleForm RF ON R.ROLE_NAME = RF.RoleName
WHERE U.USER_NAME = 'admin'
  AND RF.FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- =============================================
-- FINAL TEST: Run GetMenuData
-- =============================================
PRINT '';
PRINT '========================================';
PRINT 'Testing GetMenuData stored procedure';
PRINT '========================================';

EXEC GetMenuData @USERNAME = 'admin';

-- Look for MenuItemCode = 99 in the results
-- If it's there: The database is correct, restart IIS/browser
-- If it's NOT there: The view or user-role-form chain is broken
