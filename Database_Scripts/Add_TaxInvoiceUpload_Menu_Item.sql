-- =============================================
-- Script: Add Tax Invoice Upload Menu Item
-- Purpose: Add TaxInvoiceUpload.aspx to the Payment section menu
-- Date Created: 2024
-- Updated: File renamed to TaxInvoiceUpload.aspx
-- =============================================

-- INSTRUCTIONS:
-- 1. Review this script before execution
-- 2. Adjust MENUITEMCODE if there are conflicts with existing menu items
-- 3. Update PARENTMENUITEM to match your Payment section parent menu item code
-- 4. Configure role-based access in TYP_USER_ROLE_MENU_MASTER table
-- =============================================

USE [BCAS_Database] -- Change to your actual database name
GO

-- =============================================
-- STEP 1: Find the Parent Menu Item for "Payment" section
-- =============================================
-- Uncomment the following query to find the correct parent menu item:
/*
SELECT 
	MENUITEMCODE, 
	MENUDESCRIPTION, 
	MENULEVEL, 
	PARENTMENUITEM, 
	FORMNAME
FROM TYP_MENUMASTER
WHERE MENUDESCRIPTION LIKE '%Payment%' OR MENUDESCRIPTION LIKE '%PAYMENT%'
ORDER BY MENULEVEL, MENUITEMCODE
*/

-- =============================================
-- STEP 2: Insert the new menu item for Tax Invoice Upload
-- =============================================
-- NOTE: You need to replace the values below based on your database structure:
-- - MENUITEMCODE: Choose a unique code (e.g., next available number)
-- - PARENTMENUITEM: Set to the MENUITEMCODE of the "Payment" parent menu
-- =============================================

DECLARE @NewMenuItemCode INT = 999; -- Change this to the next available menu item code
DECLARE @PaymentParentCode INT = NULL; -- Set this to the MENUITEMCODE of Payment section

-- Try to find Payment section automatically (uncomment if needed)
-- SELECT @PaymentParentCode = MENUITEMCODE 
-- FROM TYP_MENUMASTER 
-- WHERE MENUDESCRIPTION LIKE '%Payment%' AND MENULEVEL = 1 AND WHETHERLEAFMENU = 'N'

-- Check if menu item already exists
IF NOT EXISTS (SELECT 1 FROM TYP_MENUMASTER WHERE FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	-- Insert the new menu item
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
		@NewMenuItemCode,                                      -- Unique menu item code
		'Tax Invoice Upload',                                  -- Menu text displayed to users
		2,                                                     -- Level 2 = submenu item
		'Y',                                                   -- Y = Leaf menu (no children)
		@PaymentParentCode,                                    -- Parent menu item (Payment section)
		'Forms/DetailsForms/TaxInvoiceUpload.aspx'            -- Path to the page
	)

	PRINT 'Menu item "Tax Invoice Upload" has been added successfully.'
	PRINT 'MENUITEMCODE: ' + CAST(@NewMenuItemCode AS VARCHAR(10))
END
ELSE
BEGIN
	PRINT 'Menu item already exists for TaxInvoiceUpload.aspx'
END
GO

-- =============================================
-- STEP 3: Grant access to Finance/Admin roles
-- =============================================
-- NOTE: This assumes you have a user-role-menu mapping table
-- Table name may vary (e.g., TYP_USER_ROLE_MENU_MASTER, USER_ROLE_MENU, etc.)
-- =============================================

-- Uncomment and adjust the following based on your role structure:
/*
-- Find Finance/Admin role IDs
SELECT * FROM TYP_ROLES WHERE ROLE_NAME LIKE '%Finance%' OR ROLE_NAME LIKE '%Admin%'

-- Grant access to specific roles
DECLARE @NewMenuItemCode INT = 999; -- Same as above
DECLARE @FinanceRoleID INT = 1; -- Replace with actual Finance role ID
DECLARE @AdminRoleID INT = 2;   -- Replace with actual Admin role ID

-- Grant to Finance Role
IF NOT EXISTS (SELECT 1 FROM TYP_USER_ROLE_MENU_MASTER 
			   WHERE ROLE_ID = @FinanceRoleID AND MENUITEMCODE = @NewMenuItemCode)
BEGIN
	INSERT INTO TYP_USER_ROLE_MENU_MASTER (ROLE_ID, MENUITEMCODE)
	VALUES (@FinanceRoleID, @NewMenuItemCode)
END

-- Grant to Admin Role
IF NOT EXISTS (SELECT 1 FROM TYP_USER_ROLE_MENU_MASTER 
			   WHERE ROLE_ID = @AdminRoleID AND MENUITEMCODE = @NewMenuItemCode)
BEGIN
	INSERT INTO TYP_USER_ROLE_MENU_MASTER (ROLE_ID, MENUITEMCODE)
	VALUES (@AdminRoleID, @NewMenuItemCode)
END

PRINT 'Access granted to Finance and Admin roles.'
*/
GO

-- =============================================
-- STEP 4: Verify the changes
-- =============================================
-- Run this query to verify the menu item was added correctly
SELECT 
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.MENULEVEL,
	M.WHETHERLEAFMENU,
	M.PARENTMENUITEM,
	P.MENUDESCRIPTION AS PARENT_MENU_DESCRIPTION,
	M.FORMNAME
FROM TYP_MENUMASTER M
LEFT JOIN TYP_MENUMASTER P ON M.PARENTMENUITEM = P.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx'
   OR M.MENUDESCRIPTION = 'Tax Invoice Upload'
ORDER BY M.MENUITEMCODE
GO

-- =============================================
-- TROUBLESHOOTING QUERIES
-- =============================================

-- List all top-level menu items (to find Payment section)
-- SELECT * FROM TYP_MENUMASTER WHERE PARENTMENUITEM IS NULL ORDER BY MENUITEMCODE

-- List all menu items under Payment section
-- SELECT * FROM TYP_MENUMASTER WHERE PARENTMENUITEM = [PaymentParentCode] ORDER BY MENUITEMCODE

-- Check what users have access to this menu
-- SELECT U.USER_ID, U.USER_NAME, R.ROLE_NAME, M.MENUDESCRIPTION
-- FROM TYP_USERS U
-- INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
-- INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
-- INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
-- INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
-- WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx'
