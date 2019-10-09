<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReportResultRequest.aspx.cs" Inherits="WK.Report.ReportResultRequest" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="row">
            <div class="row well">
                <h4>ข้อมูล User</h4>
            </div>

            <div class="row">
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
                <asp:HiddenField ID="HiddenField1" runat="server" />
                        </th>
                    </tr>
                </table>
            </div>

        </div>

        <br />

        <!-- Search -->
        <div class="row">
            <div class="col-lg-5" style="text-align:right">
                <asp:Label ID="lbSearch" runat="server" Text="รหัสคำขอ : "></asp:Label>
            </div>
            <div class="col-lg-7" style="text-align:left">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-lg-5" style="text-align:right">
                <asp:Label ID="lbNameSearch" runat="server" Text="ชื่อเรื่อง : "></asp:Label>
            </div>
            <div class="col-lg-7" style="text-align:left">
                <asp:TextBox ID="txtSearcjNameSubject" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col-lg-5" style="text-align:right">
                <asp:Label ID="Label1" runat="server" Text="วันที่ส่งคำขอ : "></asp:Label>
            </div>
            <div class="col-lg-7" style="text-align:left">
                <div class="row">
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtDt" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Button ID="btnCalendar" runat="server" Text="Calendar" CssClass="btn btn-success" />
                        <ajaxToolkit:CalendarExtender ID="CalendarId" runat="server" PopupButtonID="btnCalendar" TargetControlID="txtDt" Format="dd-MM-yyyy"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
            </div>
        </div><!-- Search -->

        <br />
               
        <div class="row">
            <div class="col-lg-5" style="text-align:right">
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnLinQ" runat="server" Text="Search LinQ" CssClass="btn btn-primary" OnClick="btnLinQ_Click"  />
            </div>
            
            <div class="col-lg-7" style="text-align:left">
                <asp:Button ID="btnCancelSearch" runat="server" Text="ยกเลิก" CssClass="btn btn-danger" />
            </div>
        </div><!-- Search -->
        <br />

        <div class="row">
            <asp:GridView ID="ResultGridView" runat="server" CssClass="well" with="100%" DataKeyNames="RequestID" AllowPaging="true" PageSize="15" AutoGenerateColumns="false">

                <HeaderStyle CssClass="well" />

                <Columns>

                    <asp:BoundField DataField="RequestID" HeaderText="รหัสส่งคำขอ" ItemStyle-BackColor="White" ControlStyle-CssClass="visible-md" />
                    <asp:BoundField DataField="Subject" HeaderText="ชื่อเรื่อง" ItemStyle-BackColor="White" ControlStyle-CssClass="visible-md" />
                    <asp:BoundField DataField="Description" HeaderText="คำอธิบาย" ItemStyle-BackColor="White" ControlStyle-CssClass="visible-md" />
                    <asp:BoundField DataField="CreateBy" HeaderText="ผู้สร้าง" ItemStyle-BackColor="White" ControlStyle-CssClass="visible-md" />
                    <asp:BoundField DataField="CreateDate" HeaderText="วันที่สร้าง" ItemStyle-BackColor="White" ControlStyle-CssClass="visible-md" />

                    <asp:TemplateField HeaderText="ไฟล์" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a href='<%# GetHost(Eval("Path_File"))%>' target="_blank" style="font-size: 15px;" class="btn btn-primary">คลิก
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="แก้ไข" ItemStyle-BackColor="White">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-warning" Text="Edit" OnClick="btnEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ลบ" ItemStyle-BackColor="White">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>

        <asp:Panel ID="pnl_eidt" Visible="false" runat="server">

            <div class="row">
                <div class="well">
                    <h4>Edit Data</h4>
                </div>
            </div>

            <div class="row">
                <table class="table table-bordered">
                    <tr>
                        <td>RequestID</td>
                        <th>
                            <asp:TextBox ID="txtRequestId" runat="server"></asp:TextBox>
                        </th>
                    </tr>
                    <tr>
                        <td>Subject</td>
                        <th>
                            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                        </th>
                    </tr>
                    <tr>
                        <td>Description</td>
                        <th>
                            <asp:TextBox ID="txtDescription" runat="server" ></asp:TextBox>
                        </th>
                    </tr>
                    <tr>
                        <td>Note</td>
                        <th>
                            <asp:TextBox ID="txtNote" runat="server" ></asp:TextBox>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                        </td>
                        <th>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"></asp:Button>
                        </th>
                    </tr>
                </table>
            </div>

        </asp:Panel>

    </div>

</asp:Content>
