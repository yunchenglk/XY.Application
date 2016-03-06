var sourceURL = "http://source.0359i.com";
$(function () {
    BindInputFile();
})
function UploadCallBack(result) {
    BindInputFile();
    if (result.error == "0") {
        var forname = result.forname;
        $("#" + forname).parent().append('<div id="ok_' + forname + '"><a href="' + sourceURL + result.filepath + result.newname + '" target="_blank"><img src="' + sourceURL + result.filepath + result.newname + '" style="max-height:30px; max-width:100px;" /></a>&#12288;<a href="javascript:againUP(\'' + result.forname + '\',\'' + result.filepath + '\',\'' + result.filetype + '\')" class="btn btn-blue btn-xs"> 重新上传 </a></div>');
        $("input[name=" + forname + "]").val(result.filepath + result.newname);
        $("#" + forname).hide();
    } else {
        $("#" + result.forname + " > div").append(' <input type="file" class="{required:true,messages:{required:\'图片不能为空\'}}" name="file" filepath="' + result.filepath + '" filetype="' + result.filetype + '" for="' + result.forname + '">');
        $("#" + result.forname + " > div").find(".filename").text("");
        alert(result.message);
    }
    $("#upload_div").remove();
}
function againUP(forname, filepath, filetype) {
    $("#ok_" + forname).remove();
    $("#" + forname+" > div").append(' <input type="file" class="{required:true,messages:{required:\'图片不能为空\'}}" name="file" filepath="' + filepath + '" filetype="' + filetype + '" for="' + forname + '">');
    $("input[name=" + forname + "]").val("");
    $("#" + forname).show();
}
function BindInputFile() {
    $("input[type=file]").live(($.browser.msie && ($.browser.version == "6.0")) ? 'propertychange' : 'change', function () {
        $("input[type=file]").die($.browser.msie && ($.browser.version == '6.0') ? 'propertychange' : 'change');
        var filepath = $(this).attr("filepath");
        var type = $(this).attr("filetype");
        var forname = $(this).attr("for");
        $("#img_div_" + forname).remove();
        //$(this).before('<div id="loading_file_' + forname + '"><i class="icon-spinner icon-spin"></i>文件上传中</div>');
        if ($("#upfileiframe").length == 0)
            $(this).parents("form").after('<div id="upload_div"><iframe id="upfileiframe" name="upfileiframe" style="display: none;"></iframe></div>');
        $(this).wrapAll("<form method='post' id='File_ajax' target='upfileiframe' action='" + sourceURL + "/Uploadfile/Upfile' enctype='multipart/form-data' style='display:none;'></form>");
        $("#File_ajax").append("<input name='filepath' value='" + filepath + "' />",
                                "<input name='type' value='" + type + "' />",
                                "<input name='forname' value='" + forname + "' />",
                                "<input name='backurl' value='http://" + document.domain + "/Home/JSCallBack/?' />");
        $("#upload_div").append($("#File_ajax"));
        $("#" + forname + " > div").find(".filename").text("文件上传中...");
        $("#File_ajax").submit();
    })
}