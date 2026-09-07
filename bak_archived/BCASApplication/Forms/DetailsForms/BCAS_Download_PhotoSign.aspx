<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BCAS_Download_PhotoSign.aspx.cs" Inherits="Forms_DetailsForms_BCAS_Download_PhotoSign" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" align="center">
<tr>
        <td colspan="2" align="center"  >
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"  >
            <asp:Label ID="lblHeading" runat="server" Font-Bold="True" ForeColor="#990000" 
                Text="DOWNLOAD PHOTO AND SIGNATURE OF NOMINATED CANDIDATE "></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="30%">
            &nbsp;</td>
        <td width="70%">
            &nbsp;</td>
    </tr>
    <tr align="center" >
        <td width="30%" colspan="2" style="width: 100%"  >            
            <table width="70%" align="center" border="2"  >
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblCourseSchedule" runat="server" Text="Course Schedule"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlSchedule" runat="server" DataTextField="Course_Schedule" 
                DataValueField="Schedule_ID" Width="510px" 
                AppendDataBoundItems="True" >
                <asp:ListItem Value="0">-Select Course Schedule-</asp:ListItem>
            </asp:DropDownList>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlSchedule" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr align="left">
                    <td width="30%">
            <asp:Label ID="lblimageselect" runat="server" Text="Images :"></asp:Label>
                    </td>
                    <td width="70%">
            <asp:DropDownList ID="ddlimage" runat="server"  Width="210px" 
                AppendDataBoundItems="True" >
                <asp:ListItem Value="0">- Select -</asp:ListItem>
                <asp:ListItem Value="photo">PHOTOGRAPH</asp:ListItem>
                <asp:ListItem Value="Sign">SIGNATURE</asp:ListItem>
            </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlimage" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr align="left">
                <td width="30%">
                    Name images</td>
                <td width="70%">
                    <asp:RadioButtonList ID="rbtnImageName" runat="server">
                        <asp:ListItem Value="Registration">Registration wise</asp:ListItem>
                        <asp:ListItem Value="Rollno">Rollno wise</asp:ListItem>
                    </asp:RadioButtonList>
                </td>                
                </tr>
                <tr align="left">
                <td width="30%">
                    <asp:Label ID="lblregistraion_no" runat="server" Text="Registration Number : "></asp:Label>
                </td>
                <td width="70%">
                    <asp:TextBox ID="TxtRegNo" runat="server" Width="150px">0</asp:TextBox>
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" 
                         />               
                </td>                
                </tr>
                <tr align="center"><td colspan="2"> </td></tr>                
                <tr align="center"><td colspan="2"  >                
                 <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                        ForeColor="Red"></asp:Label>                
                </td></tr>
</table>
</asp:Content>

