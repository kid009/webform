<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WK.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="container">
                <div class="row">
                    <div class="form-login" style="margin-top: 50px;">

                        <div class="row" style="text-align: center">
                            <h1>Login Workflow Document</h1>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="text-align: right">
                                Username:
                            </div>
                            <div class="col-md-6" style="text-align: left">
                                <asp:TextBox ID="txtUsername" runat="server" Class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="text-align: right">
                                Password:
                            </div>
                            <div class="col-md-6" style="text-align: left">
                                <asp:TextBox ID="TextPassword" runat="server" Class="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="text-align: right">
                                Role:
                            </div>
                            <div class="col-md-6" style="text-align: left">
                                <asp:DropDownList ID="ddlRole" runat="server" Class="dropdown form-control">
                                    <asp:ListItem Value="0"> กรุณาเลือกประเภทผู้ใช้ </asp:ListItem>
                                    <asp:ListItem Value="1"> Admin </asp:ListItem>
                                    <asp:ListItem Value="2"> User </asp:ListItem>
                                    <asp:ListItem Value="3"> Manager </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6" style="text-align: right">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" Class="btn btn-primary" />
                            </div>
                            <div class="col-md-6" style="text-align: left">
                                <asp:Button ID="Cancel" runat="server" Text="Cancel" Class="btn btn-danger" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
