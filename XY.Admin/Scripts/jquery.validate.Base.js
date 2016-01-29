$().ready(function () {
    $("#validate").validate({
        debug: true,
        errorPlacement: function (label, element) {
            element.parent().find("ul").remove();
            $(' <ul class="parsley-error-list"><li class="required" style="display: list-item;">' + label.text() + '</li></ul>').appendTo(element.parent());
            $(element).addClass("parsley-error").addClass("parsley-validated");
        },
        success: function (element) {
            $(".parsley-error").each(function (i, o) {
                $(o).removeClass("parsley-validated");
            })
            $(".parsley-validated").each(function (i, o) {
                $(o).removeClass("parsley-validated");
            })
        },
        submitHandler: function (form) {
            if (!beforeSubmit()) {
                alert("错误");
                return;
            }
            $("#validate").find("button[type=submit]").attr("defaultval", $("#validate").find("button[type=submit]").text());
            $("#validate").find("button[type=submit]").text("信息提交中....");
            if ($(form).find("button[type=submit]").val() != $(form).find("button[type=submit]").text("defaultval")) {
                $(form).ajaxSubmit({
                    async: true,
                    type: "POST",
                    success: function (result) {
                        $(form).find("button[type=submit]").val($(form).find("button[type=submit]").text($("#validate").find("button[type=submit]").attr("defaultval")));
                        if (!afterSubmit(result)) return;
                        if (result.msg != "") {
                            alert(result.msg);
                        }
                        if (result.ResultURL != "") {
                            window.location = result.ResultURL;
                        }
                    }
                })
            }
        }
    })
})
function afterSubmit(result) {
    return true;
}
function beforeSubmit() {
    return true;
}