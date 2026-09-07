<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="frmPhotoSignUpload.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_frmPhotoSignUpload" Title="BCAS-->PhotoSignUpload" meta:resourcekey="PageResource2" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .style1
        {
            text-align: left;
            width: 0%;
            vertical-align:top;
        }
        .style2
        {
            width: 45%;
        }
        .style3
        {
            height: 34px;
        }
        .style4
        {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" align="center" width="100%" height="100%">
        <tr align="center">
            <td valign="top" align="center">
                <table border="0" align="center" width="90%" height="100%">
                    <tr align="center">
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="7" width="90%" bordercolor="#ECC4B9"
                                bgcolor="#FBEBE6" align="center">
                                <tr>
                                    <td align="Right"><b>Step 2
                                    </b>
                                    </td></tr>
                                <tr>
                                    <td align="center">
                                        <strong><font color="#990000">Online Application For BCAS</font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr color="#990000" size="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    <strong><font color="#990000">Kindly keep the Registration Number for future reference. </font></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    <table style="border: thin groove #993366" width="100%" style="text-align: right" cellpadding ="5" cellspacing ="0">
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_application_no" runat="server" meta:resourcekey="lbl_application_no"></asp:Label>
                                                    </td>
                                                    <td class="style1" bgcolor="#CCFFCC">
                                        <asp:Label ID="lbl_Apl_no" runat="server"
                                                            Width="139px"  
                                            EnableTheming="False" Font-Bold="True" meta:resourcekey="lbl_Apl_noResource1"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1" rowspan="6" >
                                                    
                                <asp:Image ID="Img_Photo" runat="server" Width="120px" Height="150px" 
                                    meta:resourcekey="Image1Resource1" /><br /><br />
                                     <asp:Image ID="ImgSign" runat="server" Width="120px" Height="50px" 
                                    meta:resourcekey="Image1Resource1" /><br />
                                                    
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Candidate_Name" runat="server" 
                                            meta:resourcekey="lbl_Candidate_Name"></asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
                                        <asp:Label ID="lbl_Name" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <%--deep--%>
                                             
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_OrganisationNameofCandidate" runat="server" 
                                        meta:resourcekey="lbl_OrganisationNameofCandidate">
                                       
                                           </asp:Label>
                                                    </td>
                                                    <td width="55%" class="style1">
                                        <asp:Label ID="lbl_OrganisationName" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            
                                            <%--deep--%>
                                            
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Designation" runat="server" 
                                            meta:resourcekey="lbl_Father_Name"></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                        <asp:Label ID="lbl_valDesignation" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Date_of_birth" runat="server" 
                                            meta:resourcekey="lbl_Date_of_birth"></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                        <asp:Label ID="lbl_dob" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                            <tr>
                                                    <td style="text-align: right; vertical-align:top" class="style2">
                                        <asp:Label ID="lbl_Organisation" runat="server" 
                                            meta:resourcekey="lbl_Category"></asp:Label>
                                                    </td>
                                                    <td class="style1">
                                        <asp:Label ID="lbl_valOrg" runat="server" meta:resourcekey="Label4Resource1" 
                                            EnableTheming="False" Font-Bold="True"></asp:Label>
                                                    </td>
                                            </tr>
                                             <tr>
                                                    <td align="left" colspan="2" ><font color="#990000" size="2"> 
                                                        <span class="style4">Note: </span>If you have not uploaded images or you are not satisfied with the photograph & Signature Images shown, you can upload images. </font></td>
                                                </tr>
                                         
                                          <tr align="center" >
                                                    <td colspan="3"><b>Photo Identity Card</b><br />                                                    
                                <asp:Image ID="Img_PhotoICard" runat="server" Width="275px" Height="175px" 
                                    meta:resourcekey="Image1Resource1" /></td>
                                                </tr>
                                         
                                          <tr>
                                                    <td align="left" colspan="3" bgcolor="#cccccc">
                                                        Upload Images                                                     </td>
                                                </tr>
                                            <tr bgcolor="#ffcccc" id="R_Img" runat ="server" >
                                                    <td style="text-align: left" class="style2">
                                        <asp:Label ID="lbl_Upload_Photo" runat="server" 
                                            meta:resourcekey="lbl_Upload_Photo"></asp:Label>
                                                    </td>
                                                    <td width="55%" style="text-align: left; width: 75%;" colspan="2" 
                                                        valign="middle" >
                                                        <asp:FileUpload ID="fupload_image" runat="server" TabIndex="1" 
                                            meta:resourcekey="fupload_imageResource1" />
                                                        <asp:Button ID="btn_photo_upload" runat="server" meta:resourcekey="btn_photo_upload"
                                                            OnClick="btn_photo_upload_Click" EnableTheming ="False" Width="165px" 
                                                            TabIndex="2"/>
                                                       
                                                        <asp:Label ID="lbl_fMsg" runat="server" ForeColor="Blue" Visible="False" 
                                                            meta:resourcekey="lbl_fMsgResource1"></asp:Label>
                                                     
                                                     <%--<br />
                                <u>Note 2</u>:Application will be treated as incomplete/rejected unless accompanied by Payment of Application Fee</font>--%>                                            
                                            
                                                    </td>
                                            </tr>
                                            <tr bgcolor="#ffcccc" id="R_Sign" runat ="server">
                                                    <td style="text-align: left" class="style2">
                                    <asp:Label ID="lbl_Signature_Upload" runat="server" 
                                            meta:resourcekey="lbl_Signature_Upload"></asp:Label>
                                                    </td>
                                                    <td width="55%" style="text-align: left; width: 75%;" colspan="2" 
                                                        valign="middle">
                                                        <asp:FileUpload ID="fupload_signature" runat="server" 
                                            TabIndex="3" meta:resourcekey="fupload_signatureResource1" />
                                                        <asp:Button ID="btn_sign_upload" runat="server" meta:resourcekey="btn_sign_upload"
                                                            OnClick="btn_sign_upload_Click" 
                                            EnableTheming ="False" Width="165px" TabIndex="4" />
                                                        <asp:Label ID="lbl_SMsg" runat="server" ForeColor="Blue" Visible="False" 
                                                            meta:resourcekey="lbl_SMsgResource1"></asp:Label>
                                                        <%--<asp:Image ID="imgSignature" runat="server" Height="30px" Visible="false" 
                                                            Width="70px" />--%>
                                                    </td>
                                            </tr>                                           
                                            <tr bgcolor="#ffcccc" id="R_Icard" runat ="server">
                                                    <td style="text-align: left" class="style2">
                                    <asp:Label ID="lbl_ICard_Upload" runat="server" Text="Upload Your Scanned Photo ID Card" 
                                                            meta:resourcekey="lbl_ICard_UploadResource1"></asp:Label>
                                                    </td>
                                                    <td width="55%" style="text-align: left; width: 75%;" colspan="2" 
                                                        valign="middle">
                                                        <asp:FileUpload ID="fupload_Icard" runat="server" 
                                            TabIndex="5" meta:resourcekey="fupload_signatureResource1" />
                                                        <asp:Button ID="btn_Icard_upload" runat="server" 
                                                             
                                            EnableTheming ="False" Width="165px" TabIndex="6" onclick="btn_Icard_upload_Click" 
                                                            Text="Upload ID Card" meta:resourcekey="btn_Icard_uploadResource1" />
                                                        <asp:Label ID="lbl_IcardMsg" runat="server" ForeColor="Blue" Visible="False" 
                                                            meta:resourcekey="lbl_IcardMsgResource1"></asp:Label>
                                                    </td>
                                            </tr>                                           
                                     <tr><td colspan="2" align="left"><font color="#990000" size="2"> <u>Note </u>:Images should be in JPEG/JPG format and size should be less than 
                                         30KB</font> </td></tr>
                                               
                                    </table>
                                       </td>
                                </tr>
                                 
                                <tr>
                                    <td align="left" style="text-align: center">
                                        <asp:Label ID="lbl_ErrorMsg" runat="server" ForeColor="Red" Font-Size="Large" 
                                            meta:resourcekey="lbl_ErrorMsgResource1"></asp:Label>
                                    </td>
                                </tr>
                                 
                                <tr><td align="center" class="style3"> 
                                        
                                        <asp:Button ID="btn_save_continue" runat="server"  
                                            meta:resourcekey="btn_save_continue" onclick="btn_save_continue_Click" 
                                            TabIndex="7"  />
                                            &nbsp;</td></tr>
                                <tr><td><font color="#990000" size="2"><u>Note </u>:Please note down Registration No. given above, which will be required for further interaction with BCAS.
                                <%--<br />
                                <u>Note 2</u>:Application will be treated as incomplete/rejected unless accompanied by Payment of Application Fee</font>--%></td></tr>
                                <tr>
                                    <td align="center">
                                        
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

