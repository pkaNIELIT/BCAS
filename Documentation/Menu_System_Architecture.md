# BCAS Menu System Architecture

## System Overview

The BCAS application uses a **database-driven, role-based menu system** that dynamically generates menus based on:
1. User authentication status
2. User's assigned roles
3. Menu permissions per role

## Architecture Flow

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         USER LOGIN                                       │
│  (Authentication validates user credentials)                            │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                    SESSION INITIALIZATION                                │
│  - Session["UserId"] = username                                         │
│  - Session["Forms"] = DataTable of accessible pages                     │
│  - Session["UserRole"] = user's role(s)                                 │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                     PAGE REQUEST                                         │
│  User navigates to any page using MasterPage.master                    │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│               MasterPage.master.cs - Page_Load()                        │
│  1. Check if user is authenticated (Session["UserId"])                  │
│  2. Get username: Session["UserId"].ToString()                          │
│  3. Call TYP_MENUMASTER.GetMenuData(username)                           │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│          TYP_MENUMASTER.GetMenuData(username)                           │
│  Stored Procedure: GetMenuData                                          │
│  - Queries database for menu items accessible to user                   │
│  - Filters by user's roles                                              │
│  - Returns DataTable with menu structure                                │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│            MasterPage.master.cs - AddTopMenuItems()                     │
│  - Builds hierarchical menu from DataTable                              │
│  - Adds top-level items (PARENTMENUITEM IS NULL)                        │
│  - Recursively adds child items                                         │
│  - Binds to Menu1 control                                               │
└────────────────────────────┬────────────────────────────────────────────┘
							 │
							 ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                  MENU RENDERED TO USER                                   │
│  User sees only menu items their role permits                           │
└─────────────────────────────────────────────────────────────────────────┘
```

## Database Schema

### Core Tables

#### 1. TYP_MENUMASTER
Stores all menu items and their hierarchy.

```
┌─────────────────────────────────────────────────────────────────┐
│                      TYP_MENUMASTER                             │
├─────────────────────────────────────────────────────────────────┤
│  MENUITEMCODE (INT, PK)      - Unique menu identifier          │
│  MENUDESCRIPTION (VARCHAR)   - Menu text shown to users        │
│  MENULEVEL (INT)             - Hierarchy level (1, 2, 3...)    │
│  WHETHERLEAFMENU (CHAR)      - 'Y' = leaf, 'N' = parent        │
│  PARENTMENUITEM (INT, FK)    - Parent menu code (NULL = root)  │
│  FORMNAME (VARCHAR)          - Path to ASPX page               │
└─────────────────────────────────────────────────────────────────┘
```

**Example Data**:
```
MENUITEMCODE | MENUDESCRIPTION | MENULEVEL | WHETHERLEAFMENU | PARENTMENUITEM | FORMNAME
-------------|-----------------|-----------|------------------|----------------|------------------
50           | Payment         | 1         | N                | NULL           | NULL
501          | Payee Payment   | 2         | Y                | 50             | Forms/DetailsForms/PayeeOrganisationPayment.aspx
502          | NEFT/RTGS       | 2         | Y                | 50             | Forms/DetailsForms/NEFT_RTGS_PayeePayments.aspx
999          | Tax Invoice     | 2         | Y                | 50             | Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx
```

#### 2. TYP_ROLES
Defines user roles in the system.

```
┌─────────────────────────────────────────────────────────────────┐
│                        TYP_ROLES                                │
├─────────────────────────────────────────────────────────────────┤
│  ROLE_ID (INT, PK)           - Role identifier                 │
│  ROLE_NAME (VARCHAR)         - Role name                       │
│  ROLE_DESCRIPTION (VARCHAR)  - Role description                │
└─────────────────────────────────────────────────────────────────┘
```

#### 3. TYP_USER_ROLE_MENU_MASTER
Maps which roles can access which menu items.

```
┌─────────────────────────────────────────────────────────────────┐
│                TYP_USER_ROLE_MENU_MASTER                        │
├─────────────────────────────────────────────────────────────────┤
│  ROLE_ID (INT, FK)           - References TYP_ROLES            │
│  MENUITEMCODE (INT, FK)      - References TYP_MENUMASTER       │
└─────────────────────────────────────────────────────────────────┘
```

**Example Data**:
```
ROLE_ID | MENUITEMCODE | Description
--------|--------------|--------------------------------------
1       | 50           | Finance role → can see Payment menu
1       | 501          | Finance role → can see Payee Payment
1       | 999          | Finance role → can see Tax Invoice Upload
2       | 50           | Admin role → can see Payment menu
2       | 501          | Admin role → can see Payee Payment
2       | 999          | Admin role → can see Tax Invoice Upload
```

#### 4. TYP_USERS & TYP_USER_ROLES
Links users to their roles.

```
┌─────────────────────┐        ┌──────────────────┐
│     TYP_USERS       │        │ TYP_USER_ROLES   │
├─────────────────────┤        ├──────────────────┤
│ USER_ID (PK)        │◄───────┤ USER_ID (FK)     │
│ USER_NAME           │        │ ROLE_ID (FK)     │
│ ...                 │        └──────────────────┘
└─────────────────────┘
```

## Menu Hierarchy Example

```
Payment (MENUITEMCODE: 50)
│
├── Payee Payment (501)
│   └── Link to: Forms/DetailsForms/PayeeOrganisationPayment.aspx
│
├── NEFT/RTGS Payment (502)
│   └── Link to: Forms/DetailsForms/NEFT_RTGS_PayeePayments.aspx
│
├── sponsor Organization Payment (503)
│   └── Link to: Forms/DetailsForms/SponOrganisationPayment.aspx
│
└── Tax Invoice Upload (999) ← NEW ITEM
	└── Link to: Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx
