/*
===============================================================================
DIAGNOSTIC SCRIPT - Check BillerOrganisationPayment Menu Setup
===============================================================================
This script will help diagnose why the BillerOrganisationPayment page 
is not showing in the menu.
===============================================================================
*/

USE [BCAS]
GO

PRINT '==============================================================================='
PRINT 'DIAGNOSTIC CHECK - BillerOrganisationPayment Menu'
PRINT '==============================================================================='

PRINT ''
PRINT '1. Checking if page exists in MenuMaster:'
PRINT '-------------------------------------------'
SELECT 
	MenuItemCode,
	MenuDescription,
	MenuLevel,
	WhetherLeafMenu,
	ParentMenuItem,
	FormName,
	Enter_Date
FROM MenuMaster 
WHERE FormName LIKE '%BillerOrganisation%'

IF @@ROWCOUNT = 0
BEGIN
	PRINT 'ERROR: BillerOrganisationPayment NOT FOUND in MenuMaster!'
	PRINT 'The SQL script may not have executed successfully.'
END
ELSE
BEGIN
	PRINT 'SUCCESS: Page found in MenuMaster'
END

PRINT ''
PRINT '2. Checking RoleForm permissions:'
PRINT '-------------------------------------------'
SELECT 
	RoleName,
	FormName,
	Enter_Date
FROM RoleForm 
WHERE FormName LIKE '%BillerOrganisation%'
ORDER BY RoleName

IF @@ROWCOUNT = 0
BEGIN
	PRINT 'ERROR: NO ROLE PERMISSIONS found for BillerOrganisationPayment!'
END
ELSE
BEGIN
	PRINT 'SUCCESS: Role permissions exist'
END

PRINT ''
PRINT '3. Checking what roles can access Payment-related pages:'
PRINT '-------------------------------------------'
SELECT DISTINCT 
	RoleName,
	COUNT(*) as PaymentPagesCount
FROM RoleForm 
WHERE FormName LIKE '%Payment%'
GROUP BY RoleName
ORDER BY RoleName

PRINT ''
PRINT '4. Checking current user role (GMRGOAIAL):'
PRINT '-------------------------------------------'
SELECT 
	u.UserName,
	r.RoleName
FROM aspnet_Users u
INNER JOIN aspnet_UsersInRoles ur ON u.UserId = ur.UserId
INNER JOIN aspnet_Roles r ON ur.RoleId = r.RoleId
WHERE u.UserName = 'GMRGOAIAL'

IF @@ROWCOUNT = 0
BEGIN
	PRINT 'User GMRGOAIAL not found or has no roles assigned'
END

PRINT ''
PRINT '5. Checking all roles in the system:'
PRINT '-------------------------------------------'
SELECT DISTINCT RoleName 
FROM aspnet_Roles
ORDER BY RoleName

PRINT ''
PRINT '6. Checking what pages SPONSORING role can access:'
PRINT '-------------------------------------------'
SELECT 
	FormName
FROM RoleForm 
WHERE RoleName LIKE '%SPONSORING%' OR RoleName LIKE '%SPONSOR%'
ORDER BY FormName

PRINT ''
PRINT '7. Checking Payments parent menu:'
PRINT '-------------------------------------------'
SELECT 
	MenuItemCode,
	MenuDescription,
	MenuLevel,
	WhetherLeafMenu,
	ParentMenuItem
FROM MenuMaster
WHERE MenuDescription LIKE '%Payment%' OR MenuDescription LIKE '%Finance%'
ORDER BY MenuLevel, MenuItemCode

PRINT ''
PRINT '8. Checking child menus under Payments:'
PRINT '-------------------------------------------'
DECLARE @PaymentMenuCode INT

SELECT TOP 1 @PaymentMenuCode = MenuItemCode
FROM MenuMaster
WHERE (MenuDescription LIKE '%Payment%' OR MenuDescription LIKE '%Finance%')
  AND MenuLevel = 1
  AND WhetherLeafMenu = 'N'

IF @PaymentMenuCode IS NOT NULL
BEGIN
	PRINT 'Parent Payment menu code: ' + CAST(@PaymentMenuCode AS VARCHAR(10))
	PRINT 'Child menu items:'

	SELECT 
		MenuItemCode,
		MenuDescription,
		FormName,
		Enter_Date
	FROM MenuMaster
	WHERE ParentMenuItem = @PaymentMenuCode
	ORDER BY MenuItemCode
END
ELSE
BEGIN
	PRINT 'No parent Payment menu found'
END

PRINT ''
PRINT '==============================================================================='
PRINT 'END OF DIAGNOSTIC CHECK'
PRINT '==============================================================================='

GO
