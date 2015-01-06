<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocTransView.aspx.cs" Inherits="Adibrata.Web.DocTransView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet"/>
    <script src="js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtTest" runat="server" CssClass="table-hover"></asp:TextBox><br />
    <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" CssClass="btn btn-default btn-lg" >
    </asp:Button>
        <iframe src="http://maximus-pc/smartcms/Adibrata.DocumentSol.Windows.xbap" width="500" height="500"></iframe>
    </div>

    </form>
</body>
</html>
