function _attention(id) {

    if (confirm("确定设置关注时回复吗？\n设定后将取消其他关注时回复")) {
        $.getJSON("/_base/_subscribe", { id: id, v: Math.random() }, function (result) {
            alert(result);
        })
    }
}
function _default(id) {
    if (confirm("确定设置默认回复吗？\n设定后将取消其他默认回复")) {
        alert(id);
    }
}
function _push(id) {
    if (confirm("确定推送此消息吗？")) {
        $.getJSON("/_base/_push", { id: id, v: Math.random() }, function (result) {
            alert(result);
        })
    }
}
function _reloadToken() {
    if (confirm("确定更新Access_Token值吗？")) {
        $.getJSON("/_base/ReloadToken", { v: Math.random() }, function (result) {
            alert(result);
        })
    }
}
