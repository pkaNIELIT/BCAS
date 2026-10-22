# Quick Setup Guide: TaxInvoiceUpload.aspx

## Overview
This guide will help you add `TaxInvoiceUpload.aspx` to the Payment section menu in 5 minutes.

---

## Prerequisites
- ✅ File renamed to: `TaxInvoiceUpload.aspx`
- ✅ Code-behind: `TaxInvoiceUpload.aspx.cs`
- ✅ SQL Server Management Studio access
- ✅ Database admin rights

---

## Step-by-Step Setup (5 Steps)

### Step 1: Find Required Values (2 minutes)

Open SQL Server Management Studio and run these 3 queries:

**Query 1: Find next available menu code**
```sql
SELECT MAX(MENUITEMCODE) + 1 AS NextCode FROM TYP_MENUMASTER
```
📝 Write down the NextCode: **_______**

**Query 2: Find Payment section code**
```sql
SELECT MENUITEMCODE, MENUDESCRIPTION 
FROM TYP_MENUMASTER 
WHERE MENUDESCRIPTION LIKE '%Payment%' AND PARENTMENUITEM IS NULL
```
📝 Write down the Payment MENUITEMCODE: **_______**

**Query 3: Find Finance/Admin role IDs**
```sql
SELECT ROLE_ID, ROLE_NAME FROM TYP_ROLES 
WHERE ROLE_NAME LIKE '%Finance%' OR ROLE_NAME LIKE '%Admin%'
```
📝 Write down Finance ROLE_ID: **_______**
📝 Write down Admin ROLE_ID: **_______**

---

### Step 2: Update SQL Script (1 minute)

Open file: `Database_Scripts/Execute_This_Add_Menu.sql`

**Update these 4 lines at the top:**
```sql
DECLARE @NewMenuItemCode INT = 999;      -- Change 999 to your NextCode
DECLARE @PaymentParentCode INT = 50;     -- Change 50 to your Payment code
DECLARE @FinanceRoleID INT = 1;          -- Change 1 to your Finance role ID
DECLARE @AdminRoleID INT = 2;            -- Change 2 to your Admin role ID
```

---

### Step 3: Execute SQL Script (1 minute)

1. In SQL Server Management Studio
2. Open `Database_Scripts/Execute_This_Add_Menu.sql`
3. **Press F5** to execute
4. Check for success messages:
   - ✓ Menu item added successfully
   - ✓ Access granted to Finance role
   - ✓ Access granted to Admin role

---

### Step 4: Verify Database (30 seconds)

The script automatically shows verification results. You should see:

**MENU ITEM DETAILS:**
| MENUITEMCODE | MENUDESCRIPTION | ParentMenu | FORMNAME |
|--------------|-----------------|------------|----------|
| 999 | Tax Invoice Upload | Payment | Forms/DetailsForms/TaxInvoiceUpload.aspx |

**ROLE ACCESS:**
| ROLE_ID | ROLE_NAME | MENUDESCRIPTION |
|---------|-----------|-----------------|
| 1 | Finance | Tax Invoice Upload |
| 2 | Admin | Tax Invoice Upload |

---

### Step 5: Test in Application (1 minute)

**Test 1: Login as Finance/Admin user**
1. Login to BCAS application
2. Look for **Payment** menu
3. Click Payment → Should see **"Tax Invoice Upload"**
4. Click "Tax Invoice Upload" → Page should load

**Test 2: Login as regular user**
1. Login as non-Finance user
2. Payment menu should NOT show "Tax Invoice Upload"

---

## If Menu Doesn't Appear

### Quick Fixes:

1. **Clear browser cache**
   - Press Ctrl + F5 to hard refresh

2. **Check user's role**
   ```sql
   SELECT U.USER_NAME, R.ROLE_NAME
   FROM TYP_USERS U
   INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID
   INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
   WHERE U.USER_NAME = 'your_username'
   ```

3. **Re-login**
   - Logout completely
   - Close browser
   - Login again

4. **Verify the stored procedure**
   ```sql
   -- Test GetMenuData for your user
   EXEC GetMenuData @USERNAME = 'your_username'
   ```
   - Look for 'Tax Invoice Upload' in results

---

## File Locations

### Application Files (Already exist - no changes needed)
```
BCASApplication/
└── Forms/
	└── DetailsForms/
		├── TaxInvoiceUpload.aspx          ✅
		└── TaxInvoiceUpload.aspx.cs       ✅
```

### SQL Scripts (Created for you)
```
Database_Scripts/
├── Execute_This_Add_Menu.sql              ⭐ Run this one!
├── Add_TaxInvoiceUpload_Menu_Item.sql     (Detailed version)
└── Quick_Reference_Queries.sql            (Troubleshooting)
```

### Documentation (For reference)
```
Documentation/
├── Quick_Setup_Guide.md                   📖 This file
├── Add_TaxInvoiceUpload_To_Menu_Guide.md  (Full guide)
├── Implementation_Checklist.md            (Detailed checklist)
└── Menu_System_Architecture.md            (System details)
```

---

## Common Issues & Solutions

### Issue 1: "Menu item already exists"
**Solution:** Run this to check:
```sql
SELECT * FROM TYP_MENUMASTER 
WHERE FORMNAME = 'Forms/DetailsForms/TaxInvoiceUpload.aspx'
```
If it exists, just grant role access using queries from Step 3 & 4 in the script.

### Issue 2: "Invalid object name 'TYP_USER_ROLE_MENU_MASTER'"
**Solution:** Your table might have a different name. Check:
```sql
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME LIKE '%MENU%' AND TABLE_NAME LIKE '%ROLE%'
```

### Issue 3: Page loads but shows blank
**Solution:** Check the page code-behind has proper initialization in `Page_Load`.

### Issue 4: "Access Denied" even for Finance users
**Solution:** 
1. Verify role mapping:
   ```sql
   SELECT * FROM TYP_USER_ROLE_MENU_MASTER 
   WHERE MENUITEMCODE = [your menu code]
   ```
2. Check user has Finance/Admin role:
   ```sql
   SELECT U.USER_NAME, R.ROLE_NAME
   FROM TYP_USERS U
   INNER JOIN TYP_USER_ROLES UR ON U.USER_ID = UR.USER_ID  
   INNER JOIN TYP_ROLES R ON UR.ROLE_ID = R.ROLE_ID
   WHERE U.USER_NAME = 'test_user'
   ```

---

## Success Checklist

- [ ] SQL script executed successfully
- [ ] Verification queries show menu item exists
- [ ] Finance role has access
- [ ] Admin role has access
- [ ] Finance user can see menu item
- [ ] Finance user can open page
- [ ] Regular user cannot see menu item
- [ ] Page functions correctly

---

## Need More Help?

**Detailed Documentation:**
- `Documentation/Add_TaxInvoiceUpload_To_Menu_Guide.md` - Complete guide
- `Documentation/Menu_System_Architecture.md` - System architecture
- `Documentation/Implementation_Checklist.md` - Full checklist

**Troubleshooting:**
- `Database_Scripts/Quick_Reference_Queries.sql` - All diagnostic queries

**Support:**
1. Check existing Payment menu items for examples
2. Review `PayeeOrganisationPayment.aspx` as reference
3. Test with known Finance/Admin account first

---

## Completed! 🎉

Once you see the menu item and can access the page:
- ✅ Implementation complete
- ✅ Ready for production use
- 📝 Document the MENUITEMCODE used: **_______**

---

**Last Updated:** December 2024
**File:** TaxInvoiceUpload.aspx
