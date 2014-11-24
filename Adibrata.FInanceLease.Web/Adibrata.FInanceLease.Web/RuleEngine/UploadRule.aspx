<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadRule.aspx.cs" Inherits="Adibrata.FinanceLease.Web.UploadRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type ="text/javascript">
    function CheckFileExists() {
        PageMethods.CheckFileExists(document.getElementById("fuExcel").value, FileExistsCallBack);
        return false;
    }


    function FileExistsCallBack(result) {
        if (result) //if file exists on server, confirm with user
        {
            if (confirm("File already exists. Do you want to overwrite"))
                __doPostBack('btnSave', '');
        }
        else
            __doPostBack('btnSave', ''); //file doesn't exists on server.. So upload it
    }
</script>
    
    <table>
        <tr>
            <td>Rule Code </td>
            <td><asp:TextBox ID="txtRuleCode" runat="server"></asp:TextBox> /></td>
        </tr>
        <tr>
            <td>Rule Name</td>
            <td><asp:TextBox ID="txtRuleName" runat="server"></asp:TextBox> /></td>
        </tr>
        <tr>
            <td>File Rule (.xlsx) 
            </td>
            <td>
                <asp:FileUpload ID="fuExcel" runat="server" /></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            <asp:Button ID="btnSave" runat="server" text="Save"  CausesValidation="true"  OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat ="server" text="Back" OnClick="btnCancel_Click" />
                </td>
        </tr>
    </table>
</asp:Content>
