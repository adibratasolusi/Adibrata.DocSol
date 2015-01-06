<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocTransView.aspx.cs" Inherits="Adibrata.Web.DocTransView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adibrata.web</title> 

    <link href="css/AdibrataStyleSheet.css" rel="stylesheet" />

</head>
<body>
  
   <form id="GridView" runat="server">
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="NIM">
    <Columns>
     <asp:BoundField DataField="FileName" HeaderText="DocTransCode" ReadOnly="True" SortExpression="DocTransCode" runat="server" />
   
      </Columns>
</asp:GridView></form>
</body>
</html>
