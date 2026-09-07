-- =============================================
-- Get the vw_formname view definition
-- =============================================

EXEC sp_helptext 'vw_formname';

-- After you see the view definition above, we can identify the issue.
-- The view should be something like:

-- CREATE VIEW vw_formname AS
-- SELECT 
--     U.USER_NAME as username,
--     SUBSTRING(RF.FormName, ...) as formname
-- FROM TYP_USERS U
-- INNER JOIN TYP_USER_ROLES UR ON ...
-- INNER JOIN TYP_ROLES R ON ...
-- INNER JOIN RoleForm RF ON R.ROLE_NAME = RF.RoleName
-- 
-- If the view doesn't exist or has errors, we need to create/fix it.
