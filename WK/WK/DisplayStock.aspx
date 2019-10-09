<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DisplayStock.aspx.cs" Inherits="WK.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="well">
                <h1><asp:Label ID="lbReqId" runat="server"></asp:Label></h1>
            </div>
        </div>

        <div class="row">
            <asp:GridView ID="ResultGridView" runat="server" CssClass="well">
            </asp:GridView>
        </div>
        <br />
        <asp:Button ID="back" runat="server" OnClick="back_Click" />
    </div>

</asp:Content>
