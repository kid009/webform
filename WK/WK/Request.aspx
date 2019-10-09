<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="WK.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        
        <div class="row">
             <div class="well">
                <h1>ส่งคำขอ</h1>
            </div>

            <table class="table table-bordered">
                <tr>
                    <td>UserName</td>
                    <th>
                        <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>Name</td>
                    <th>
                        <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>Position</td>
                    <th>
                        <asp:Label ID="lbPosition" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>Dept</td>
                    <th>
                        <asp:Label ID="lbDept" runat="server" Text=""></asp:Label>
                    </th>
                </tr>
            </table>
        </div>

        <div class="row">

            <ul class="nav nav-tabs">
                <li role="presentation" class="active">
                    <a href="Request.aspx">
                        รายละเอียดทั่วไป
                    </a>
                </li>
                <li role="presentation">
                    <a href="RequestBuy.aspx">
                        รายละเอียดสั่งซื้อสินค้า
                    </a>
                </li>
            </ul>

            <br />




            <%--<div class="well">
                <h1>รายละเอียดคำขอ</h1>
            </div>--%>

            <table class="table table-bordered">
                <tr>
                    <td>เลือกผู้อนุมัติ</td>
                    <th>                        
                        <asp:DropDownList ID="ddlApprove" runat="server">
                        </asp:DropDownList>
                    </th>
                </tr>
                <tr>
                    <td>หัวข้อ</td>
                    <th>                        
                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    </th>
                </tr>
                <tr>
                    <td>รายละเอียดคำขอ</td>
                    <th>                       
                        <asp:TextBox id="txtDescription" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </th>
                </tr>
                <tr>
                    <td>เลือกไฟล์</td>
                    <th><asp:FileUpload ID="fileUpload" runat="server" /></th>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSend" runat="server" Text="บันทึกส่งคำขอ" class="btn btn-primary" OnClick="btnSend_Click" />

                    </td>
                    <th>                       
                        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิกคำขอ" class="btn btn-danger" />                       
                    </th>
                </tr>
            </table>

            <asp:HiddenField ID="hdUserName" runat="server" />
        </div>

    </div>

</asp:Content>
