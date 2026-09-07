	-- =============================================
	-- NUCLEAR OPTION: Manually link everything
	-- Use this ONLY if the diagnostic shows broken relationships
	-- =============================================

	-- SCENARIO 1: If 'admin' user doesn't have ADMINISTRATOR role
	-- First, find the admin user ID
	DECLARE @AdminUserId INT;
	SELECT @AdminUserId = USER_ID FROM TYP_USERS WHERE USER_NAME = 'admin';
	PRINT 'Admin User ID: ' + CAST(@AdminUserId AS VARCHAR);

	-- Find the ADMINISTRATOR role ID
	DECLARE @AdminRoleId INT;
	SELECT @AdminRoleId = ROLE_ID FROM TYP_ROLES WHERE ROLE_NAME = 'ADMINISTRATOR';
	PRINT 'ADMINISTRATOR Role ID: ' + CAST(@AdminRoleId AS VARCHAR);

	-- Check if the relationship exists
	IF NOT EXISTS (
		SELECT 1 FROM TYP_USER_ROLES 
		WHERE USER_ID = @AdminUserId AND ROLE_ID = @AdminRoleId
	)
	BEGIN
		PRINT 'WARNING: admin user is NOT linked to ADMINISTRATOR role!';
		PRINT 'Adding the relationship now...';

		INSERT INTO TYP_USER_ROLES (USER_ID, ROLE_ID)
		VALUES (@AdminUserId, @AdminRoleId);

		PRINT 'SUCCESS: admin user is now linked to ADMINISTRATOR role.';
	END
	ELSE
	BEGIN
		PRINT 'OK: admin user already has ADMINISTRATOR role.';
	END

	-- SCENARIO 2: Verify ADMINISTRATOR role has access to TaxInvoiceUpload
	IF NOT EXISTS (
		SELECT 1 FROM RoleForm 
		WHERE RoleName = 'ADMINISTRATOR' 
		  AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
	)
	BEGIN
		PRINT 'WARNING: ADMINISTRATOR role does not have access to TaxInvoiceUpload!';
		PRINT 'Adding the permission now...';

		INSERT INTO RoleForm (RoleName, FormName, Enter_By, Enter_Date)
		VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

		PRINT 'SUCCESS: ADMINISTRATOR role now has access to TaxInvoiceUpload.';
	END
	ELSE
	BEGIN
		PRINT 'OK: ADMINISTRATOR role already has access to TaxInvoiceUpload.';
	END

	-- SCENARIO 3: Refresh everything
	EXEC sp_refreshview 'vw_formname';
	EXEC sp_recompile 'GetMenuData';

	-- SCENARIO 4: Test the result
	PRINT '';
	PRINT '========================================';
	PRINT 'Testing GetMenuData after fixes...';
	PRINT '========================================';
	EXEC GetMenuData @USERNAME = 'admin';

	PRINT '';
	PRINT 'Look for MenuItemCode = 99 above.';
	PRINT 'If you see it: SUCCESS! Restart IIS and browser.';
	PRINT 'If you do NOT see it: Run DEBUG_Step_By_Step.sql and share the results.';
