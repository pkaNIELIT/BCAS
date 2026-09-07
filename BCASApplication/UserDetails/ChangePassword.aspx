<%@ Page Title="ChangePassword" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="UserDetails_ChangePassword" culture="en-GB"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BCASClassLibrary" Namespace="BCASClassLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <%--<script src="../JScript/Convert.js" type="text/javascript"></script>--%>

<script type="text/javascript">

    function SHA1(msg) {

        function rotate_left(n, s) {
            var t4 = (n << s) | (n >>> (32 - s));
            return t4;
        };

        function lsb_hex(val) {
            var str = "";
            var i;
            var vh;
            var vl;

            for (i = 0; i <= 6; i += 2) {
                vh = (val >>> (i * 4 + 4)) & 0x0f;
                vl = (val >>> (i * 4)) & 0x0f;
                str += vh.toString(16) + vl.toString(16);
            }
            return str;
        };

        function cvt_hex(val) {
            var str = "";
            var i;
            var v;

            for (i = 7; i >= 0; i--) {
                v = (val >>> (i * 4)) & 0x0f;
                str += v.toString(16);
            }
            return str;
        };


        function Utf8Encode(string) {
            string = string.replace(/\r\n/g, "\n");
            var utftext = "";

            for (var n = 0; n < string.length; n++) {

                var c = string.charCodeAt(n);

                if (c < 128) {
                    utftext += String.fromCharCode(c);
                }
                else if ((c > 127) && (c < 2048)) {
                    utftext += String.fromCharCode((c >> 6) | 192);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
                else {
                    utftext += String.fromCharCode((c >> 12) | 224);
                    utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                    utftext += String.fromCharCode((c & 63) | 128);
                }

            }

            return utftext;
        };

        var blockstart;
        var i, j;
        var W = new Array(80);
        var H0 = 0x67452301;
        var H1 = 0xEFCDAB89;
        var H2 = 0x98BADCFE;
        var H3 = 0x10325476;
        var H4 = 0xC3D2E1F0;
        var A, B, C, D, E;
        var temp;

        msg = Utf8Encode(msg);

        var msg_len = msg.length;

        var word_array = new Array();
        for (i = 0; i < msg_len - 3; i += 4) {
            j = msg.charCodeAt(i) << 24 | msg.charCodeAt(i + 1) << 16 |
		msg.charCodeAt(i + 2) << 8 | msg.charCodeAt(i + 3);
            word_array.push(j);
        }

        switch (msg_len % 4) {
            case 0:
                i = 0x080000000;
                break;
            case 1:
                i = msg.charCodeAt(msg_len - 1) << 24 | 0x0800000;
                break;

            case 2:
                i = msg.charCodeAt(msg_len - 2) << 24 | msg.charCodeAt(msg_len - 1) << 16 | 0x08000;
                break;

            case 3:
                i = msg.charCodeAt(msg_len - 3) << 24 | msg.charCodeAt(msg_len - 2) << 16 | msg.charCodeAt(msg_len - 1) << 8 | 0x80;
                break;
        }

        word_array.push(i);

        while ((word_array.length % 16) != 14) word_array.push(0);

        word_array.push(msg_len >>> 29);
        word_array.push((msg_len << 3) & 0x0ffffffff);


        for (blockstart = 0; blockstart < word_array.length; blockstart += 16) {

            for (i = 0; i < 16; i++) W[i] = word_array[blockstart + i];
            for (i = 16; i <= 79; i++) W[i] = rotate_left(W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16], 1);

            A = H0;
            B = H1;
            C = H2;
            D = H3;
            E = H4;

            for (i = 0; i <= 19; i++) {
                temp = (rotate_left(A, 5) + ((B & C) | (~B & D)) + E + W[i] + 0x5A827999) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 20; i <= 39; i++) {
                temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0x6ED9EBA1) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 40; i <= 59; i++) {
                temp = (rotate_left(A, 5) + ((B & C) | (B & D) | (C & D)) + E + W[i] + 0x8F1BBCDC) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            for (i = 60; i <= 79; i++) {
                temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0xCA62C1D6) & 0x0ffffffff;
                E = D;
                D = C;
                C = rotate_left(B, 30);
                B = A;
                A = temp;
            }

            H0 = (H0 + A) & 0x0ffffffff;
            H1 = (H1 + B) & 0x0ffffffff;
            H2 = (H2 + C) & 0x0ffffffff;
            H3 = (H3 + D) & 0x0ffffffff;
            H4 = (H4 + E) & 0x0ffffffff;

        }

        var temp = cvt_hex(H0) + cvt_hex(H1) + cvt_hex(H2) + cvt_hex(H3) + cvt_hex(H4);

        return temp.toUpperCase();
    }

    function SpPwd() {

        if (document.getElementById("<%=txt_OldPassword.ClientID %>").value != "") {
            var e1 = document.getElementById("<%=txt_OldPassword.ClientID%>").value;
            document.getElementById("<%=txt_OldPassword.ClientID%>").value = SHA1(e1);

            var e2 = document.getElementById("<%=txt_Password.ClientID%>").value;
            document.getElementById("<%=txt_Password.ClientID%>").value = SHA1(e2);


            var e3 = document.getElementById("<%=txt_ConfirmPassword.ClientID%>").value;
            document.getElementById("<%=txt_ConfirmPassword.ClientID%>").value = SHA1(e3);
        }
    }

    </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <table border="0" width="80%" align="center" bgcolor="white" valign="top" border="10"
        cellspacing="0">
        <tr align="center">
            <td>
                <table width="70%" align="center" border="6" bordercolor="#CEEDEE">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lbl_Heading" runat="server" Width="350px" meta:resourcekey="lbl_Heading"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 87px; height: 16px" align="left">
                            <asp:Label ID="lbl_OldPassword" runat="server" Text="Current Password"></asp:Label>
                        </td>
                        <td style="width: 131px; height: 16px" align="left">
                            <%--<asp:TextBox ID="txt_OldPassword" runat="server" TextMode="Password" meta:resourcekey="txt_OldPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_OldPassword"
                                ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>--%>
                                
                                 <asp:TextBox ID="txt_OldPassword" runat="server"  TextMode="Password"
                                meta:resourcekey="txtusernameResource1" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_OldPassword"
                                ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="avbl2" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbl_Password" runat="server"  Text="New Password"></asp:Label>
                        </td>
                        <td align="left">
                            <%--<asp:TextBox ID="txt_Password" runat="server" MaxLength="50" TextMode="Password"
                               ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCuurentPwd" runat="server" ControlToValidate="txt_Password"
                                ErrorMessage="Password Must be Min 8 Chars, at least one uppercase letter, one lowercase letter, one number and one special character only."
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}" ></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Password"
                                ErrorMessage="*" ></asp:RequiredFieldValidator>--%>
                                
                                 <asp:TextBox ID="txt_Password" runat="server" MaxLength="50" TextMode="Password" Text="anita"
                                meta:resourcekey="txt_PasswordResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txt_Password" ErrorMessage="*" 
                                meta:resourcekey="RequiredFieldValidator2Resource1" ValidationGroup="avbl2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revCuurentPwd" runat="server" Font-Size="Smaller"
                                ControlToValidate="txt_Password" 
                                ErrorMessage="Password having Length of min. 8 character, atleast 01 Uppercase, 01 Lowercase, 01 number & 01 special character." 
                                meta:resourcekey="revCuurentPwdResource1" 
                                ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$"></asp:RegularExpressionValidator>
                                
                               
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbl_ConfirmPassword" runat="server" Text=" Confirm Password"></asp:Label>
                        </td>
                        <td align="left" class="style8">
                            <%--<asp:TextBox ID="txt_ConfirmPassword" runat="server" TextMode="Password" meta:resourcekey="txt_ConfirmPasswordResource1"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ConfirmPassword"
                                ErrorMessage="*" ></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_Password"
                                ControlToValidate="txt_ConfirmPassword" ErrorMessage="Password and Confirm Password must match"
                                ></asp:CompareValidator>--%>
                                
                                 <asp:TextBox ID="txt_ConfirmPassword" runat="server" TextMode="Password" meta:resourcekey="txt_ConfirmPasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ConfirmPassword"
                                ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1" ValidationGroup="avbl2"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_Password"
                                ControlToValidate="txt_ConfirmPassword" ErrorMessage="New Password and Confirm Password must match"
                                meta:resourcekey="CompareValidator1Resource1" ValidationGroup="avbl2"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <asp:Label ID="lbl_ImageCode" runat="server" Text=" Image Verification" Style="text-align: left"></asp:Label>
                        </td>
                        <td valign="top" style="width: 251px; height: 16px" align="left">
                            <%--<cc1:ImageVerifier ID="ImageVerifier1" runat="server" meta:resourcekey="ImageVerifier1Resource1" />--%>
                            <cc2:ImageVerifier ID="ImageVerifier1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style6">
                            <asp:Label ID="lbl_ImgVerifyCode" runat="server" Text=" Verification Code"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txt_ImgVerifyCode" runat="server" meta:resourcekey="txt_ImgVerifyCodeResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_ImgVerifyCode"
                                ErrorMessage="*" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style6">
                            &nbsp;
                        </td>
                        <td align="left" valign="top">
                            <asp:Button ID="btn_ChangePassword" runat="server"  Text="Change Password"  ValidationGroup="avbl2"
                                OnClick="btn_ChangePassword_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
            <td>
                <asp:Label ID="lbl_Message" runat="server" Font-Bold="True" ForeColor="#FF3300" ></asp:Label>
                <asp:Label ID="lbl_msg" runat="server" Font-Size="Medium" ForeColor="#0033CC" Style="text-align: center"
                    Visible="False" ></asp:Label>
               <%-- <asp:BulletedList ID="bullet_ChangePassword" runat="server" Width="350px" BulletStyle="Numbered"
                    Font-Bold="True" ForeColor="#FF3300" meta:resourcekey="bullet_ChangePasswordResource1">
                </asp:BulletedList>--%>
            </td>
        </tr>
      <%--  <tr>
            <td>
                <asp:RequiredFieldValidator ID="rfv_OldPassword" runat="server" ControlToValidate="txt_OldPassword"
                    ErrorMessage="Old Password must be entered" Display="None" meta:resourcekey="rfv_OldPasswordResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vce_OldPassword" runat="server" TargetControlID="rfv_OldPassword"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>--%>
                <%--<asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_Password"
        ErrorMessage="New Password must be entered" Display="None"></asp:RequiredFieldValidator>
    <cc1:ValidatorCalloutExtender ID="vce_Password" runat="server" TargetControlID="rfv_Password">
    </cc1:ValidatorCalloutExtender>
    <asp:RequiredFieldValidator ID="rfv_ConfirmPassword" runat="server" ControlToValidate="txt_ConfirmPassword"
        ErrorMessage="Confirm Password must be entered" Display="None"></asp:RequiredFieldValidator>
    <cc1:ValidatorCalloutExtender ID="vce_ConfirmPassword" runat="server" TargetControlID="rfv_ConfirmPassword">
    </cc1:ValidatorCalloutExtender>--%>
                <%--<asp:RequiredFieldValidator ID="rfv_ImgVerifyCode" runat="server" ControlToValidate="txt_ImgVerifyCode"
                    ErrorMessage="Verification Code must be entered" Display="None"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vce_ImgVerifyCode0" runat="server" TargetControlID="rfv_ImgVerifyCode">
                </cc1:ValidatorCalloutExtender>--%>
            </td>
        </tr>
    </table>

<%--<table width="90%" align="center" >
    <tr>
        <td align="center"  >
            </td>
    </tr>
    <tr>
        <td align="center"  >
                           </td>
    </tr>
    <tr>
        <td align="center"  >
                            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Times New Roman"
                                Font-Size="X-Large" ForeColor="#990033" Text="Change Password" 
                                EnableTheming="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center"  >
                            &nbsp;</td>
    </tr>
    <tr>
        <td align="center"  >
                <table width="60%" border="10" bordercolor="#CEEDEE" height="170px" >
                    <tr >
                        <td align="center">
    <asp:ChangePassword ID="ChangePassword1" runat="server" 
        ContinueDestinationPageUrl="~/Home.aspx" >
    </asp:ChangePassword>
                        </td>
                    </tr>
                    </table>
        </td>
    </tr>
    <tr>
        <td >
            </td>
    </tr> 
    
    </table>--%> 

    </asp:Content>

