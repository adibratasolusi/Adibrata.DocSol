<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JournalSchemeDetailAddEdit.aspx.cs" Inherits="Adibrata.FinanceLease.Web.JournalSchemeDetailAddEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 25%;
        }
        .auto-style2 {
            width: 25%;
        }
        .auto-style3 {
            width: 25%;
        }
        .auto-style4 {
            width: 25%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr style="text-align: left;width='25%'">       
     <td class="auto-style3">CoaName</td><td class="auto-style4"><asp:TextBox runat="server" ID="CoaName"></asp:TextBox></td>
     <td class="auto-style2">CoaSourceTable</td><td class="auto-style1"><asp:TextBox runat="server" ID="CoaSourceTable"></asp:TextBox></td></tr>
     <tr><td class="auto-style3">IsCoaHeader</td><td class="auto-style4"><asp:TextBox runat="server" ID="IsCoaHeader"></asp:TextBox></td>
     <td class="auto-style2">Post</td><td class="auto-style1"><asp:TextBox runat="server" ID="Post"></asp:TextBox></td></tr>
     <tr><td class="auto-style3">IsMultipleDtl</td><td class="auto-style4"><asp:TextBox runat="server" ID="IsMultipleDtl"></asp:TextBox></td>
     <td class="auto-style2">TblSourceDtl</td><td class="auto-style1"><asp:TextBox runat="server" ID="TblSourceDtl"></asp:TextBox></td></tr>
     <tr><td class="auto-style3">ColSourceDtl</td><td class="auto-style4"><asp:TextBox runat="server" ID="ColSourceDtl"></asp:TextBox></td>
     <td class="auto-style2">ColFilterDtlID</td><td class="auto-style1"><asp:TextBox runat="server" ID="TextBColFilterDtlIDox7"></asp:TextBox></td></tr>
     <tr><td class="auto-style3">IsCreatePaymentHistoryDetail</td><td class="auto-style4"><asp:TextBox runat="server" ID="IsCreatePaymentHistoryDetail"></asp:TextBox></td>
     <td class="auto-style2">IsCreateCashBankMutationDetail</td><td class="auto-style1"><asp:TextBox runat="server" ID="IsCreateCashBankMutationDetail"></asp:TextBox></td></tr>
     <tr><td class="auto-style3">IsCreateJournalDetail</td><td class="auto-style4"><asp:TextBox runat="server" ID="IsCreateJournalDetail"></asp:TextBox></td>
     <td class="auto-style2">DepartID</td><td class="auto-style1"><asp:TextBox runat="server" ID="DepartID"></asp:TextBox></td></tr>

        <tr> <td class="auto-style3"></td><td class="auto-style4"></td>
            <td class="auto-style2"><asp:Button Text="Save" runat="server" style="text-align: right" /> </td>
            <td class="auto-style1"><asp:Button Text="Back" runat="server" /> </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
