<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocTransView.aspx.cs" Inherits="Adibrata.Web.DocTransView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adibrata.web</title>

    <link href="css/AdibrataStyleSheet.css" rel="stylesheet" />

</head>
<body>
    
    <div class="page" runat="server">
        <div class="header" runat="server">
           
            <div class="loginDisplay">
              <h1>
                    Document
                </h1>
               
            </div></div>
            <div class="main" runat="server">
 <form id="GridView" runat="server">
        Content
       <asp:GridView ID="gvContent" runat="server" AutoGenerateColumns="False" DataKeyNames="" Width="714px">
           <Columns>
               <asp:BoundField DataField="ContentName" HeaderText="ContentName" ReadOnly="True" SortExpression="ContentName" runat="server" />
               <asp:BoundField DataField="value" HeaderText="value" ReadOnly="True" SortExpression="value" runat="server" />
           </Columns>
       </asp:GridView>
        <br />

        Binary
        <asp:GridView runat="server" ID="gvBinary" OnSelectedIndexChanged="gvBinary_SelectedIndexChanged" AutoGenerateColumns="False" Width="710px">
            <Columns>
                <asp:BoundField DataField="FileName" HeaderText="FileName" ReadOnly="True"  runat="server" />
                <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" ReadOnly="True" runat="server" />
                <asp:BoundField DataField="SizeFileBytes" HeaderText="SizeFileBytes" ReadOnly="True" runat="server" />
                <asp:BoundField DataField="Pixel" HeaderText="Pixel" ReadOnly="True" runat="server" />
                <asp:BoundField DataField="DPI" HeaderText="DPI" ReadOnly="True" runat="server" Visible="False" />
                 <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" ReadOnly="True" runat="server" Visible="False" />
          
                <asp:BoundField DataField="ComputerName" HeaderText="ComputerName" SortExpression="ComputerName" />
          
           </Columns>
        </asp:GridView>
    </form> </div>
   </div>
</body>
</html>
