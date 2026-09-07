# TROUBLESHOOTING GUIDE - Organization Details Pages Not Showing

## Problem Summary
1. **404 Error**: Browser looking for pages in wrong location (`/Forms/DetailsForms/` instead of `/UserDetails/`)
2. **Menu Not Showing**: Admin user cannot see Payee Organization page in menu

---

## Root Cause
The database tables (MenuMaster and RoleForm) have **incorrect file paths** stored. 

### Wrong Paths (Currently in Database):
```
PayeeOrganizationDetails.aspx
SponsoringOrganizationDetails.aspx
```

### Correct Paths (Should be):
```
~/UserDetails/PayeeOrganizationDetails.aspx
~/UserDetails/SponsoringOrganizationDetails.aspx
```

---

## Solution Steps

### STEP 1: Run the SQL Fix Script
Execute: `BCASApplication/Database_Scripts/Fix_OrganizationDetails_MenuEntries.sql`

This script will:
1. Delete incorrect entries
2. Insert correct entries with proper paths
3. Verify the changes

### STEP 2: Important - Update ParentMenuItem Code
Before running the script, you need to find your **User Management** menu item code:

```sql
-- Run this query first
SELECT MenuItemCode, MenuDescription, FormName 
FROM [dbo].[MenuMaster] 
WHERE MenuDescription LIKE '%User%' 
   OR ParentMenuItem IS NULL
ORDER BY MenuLevel, MenuItemCode;
```

Look for the menu item that represents "User Management" or "User Details" section.

Then in the fix script, **replace line 47 and 60**:
```sql
-- Change this:
ParentMenuItem = 100,    -- CHANGE THIS!

-- To your actual parent menu code, for example:
ParentMenuItem = 25,     -- (use the code you found)
```

### STEP 3: Verify Database Structure

Check MenuMaster table should have these columns:
- `MenuItemCode` (int) - Primary Key
- `MenuDescription` (varchar) - Display name
- `MenuLevel` (int) - 1=top level, 2=submenu
- `WhetherLeafMenu` (char) - 'Y' or 'N'
- `ParentMenuItem` (int) - Parent menu code
- `FormName` (varchar) - Page path
- `Enter_By` (varchar)
- `Enter_Date` (datetime)

Check RoleForm table should have these columns:
- `RoleName` (varchar) - Role name (e.g., 'ADMINISTRATOR')
- `FormName` (varchar) - Page path
- `Enter_By` (varchar)
- `Enter_Date` (datetime)

### STEP 4: Clear Browser Cache
After updating database:
1. Close all browser tabs
2. Clear browser cache (Ctrl + Shift + Delete)
3. Restart browser
4. Login again

### STEP 5: Restart IIS / Application Pool
```powershell
# Run in PowerShell as Administrator
iisreset
```

Or restart your specific application pool in IIS Manager.

---

## Quick Verification Queries

### Check if pages exist in MenuMaster:
```sql
SELECT * FROM MenuMaster 
WHERE FormName LIKE '%OrganizationDetails%';
```

### Check if admin has access in RoleForm:
```sql
SELECT * FROM RoleForm 
WHERE RoleName = 'ADMINISTRATOR' 
  AND FormName LIKE '%OrganizationDetails%';
```

### Check all roles that have access:
```sql
SELECT RoleName, FormName 
FROM RoleForm 
WHERE FormName LIKE '%OrganizationDetails%'
ORDER BY RoleName;
```

---

## Expected Results

### For ADMINISTRATOR:
Should see BOTH pages in menu:
- ✅ Payee Organization Details
- ✅ Sponsoring Organization Details

### For PAYEEORGANISATION role:
Should see only:
- ✅ Payee Organization Details

### For SPONSORINGORGANISATION role:
Should see only:
- ✅ Sponsoring Organization Details

---

## Common Mistakes to Avoid

❌ **Wrong**: `PayeeOrganizationDetails.aspx`
❌ **Wrong**: `/UserDetails/PayeeOrganizationDetails.aspx`
❌ **Wrong**: `Forms/DetailsForms/PayeeOrganizationDetails.aspx`

✅ **Correct**: `~/UserDetails/PayeeOrganizationDetails.aspx`

The `~/` prefix is important - it resolves to the application root path.

---

## Still Not Working?

### Check 1: Verify Login Role
```sql
-- Check what role the admin user has
SELECT UserName, RoleName 
FROM aspnet_UsersInRoles ur
JOIN aspnet_Users u ON ur.UserId = u.UserId
JOIN aspnet_Roles r ON ur.RoleId = r.RoleId
WHERE UserName = 'admin';
```

### Check 2: Verify Menu Generation in MasterPage
The menu is generated in `MasterPage.master.cs` using:
```csharp
DataTable menudata = TYP_MENUMASTER.GetMenuData(UserName);
```

This looks up:
1. User's role(s) from ASP.NET membership
2. Forms allowed for that role from RoleForm table
3. Menu structure from MenuMaster table

### Check 3: Session Variables
After login, check if these session variables are set:
- `Session["UserId"]` - Username
- `Session["UserOrg"]` - Organization code
- `Session["OrgName"]` - Organization name

---

## Contact Support
If issues persist, provide:
1. Screenshot of MenuMaster entries for these pages
2. Screenshot of RoleForm entries for ADMINISTRATOR role
3. Output of the verification queries above
