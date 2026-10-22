# Implementation Checklist: Add Tax Invoice Upload to Payment Menu

## Pre-Implementation
- [ ] Backup the database before making any changes
- [ ] Identify a test Finance/Admin user account for testing
- [ ] Verify the page files exist:
  - [ ] `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
  - [ ] `BCASApplication/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx.cs`

## Database Discovery
- [ ] Run discovery queries to understand current menu structure
- [ ] Find the Payment section parent menu item code
  - Payment Parent MENUITEMCODE: _____________
- [ ] Find the next available MENUITEMCODE
  - Next Available Code: _____________
- [ ] Identify Finance/Admin role IDs
  - Finance Role ID: _____________
  - Admin Role ID: _____________

## Database Changes
- [ ] Update the SQL script with discovered values
  - [ ] Set `@NewMenuItemCode` variable
  - [ ] Set `@PaymentParentCode` variable
- [ ] Execute `Add_TaxInvoiceUpload_Menu_Item.sql`
- [ ] Verify menu item was added (run verification query)
- [ ] Grant access to Finance role
- [ ] Grant access to Admin role
- [ ] Verify role access was granted

## Application Testing

### Test 1: Menu Visibility - Finance/Admin User
- [ ] Login as Finance user
- [ ] Navigate to Payment section in menu
- [ ] Verify "Tax Invoice Upload" menu item appears
- [ ] Click "Tax Invoice Upload" menu item
- [ ] Verify page loads successfully
- [ ] Test page functionality

### Test 2: Menu Visibility - Regular User
- [ ] Login as regular (non-Finance/Admin) user
- [ ] Navigate to Payment section (if accessible)
- [ ] Verify "Tax Invoice Upload" menu item does NOT appear

### Test 3: Direct Access - Finance/Admin User
- [ ] Login as Finance user
- [ ] Navigate directly to URL:
  `/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
- [ ] Verify page loads successfully
- [ ] Verify no errors in browser console

### Test 4: Direct Access - Regular User
- [ ] Login as regular user
- [ ] Navigate directly to URL:
  `/Forms/DetailsForms/CourseWisePaymentDetails_TaxInvoiceUpload.aspx`
- [ ] Verify user is redirected or shown access denied

### Test 5: Session Validation
- [ ] Login as Finance user
- [ ] Open browser developer tools (F12)
- [ ] Navigate to the page
- [ ] Verify no session errors in console
- [ ] Check that page name exists in `Session["Forms"]`

## Code Review (Optional but Recommended)
- [ ] Review `CourseWisePaymentDetails_TaxInvoiceUpload.aspx.cs`
- [ ] Verify `Page_Load` has proper authorization checks:
  - [ ] Session["UserId"] validation
  - [ ] Role-based authorization
  - [ ] Redirect logic for unauthorized access
- [ ] Check for any hardcoded dependencies that might cause issues

## Post-Implementation Verification

### Database Verification
- [ ] Run verification queries from `Quick_Reference_Queries.sql`
- [ ] Confirm menu hierarchy is correct
- [ ] Verify no orphaned or duplicate entries
- [ ] Check that all Finance/Admin users have access

### Application Verification
- [ ] Test with multiple Finance users
- [ ] Test with multiple Admin users  
- [ ] Test with multiple regular users (should not see menu)
- [ ] Test page functionality end-to-end
- [ ] Check browser compatibility (IE 9+, Chrome, Firefox)

### Performance Check
- [ ] Verify menu loads quickly for all users
- [ ] Check that `GetMenuData` stored procedure performs well
- [ ] Monitor for any timeout issues

## Documentation
- [ ] Update application documentation with new menu item
- [ ] Document the MENUITEMCODE used: _____________
- [ ] Note any custom configurations or special requirements
- [ ] Update user manual (if applicable)

## Rollback Plan (If Needed)
- [ ] Document menu item details for removal:
  - MENUITEMCODE: _____________
  - SQL to remove role access: 
	```sql
	DELETE FROM TYP_USER_ROLE_MENU_MASTER WHERE MENUITEMCODE = [code]
	```
  - SQL to remove menu item:
	```sql
	DELETE FROM TYP_MENUMASTER WHERE MENUITEMCODE = [code]
	```
- [ ] Keep database backup accessible for restoration

## Sign-Off
- [ ] Implementation completed by: _________________ Date: _________
- [ ] Testing completed by: _________________ Date: _________
- [ ] Approved by: _________________ Date: _________

## Notes and Issues
(Record any issues encountered and resolutions)

Issue 1:
- Description: 
- Resolution: 

Issue 2:
- Description: 
- Resolution: 

## Additional Comments



---
**Implementation Status**: 
- [ ] Not Started
- [ ] In Progress
- [ ] Completed
- [ ] Verified
- [ ] Production Deployment

**Final Date**: _____________
