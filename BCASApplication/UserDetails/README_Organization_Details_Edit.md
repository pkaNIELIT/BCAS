# Organization Details Pages - Edit Functionality

## Overview
Two web forms pages have been successfully created and enhanced with Edit functionality in the `BCASApplication/UserDetails` folder:
1. **Payee Organization Details**
2. **Sponsoring Organization Details**

## Features Implemented

### 1. View Organization Details
- Both pages display organization details in a GridView with pagination (20 records per page)
- Data is filtered based on the logged-in user's organization (from Session["UserOrg"])
- Session validation ensures only authenticated users can access the pages

### 2. Edit Organization Details
- **Edit Button**: Each row has an "Edit" link button
- **Inline Editing**: Clicking "Edit" converts the row into editable text boxes
- **Update/Cancel**: When in edit mode, the Edit button changes to "Update" and "Cancel" buttons
- **All users can edit**: Payee organizations, Sponsoring organizations, and Admins can all edit any columns

### 3. Editable Columns

#### Payee Organization Details:
- Payee Organisation Name
- Payee Organisation State
- Payee Organisation Address
- Location Pincode
- GST Number
- State Code

**Read-Only**: Payee Organisation Code (Primary Key)

#### Sponsoring Organization Details:
- Sponsoring Organisation Name
- Sponsoring Organisation State
- Sponsoring Organisation Address
- GST Number
- State Code

**Read-Only**: Sponsoring Organisation Code (Primary Key)

## Files Created/Modified

### ASPX Pages
1. `BCASApplication/UserDetails/PayeeOrganizationDetails.aspx`
   - GridView with DataKeyNames="Payee_Organisation_Code"
   - CommandField with ShowEditButton="True"
   - Event handlers: OnRowEditing, OnRowUpdating, OnRowCancelingEdit

2. `BCASApplication/UserDetails/SponsoringOrganizationDetails.aspx`
   - GridView with DataKeyNames="Sponsoring_Organisation_Code"
   - CommandField with ShowEditButton="True"
   - Event handlers: OnRowEditing, OnRowUpdating, OnRowCancelingEdit

### Code-Behind Files
1. `BCASApplication/UserDetails/PayeeOrganizationDetails.aspx.cs`
   - `Page_Load`: Session validation and grid binding
   - `BindPayeeOrganizationGrid()`: Calls GetPayeeOrganizationDetails stored procedure with org filter
   - `grdPayeeOrganizationDetails_RowEditing()`: Enters edit mode
   - `grdPayeeOrganizationDetails_RowUpdating()`: Updates data via UpdatePayeeOrganizationDetails SP
   - `grdPayeeOrganizationDetails_RowCancelingEdit()`: Cancels edit mode
   - `grdPayeeOrganizationDetails_PageIndexChanging()`: Handles pagination

2. `BCASApplication/UserDetails/SponsoringOrganizationDetails.aspx.cs`
   - Same structure as Payee Organization Details but for Sponsoring Organizations
   - Calls UpdateSponsoringOrganizationDetails stored procedure

### Database Scripts
File: `BCASApplication/Database_Scripts/Update_Organization_Details_StoredProcedures.sql`

#### Stored Procedures Created:

1. **UpdatePayeeOrganizationDetails**
   ```sql
   Parameters:
   - @Payee_Organisation_Code (Primary Key)
   - @Payee_Organisation_Name
   - @Payee_Organisation_State
   - @Payee_Address
   - @Location_Pincode
   - @GST_NUMBER
   - @State_Code
   - @Modified_By (Logged-in user from Session)

   Updates: PayeeOrganisation table
   Sets: Modified_Date = GETDATE(), Modified_By = @Modified_By
   ```

2. **UpdateSponsoringOrganizationDetails**
   ```sql
   Parameters:
   - @Sponsoring_Organisation_Code (Primary Key)
   - @Sponsoring_Organisation_Name
   - @Sponsoring_Organisation_State
   - @Sponsoring_Address
   - @GST_NUMBER
   - @State_Code
   - @Modified_By (Logged-in user from Session)

   Updates: Organisation table
   Sets: Modified_Date = GETDATE(), Modified_By = @Modified_By
   ```

## How to Deploy

### Step 1: Run Database Script
Execute the SQL script to create the update stored procedures:
```sql
-- Run this script in SQL Server Management Studio
BCASApplication/Database_Scripts/Update_Organization_Details_StoredProcedures.sql
```

