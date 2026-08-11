<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadExcel.aspx.cs" Culture="en-GB" Inherits="Forms_DetailsForms_UploadExcel" Title="BCAS-->UploadExcel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .header-outer {
            width: 100%;
            background-color: #fff;
            margin-top: 0px;
            margin-right: auto;
            margin-bottom: 0px;
            margin-left: auto;
            padding-top: 10px;
            padding-right: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }

            .header-outer .header {
                width: 100%;
                max-width: 1180px;
                padding: 0px;
                margin-top: 0px;
                margin-right: auto;
                margin-bottom: 0px;
                margin-left: auto;
            }

        .header-left {
            float: left;
            width: 50%;
        }

        .header-outer .header .header-left .header-left-text {
            border-left: 1px solid #000;
            float: left;
            margin-left: 20px;
            margin-top: 15px;
            padding-left: 15px;
        }


        .header-outer .header .header-left .header-left-logo {
            float: left;
        }

        .header-outer .header .header-left .header-left-text p.hindi {
            margin: 0px;
            padding: 0px;
            font-size: 20px;
            font-family: Arial,Helvetica,sans-serif;
        }

        p {
            line-height: 1.567;
        }

        #rightlogo {
            float: left;
            width: 20%;
            text-align: center;
        }

        .clear {
            clear: both;
        }

        .header-right {
            float: left;
            width: 45%;
        }

        #headerrighttext {
            font-family: arial;
            font-size: 14px;
            text-align: right;
            font-weight: bold;
        }

        #headerrighttext1 {
            font-family: arial;
            font-size: 12px;
            padding-top: 5px;
            text-align: right;
        }

        .header-right-sub-left {
            margin-bottom: 5px;
            width: 74%;
            float: left;
        }

        .header-right-sub-right {
            width: 26%;
            float: left;
        }

        #english {
            float: left;
            margin-top: 20px;
            margin-right: 5px;
            width: 75%;
        }

        #rightlogo {
            float: left;
            width: 20%;
            text-align: center;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
           <table>
              <br />
               <br />
               <tr>
                <td colspan ="2">
                    Download <a href="../../Upload/BCASUploadSample.xlsx">Format</a> for Upload/Cross Checking<br /><br />
                </td>
                   </tr><tr>
                                  <td>
                     <asp:FileUpload ID="fileUploadExcel" runat="server" /> &nbsp;&nbsp;
                   <asp:Button ID="btnUpload" runat="server" Text="Upload Excel File" OnClick="btnUpload_Click" />
              
                   </td>
                   </tr>
               <tr>
                   <td colspan ="2">
               
                     <asp:Label ID="SuccessLabel" runat="server" Text="" Style="margin-top:100px;color:blueviolet"></asp:Label>
                    <asp:Label ID="lblError" runat ="server" Text="" Font-Bold="true" ForeColor ="Red"></asp:Label>

          
                       </td>
                   </tr>
  </table>
    <table>
        <tr>
            <td>
                <asp:GridView ID="grdFailed" runat ="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="EBCASNo" HeaderText="EBCASID" SortExpression="EBCASNo" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="DOB" HeaderText="DOB" SortExpression="DOB" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BCASConnectionString %>" SelectCommand="getEBCASUploadedDuplicate" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblfilename" Name="pFilename" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;<asp:Label ID="lblfilename" runat ="server" Text="" Visible ="false"  ></asp:Label></td>
        </tr>
    </table>
    </asp:Content>
