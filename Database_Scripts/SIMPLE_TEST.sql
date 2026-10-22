-- =============================================
-- SIMPLE TEST: What username should we use?
-- =============================================

-- Run this to see what your actual login username is
-- Look at the "Welcome: XXXXX" text at the top of your page
-- The XXXXX is what's stored in Session["UserId"]

-- Let's check all possible usernames in your database:
PRINT '========================================';
PRINT 'Checking which Users table exists...';
PRINT '========================================';

-- Try different possible table names
BEGIN TRY
	SELECT TOP 20
		USER_ID,
		USER_NAME
	FROM Users
	ORDER BY USER_NAME;
	PRINT 'Using table: Users';
END TRY
BEGIN CATCH
	BEGIN TRY
		SELECT TOP 20
			USER_ID,
			USER_NAME
		FROM TYP_USERS
		ORDER BY USER_NAME;
		PRINT 'Using table: TYP_USERS';
	END TRY
	BEGIN CATCH
		BEGIN TRY
			SELECT TOP 20 * FROM aspnet_Users;
			PRINT 'Using table: aspnet_Users';
		END TRY
		BEGIN CATCH
			PRINT 'ERROR: Could not find Users table. Check your database schema.';
			PRINT 'Run this query to find user-related tables:';
			PRINT 'SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE ''%user%'';';
		END CATCH
	END CATCH
END CATCH

-- Now test GetMenuData for each possible admin username:
PRINT '';
PRINT '========================================';
PRINT 'Testing GetMenuData for: admin (lowercase)';
PRINT '========================================';
EXEC GetMenuData @USERNAME = 'admin';

PRINT '';
PRINT '========================================';
PRINT 'Testing GetMenuData for: Admin (capitalized)';
PRINT '========================================';
EXEC GetMenuData @USERNAME = 'Admin';

PRINT '';
PRINT '========================================';
PRINT 'Testing GetMenuData for: ADMIN (uppercase)';
PRINT '========================================';
EXEC GetMenuData @USERNAME = 'ADMIN';

-- Look through the results above. If you see MenuItemCode = 99, then that's the correct username!
-- If you DON'T see it in ANY of the results, continue to Part 2 below...

PRINT '';
PRINT '========================================';
PRINT 'PART 2: If MenuItemCode 99 was NOT in any results above';
PRINT '========================================';

-- Check the vw_formname view for TaxInvoiceUpload
SELECT * FROM vw_formname
WHERE formname = 'TaxInvoiceUpload.aspx';
-- This should show which users CAN see it

PRINT '';
PRINT 'If the query above returned NO rows, then the view is not working.';
PRINT 'Run the NUCLEAR_OPTION_Fix_Everything.sql script next.';
