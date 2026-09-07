-- =============================================
-- QUICK FIX: Add ALL possible role variations
-- Run this if PAYEE still cannot access after running main script
-- =============================================

USE [BCAS]  -- Change to your database name if different
GO

PRINT '========================================';
PRINT 'QUICK FIX: Adding all role variations';
PRINT '========================================';
PRINT '';

-- Array of all possible role names that might be used
DECLARE @Roles TABLE (RoleName VARCHAR(100));
INSERT INTO @Roles VALUES 
	('PAYEE'),
	('Payee'),
	('payee'),
	('PAYEE ORGANISATION'),
	('Payee Organisation'),
	('PayeeOrganisation'),
	('ADMINISTRATOR'),
	('Administrator'),
	('Admin'),
	('admin'),
	('FINANCE'),
	('Finance'),
	('finance');

-- Add permission for each role if it doesn't exist
DECLARE @RoleName VARCHAR(100);
DECLARE @FormName VARCHAR(200) = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

DECLARE role_cursor CURSOR FOR SELECT RoleName FROM @Roles;
OPEN role_cursor;
FETCH NEXT FROM role_cursor INTO @RoleName;

WHILE @@FETCH_STATUS = 0
BEGIN
	IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = @RoleName AND FormName = @FormName)
	BEGIN
		INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
		VALUES (@RoleName, @FormName, 'admin', GETDATE());
		PRINT '✓ Added: ' + @RoleName;
	END
	ELSE
	BEGIN
		PRINT '  Already exists: ' + @RoleName;
	END

	FETCH NEXT FROM role_cursor INTO @RoleName;
END

CLOSE role_cursor;
DEALLOCATE role_cursor;

PRINT '';
PRINT '========================================';
PRINT 'VERIFICATION';
PRINT '========================================';

-- Show all added permissions
SELECT RoleName, FormName, Enter_Date
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
ORDER BY RoleName;

PRINT '';
PRINT '========================================';
PRINT 'TOTAL ROLES WITH ACCESS:';
SELECT COUNT(*) AS TotalRoles
FROM RoleForm 
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT '';
PRINT '========================================';
PRINT 'NEXT STEPS:';
PRINT '1. Run: iisreset  (in PowerShell as Administrator)';
PRINT '2. Clear browser cache';
PRINT '3. Re-login as PAYEE user';
PRINT '4. Try accessing page directly OR check menu';
PRINT '========================================';
