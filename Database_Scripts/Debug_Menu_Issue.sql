-- =============================================
-- Debug: Check Menu Configuration
-- Run these queries to diagnose why menu item is not showing
-- =============================================

-- 1. Verify the menu item exists in MenuMaster
SELECT * FROM [dbo].[MenuMaster]
WHERE MenuItemCode = 99 OR FormName LIKE '%TaxInvoiceUpload%';

-- 2. Verify role permissions exist
SELECT * FROM [dbo].[RoleForm]
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- 3. Check what the stored procedure returns for your user
-- Replace 'admin' with your actual username if different
EXEC GetMenuData @USERNAME = 'admin';

-- 4. View the GetMenuData stored procedure definition
EXEC sp_helptext 'GetMenuData';

-- 5. Check if there are any other Payments submenu items to compare
SELECT * FROM [dbo].[MenuMaster]
WHERE ParentMenuItem = 38  -- 38 is the Payments menu
ORDER BY MenuItemCode;

-- 6. Verify the admin user's role
-- (You may need to adjust table/column names based on your schema)
-- SELECT * FROM TYP_USER_ROLES WHERE USER_NAME = 'admin';
