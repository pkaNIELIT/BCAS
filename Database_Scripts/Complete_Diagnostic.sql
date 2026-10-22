-- =============================================
-- COMPLETE DIAGNOSTIC SCRIPT
-- Run this step by step to find why Tax Invoice Upload is not showing
-- =============================================

PRINT '========================================';
PRINT 'STEP 1: Check if menu item exists';
PRINT '========================================';
SELECT 
	MenuItemCode,
	MenuDescription,
	ParentMenuItem,
	FormName,
	MenuLevel,
	WhetherLeafMenu
FROM [dbo].[MenuMaster]
WHERE MenuItemCode = 99;

PRINT '';
PRINT '========================================';
PRINT 'STEP 2: Check all Payments sub-items';
PRINT '========================================';
SELECT 
	MenuItemCode,
	MenuDescription,
	FormName,
	MenuLevel,
	WhetherLeafMenu
FROM [dbo].[MenuMaster]
WHERE ParentMenuItem = 38
ORDER BY MenuItemCode;

PRINT '';
PRINT '========================================';
PRINT 'STEP 3: Check role permissions';
PRINT '========================================';
SELECT 
	RoleName,
	FormName,
	Enter_By,
	Enter_Date
FROM [dbo].[RoleForm]
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'STEP 4: Test GetMenuData stored procedure';
PRINT '========================================';
-- This will show what menu items the admin user can see
EXEC GetMenuData @USERNAME = 'admin';

PRINT '';
PRINT '========================================';
PRINT 'STEP 5: Check if other similar pages work';
PRINT '========================================';
SELECT 
	RoleName,
	FormName
FROM [dbo].[RoleForm]
WHERE FormName LIKE '%CourseWisePaymentDetails%'
   OR FormName LIKE '%TaxInvoice%';
