-- =============================================
-- List All Tables and Their Columns in BCAS Database
-- =============================================

-- Method 1: Detailed view with data types
PRINT '========================================';
PRINT 'ALL TABLES AND COLUMNS (Detailed)';
PRINT '========================================';

SELECT 
	t.TABLE_SCHEMA AS [Schema],
	t.TABLE_NAME AS [Table Name],
	c.COLUMN_NAME AS [Column Name],
	c.ORDINAL_POSITION AS [Position],
	c.DATA_TYPE AS [Data Type],
	c.CHARACTER_MAXIMUM_LENGTH AS [Max Length],
	c.IS_NULLABLE AS [Nullable],
	CASE 
		WHEN pk.COLUMN_NAME IS NOT NULL THEN 'YES'
		ELSE 'NO'
	END AS [Is Primary Key]
FROM 
	INFORMATION_SCHEMA.TABLES t
INNER JOIN 
	INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_NAME = c.TABLE_NAME AND t.TABLE_SCHEMA = c.TABLE_SCHEMA
LEFT JOIN 
	(
		SELECT ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
		FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
		INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
			ON tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
			AND tc.TABLE_SCHEMA = ku.TABLE_SCHEMA
			AND tc.TABLE_NAME = ku.TABLE_NAME
		WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
	) pk 
	ON c.TABLE_NAME = pk.TABLE_NAME 
	AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
	AND c.COLUMN_NAME = pk.COLUMN_NAME
WHERE 
	t.TABLE_TYPE = 'BASE TABLE'
ORDER BY 
	t.TABLE_SCHEMA,
	t.TABLE_NAME,
	c.ORDINAL_POSITION;

-- Method 2: Simple grouped view by table
PRINT '';
PRINT '========================================';
PRINT 'TABLES WITH COLUMN COUNT';
PRINT '========================================';

SELECT 
	t.TABLE_SCHEMA AS [Schema],
	t.TABLE_NAME AS [Table Name],
	COUNT(c.COLUMN_NAME) AS [Column Count]
FROM 
	INFORMATION_SCHEMA.TABLES t
INNER JOIN 
	INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_NAME = c.TABLE_NAME
WHERE 
	t.TABLE_TYPE = 'BASE TABLE'
GROUP BY 
	t.TABLE_SCHEMA,
	t.TABLE_NAME
ORDER BY 
	t.TABLE_SCHEMA,
	t.TABLE_NAME;

-- Method 3: Just table names (quick reference)
PRINT '';
PRINT '========================================';
PRINT 'ALL TABLE NAMES (Alphabetical)';
PRINT '========================================';

SELECT 
	TABLE_SCHEMA AS [Schema],
	TABLE_NAME AS [Table Name],
	TABLE_TYPE AS [Type]
FROM 
	INFORMATION_SCHEMA.TABLES
WHERE 
	TABLE_TYPE = 'BASE TABLE'
ORDER BY 
	TABLE_NAME;

-- Method 4: Search for specific tables
PRINT '';
PRINT '========================================';
PRINT 'MENU-RELATED TABLES';
PRINT '========================================';

SELECT 
	TABLE_NAME AS [Table Name],
	COUNT(*) AS [Column Count]
FROM 
	INFORMATION_SCHEMA.COLUMNS
WHERE 
	TABLE_NAME LIKE '%menu%'
GROUP BY TABLE_NAME
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'USER/ROLE-RELATED TABLES';
PRINT '========================================';

SELECT 
	TABLE_NAME AS [Table Name],
	COUNT(*) AS [Column Count]
FROM 
	INFORMATION_SCHEMA.COLUMNS
WHERE 
	TABLE_NAME LIKE '%user%'
	OR TABLE_NAME LIKE '%role%'
GROUP BY TABLE_NAME
ORDER BY TABLE_NAME;

PRINT '';
PRINT '========================================';
PRINT 'FORM-RELATED TABLES';
PRINT '========================================';

SELECT 
	TABLE_NAME AS [Table Name],
	COUNT(*) AS [Column Count]
FROM 
	INFORMATION_SCHEMA.COLUMNS
WHERE 
	TABLE_NAME LIKE '%form%'
GROUP BY TABLE_NAME
ORDER BY TABLE_NAME;

-- Method 5: All views in the database
PRINT '';
PRINT '========================================';
PRINT 'ALL VIEWS IN DATABASE';
PRINT '========================================';

SELECT 
	TABLE_SCHEMA AS [Schema],
	TABLE_NAME AS [View Name]
FROM 
	INFORMATION_SCHEMA.TABLES
WHERE 
	TABLE_TYPE = 'VIEW'
ORDER BY 
	TABLE_NAME;

-- Method 6: Export to table (optional - uncomment to use)
/*
-- This creates a temporary result you can export to Excel
SELECT 
	t.TABLE_NAME AS TableName,
	c.COLUMN_NAME AS ColumnName,
	c.DATA_TYPE AS DataType,
	c.CHARACTER_MAXIMUM_LENGTH AS MaxLength,
	c.IS_NULLABLE AS IsNullable
FROM 
	INFORMATION_SCHEMA.TABLES t
INNER JOIN 
	INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_NAME = c.TABLE_NAME
WHERE 
	t.TABLE_TYPE = 'BASE TABLE'
ORDER BY 
	t.TABLE_NAME,
	c.ORDINAL_POSITION;
*/
