function createPage(objs, pageIndex, pageSize, total, url, wheres, sortname, sortorder) {
    $(".pagination:first").jBootstrapPage({
        pageSize: pageSize,
        total: total,
        maxPageButton: 10,
        onPageClicked: function (obj, pageIndex) {
            $(objs).bindPage({
                url: url,
                pageindex: pageIndex + 1,
                pagesize: pageSize,
                where: wheres,
                sortorder: sortorder,
                sortname: sortname
            });
        }
    });
}
function pageAfter(result, obj) {
    $("#dataModel").tmpl(result.content).appendTo(obj);
    $(".gradeA").on("mousemove", function () {
        $(this).addClass("row_selected");
    }).on("mouseout", function () {
        $(this).removeClass("row_selected");
    })
    $(".sorting").parent().css({ "cursor": "pointer" });
    // $("tbody").find("tr:odd").addClass("success");
}
function ChangeDateFormat(cellval) {
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}
function del(id, obj, controller, con) {
    if (confirm(con == null ? "确定删除吗？" : con))
        $.getJSON("/" + controller + "/Delete/", { id: id, v: Math.random() }, function (result) {
            if (result.status == 1) {
                alert("操作成功");
                $("#" + id).remove();
            } else {
                alert("操作失败");
            }
        })
}
function clearOldDate(obj) {
    $(obj).find("tr").remove();
}
(function ($) {
    $.fn.bindPage = function (opts) {
        var obj = $(this);
        var total;
        if (opts == undefined)
            opts = {};
        opts = jQuery.extend({
            url: opts.url == undefined ? $(obj).parents("table").attr("url") : opts.url,
            pageindex: opts.pageindex == undefined ? 1 : opts.pageindex,
            pagesize: opts.pagesize == undefined ? 10 : opts.pagesize,
            where: opts.where == undefined ? "DR|equal|0" : opts.where,
            sortorder: "",
            sortname: ""
        }, opts || {});
        clearOldDate(obj)
        $.ajax({
            type: "POST",
            async: false,
            url: opts.url,
            data: {
                pageIndex: opts.pageindex,
                pageSize: opts.pagesize,
                wheres: opts.where,
                sortorder: opts.sortorder,
                sortname: opts.sortname
            },
            success: function (result) {
                total = result.total;
                pageAfter(result, obj);
            }
        })
        if (opts.pageindex == 1) {
            createPage(obj, opts.pageindex, opts.pagesize, total, opts.url, opts.where, opts.sortname, opts.sortorder)
        }
    };
})(jQuery);