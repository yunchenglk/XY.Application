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
        $.getJSON("/GetJson/_push", { id: id, v: Math.random() }, function (result) {
            alert(result);
        })
    }
}
function _push(rid, gid) {
    if (confirm("确定推送此消息吗？")) {
        $.getJSON("/GetJson/_pushByGroup", { rid: rid, gid: gid, v: Math.random() }, function (result) {
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
//同步用户分组
function syncGroups() {
    if ($.trim($("#syncbtn").text()) == "同步用户分组列表") {
        $("#syncbtn").text("用户分组列表中....");
        $.getJSON("/wxUserInfo/syncGroups", { v: Math.random() }, function (result) {
            $("#syncbtn").text("同步用户分组列表");
            alert(result);
            location.reload();
        })
    }
}
//同步用户
function syncOnle() {
    if ($.trim($("#syncbtn").text()) == "同步用户列表") {
        $("#syncbtn").text("用户列表同步中....");
        $.getJSON("/wxUserInfo/syncOnlineUser", { v: Math.random() }, function (result) {
            $("#syncbtn").text("同步用户列表");
            alert(result);
            location.reload();
        })
    }
}
