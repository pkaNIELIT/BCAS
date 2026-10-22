-- =============================================
-- Add ADMINISTRATOR Role Permission for Tax Invoice Upload
-- This is the main role used in your system
-- =============================================

-- Add permission for ADMINISTRATOR role
INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Verify it was added
SELECT * FROM [dbo].[RoleForm]
WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
ORDER BY RoleName;
