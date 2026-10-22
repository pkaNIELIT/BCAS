-- =============================================
-- FIND CORRECT TABLE NAMES IN YOUR DATABASE
-- =============================================

PRINT '========================================';
PRINT 'Step 1: Find all user-related tables';
PRINT '========================================';
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%user%'
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'Step 2: Find all role-related tables';
PRINT '========================================';
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%role%'
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'Step 3: Find all menu-related tables';
PRINT '========================================';
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%menu%'
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'Step 4: Find all form-related tables/views';
PRINT '========================================';
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%form%'
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'Step 5: Find the vw_formname view';
PRINT '========================================';
SELECT TABLE_NAME, TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'vw_formname';

PRINT '';
PRINT '========================================';
PRINT 'COPY THE RESULTS ABOVE AND SHARE THEM';
PRINT 'Then I will create the correct fix script';
PRINT '========================================';
