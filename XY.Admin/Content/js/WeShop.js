function vdian_shop_cate_add(id) {
    $.getJSON("/WeShop/vdian_shop_cate_add", { id: id, v: Math.random() }, function (data) {
        alert(data);
        window.location.reload();
    })
}
function vdian_shop_cate_del(id) {
    if (confirm("确定从微店中移除吗？"))
        $.getJSON("/WeShop/vdian_shop_cate_del", { id: id, v: Math.random() }, function (data) {
            alert(data);
            window.location.reload();
        })
}
