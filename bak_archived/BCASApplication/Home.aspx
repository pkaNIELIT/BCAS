<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="_Default" Title="BCAS--> Home" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BCASClassLibrary" Namespace="BCASClassLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


<script src="../JScript/Convert.js" type="text/javascript"></script>

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
        //alert("Start: before encryption");
        //alert(document.getElementById("<%=TxtPassword.ClientID %>").value);

        if (document.getElementById("<%=TxtPassword.ClientID %>").value != "")
        {

            var e = document.getElementById("<%=TxtPassword.ClientID %>").value;
            document.getElementById("<%=TxtPassword.ClientID %>").value = SHA1(e);
            var b = SHA1(e);
            
            alert(b);
            
            var id1 = '<%=Session["RNDam"].ToString() %>';
            var c;
            c = b + id1;
            document.getElementById("<%=TxtPassword.ClientID %>").value = SHA1(c);
           
        }
        
    }

    </script>

    <style type="text/css">
        .style1
        {
            text-align: justify;
        }
        .style2
        {
            height: 21px;
        }
        .style3
        {
            height: 10px;
        }
    </style>
    <%-- <script language="javascript" type="text/javascript">
        function ValidateLoginFields() {

            if (document.getElementById("<%=txtUserName.ClientID %>").value.length === 0) {
                document.getElementById("<%=txtUserName.ClientID %>").focus();
                alert("Please Enter User Name");
                return false;
            }
            if (document.getElementById("<%=txtPwd.ClientID %>").value.length === 0) {
                document.getElementById("<%=txtPwd.ClientID %>").focus();
                alert("Please Enter Password");
                return false;
            }
            if (document.getElementById("<%=txt_ImgVerifyCode.ClientID %>").value.length === 0) {
                document.getElementById("<%=txt_ImgVerifyCode.ClientID %>").focus();
                alert("Please Enter Captcha code");
                return false;
            }
        }  
            </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table width="100%">
        <tr>
            <td colspan="2" class="style2">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="Maroon" Text="National Institute of Electronics &amp; Information Technology (NIELIT)"
                    Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr style="border: thin solid #000000">
            <td width="15%" style="width: 70%" class="style1">
                <table width="100%">
                    <tr>
                        <td width="15%" class="style1" valign="top">
                            <img src="Images/logo.jpg" />
                        </td>
                        <td width="85%" class="style1" valign="top">
                            National Institute of Electronics &amp; Information Technology (NIELIT),(erstwhile
                            DOEACC Society), an Autonomous Scientific Society under the administrative control
                            of Department of Electronics &amp; Information Technology (DeitY), Ministry of Communications
                            and Information Technology, Government of India, was set up to carry out Human Resource
                            Development and related activities in the area of Information, Electronics &amp;
                            Communications Technology (IECT).
                        </td>
                    </tr>
                </table>
                <div class="style1">
                    NIELIT is engaged both in Formal &amp; Non-Formal Education in the area of IECT
                    besides development of industry oriented quality education and training programmes
                    in the state-of-the-art areas. NIELIT has endeavoured to establish standards to
                    be the country’s premier institution for Examination and Certification in the field
                    of IECT. It is also one of the National Examination Body, which accredits institutes/organizations
                    for conducting courses in IT in the non-formal sector.
                    <br />
                    <br />
                    <b>NIELIT is entrusted for conduct of Online Examination for AVSEC (Aviation Security)
                        Courses. Online Examination shall be conducted in major cities on regular basis.
                    </b>
                </div>
            </td>
            <td width="30%" valign="top" align="center" >
           
          
                <div><table border="1"  > <tr><td colspan="2">
                <table width="98%" border="0" ><tbody><tr> <td colspan="2" width="25%%" valign="top" align="center" >
                    <asp:Label ID="Label3" runat="server" Text="Log In" BackColor="#6bd498" 
                        Font-Bold="True" ForeColor="White" Height="33px" Width="309px"></asp:Label></td></tr>
                <tr>
                <td colspan="2" width="1%" valign="top" align="center" class="style3"></td></tr>
                <tr>
                <td width="15%" valign="top" align="center">
               <asp:Label ID="Label1" runat="server"  Text="User Name : " Font-Bold="True" 
                     ForeColor="Blue"></asp:Label> </td>
                     <td width="27%" valign="top" align="Left">
                 <asp:TextBox
                    ID="UserName" runat="server" Width="160px" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator id="txtusername1" runat="server"
  ControlToValidate="UserName"   ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                     <tr>
                <td colspan="2" width="1%" valign="top" align="center" class="style3"></td></tr>
              <tr><td width="15%" valign="top" align="center">
               &nbsp; &nbsp;  <asp:Label ID="lblpwd" runat="server" Text="Password :" Font-Bold="True" 
                     ForeColor="Blue"></asp:Label></td>
                     <td width="27%" valign="top" align="left">
                <asp:TextBox ID="TxtPassword" runat="server" Width="160px" TextMode="Password" autocomplete="off" TabIndex="3"></asp:TextBox> 
                                 <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
  ControlToValidate="TxtPassword"   ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hfKey" runat="server" Value="0" />
                
               </td></tr>
                <tr>
                <td colspan="2" width="1%" valign="top" align="center" class="style3"></td></tr>
               
                
                <tr align="left">
                    <td width="45%">
            <asp:Label ID="lbl_ImageCode" runat="server" ForeColor="Blue" 
                            meta:resourceKey="lbl_ImageCode" Font-Bold="True">Image Verification</asp:Label>
                        </td>
                        
                     <td width="55%">
        
                         <cc2:ImageVerifier ID="ImageVerifier1" runat="server" TabIndex="5" />
                    </td>
                </tr>
                            
                <tr align="left">
                    <td width="45%">
                <asp:Label ID="lbl_ImgVerifyCode" runat="server" ForeColor="Blue" 
                            meta:resourceKey="lbl_ImgVerifyCode" Font-Bold="True">Verification Code</asp:Label>
                        </td>
                     <td width="45%">
        <asp:TextBox ID="txt_ImgVerifyCode" runat="server" width="150px" TabIndex="8" autocomplete="off"
                                    onpaste="return false;" oncopy="return false;" oncut="return false;"></asp:TextBox>
                         </td>
                </tr>
                <tr><td colspan="2" width="25%" valign="top" align="right"> <asp:Button ID="BtnLogin" 
                       runat="server" Text="Log In"   
                       onclick="BtnLogin_Click" TabIndex="10" /><br />
                    <!--OnClientClick="return SpPwd();"-->
                    
               </td></tr> <tr><td colspan="2" width="25%" valign="top" align="center">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>&nbsp;</td></tr>
              </tbody>  </table></td></tr></table></div>   
            </td>
        </tr>
    </table>
</asp:Content>
