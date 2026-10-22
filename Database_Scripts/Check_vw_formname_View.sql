-- =============================================
-- FOUND THE ISSUE: The stored procedure uses vw_formname view
-- This view needs to be checked
-- =============================================

-- STEP 1: Check what vw_formname contains for your user
SELECT * FROM vw_formname
WHERE username = 'admin'
ORDER BY formname;

-- STEP 2: Look specifically for TaxInvoiceUpload
SELECT * FROM vw_formname
WHERE username = 'admin' 
  AND formname LIKE '%TaxInvoiceUpload%';

-- STEP 3: View the vw_formname view definition
EXEC sp_helptext 'vw_formname';

-- STEP 4: Check if the issue is with the FormName format
-- The stored procedure uses this complex logic:
-- substring(FormName, len(FormName) - case when charindex('/',reverse(FormName))=0 then len(FormName)-1 else charindex('/',reverse(FormName)) - 2 end, len(FormName))
-- This extracts just the filename from the path

-- Let's test what filename is being extracted from our FormName:
DECLARE @FormName VARCHAR(200) = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx';
SELECT 
	@FormName AS FullPath,
	substring(@FormName, 
		len(@FormName) - 
		case when charindex('/',reverse(@FormName))=0 
			then len(@FormName)-1 
			else charindex('/',reverse(@FormName)) - 2 
		end, 
		len(@FormName)) AS ExtractedFileName;

-- This should return: TaxInvoiceUpload.aspx

-- STEP 5: Check if TaxInvoiceUpload.aspx exists in vw_formname
SELECT * FROM vw_formname
WHERE username = 'admin'
  AND formname = 'TaxInvoiceUpload.aspx';

-- STEP 6: If the view doesn't show it, check the underlying data
SELECT 
	U.username,
	R.RoleName,
	RF.FormName,
	-- Extract just filename
	substring(RF.FormName, 
		len(RF.FormName) - 
		case when charindex('/',reverse(RF.FormName))=0 
			then len(RF.FormName)-1 
			else charindex('/',reverse(RF.FormName)) - 2 
		end, 
		len(RF.FormName)) AS ExtractedFileName
FROM RoleForm RF
INNER JOIN TYP_ROLES R ON RF.RoleName = R.ROLE_NAME  -- Adjust table/column names as needed
INNER JOIN TYP_USER_ROLES UR ON R.ROLE_ID = UR.ROLE_ID
INNER JOIN TYP_USERS U ON UR.USER_ID = U.USER_ID
WHERE RF.FormName = '~/Forms/DetailsForms/TaxInvoiceUpload.aspx'
  AND U.username = 'admin';
