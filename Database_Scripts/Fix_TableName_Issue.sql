-- =============================================
-- CRITICAL FIX: Check actual table names and schema
-- =============================================

-- Based on your earlier queries, you're using:
-- [BCAS].[dbo].[MenuMaster]  (NOT TYP_MENUMASTER)
-- [BCAS].[dbo].[RoleForm]    (NOT TYP_RoleForm)

-- STEP 1: Verify the menu item was added to the CORRECT table
SELECT * FROM MenuMaster WHERE MenuItemCode = 99;

-- STEP 2: Verify role permission was added to the CORRECT table  
SELECT * FROM RoleForm WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- STEP 3: Check if maybe you have BOTH table structures (old and new)
-- If this returns results, you have the TYP_ prefix tables
-- SELECT * FROM TYP_MENUMASTER WHERE MENUITEMCODE = 99;

-- STEP 4: Execute the stored procedure for your user
EXEC GetMenuData @USERNAME = 'admin';

-- STEP 5: If GetMenuData returns nothing, check the stored procedure code
EXEC sp_helptext 'GetMenuData';
