//弹出窗口
function pop(obj) {
    makeCenter();
    initProvince(obj);
}
//隐藏窗口
function hide() {
    $('#choose-box-wrapper').css("display", "none");
}
function initProvince(obj) {
    // $('#choose-a-province').html('');
    $('#choose-a-province').append('<a href="javascript:void(0);" class="province-item" province-id="1">文本</a>');
    $('#choose-a-province').append('<a href="javascript:void(0);" class="province-item" province-id="2">图文</a>');
    $('#choose-a-province').append('<a href="javascript:void(0);" class="province-item" province-id="3">语音</a>');
    $('#choose-a-province').append('<a href="javascript:void(0);" class="province-item" province-id="4">视频</a>');
    $('.province-item').die('click');
    $('.province-item').live('click', function () {
        var item = $(this);
        var province = item.attr('province-id');
        $(".province-item").removeClass('choosen');
        item.addClass('choosen');
        initSchool(province, obj);
    });
}
function initSchool(provinceID, obj) {
    $('#choose-a-school').html('');
    $.getJSON("/wxRule/GetKeywordsByType", { type: provinceID, v: Math.random() }, function (data) {
        if (data.length > 0)
            $.each(data, function (i, n) {
                $('#choose-a-school').append('<a href="javascript:void(0);" class="school-item" school-id="' + n + '">' + n + '</a>');
            })
    })
    $('.school-item').die('click');
    $('.school-item').live('click', function () {
        var item = $(this);
        var school = item.attr('school-id');
        $(obj).val(item.text());
        //关闭弹窗
        hide();
    });
}
function makeCenter() {
    $('#choose-a-province').html('');
    $('#choose-a-school').html('');
    $('#choose-box-wrapper').css("display", "block");
    $('#choose-box-wrapper').css("position", "absolute");
    $('#choose-box-wrapper').css("top", Math.max(0, (($(window).height() - $('#choose-box-wrapper').height()) / 2) - $(".topheader").height() - $(".header").height() + $(window).scrollTop()) + "px");
    $('#choose-box-wrapper').css("left", Math.max(0, (($(window).width() - $('#choose-box-wrapper').width()) / 2) - $(".iconmenu").width() + $(window).scrollLeft()) + "px");
}
//获取当前菜单
function _getMenuOnline() {
    $.getJSON("/wxMenu/getMenuOnline", { v: Math.random() }, function (result) {
        if (result != "error") {
            $("#mlist > tbody").find("input").val("");
            $.each(result.menu.button, function (i, n) {
                if (n.sub_button == undefined) {
                    $("#mlist > tbody").find("td[list=col" + i + "]:eq(0)").find("input[name=title]").val(n.name);
                    if (n.type == "view") {
                        $("#mlist > tbody").find("td[list=col" + i + "]:eq(0)").find("input[name=url]").val(n.url);
                    } else if (n.type = "click") {
                        $("#mlist > tbody").find("td[list=col" + i + "]:eq(0)").find("input[name=key]").val(n.key);

                    }
                } else {
                    $("#mlist > tbody").find("td[list=col" + i + "]:eq(0)").find("input[name=title]").val(n.name);
                    $.each(n.sub_button, function (x, y) {
                        $("#mlist > tbody").find("td[list=col" + i + "]:eq(" + x + 1 + ")").find("input[name=title]").val(y.name);
                        if (y.type == "view") {
                            $("#mlist > tbody").find("td[list=col" + i + "]:eq(" + x + 1 + ")").find("input[name=url]").val(y.url);
                        } else if (y.type = "click") {
                            $("#mlist > tbody").find("td[list=col" + i + "]:eq(" + x + 1 + ")").find("input[name=key]").val(y.key);

                        }
                    })
                }
            })
            alert("成功");
        }
        else
            alert("错误");
    })
}
//生成自定义菜单
function _makeMenuOnline() {
    var menus = [];
    for (var i = 0; i < 3; i++) {
        var menulist = [];
        $.each($("#mlist > tbody").find("td[list=col" + i + "]"), function (i, n) {
            var title = $(n).find("input[name=title]").val();
            var key = $(n).find("input[name=key]").val();
            var url = $(n).find("input[name=url]").val();
            if ($.trim(title).length > 0)
                menulist.push({ sort: i, title: title, key: key, url: url });
        });
        menus.push(menulist);
    }
    $.ajax({
        url: "/wxMenu/syncMenuOnline",
        type: "POST",
        data: { menus: menus, v: Math.random() },
        async: false,
        success: function (result) {
            alert(result.errmsg);
        }, error: function () {
            alert("错误");
        }
    })
}

