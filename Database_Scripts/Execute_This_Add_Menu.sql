-- =============================================
-- SIMPLE SQL SCRIPT: Add Tax Invoice Upload Menu
-- File: TaxInvoiceUpload.aspx
-- =============================================
-- BEFORE RUNNING: Update the 3 variables below
-- =============================================

USE [BCAS_Database] -- UPDATE: Change to your database name
GO

-- =============================================
-- STEP 1: SET THESE VALUES
-- =============================================

-- 1. Find next available menu code (run this first):
-- SELECT MAX(MENUITEMCODE) + 1 FROM TYP_MENUMASTER

DECLARE @NewMenuItemCode INT = 999;  -- UPDATE: Set to next available code

-- 2. Find Payment parent code (run this first):
-- SELECT MENUITEMCODE, MENUDESCRIPTION FROM TYP_MENUMASTER 
-- WHERE MENUDESCRIPTION LIKE '%Payment%' AND PARENTMENUITEM IS NULL

DECLARE @PaymentParentCode INT = 50;  -- UPDATE: Set to Payment section code

-- 3. Set your Finance/Admin role IDs (run this first):
-- SELECT ROLE_ID, ROLE_NAME FROM TYP_ROLES 
-- WHERE ROLE_NAME LIKE '%Finance%' OR ROLE_NAME LIKE '%Admin%'

DECLARE @FinanceRoleID INT = 1;  -- UPDATE: Set to Finance role ID
DECLARE @AdminRoleID INT = 2;    -- UPDATE: Set to Admin role ID

-- =============================================
-- STEP 2: ADD MENU ITEM
-- =============================================

-- Check if already exists
IF NOT EXISTS (SELECT 1 FROM TYP_MENUMASTER WHERE FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx')
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
		@NewMenuItemCode,
		'Tax Invoice Upload',
		2,
		'Y',
		@PaymentParentCode,
		'Forms/DetailsForms/TaxInvoiceUpload.aspx'
	)

	PRINT '✓ Menu item added successfully'
	PRINT 'MENUITEMCODE: ' + CAST(@NewMenuItemCode AS VARCHAR(10))
END
ELSE
BEGIN
	PRINT '✗ Menu item already exists'
END

-- =============================================
-- STEP 3: GRANT ACCESS TO FINANCE ROLE
-- =============================================

IF NOT EXISTS (SELECT 1 FROM TYP_USER_ROLE_MENU_MASTER 
			   WHERE ROLE_ID = @FinanceRoleID AND MENUITEMCODE = @NewMenuItemCode)
BEGIN
	INSERT INTO TYP_USER_ROLE_MENU_MASTER (ROLE_ID, MENUITEMCODE)
	VALUES (@FinanceRoleID, @NewMenuItemCode)

	PRINT '✓ Access granted to Finance role'
END
ELSE
BEGIN
	PRINT '○ Finance role already has access'
END

-- =============================================
-- STEP 4: GRANT ACCESS TO ADMIN ROLE
-- =============================================

IF NOT EXISTS (SELECT 1 FROM TYP_USER_ROLE_MENU_MASTER 
			   WHERE ROLE_ID = @AdminRoleID AND MENUITEMCODE = @NewMenuItemCode)
BEGIN
	INSERT INTO TYP_USER_ROLE_MENU_MASTER (ROLE_ID, MENUITEMCODE)
	VALUES (@AdminRoleID, @NewMenuItemCode)

	PRINT '✓ Access granted to Admin role'
END
ELSE
BEGIN
	PRINT '○ Admin role already has access'
END

-- =============================================
-- STEP 5: VERIFY THE CHANGES
-- =============================================

PRINT ''
PRINT '============================================='
PRINT 'VERIFICATION'
PRINT '============================================='

-- Show the added menu item
SELECT 
	'MENU ITEM DETAILS' AS CheckType,
	M.MENUITEMCODE,
	M.MENUDESCRIPTION,
	M.MENULEVEL,
	M.WHETHERLEAFMENU,
	M.PARENTMENUITEM,
	P.MENUDESCRIPTION AS ParentMenu,
	M.FORMNAME
FROM TYP_MENUMASTER M
LEFT JOIN TYP_MENUMASTER P ON M.PARENTMENUITEM = P.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- Show which roles have access
SELECT 
	'ROLE ACCESS' AS CheckType,
	R.ROLE_ID,
	R.ROLE_NAME,
	M.MENUDESCRIPTION
FROM TYP_ROLES R
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT ''
PRINT '✓ Script completed successfully!'
PRINT '============================================='
GO
