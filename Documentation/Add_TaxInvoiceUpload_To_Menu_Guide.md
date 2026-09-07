# Adding Tax Invoice Upload Page to Payment Section Menu

## Overview
This guide will help you add the `CourseWisePaymentDetails_TaxInvoiceUpload.aspx` page to the Payment section of the BCAS application menu system.

## Prerequisites
- Database access to the BCAS database
- SQL Server Management Studio or similar tool
- Knowledge of the existing menu structure and role assignments

## Current Status
✅ The page file already exists at: `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
✅ The code-behind file exists at: `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx.cs`

## Step-by-Step Implementation

### Step 1: Understand the Menu System
The BCAS application uses a database-driven menu system:
- **Table**: `TYP_MENUMASTER`
- **Stored Procedure**: `GetMenuData` (called from `MasterPage.master.cs`)
- **Menu Structure**: Hierarchical with PARENTMENUITEM relationships

### Step 2: Execute SQL Script
1. Open SQL Server Management Studio
2. Connect to your BCAS database
3. Open the file: `Database_Scripts/Add_TaxInvoiceUpload_Menu_Item.sql`
4. **IMPORTANT**: Before executing, you MUST:

   a. **Find the Payment section parent menu code**:
   ```sql
   SELECT 
	   MENUITEMCODE, 
	   MENUDESCRIPTION, 
	   MENULEVEL, 
	   PARENTMENUITEM
   FROM TYP_MENUMASTER
   WHERE MENUDESCRIPTION LIKE '%Payment%' 
	  OR MENUDESCRIPTION LIKE '%PAYMENT%'
   ORDER BY MENULEVEL, MENUITEMCODE
   ```

   b. **Find the next available MENUITEMCODE**:
   ```sql
   SELECT MAX(MENUITEMCODE) + 1 AS NextAvailableCode
   FROM TYP_MENUMASTER
   ```

   c. **Update the script variables**:
   - Set `@NewMenuItemCode` to the next available code
   - Set `@PaymentParentCode` to the MENUITEMCODE of the Payment section

5. Execute the script

### Step 3: Configure Role-Based Access
The page should only be accessible to Finance/Admin users.

1. **Identify Finance/Admin roles**:
```sql
SELECT ROLE_ID, ROLE_NAME, ROLE_DESCRIPTION
FROM TYP_ROLES
WHERE ROLE_NAME LIKE '%Finance%' 
   OR ROLE_NAME LIKE '%Admin%'
   OR ROLE_NAME LIKE '%Financial%'
```

2. **Grant menu access to roles**:
```sql
-- Assuming you have a USER_ROLE_MENU mapping table
-- Table name may vary (check your schema)

DECLARE @MenuItemCode INT = [Your New Menu Item Code]
DECLARE @FinanceRoleID INT = [Your Finance Role ID]

INSERT INTO TYP_USER_ROLE_MENU_MASTER (ROLE_ID, MENUITEMCODE)
VALUES (@FinanceRoleID, @MenuItemCode)
```

### Step 4: Update GetMenuData Stored Procedure (If Needed)
Check if the `GetMenuData` stored procedure includes role-based filtering. If it does, ensure it properly filters menu items based on user roles.

```sql
-- View the stored procedure
EXEC sp_helptext 'GetMenuData'
```

If modifications are needed, they should maintain the existing logic while including the new menu item.

### Step 5: Verify Page Security
Ensure the page itself has proper authorization checks in the code-behind:

**File**: `CourseWisePaymentDetails_TaxInvoiceUpload.aspx.cs`

The page should include authorization logic in `Page_Load`:
```csharp
protected void Page_Load(object sender, EventArgs e)
{
	// Check if user is logged in
	if (Session["UserId"] == null || Session["UserId"].ToString().Trim().Length == 0)
	{
		Response.Redirect("~/Home.aspx");
		return;
	}

	// Check if user has proper role
	// Add role validation here if not already present
	string userRole = Session["UserRole"]?.ToString();
	if (userRole != "Finance" && userRole != "Admin")
	{
		Response.Redirect("~/AccessDenied.aspx");
		return;
	}

	// Rest of page load logic...
}
```

### Step 6: Test the Implementation

1. **Login as Finance/Admin user**:
   - Navigate to the application
   - Login with Finance or Admin credentials
   - Check if "Tax Invoice Upload" appears in the Payment section menu

2. **Login as regular user**:
   - Login with non-Finance/Admin credentials
   - Verify the menu item does NOT appear
   - Try direct URL access: `/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
   - Should be redirected if not authorized

