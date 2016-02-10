$().ready(function () {
    $('input:checkbox, input:radio, select.uniformselect, input:file').uniform();

    $("#validate").validate({
        debug: true,
        errorPlacement: function (label, element) {
            var _for = label[0].htmlFor;
            var _txt = label.text();
            $('<label for="' + _for + '" generated="true" class="error">' + _txt + '</label>').appendTo(element.parent().after())

        },
        submitHandler: function (form) {
            if (!beforeSubmit()) {
                alert("错误");
                return;
            }
            $(".mask,#loader").slideDown("slow");
            $("#validate").find("button[type=submit]").attr("defaultval", $("#validate").find("button[type=submit]").text());
            $("#validate").find("button[type=submit]").text("信息提交中....");
            if ($(form).find("button[type=submit]").val() != $(form).find("button[type=submit]").text("defaultval")) {
                $(form).ajaxSubmit({
                    async: true,
                    type: "POST",
                    success: function (result) {
                        if (!afterSubmit(result)) return;
                        if (result.msg != "") {
                            alert(result.msg);
                        }
                        window.history.go(-1);
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