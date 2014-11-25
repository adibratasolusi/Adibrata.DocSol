<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JournalSchemeHeaderAddEdit.aspx.cs" Inherits="Adibrata.FinanceLease.Web.JournalSchemeHeaderAddEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td>Journal Scheme Code</td><td> <asp:TextBox runat="server" ID ="txtTrxSchmCode"></asp:TextBox></td>
            <td>Scheme Description</td><td> <asp:TextBox runat="server" ID ="txtTrxSchmDesc"></asp:TextBox></td></tr>
        <tr><td>Table Transaction </td><td> <asp:TextBox runat="server" ID ="txtTableTrx"></asp:TextBox></td>
            <td>Transaction Code</td><td> <asp:TextBox runat="server" ID ="txtTrx_Code"></asp:TextBox></td></tr>
        <tr><td>Office ID</td><td> <asp:TextBox runat="server" ID ="txtOfficeID"></asp:TextBox></td>
            <td>Agreement ID</td><td> <asp:TextBox runat="server" ID ="txtAgrmntID"></asp:TextBox></td></tr>
        <tr><td>Office ID X </td><td> <asp:TextBox runat="server" ID ="txtOfficeID_X"></asp:TextBox></td>
            <td>Posting Date</td><td> <asp:TextBox runat="server" ID ="txtPostingDt"></asp:TextBox></td></tr>
         <tr>   <td>Value Date </td><td> <asp:TextBox runat="server" ID ="txtValueDt"></asp:TextBox></td>
            <td>Refference No</td><td> <asp:TextBox runat="server" ID ="txtReffNo"></asp:TextBox></td></tr>
         <tr><td>Bank Account ID</td><td> <asp:TextBox runat="server" ID ="txtBankAccId"></asp:TextBox></td>
          <td>Currency ID</td><td> <asp:TextBox runat="server" ID ="txtCurrID"></asp:TextBox></td></tr>
          <tr>  <td>Currency Rate </td><td> <asp:TextBox runat="server" ID ="txtCurrRate"></asp:TextBox></td>
          <td>Bank Portion  </td><td> <asp:TextBox runat="server" ID ="txtBankPortion"></asp:TextBox></td></tr>
          <tr><td>Receive Disburse Flag</td><td> <asp:TextBox runat="server" ID ="txtRcvDisbFlag"></asp:TextBox></td> 
            <td>Cashier ID</td><td> <asp:TextBox runat="server" ID ="txtCashierID"></asp:TextBox></td></tr>
          <tr><td>Cashier Open Sequence No </td><td> <asp:TextBox runat="server" ID ="txtCashierOpen"></asp:TextBox></td>
            <td>Amount Transaction</td><td> <asp:TextBox runat="server" ID ="txtAmountTrx"></asp:TextBox></td></tr>
          <tr>  <td>Way Of Payment </td><td> <asp:TextBox runat="server" ID ="txtWOP"></asp:TextBox></td>
            <td>Journal Flags (Receive / Disburse / Other></td><td> <asp:TextBox runat="server" ID ="txtJrnlFlags"></asp:TextBox></td></tr>
            <tr><td>Received From </td><td> <asp:TextBox runat="server" ID ="txtReceivedFrom"></asp:TextBox></td>
           <td>Receipt No</td><td> <asp:TextBox runat="server" ID ="txtReceiptNo"></asp:TextBox></td>
            <tr><td>IS Create Payment History Header</td><td> <asp:TextBox runat="server" ID ="txtIsCreatePaymentHistory"></asp:TextBox></td>
            <td>Is Create Journal Header</td><td> <asp:TextBox runat="server" ID ="txtIsCreateJournal"></asp:TextBox></td></tr>
            <tr><td>IS Create Cash Bank Mutation Header </td><td> <asp:TextBox runat="server" ID ="txtIsCreateCashBankMutation"></asp:TextBox></td>
            <td>Transaction Type</td><td> <asp:TextBox runat="server" ID ="txtJrnlTrxID"></asp:TextBox></td></tr>

        </tr>
    </table>
    </div>
    </form>
</body>
</html>
