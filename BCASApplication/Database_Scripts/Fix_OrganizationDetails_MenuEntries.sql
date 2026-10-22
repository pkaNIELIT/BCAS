-- ===============================================
-- FIX DATABASE ENTRIES FOR ORGANIZATION DETAILS PAGES
-- ===============================================
-- Run this script to correct the paths in MenuMaster and RoleForm tables
-- ===============================================

USE [BCAS]
GO

-- ===============================================
-- STEP 1: Check current entries in MenuMaster
-- ===============================================
SELECT * FROM [dbo].[MenuMaster] 
WHERE FormName LIKE '%OrganizationDetails%'
ORDER BY MenuItemCode;

-- ===============================================
-- STEP 2: Check current entries in RoleForm
-- ===============================================
SELECT * FROM [dbo].[RoleForm] 
WHERE FormName LIKE '%OrganizationDetails%'
ORDER BY RoleName, FormName;

-- ===============================================
-- STEP 3: DELETE INCORRECT ENTRIES (if they exist with wrong paths)
-- ===============================================

-- Delete from RoleForm first (due to foreign key constraints)
DELETE FROM [dbo].[RoleForm] 
WHERE FormName IN (
	'PayeeOrganizationDetails.aspx',
	'SponsoringOrganizationDetails.aspx'
);

-- Delete from MenuMaster
DELETE FROM [dbo].[MenuMaster] 
WHERE FormName IN (
	'PayeeOrganizationDetails.aspx',
	'SponsoringOrganizationDetails.aspx'
);

-- ===============================================
-- STEP 4: INSERT CORRECT ENTRIES INTO MenuMaster
-- ===============================================
-- Note: Adjust MenuItemCode and ParentMenuItem values based on your existing menu structure
-- You may need to find the correct ParentMenuItem code for "User Management" section

-- Find the User Management parent menu item code
SELECT MenuItemCode, MenuDescription, FormName 
FROM [dbo].[MenuMaster] 
WHERE MenuDescription LIKE '%User%Management%' OR MenuDescription LIKE '%User%Detail%'
ORDER BY MenuItemCode;

-- Get the next available MenuItemCode
DECLARE @NextMenuCode INT;
SELECT @NextMenuCode = ISNULL(MAX(MenuItemCode), 0) + 1 FROM [dbo].[MenuMaster];

PRINT 'Next Available MenuItemCode: ' + CAST(@NextMenuCode AS VARCHAR(10));

-- Insert Payee Organization Details menu item
-- ADJUST ParentMenuItem (currently set to 100 - change this to your User Management menu code)
INSERT INTO [dbo].[MenuMaster] (MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName, Enter_By, Enter_Date)
VALUES 
(
	@NextMenuCode,                                      -- MenuItemCode: Use next available code
	'Payee Organization Details',                       -- MenuDescription
	2,                                                   -- MenuLevel (2 = sub-menu)
	'Y',                                                 -- WhetherLeafMenu ('Y' = leaf menu, no sub-items)
	100,                                                 -- ParentMenuItem: CHANGE THIS to your User Management menu code
	'~/UserDetails/PayeeOrganizationDetails.aspx',     -- FormName: Correct path with ~/ prefix
	'admin',                                            -- Enter_By
	GETDATE()                                           -- Enter_Date
);

-- Insert Sponsoring Organization Details menu item
INSERT INTO [dbo].[MenuMaster] (MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName, Enter_By, Enter_Date)
VALUES 
(
	@NextMenuCode + 1,                                  -- MenuItemCode: Next code
	'Sponsoring Organization Details',                  -- MenuDescription
	2,                                                   -- MenuLevel (2 = sub-menu)
	'Y',                                                 -- WhetherLeafMenu
	100,                                                 -- ParentMenuItem: CHANGE THIS to your User Management menu code
	'~/UserDetails/SponsoringOrganizationDetails.aspx', -- FormName: Correct path with ~/ prefix
	'admin',                                            -- Enter_By
	GETDATE()                                           -- Enter_Date
);

-- ===============================================
-- STEP 5: INSERT ENTRIES INTO RoleForm
-- ===============================================

-- Insert for ADMINISTRATOR role
INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
VALUES 
	('ADMINISTRATOR', '~/UserDetails/PayeeOrganizationDetails.aspx', 'admin', GETDATE()),
	('ADMINISTRATOR', '~/UserDetails/SponsoringOrganizationDetails.aspx', 'admin', GETDATE());

-- Insert for PAYEEORGANISATION role (if this role should see Payee page)
INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
VALUES 
	('PAYEEORGANISATION', '~/UserDetails/PayeeOrganizationDetails.aspx', 'admin', GETDATE());

-- Insert for SPONSORINGORGANISATION role (if this role should see Sponsoring page)
INSERT INTO [dbo].[RoleForm] (RoleName, FormName, Enter_By, Enter_Date)
VALUES 
	('SPONSORINGORGANISATION', '~/UserDetails/SponsoringOrganizationDetails.aspx', 'admin', GETDATE());

-- ===============================================
-- STEP 6: VERIFY THE INSERTIONS
-- ===============================================

-- Check MenuMaster entries
SELECT MenuItemCode, MenuDescription, MenuLevel, WhetherLeafMenu, ParentMenuItem, FormName, Enter_By, Enter_Date
FROM [dbo].[MenuMaster] 
WHERE FormName LIKE '%OrganizationDetails%'
ORDER BY MenuItemCode;

-- Check RoleForm entries
SELECT RoleName, FormName, Enter_By, Enter_Date
FROM [dbo].[RoleForm] 
WHERE FormName LIKE '%OrganizationDetails%'
ORDER BY RoleName, FormName;

-- ===============================================
-- STEP 7: CHECK MENU HIERARCHY
-- ===============================================
-- This helps you verify the menu structure

SELECT 
	m1.MenuItemCode,
	m1.MenuDescription,
	m1.MenuLevel,
	m1.FormName,
	m2.MenuDescription AS ParentMenuDescription
FROM [dbo].[MenuMaster] m1
LEFT JOIN [dbo].[MenuMaster] m2 ON m1.ParentMenuItem = m2.MenuItemCode
WHERE m1.FormName LIKE '%OrganizationDetails%'
ORDER BY m1.MenuItemCode;

GO
