<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReportResultRequest.aspx.cs" Inherits="WK.Report.ReportResultRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="row">
            <div class="row well">
                <h4>ข้อมูล User</h4>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-lg-3">

            </div>
            <div class="col-lg-9">

            </div>
        </div>

        <br />

        <div class="row">
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>

    </div>


</asp:Content>
