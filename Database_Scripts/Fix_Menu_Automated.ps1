# =============================================
# PowerShell Script to Execute SQL and Fix Menu
# =============================================

# UPDATE THESE CONNECTION DETAILS:
$serverName = "localhost"  # Change to your SQL Server name
$databaseName = "BCAS"     # Change to your database name
$integratedSecurity = $true # Set to $false if using SQL authentication

# If using SQL authentication, uncomment and fill these:
# $username = "sa"
# $password = "YourPassword"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "BCAS Tax Invoice Upload Menu Fix" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Build connection string
if ($integratedSecurity) {
	$connectionString = "Server=$serverName;Database=$databaseName;Integrated Security=True;"
} else {
	$connectionString = "Server=$serverName;Database=$databaseName;User Id=$username;Password=$password;"
}

Write-Host "Connecting to: $serverName/$databaseName" -ForegroundColor Yellow

try {
	# Load SQL Server assembly
	Add-Type -AssemblyName "System.Data"

	$connection = New-Object System.Data.SqlClient.SqlConnection
	$connection.ConnectionString = $connectionString
	$connection.Open()

	Write-Host "✓ Connected successfully!" -ForegroundColor Green
	Write-Host ""

	# ================================================
	# STEP 1: Check which tables exist
	# ================================================
	Write-Host "STEP 1: Checking which tables exist..." -ForegroundColor Cyan

	$query = @"
		SELECT 
			CASE WHEN OBJECT_ID('MenuMaster', 'U') IS NOT NULL THEN 'MenuMaster EXISTS' ELSE 'MenuMaster MISSING' END AS MenuMaster_Status,
			CASE WHEN OBJECT_ID('TYP_MENUMASTER', 'U') IS NOT NULL THEN 'TYP_MENUMASTER EXISTS' ELSE 'TYP_MENUMASTER MISSING' END AS TYP_MENUMASTER_Status,
			CASE WHEN OBJECT_ID('RoleForm', 'U') IS NOT NULL THEN 'RoleForm EXISTS' ELSE 'RoleForm MISSING' END AS RoleForm_Status
"@

	$command = $connection.CreateCommand()
	$command.CommandText = $query
	$reader = $command.ExecuteReader()

	while ($reader.Read()) {
		Write-Host "  MenuMaster: " -NoNewline
		Write-Host $reader["MenuMaster_Status"] -ForegroundColor $(if ($reader["MenuMaster_Status"] -like "*EXISTS*") { "Green" } else { "Red" })
		Write-Host "  TYP_MENUMASTER: " -NoNewline
		Write-Host $reader["TYP_MENUMASTER_Status"] -ForegroundColor $(if ($reader["TYP_MENUMASTER_Status"] -like "*EXISTS*") { "Green" } else { "Red" })
		Write-Host "  RoleForm: " -NoNewline
		Write-Host $reader["RoleForm_Status"] -ForegroundColor $(if ($reader["RoleForm_Status"] -like "*EXISTS*") { "Green" } else { "Red" })
	}
	$reader.Close()
	Write-Host ""

	# ================================================
	# STEP 2: Check if menu item exists in correct table
	# ================================================
	Write-Host "STEP 2: Checking if menu item 99 exists..." -ForegroundColor Cyan

	$checkQuery = @"
		DECLARE @InMenuMaster INT = 0;
		DECLARE @InTYPMenuMaster INT = 0;

		IF OBJECT_ID('MenuMaster', 'U') IS NOT NULL
			SELECT @InMenuMaster = COUNT(*) FROM MenuMaster WHERE MenuItemCode = 99;

		IF OBJECT_ID('TYP_MENUMASTER', 'U') IS NOT NULL
			SELECT @InTYPMenuMaster = COUNT(*) FROM TYP_MENUMASTER WHERE MENUITEMCODE = 99;

		SELECT @InMenuMaster AS InMenuMaster, @InTYPMenuMaster AS InTYPMenuMaster;
"@

	$command.CommandText = $checkQuery
	$reader = $command.ExecuteReader()

	$needsInsertMenuMaster = $false
	$needsInsertTYPMenuMaster = $false

	while ($reader.Read()) {
		$inMenuMaster = $reader["InMenuMaster"]
		$inTYPMenuMaster = $reader["InTYPMenuMaster"]

		Write-Host "  Menu item 99 in MenuMaster: " -NoNewline
		Write-Host $inMenuMaster -ForegroundColor $(if ($inMenuMaster -gt 0) { "Green" } else { "Yellow" })

		Write-Host "  Menu item 99 in TYP_MENUMASTER: " -NoNewline
		Write-Host $inTYPMenuMaster -ForegroundColor $(if ($inTYPMenuMaster -gt 0) { "Green" } else { "Yellow" })

		$needsInsertMenuMaster = ($inMenuMaster -eq 0)
		$needsInsertTYPMenuMaster = ($inTYPMenuMaster -eq 0)
	}
	$reader.Close()
	Write-Host ""

	# ================================================
	# STEP 3: Insert menu item if needed
	# ================================================
	if ($needsInsertTYPMenuMaster) {
		Write-Host "STEP 3: Adding menu item to TYP_MENUMASTER..." -ForegroundColor Cyan

		$insertQuery = @"
			SET IDENTITY_INSERT [dbo].[TYP_MENUMASTER] ON;

			INSERT INTO [dbo].[TYP_MENUMASTER] 
			(MENUITEMCODE, MENUDESCRIPTION, MENULEVEL, WHETHERLEAFMENU, PARENTMENUITEM, FORMNAME, Enter_By, Enter_Date)
			VALUES 
			(99, 'Tax Invoice Upload', 2, 'Y', 38, '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());

			SET IDENTITY_INSERT [dbo].[TYP_MENUMASTER] OFF;
"@

		$command.CommandText = $insertQuery
		$command.ExecuteNonQuery() | Out-Null
		Write-Host "  ✓ Menu item added to TYP_MENUMASTER!" -ForegroundColor Green
	} else {
		Write-Host "STEP 3: Menu item already exists in TYP_MENUMASTER" -ForegroundColor Green
	}
	Write-Host ""

	# ================================================
	# STEP 4: Add role permissions
	# ================================================
	Write-Host "STEP 4: Adding role permissions..." -ForegroundColor Cyan

	$roleQuery = @"
		IF NOT EXISTS (SELECT 1 FROM RoleForm WHERE RoleName = 'ADMINISTRATOR' AND FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx')
		BEGIN
			INSERT INTO RoleForm (RoleName, FormName, Enter_By, Enter_Date)
			VALUES ('ADMINISTRATOR', '~/Forms/DetailsForms/TaxInvoiceUpload.aspx', 'admin', GETDATE());
			SELECT 'ADDED' AS Status;
		END
		ELSE
		BEGIN
			SELECT 'EXISTS' AS Status;
		END
"@

	$command.CommandText = $roleQuery
	$reader = $command.ExecuteReader()
	while ($reader.Read()) {
		if ($reader["Status"] -eq "ADDED") {
			Write-Host "  ✓ ADMINISTRATOR role permission added!" -ForegroundColor Green
		} else {
			Write-Host "  ✓ ADMINISTRATOR role permission already exists!" -ForegroundColor Green
		}
	}
	$reader.Close()
	Write-Host ""

	# ================================================
	# STEP 5: Refresh view and stored procedure
	# ================================================
	Write-Host "STEP 5: Refreshing database objects..." -ForegroundColor Cyan

	try {
		$command.CommandText = "EXEC sp_refreshview 'vw_formname';"
		$command.ExecuteNonQuery() | Out-Null
		Write-Host "  ✓ View vw_formname refreshed!" -ForegroundColor Green
	} catch {
		Write-Host "  ⚠ Could not refresh vw_formname (may not exist)" -ForegroundColor Yellow
	}

	try {
		$command.CommandText = "EXEC sp_recompile 'GetMenuData';"
		$command.ExecuteNonQuery() | Out-Null
		Write-Host "  ✓ Stored procedure GetMenuData recompiled!" -ForegroundColor Green
	} catch {
		Write-Host "  ⚠ Could not recompile GetMenuData" -ForegroundColor Yellow
	}
	Write-Host ""

	# ================================================
	# STEP 6: Verify
	# ================================================
	Write-Host "STEP 6: Verifying changes..." -ForegroundColor Cyan

	$verifyQuery = "EXEC GetMenuData @USERNAME = 'admin';"
	$command.CommandText = $verifyQuery
	$reader = $command.ExecuteReader()

	$found = $false
	while ($reader.Read()) {
		if ($reader["MenuItemCode"] -eq 99) {
			$found = $true
			break
		}
	}
	$reader.Close()

	if ($found) {
		Write-Host "  ✓ SUCCESS! Menu item 99 found in GetMenuData results!" -ForegroundColor Green
	} else {
		Write-Host "  ⚠ Menu item 99 NOT found in GetMenuData results" -ForegroundColor Yellow
		Write-Host "    You may need to check user-role relationships" -ForegroundColor Yellow
	}
	Write-Host ""

	$connection.Close()

	Write-Host "========================================" -ForegroundColor Cyan
	Write-Host "NEXT STEPS:" -ForegroundColor Cyan
	Write-Host "========================================" -ForegroundColor Cyan
	Write-Host "1. Log out of the application" -ForegroundColor White
	Write-Host "2. Restart IIS / Application Pool" -ForegroundColor White
	Write-Host "3. Clear browser cache (Ctrl+Shift+Delete)" -ForegroundColor White
	Write-Host "4. Log back in" -ForegroundColor White
	Write-Host "5. Check Payments menu for 'Tax Invoice Upload'" -ForegroundColor White
	Write-Host ""

} catch {
	Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
	Write-Host ""
	Write-Host "Please check:" -ForegroundColor Yellow
	Write-Host "- SQL Server name and database name are correct" -ForegroundColor Yellow
	Write-Host "- You have permissions to connect and modify the database" -ForegroundColor Yellow
	Write-Host "- SQL Server is running" -ForegroundColor Yellow
} finally {
	if ($connection.State -eq 'Open') {
		$connection.Close()
	}
}

Read-Host "Press Enter to exit"
