$(function () {
    $("#form").validation();
    $("#form").find("button[type=submit]").on('click', function (event) {
        if ($("#form").valid(this, '填写信息不完整...') == false) {
            return false;
        }
    })
    $("#form").ajaxForm({
        type: "POST",
        success: function (result) {
            var mtype = result.status == 1 ? "success" : "error";
            alert(result.msg, mtype);
            submitAfter(result);
        }
    })
})
function submitAfter(obj) {
    window.location = '/' + window.location.pathname.split('/')[1] + '/List';
}