function m_click(code) {

    if(code == "001"){
        location.href = "Default.aspx";
    }

    if (code == "002") {
        location.href = "../Report/ReportResultRequest.aspx";
    }

    if (code == "003") {
        location.href = "../Report/ReportResultApprove.aspx";
    }

}

function Message_Show(message) {
    alert(message);
}