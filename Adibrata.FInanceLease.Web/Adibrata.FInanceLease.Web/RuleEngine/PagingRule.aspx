<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagingRule.aspx.cs" Inherits="Adibrata.FinanceLease.Web.PagingRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>LIST RULE
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:GridView ID="grdRule" runat="server" AutoGenerateColumns="false" tDataKeyNames="Rule Code" OnSelectedIndexChanged="grdRule_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField ="number" HeaderText ="#" ReadOnly ="true"/>
                        <asp:BoundField DataField ="Rule Code" HeaderText ="Rule Code" ReadOnly ="True" />
                        <asp:BoundField DataField ="Rule Description" HeaderText ="Rule Name" ReadOnly ="True" />
                        <asp:TemplateField > <ItemTemplate><asp:Button ID ="btnEdit" runat="server" text="Edit"/></ItemTemplate></asp:TemplateField>
                    </Columns>


                </asp:GridView>
            </td>
        </tr>
    </table>
    <table> 
        <tr>
            <td>
                <asp:Button ID="btnAdd" Text ="Add" runat="server" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
