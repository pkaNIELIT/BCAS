-- =============================================
-- Add BCAS Signed Tax Invoice Download Payee Menu Item
-- Under Payments Menu
-- =============================================

-- First, let's check what the Payments parent menu code is
PRINT 'Step 1: Finding Payments menu parent code';
SELECT 
	MENUITEMCODE, 
	MENUDESCRIPTION, 
	MENULEVEL, 
	PARENTMENUITEM,
	FORMNAME
FROM TYP_MENUMASTER
WHERE MENUDESCRIPTION LIKE '%Payment%'
ORDER BY MENUITEMCODE;

PRINT '';
PRINT '==============================================';
PRINT 'Step 2: Finding next available MenuItemCode';
PRINT '==============================================';

-- Find the next available MenuItemCode
DECLARE @NextMenuCode INT;
SELECT @NextMenuCode = ISNULL(MAX(MENUITEMCODE), 0) + 1 FROM TYP_MENUMASTER;
PRINT 'Next available MenuItemCode: ' + CAST(@NextMenuCode AS VARCHAR(10));

-- Find the Payments parent menu code (usually around 38 or similar)
DECLARE @PaymentsMenuCode INT;
SELECT @PaymentsMenuCode = MENUITEMCODE 
FROM TYP_MENUMASTER 
WHERE MENUDESCRIPTION LIKE '%Payment%' 
  AND WHETHERLEAFMENU = 'N' 
  AND MENULEVEL = 1;

PRINT 'Payments Parent MenuItemCode: ' + CAST(@PaymentsMenuCode AS VARCHAR(10));
PRINT '';

-- =============================================
-- Insert the new menu item
-- =============================================
PRINT '==============================================';
PRINT 'Step 3: Adding menu item to TYP_MENUMASTER';
PRINT '==============================================';

IF NOT EXISTS (SELECT 1 FROM TYP_MENUMASTER WHERE FORMNAME = '~/Forms/DetailsForms/BCAS_Signed_TaxInvoice_Download_Payee.aspx')
BEGIN
	INSERT INTO TYP_MENUMASTER 
	(
		MENUITEMCODE,
		MENUDESCRIPTION,
		MENULEVEL,
		WHETHERLEAFMENU,
		PARENTMENUITEM,
		FORMNAME
	)
	VALUES 
	(
		@NextMenuCode,                                                    -- Next available MenuItemCode
		'Signed Tax Invoice Download (Payee)',                           -- Menu Description
		2,                                                                -- MenuLevel (2 = sub-menu)
		'Y',                                                              -- WhetherLeafMenu (Y = leaf/actual page)
		@PaymentsMenuCode,                                               -- ParentMenuItem (Payments)
		'~/Forms/DetailsForms/BCAS_Signed_TaxInvoice_Download_Payee.aspx' -- FormName
	);

	PRINT 'Menu item added successfully with MenuItemCode: ' + CAST(@NextMenuCode AS VARCHAR(10));
END
ELSE
BEGIN
	PRINT 'Menu item already exists!';
END

PRINT '';
PRINT '==============================================';
PRINT 'Step 4: Verification - Check the new menu item';
PRINT '==============================================';

-- Verify the menu item was added
SELECT 
	MENUITEMCODE,
	MENUDESCRIPTION,
	MENULEVEL,
	WHETHERLEAFMENU,
	PARENTMENUITEM,
	FORMNAME
FROM TYP_MENUMASTER
WHERE FORMNAME = '~/Forms/DetailsForms/BCAS_Signed_TaxInvoice_Download_Payee.aspx';

PRINT '';
PRINT '==============================================';
PRINT 'Step 5: Check all menu items under Payments';
PRINT '==============================================';

-- Check all menu items under Payments
SELECT 
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.MENULEVEL,
	M.WHETHERLEAFMENU,
	M.FORMNAME
FROM TYP_MENUMASTER M
WHERE M.PARENTMENUITEM = @PaymentsMenuCode
ORDER BY M.MENUITEMCODE;

PRINT '';
PRINT '==============================================';
PRINT 'IMPORTANT: Role Permissions';
PRINT '==============================================';
PRINT 'The menu item has been added, but you need to grant permissions to user roles.';
PRINT 'Check if you have a RoleForm or similar table for permissions.';
PRINT '';
PRINT 'If you need to add role permissions, uncomment and run the following:';
PRINT '';
PRINT '-- Example: Grant permission to Admin role';
PRINT '-- INSERT INTO RoleForm (RoleName, FormName, Enter_By, Enter_Date)';
PRINT '-- VALUES (''Admin'', ''~/Forms/DetailsForms/BCAS_Signed_TaxInvoice_Download_Payee.aspx'', ''admin'', GETDATE());';
PRINT '';
PRINT 'Script completed successfully!';
PRINT 'Please refresh your web application to see the new menu item.';