```

## Security Layers

### Layer 1: Menu Visibility (Database)
- User's role determines which menu items appear
- Controlled by `TYP_USER_ROLE_MENU_MASTER` table
- Enforced by `GetMenuData` stored procedure

### Layer 2: Session Validation (MasterPage)
```csharp
// In MasterPage.master.cs
string pagename = System.IO.Path.GetFileName(Request.PhysicalPath);
DataTable dt1 = (DataTable)Session["Forms"];
DataRow foundRow = dt1.Rows.Find(pagename);
if (foundRow == null)
{
	HttpContext.Current.Response.Redirect("~/Home.aspx");
}
```

### Layer 3: Page-Level Authorization (Code-Behind)
```csharp
// In each page's Page_Load
if (Session["UserId"] == null || Session["UserId"].ToString().Trim().Length == 0)
{
	Response.Redirect("~/Home.aspx");
}

// Additional role check
string userRole = Session["UserRole"]?.ToString();
if (userRole != "Finance" && userRole != "Admin")
{
	Response.Redirect("~/AccessDenied.aspx");
}
```

## Adding Tax Invoice Upload - Data Flow

```
Step 1: Add Menu Item to Database
─────────────────────────────────────
INSERT INTO TYP_MENUMASTER
VALUES (999, 'Tax Invoice Upload', 2, 'Y', 50, 
		'Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx')

		↓

Step 2: Grant Role Access
─────────────────────────────────────
INSERT INTO TYP_USER_ROLE_MENU_MASTER
VALUES (1, 999)  -- Finance role
VALUES (2, 999)  -- Admin role

		↓

Step 3: User Login (Finance User)
─────────────────────────────────────
User: john_finance
Role: Finance (ROLE_ID=1)
Session initialized

		↓

Step 4: Menu Load
─────────────────────────────────────
GetMenuData('john_finance') executes
Returns: All menu items for Finance role
Including: MENUITEMCODE 999 (Tax Invoice Upload)

		↓

