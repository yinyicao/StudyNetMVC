//验证非空
function Verify() {
    var len = arguments.length;
    for (var i = 0; i < len; i++) {
        if (arguments[i].length == 0)
            return false;
    }
    return true;
}

$("#btn_modifyPass").click(function () {
    var $btn;
    var thisBtn = $(this);
    //var oldPass = $("#oldPass").val();
    var newPass = $("#newPass").val();
    var newPass2 = $("#newPass2").val();
    var vRes = Verify(newPass, newPass2);
    if (vRes == false) {
        layer.msg("请填写完整");//弹出提示信息
        return;
    } else if (newPass != newPass2) {
        layer.msg("两次密码不同！");//弹出提示信息
        return;
    } else {
        $.ajax({
            type: "Post", //提交方式 
            url: "/Home/ModifyPassAPI",//路径 
            data: {
                "newPass": newPass,
                "newPass2": newPass2
            },//数据，这里使用的是Json格式进行传输 
            beforeSend: function () {
                $btn = thisBtn.button('loading')
            },
            success: function (result) {//返回数据根据结果进行相应的处理 
                if (result == "True") {
                    layer.msg("修改成功,请重新登录！", {
                        time: 1000
                    }, function () {
                        $.session.remove('user');
                        window.location.href = '/Home/Index';
                    }
                  );
                } else {
                    layer.msg("修改失败！请重试！");
                }

            },
            error: function () {
                layer.msg("网络错误！！！");
            },
            complete: function () {
                $btn.button('reset')
            }
        }); 
    }
})