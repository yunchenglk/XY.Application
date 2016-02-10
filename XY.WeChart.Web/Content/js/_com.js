function _attention(id) {

    if (confirm("确定设置关注时回复吗？\n设定后将取消其他关注时回复")) {
        alert(id);
    }
}
function _default(id) {
    if (confirm("确定设置默认回复吗？\n设定后将取消其他默认回复")) {
        alert(id);
    }
}
function _push(id) {
    if (confirm("确定推送此消息吗？")) {
        alert(id);
    }
}
