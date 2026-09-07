<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoggedInHome.aspx.cs" Inherits="LoggedInHome" Title="BCAS-->HomePage" culture="en-GB" EnableEventValidation ="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">

.style38 {color: #333333}
.style56 {text-decoration: none; font-family: Verdana, Arial, Helvetica, sans-serif;}
.style58 {font-size: 10px; font-weight: bold; color: #333333; }
.style6 {	font-size: 9px;
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
}
.style60 {font-family: Verdana, Arial, Helvetica, sans-serif}
.style63 {font-size: 10px; font-weight: bold; color: #F26100; }
.style64 {color: #F26100; font-size: 10px; }
.style69 {color: #FF0000}
.style70 {font-size: 12px;
	color: #990000;
}
.style71 {color: #CCCCCC}
.style72 {color: #EFEFEF}

    </style>
    <script type="text/javascript" language="JavaScript">
        if (top.location != self.location) {
            top.location = self.location.href
        }
        window.onbeforeunload = function doUnload(e) {
            if ((window.event.clientX < 0) || (window.event.clientY < 0)) {
                __doPostBack("LoginStatus_LoggedOut", "");
               
            }
        }
        function ShowAlert(eventArgument, context) {
            if (eventArgument != '') {
                alert(eventArgument);
                __doPostBack("LoginStatus_LoggedOut", "");
            }
        }
        function AutoCheck() {
            startTime();
            setInterval(function() { CheckLog() }, 10000);
        }

      
        
    </script>
<script type="text/javascript">

    /******************************************
    * Popup Box- By Jim Silver @ jimsilver47@yahoo.com
    * Visit http://www.dynamicdrive.com/ for full source code
    * This notice must stay intact for use
    ******************************************/

    var ns4 = document.layers
    var ie4 = document.all
    var ns6 = document.getElementById && !document.all

    //drag drop function for NS 4////
    /////////////////////////////////

    var dragswitch = 0
    var nsx
    var nsy
    var nstemp

    function drag_dropns(name) {
        if (!ns4)
            return
        temp = eval(name)
        temp.captureEvents(Event.MOUSEDOWN | Event.MOUSEUP)
        temp.onmousedown = gons
        temp.onmousemove = dragns
        temp.onmouseup = stopns
    }

    function gons(e) {
        temp.captureEvents(Event.MOUSEMOVE)
        nsx = e.x
        nsy = e.y
    }
    function dragns(e) {
        if (dragswitch == 1) {
            temp.moveBy(e.x - nsx, e.y - nsy)
            return false
        }
    }

    function stopns() {
        temp.releaseEvents(Event.MOUSEMOVE)
    }

    //drag drop function for ie4+ and NS6////
    /////////////////////////////////


    function drag_drop(e) {
        if (ie4 && dragapproved) {
            crossobj.style.left = tempx + event.clientX - offsetx
            crossobj.style.top = tempy + event.clientY - offsety
            return false
        }
        else if (ns6 && dragapproved) {
            crossobj.style.left = tempx + e.clientX - offsetx + "px"
            crossobj.style.top = tempy + e.clientY - offsety + "px"
            return false
        }
    }

    function initializedrag(e) {
        crossobj = ns6 ? document.getElementById("showimage") : document.all.showimage
        var firedobj = ns6 ? e.target : event.srcElement
        var topelement = ns6 ? "html" : document.compatMode != "BackCompat" ? "documentElement" : "body"
        while (firedobj.tagName != topelement.toUpperCase() && firedobj.id != "dragbar") {
            firedobj = ns6 ? firedobj.parentNode : firedobj.parentElement
        }

        if (firedobj.id == "dragbar") {
            offsetx = ie4 ? event.clientX : e.clientX
            offsety = ie4 ? event.clientY : e.clientY

            tempx = parseInt(crossobj.style.left)
            tempy = parseInt(crossobj.style.top)

            dragapproved = true
            document.onmousemove = drag_drop
        }
    }
    document.onmouseup = new Function("dragapproved=false")

    ////drag drop functions end here//////

    function hidebox() {
        crossobj = ns6 ? document.getElementById("showimage") : document.all.showimage
        if (ie4 || ns6)
            crossobj.style.visibility = "hidden"
        else if (ns4)
            document.showimage.visibility = "hide"
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--deep--%>



    

<%--deep--%>
    <table width="90%" align="center" >
        <tr>
            <td  >
                <%--<asp:Menu ID="Menu1" runat="server" BackColor="White" BorderColor="Black" 
                    BorderStyle="None" DynamicHorizontalOffset="2" Font-Names="Arial Narrow" 
                    Font-Size="15px" ForeColor="#990000" Orientation="Horizontal">
                    <StaticSelectedStyle ItemSpacing="5px" />
                    <StaticMenuItemStyle ItemSpacing="15px" />
                    <DynamicMenuStyle BorderColor="#6A0C23" CssClass="DynamicMenuZIndex" />
                    <DynamicMenuItemStyle BorderColor="#6A0C23" BorderWidth="1px" 
                        HorizontalPadding="10px" />
                </asp:Menu>--%>
            </td>
        </tr>
        <tr>
            <td >
               <%--deep--%>
               
               <div id="showimage" style="position:absolute;width:250px;left:400px;top:100px;z-index: 5000;">
<table border="0" width="500" bgcolor="#008B8B" cellspacing="0" cellpadding="2">
<tr>
<td width="100%"><table border="0" width="100%" cellspacing="0" cellpadding="0" >

<tr><td id="dragbar" style="cursor:hand; cursor:pointer" width="100%" onmousedown="initializedrag(event)">
<ilayer width="100%" onSelectStart="return false"><layer width="100%" onmouseover="dragswitch=1;if (ns4) drag_dropns(showimage)" onmouseout="dragswitch=0"><font face="Verdana" color="#FFFFFF"><strong><small>Important &nbsp;<img src="https://student.nielit.gov.in/images/new1.gif" alt="New" width="32" height="12" style="font-family: 'Times New Roman'; text-align: justify;"></small></strong></font></layer></ilayer></td>

<td style="cursor:hand"><a href="#" onClick="hidebox();return false"><img src="https://student.nielit.gov.in/images/cancel.gif" width="16px"
height="14px" border=0></a></td></tr>

<tr>
<td width="100%" bgcolor="#FFFFFF" style="padding:4px" colspan="2">
     	
	<%--<strong><div align="justify"> <font size="2"><img src='https://student.nielit.gov.in/ar.gif' width='6' height='9'>&nbsp;<font size="2"> It is hereby informed to all concerned that w.e.f. <font size="2" color="red"><b>15 November 2015</b></font>  , Service Tax has been revised from 14% to <font size="2" color="red"><b>14.5%</b></font>, now, BCAS fee per candidate would be Rs.572/- (Five Hundred Seventy Two only). You are requested to kindly make the payment accordingly.</font> <font size="3" color="red"><b></b></font></font></div>
	</strong>--%>
	<%--<strong><div align="justify"> <font size="2"><img src='https://student.nielit.gov.in/ar.gif' width='6' height='9'>&nbsp;<font size="2"> <i>It is hereby informed to all concerned that w.e.f. <font size="2" color="red"><b>15 November 2015</b></font>  , Service Tax has been revised from 14% to <font size="2" color="red"><b>14.5% (14% ST +0.5% SBC (Swachh Bharat Cess)).</b></font> BCAS fee per candidate would be Rs.573/- (Five Hundred Seventy Three only). You are requested to kindly make the payment accordingly.</i></font> <font size="3" color="red">
	<b></b></font></font></div></strong>--%>
	<strong><div align="justify"> <font size="2"><img src='https://student.nielit.gov.in/ar.gif' width='6' height='9'>&nbsp;<font size="2" color="Blue"> <i>It is hereby informed to all concerned that w.e.f. <font size="2" color="red"><b>24th July 2023</b></font>    Basic Avsec  Exam fee  per candidate would be Rs.750/- (Seven Hundred and Fifty only <font size="2" color="red"><b>excluding 18% GST </b></font> ). You are requested to kindly make the payment accordingly.</i></font> <font size="3" color="red">
	<b></b></font></font></div></strong>
	
	<br>
	
	
</td></tr>
</table> 
<asp:HiddenField ID="hfVal" runat="server" Value="0" /> 
</td >
</tr>
</table>
</div>    
                  
               
              <%-- deep --%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>

