-- =============================================
-- THE REAL FIX - Update RoleForm to use filename only
-- The stored procedure extracts just the filename and compares with vw_formname
-- =============================================

PRINT '========================================';
PRINT 'Fixing TaxInvoiceUpload RoleForm entries';
PRINT '========================================';

-- Step 1: Delete the incorrect entries (with full path)
DELETE FROM RoleForm
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

PRINT 'Deleted old entries with full path';

-- Step 2: Add correct entries (filename only)
INSERT INTO RoleForm (RoleName, FormName, Enter_By, Enter_Date)
VALUES 
('ADMINISTRATOR', 'TaxInvoiceUpload.aspx', 'admin', GETDATE()),
('Admin', 'TaxInvoiceUpload.aspx', 'admin', GETDATE()),
('FINANCE', 'TaxInvoiceUpload.aspx', 'admin', GETDATE());

PRINT '✓ Added new entries with filename only';

PRINT '';
PRINT '========================================';
PRINT 'Refreshing view and stored procedure';
PRINT '========================================';

-- Refresh the view
EXEC sp_refreshview 'vw_formname';
PRINT '✓ View refreshed';

-- Recompile stored procedure
EXEC sp_recompile 'GetMenuData';
PRINT '✓ Stored procedure recompiled';

PRINT '';
PRINT '========================================';
PRINT 'Verification';
PRINT '========================================';

-- Check what's in RoleForm now
SELECT RoleName, FormName FROM RoleForm
WHERE FormName LIKE '%TaxInvoice%';

PRINT '';
PRINT 'Checking vw_formname...';
SELECT username, rolename, formname FROM vw_formname
WHERE formname LIKE '%TaxInvoice%';

PRINT '';
PRINT '========================================';
PRINT 'FINAL TEST - Run GetMenuData';
PRINT '========================================';
EXEC GetMenuData @USERNAME = 'admin';

PRINT '';
PRINT 'Look for MenuItemCode = 99 in the results above!';
PRINT 'If you see it, RESTART IIS and browser, then log in again.';
