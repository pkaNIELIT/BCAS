-- =============================================
-- Add Tax Invoice Upload Menu Item
-- Under Payments (MenuItemCode = 38)
-- =============================================

-- Step 1: Add menu item to MenuMaster
-- Enable explicit insert for IDENTITY column
SET IDENTITY_INSERT [dbo].[MenuMaster] ON;

INSERT INTO [dbo].[MenuMaster] 
(
	[MenuItemCode],
	[MenuDescription],
	[MenuLevel],
	[WhetherLeafMenu],
	[ParentMenuItem],
	[FormName],
	[Enter_By],
	[Enter_Date]
)
VALUES 
(
	99,                                                  -- Next available MenuItemCode
	'Tax Invoice Upload',                                -- Menu Description
	2,                                                   -- MenuLevel (2 = sub-menu)
	'Y',                                                 -- WhetherLeafMenu (Y = leaf/actual page)
	38,                                                  -- ParentMenuItem (38 = Payments)
	'~/Forms/DetailsForms/TaxInvoiceUpload.aspx',       -- FormName
	'admin',                                             -- Enter_By
	GETDATE()                                            -- Enter_Date
);

-- Disable explicit insert for IDENTITY column
SET IDENTITY_INSERT [dbo].[MenuMaster] OFF;

-- Step 2: Add role permissions (OPTIONAL - Uncomment and modify role names as needed)
-- Add for Admin role
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('Admin', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for Finance role
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('Finance', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

-- Add for other roles as needed
-- INSERT INTO [dbo].[RoleForm] ([RoleName], [FormName], [Enter_By], [Enter_Date])
-- VALUES ('YourRoleName', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());


-- =============================================
-- Verification Queries
-- =============================================

-- Verify the menu item was added
SELECT * FROM [dbo].[MenuMaster]
WHERE MenuItemCode = 99;

-- Check all menu items under Payments
SELECT * FROM [dbo].[MenuMaster]
WHERE ParentMenuItem = 38
ORDER BY MenuItemCode;

-- Verify role permissions (if added)
-- SELECT * FROM [dbo].[RoleForm]
-- WHERE FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';
