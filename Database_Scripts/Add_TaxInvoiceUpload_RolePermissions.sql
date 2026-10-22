-- =============================================
-- Add Role Permissions for Tax Invoice Upload
-- =============================================

-- Step 1: Check what roles exist in your system
-- Run this first to see what role names are available
SELECT DISTINCT RoleName 
FROM [dbo].[RoleForm] 
ORDER BY RoleName;

-- Step 2: Add permissions for the roles that need access
-- UNCOMMENT THE ROLES THAT SHOULD HAVE ACCESS TO THIS PAGE

-- Add for ADMINISTRATOR role (most common in your system)
INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for admin role (lowercase)
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('admin', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for Admin role (already added)
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('Admin', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for Finance role (if exists)
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('Finance', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for BCAS Admin role (if exists)
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('BCAS Admin', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for other roles as needed
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('YourRoleName', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());


-- =============================================
-- Verification Query
-- =============================================

-- Verify the role permissions were added
SELECT * FROM [dbo].[RoleForm]
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';

-- Check all roles that have access
SELECT DISTINCT RoleName 
FROM [dbo].[RoleForm]
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';