3. **Verify functionality**:
   - Click the menu item
   - Page should load properly
   - Test all features on the page

### Step 7: Session and Form Validation
The application uses a form validation system in `MasterPage.master.cs`:
```csharp
string pagename = System.IO.Path.GetFileName(Request.PhysicalPath);
DataTable dt1 = (DataTable)Session["Forms"];
dt1.PrimaryKey = new DataColumn[] { dt1.Columns[0] };

DataRow foundRow = dt1.Rows.Find(pagename);
if (foundRow == null)
{
	HttpContext.Current.Response.Redirect("~/Home.aspx");
}
```

Ensure that `CourseWisePaymentDetails_TaxInvoiceUpload.aspx` is properly loaded into the `Session["Forms"]` DataTable during login.

## Database Schema Reference

### TYP_MENUMASTER Table Structure
| Column | Type | Description |
|--------|------|-------------|
| MENUITEMCODE | INT | Unique identifier for menu item |
| MENUDESCRIPTION | VARCHAR | Menu text displayed to users |
| MENULEVEL | INT | Hierarchy level (1=top, 2=submenu, etc.) |
| WHETHERLEAFMENU | CHAR(1) | 'Y' if leaf (no children), 'N' if parent |
| PARENTMENUITEM | INT | Reference to parent menu item code |
| FORMNAME | VARCHAR | Relative path to the ASPX page |

## Troubleshooting

### Menu Item Not Appearing
1. Check if user's role has access to the menu item
2. Verify `GetMenuData` stored procedure includes the new item
3. Clear browser cache and session
4. Check if PARENTMENUITEM is correctly set

### Page Access Denied
1. Verify page is in `Session["Forms"]` DataTable
2. Check role-based authorization in code-behind
3. Review web.config authorization settings

### Menu Hierarchy Issues
1. Ensure MENULEVEL is correct (2 for submenu under Payment)
2. Verify PARENTMENUITEM matches the Payment section code
3. Check WHETHERLEAFMENU is set to 'Y'

## Verification Queries

**Check menu item was added**:
```sql
SELECT * FROM TYP_MENUMASTER 
WHERE FORMNAME = 'Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx'
```

**View complete menu hierarchy**:
```sql
SELECT 
	M1.MENUITEMCODE,
	M1.MENUDESCRIPTION AS 'Top Level',
	M2.MENUDESCRIPTION AS 'Sub Menu',
	M3.MENUDESCRIPTION AS 'Page',
	M3.FORMNAME
FROM TYP_MENUMASTER M1
LEFT JOIN TYP_MENUMASTER M2 ON M1.MENUITEMCODE = M2.PARENTMENUITEM
LEFT JOIN TYP_MENUMASTER M3 ON M2.MENUITEMCODE = M3.PARENTMENUITEM
WHERE M1.PARENTMENUITEM IS NULL
ORDER BY M1.MENUITEMCODE, M2.MENUITEMCODE, M3.MENUITEMCODE
```

**Check which users have access**:
```sql
SELECT DISTINCT
	U.USER_ID,
	U.USER_NAME,
	R.ROLE_NAME
FROM TYP_USERS U
INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
INNER JOIN TYP_USER_ROLE_MENU_MASTER RM ON R.ROLE_ID = RM.ROLE_ID
INNER JOIN TYP_MENUMASTER M ON RM.MENUITEMCODE = M.MENUITEMCODE
WHERE M.FORMNAME = 'Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx'
```

## Files Involved

### Application Files (No changes needed - already exist)
- ✅ `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
- ✅ `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx.cs`

### System Files (Reference only - do not modify)
- `BCASApplication/MasterPage.master`
- `BCASApplication/MasterPage.master.cs`
- `BCASClassLibrary/TYP_MENUMASTER.cs`

### Database Objects
- Table: `TYP_MENUMASTER`
- Table: `TYP_USER_ROLE_MENU_MASTER` (or similar)
- Table: `TYP_ROLES`
- Stored Procedure: `GetMenuData`

## Support and Additional Information

For questions or issues:
1. Check the existing Payment section menu items as examples
2. Review similar pages like `PayeeOrganisationPayment.aspx`
3. Test with a Finance/Admin user account first
4. Document any custom role names or table structures specific to your deployment

## Notes
- The menu system is loaded dynamically on each page load for authenticated users
- Menu items are filtered by user roles via the `GetMenuData` stored procedure
- Page-level security should always complement menu-level security
- The application uses Session-based authentication and authorization
