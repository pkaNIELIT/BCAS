-- =============================================
-- FIX: Add all possible PAYEE role variations
-- =============================================

PRINT '========================================';
PRINT 'CHECKING EXISTING PAYEE ROLES';
PRINT '========================================';

-- First, let's see what role names exist with "PAYEE" or similar
SELECT DISTINCT RoleName 
FROM RoleForm 
WHERE RoleName LIKE '%PAY%' 
   OR RoleName LIKE '%payee%'
   OR RoleName LIKE '%Payee%'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'ADDING MISSING PAYEE ROLE VARIATIONS';
PRINT '========================================';

-- Add permission for exact 'PAYEE' role
IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'PAYEE' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'Adding PAYEE role...';
	INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES ('PAYEE', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
	PRINT '✓ PAYEE added';
END
ELSE
	PRINT '✓ PAYEE already exists';

-- Add permission for 'Payee' role (with capital P)
IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'Payee' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'Adding Payee role...';
	INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES ('Payee', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
	PRINT '✓ Payee added';
END
ELSE
	PRINT '✓ Payee already exists';

-- Add permission for 'PayeeOrganisation' role
IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'PayeeOrganisation' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'Adding PayeeOrganisation role...';
	INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES ('PayeeOrganisation', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
	PRINT '✓ PayeeOrganisation added';
END
ELSE
	PRINT '✓ PayeeOrganisation already exists';

-- Add permission for 'Payee Organisation' role (with space)
IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'Payee Organisation' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
BEGIN
	PRINT 'Adding Payee Organisation role...';
	INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
	VALUES ('Payee Organisation', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
	PRINT '✓ Payee Organisation added';
END
ELSE
	PRINT '✓ Payee Organisation already exists';

PRINT '';
PRINT '========================================';
PRINT 'VERIFICATION';
PRINT '========================================';

-- Show all TaxInvoiceUpload permissions
SELECT RoleName, FormName, Enter_Date
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'COMPARE WITH OTHER PAYEE PAGES';
PRINT '========================================';

-- Show what roles can access PayeeOrganisationPayment page (known PAYEE page)
SELECT DISTINCT RoleName
FROM RoleForm 
WHERE FormName LIKE '%PayeeOrganisation%'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'NEXT: Run the Debug script to check ASP.NET roles';
PRINT '========================================';
