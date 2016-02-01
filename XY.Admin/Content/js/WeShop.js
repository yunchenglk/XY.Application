function vdian_shop_cate_add(id) {
    $(".mask,#loader").slideDown("slow");
    $.getJSON("/WeShop/vdian_shop_cate_add", { id: id, v: Math.random() }, function (data) {
        alert(data);
        window.location.reload();
    })
}
function vdian_shop_cate_del(id) {
    if (confirm("确定从微店中移除吗？")) {
        $(".mask,#loader").slideDown("slow");
        $.getJSON("/WeShop/vdian_shop_cate_del", { id: id, v: Math.random() }, function (data) {
            alert(data);
            window.location.reload();
        })
    }
}
function vdian_async_all(id) {
    if (confirm("确定一键添加商品到微店吗？")) {
        $(".mask,#loader").slideDown("slow");
        $.getJSON("/WeShop/Product_Async", { id: id, v: Math.random() }, function (data) {
            alert("ok");
            window.location.reload();
        })
    }
}
