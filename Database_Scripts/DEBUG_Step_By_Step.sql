-- =============================================
-- COMPREHENSIVE DEBUG: Why Tax Invoice Upload Still Not Showing
-- =============================================

PRINT '========================================';
PRINT 'STEP 1: Check if menu item exists';
PRINT '========================================';
SELECT * FROM MenuMaster WHERE MenuItemCode = 99;

PRINT '';
PRINT '========================================';
PRINT 'STEP 2: Check role permissions';
PRINT '========================================';
SELECT * FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'STEP 3: What USER_NAME are you actually logged in as?';
PRINT '========================================';
-- Check all users in the system
SELECT USER_ID, USER_NAME FROM TYP_USERS ORDER BY USER_NAME;
-- Is it 'admin', 'Admin', 'ADMIN', or something else?

PRINT '';
PRINT '========================================';
PRINT 'STEP 4: What roles does the admin user have?';
PRINT '========================================';
SELECT 
	U.USER_NAME,
	R.ROLE_NAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
WHERE U.USER_NAME IN ('admin', 'Admin', 'ADMIN');

PRINT '';
PRINT '========================================';
PRINT 'STEP 5: What does vw_formname show for admin?';
PRINT '========================================';
SELECT * FROM vw_formname 
WHERE username IN ('admin', 'Admin', 'ADMIN')
  AND formname LIKE '%TaxInvoice%';

PRINT '';
PRINT '========================================';
PRINT 'STEP 6: What does GetMenuData return for admin?';
PRINT '========================================';
EXEC GetMenuData @USERNAME = 'admin';
-- Look for MenuItemCode = 99

PRINT '';
PRINT '========================================';
PRINT 'STEP 7: Try other username variations';
PRINT '========================================';
-- EXEC GetMenuData @USERNAME = 'Admin';
-- EXEC GetMenuData @USERNAME = 'ADMIN';

PRINT '';
PRINT '========================================';
PRINT 'STEP 8: Check what a working menu item looks like';
PRINT '========================================';
-- Compare with CourseWisePaymentDetails which we know works
SELECT * FROM vw_formname 
WHERE username IN ('admin', 'Admin', 'ADMIN')
  AND formname LIKE '%CourseWisePaymentDetails%';
