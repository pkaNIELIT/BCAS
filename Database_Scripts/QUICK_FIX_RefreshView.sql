-- =============================================
-- QUICK FIX: Most Common Issues
-- Run these in order until menu appears
-- =============================================

-- FIX 1: Refresh the vw_formname view
-- This forces SQL Server to recompile the view with latest data
EXEC sp_refreshview 'vw_formname';
GO

-- FIX 2: Recompile the stored procedure
-- This forces it to use the latest view definition
EXEC sp_recompile 'GetMenuData';
GO

-- FIX 3: Clear procedure cache (use with caution on production!)
-- This clears all cached query plans
-- DBCC FREEPROCCACHE;
-- GO

-- FIX 4: Verify the fix worked
PRINT 'Testing if Tax Invoice Upload now appears in menu...';
EXEC GetMenuData @USERNAME = 'admin';
-- Look for MenuItemCode = 99 or MenuDescription = 'Tax Invoice Upload'

-- If you see it in the results above, the database is fixed!
-- Now you need to:
-- 1. Log out of the application
-- 2. Restart IIS / Application Pool / Visual Studio Debug Server
-- 3. Clear browser cache (Ctrl+Shift+Delete)
-- 4. Log back in
-- 5. Menu item should appear under Payments
