-- =============================================
-- SOLUTION: Make Tax Invoice Upload Menu Visible
-- Run these steps IN ORDER
-- =============================================

-- STEP 1: Run the debug script in SQL Server Management Studio
-- This will show if your data is correct
EXEC GetMenuData @USERNAME = 'admin';
-- Look for Menu Item 99 in the results. If it's NOT there, continue to Step 2.

-- STEP 2: View the GetMenuData stored procedure
EXEC sp_helptext 'GetMenuData';
-- Copy the output and check if it references:
--   - MenuMaster or TYP_MENUMASTER?
--   - RoleForm or TYP_RoleForm?

-- STEP 3: If GetMenuData uses TYP_MENUMASTER, you need to check if both tables exist
SELECT 'MenuMaster' AS TableName, COUNT(*) AS RecordCount FROM MenuMaster WHERE MenuItemCode = 99
UNION ALL
SELECT 'TYP_MENUMASTER', COUNT(*) FROM TYP_MENUMASTER WHERE MENUITEMCODE = 99;

-- If TYP_MENUMASTER exists and shows 0 records, ADD the menu item there too:
/*
SET IDENTITY_INSERT [dbo].[TYP_MENUMASTER] ON;

INSERT INTO [dbo].[TYP_MENUMASTER] 
(
	[MENUITEMCODE],
	[MENUDESCRIPTION],
	[MENULEVEL],
	[WHETHERLEAFMENU],
	[PARENTMENUITEM],
	[FORMNAME],
	[Enter_By],
	[Enter_Date]
)
VALUES 
(
	99,
	'Tax Invoice Upload',
	2,
	'Y',
	38,
	'~/Forms/DetailsForms/TaxInvoiceUpload.aspx',
	'admin',
	GETDATE()
);

SET IDENTITY_INSERT [dbo].[TYP_MENUMASTER] OFF;
*/

-- STEP 4: Do the same for RoleForm
/*
INSERT INTO [dbo].[TYP_RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
*/

-- =============================================
-- AFTER RUNNING THE SQL FIXES ABOVE:
-- =============================================
-- 1. Close ALL browser windows and tabs
-- 2. In IIS Manager, restart the application pool for BCAS
--    OR restart the development server in Visual Studio
-- 3. Clear browser cache (Ctrl+Shift+Delete)
-- 4. Log in again
-- 5. The menu item should now appear