### Step 2: Verify the pages are in place
The following files should already be in your workspace:
- BCASApplication/UserDetails/PayeeOrganizationDetails.aspx
- BCASApplication/UserDetails/PayeeOrganizationDetails.aspx.cs
- BCASApplication/UserDetails/SponsoringOrganizationDetails.aspx
- BCASApplication/UserDetails/SponsoringOrganizationDetails.aspx.cs

### Step 3: Build and Deploy
1. Build the solution (already successful)
2. If deploying to IIS, copy the files to the web server directory
3. Recycle the application pool or restart IIS

### Step 4: Verify Menu Access
Ensure the menu entries exist in MenuMaster and RoleForm tables for appropriate user roles.

## How It Works

### User Flow:
1. User logs in (Session["UserId"] and Session["UserOrg"] are set)
2. User navigates to either Payee or Sponsoring Organization Details page
3. Page loads and displays organization details filtered by user's organization
4. User clicks "Edit" on any row
5. Row converts to editable text boxes
6. User modifies the values
7. User clicks "Update" to save changes OR "Cancel" to discard
8. Success/error message is displayed
9. Grid refreshes with updated data

### Security:
- Session validation on every page load
- User redirected to Home.aspx if not authenticated
- Modified_By field tracks who made the change
- Modified_Date automatically set to current timestamp
- Primary key (Organization Code) is read-only

### Error Handling:
- Try-catch blocks in all event handlers
- Friendly error messages displayed to users
- SQL errors captured and reported
- Transaction safety in stored procedures

## Testing Checklist

- [ ] Login as Payee Organization user
  - [ ] Verify Payee Organization Details page loads correctly
  - [ ] Verify only your organization's data is displayed
  - [ ] Click Edit and verify row enters edit mode
  - [ ] Modify values and click Update
  - [ ] Verify success message and data refresh
  - [ ] Try Cancel button

- [ ] Login as Sponsoring Organization user
  - [ ] Verify Sponsoring Organization Details page loads correctly
  - [ ] Verify only your organization's data is displayed
  - [ ] Click Edit and verify row enters edit mode
  - [ ] Modify values and click Update
  - [ ] Verify success message and data refresh

- [ ] Login as Admin user
  - [ ] Verify both pages are accessible
  - [ ] Verify all organizations' data is displayed (if admin has no org filter)
  - [ ] Edit any organization's details
  - [ ] Verify updates work correctly

- [ ] Test pagination with Edit mode
  - [ ] Enter edit mode on a row
  - [ ] Click pagination
  - [ ] Verify edit mode is cancelled

## Notes

1. **Organization Code is Read-Only**: The primary key cannot be edited to maintain data integrity
2. **Session-Based Filtering**: Only displays organization data relevant to the logged-in user
3. **Audit Trail**: Modified_By and Modified_Date fields track all changes
4. **Build Status**: ✅ Build successful
5. **Database Assumptions**: 
   - PayeeOrganisation table has columns: Payee_Organisation_Code, Payee_Organisation_Name, Payee_Organisation_State, Payee_Address, Location_Pincode, GST_NUMBER, State_Code, Modified_Date, Modified_By
   - Organisation table has columns: Sponsoring_Organisation_Code, Sponsoring_Organisation_Name, Sponsoring_Organisation_State, Sponsoring_Address, GST_NUMBER, State_Code, Modified_Date, Modified_By

## Troubleshooting

### Page returns 404 error
- Verify the files exist in the deployed IIS directory (not just your source folder)
- Recycle IIS application pool or restart IIS
- Check if the pages are included in the project/website

### "No Sponsoring/Payee Organization details found"
- Verify Session["UserOrg"] is set correctly after login
- Check if the stored procedure returns data for that organization code
- Verify the organization exists in the database

### Update fails silently
- Check if UpdatePayeeOrganizationDetails or UpdateSponsoringOrganizationDetails stored procedures exist
- Verify the user has execute permissions on these stored procedures
- Check database connection string in web.config

### Edit button doesn't appear
- Verify GridView has DataKeyNames attribute set
- Verify CommandField ShowEditButton="True" is present
- Check if OnRowEditing event handler is wired up correctly

## Future Enhancements (Optional)

- Add validation for GST Number format
- Add validation for Pincode (numeric, 6 digits)
- Add confirmation dialog before update
- Add Delete functionality (if required)
- Add bulk edit/import functionality
- Add audit log page to view all modifications
- Add email notifications on organization details changes