Step 5: Menu Rendering
─────────────────────────────────────
MasterPage.master.cs builds menu
Payment → Sub-items including "Tax Invoice Upload"
Menu displayed to user

		↓

Step 6: User Clicks Menu
─────────────────────────────────────
Navigate to: CourseWisePaymentDetails_TaxInvoiceUpload.aspx
Session validation passes
Page loads successfully
```

## Key Methods

### MasterPage.master.cs

```csharp
// Loads menu data for authenticated user
DataTable menudata = TYP_MENUMASTER.GetMenuData(UserName);

// Builds top-level menu items
private void AddTopMenuItems(DataTable menuData)
{
	DataView view = new DataView(menuData);
	view.RowFilter = "PARENTMENUITEM IS NULL";
	foreach (DataRowView row in view)
	{
		MenuItem NewMenuItem = new MenuItem(
			CleanMenuText(row["MENUDESCRIPTION"].ToString()),
			row["MENUITEMCODE"].ToString(),
			"",
			row["FORMNAME"].ToString()
		);
		Menu1.Items.Add(NewMenuItem);
		AddChildMenuItems(menuData, NewMenuItem);
	}
}

// Recursively adds child menu items
private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
{
	DataView view = new DataView(menuData);
	view.RowFilter = "PARENTMENUITEM=" + parentMenuItem.Value;
	foreach (DataRowView row in view)
	{
		MenuItem NewMenuItem = new MenuItem(
			CleanMenuText(row["MENUDESCRIPTION"].ToString()),
			row["MENUITEMCODE"].ToString(),
			"",
			row["FORMNAME"].ToString()
		);
		parentMenuItem.ChildItems.Add(NewMenuItem);
		AddChildMenuItems(menuData, NewMenuItem);
	}
}
```

### TYP_MENUMASTER.cs

```csharp
// Retrieves menu data from database
public static DataTable GetMenuData(string pUserName)
{
	using (SqlCommand cmd = new SqlCommand("GetMenuData", con))
	{
		cmd.CommandType = CommandType.StoredProcedure;
		cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.VarChar, 50));
		cmd.Parameters["@USERNAME"].Value = pUserName;

		using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
		{
			sda.Fill(dtblMenu);
		}
	}
	return dtblMenu;
}
```

## Troubleshooting Decision Tree

```
Menu item not visible?
│
├─ Is user logged in?
│  ├─ NO → User must login first
│  └─ YES ↓
│
├─ Does user's role have access?
│  ├─ NO → Add role mapping in TYP_USER_ROLE_MENU_MASTER
│  └─ YES ↓
│
├─ Is GetMenuData returning the item?
│  ├─ NO → Check stored procedure logic
│  └─ YES ↓
│
├─ Is PARENTMENUITEM correct?
│  ├─ NO → Update to correct parent code
│  └─ YES ↓
│
└─ Clear browser cache/session and retry
```

## Best Practices

1. **Always test with multiple users**: Finance, Admin, Regular
2. **Verify both menu visibility AND direct URL access**
3. **Keep MENUITEMCODE unique** - use next available number
4. **Set MENULEVEL correctly** - 2 for submenu under top-level item
5. **Use relative paths** in FORMNAME - e.g., `Forms/DetailsForms/...`
6. **WHETHERLEAFMENU**: 'Y' for pages, 'N' for parent menus only
7. **Document all changes** - especially the MENUITEMCODE assigned

## Performance Considerations

- `GetMenuData` is called on EVERY page load for authenticated users
- Keep the stored procedure optimized
- Menu data can be cached if needed but requires cache invalidation strategy
- Recursive menu building (AddChildMenuItems) works well for reasonable menu depth (5-6 levels max)

## Related Files

- **View**: `MasterPage.master` - Menu1 control
- **Controller**: `MasterPage.master.cs` - Menu building logic
- **Model**: `TYP_MENUMASTER.cs` - Database access
- **Database**: SQL Server stored procedure `GetMenuData`
- **Security**: Role-based filtering at database and application level
