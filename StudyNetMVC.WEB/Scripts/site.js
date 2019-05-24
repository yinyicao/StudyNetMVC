$().ready(function () {//页面加载事件（右上角的控制）
    var user = $.session.get('user');;
    if ((typeof (user) == undefined) || !user) {
        $('#plogin').css('display', 'block');
        $('#pregister').css('display', 'block');
    } else {
        $('#currentUser').text("欢迎您，" + user + "!");
        $('#currentUser').parent().css('display', 'block');
        $('#personalCenter').css('display', 'block');
    }
})

//点击登录按钮（右上）
$("#plogin").click(function () {
    window.location.href = '/Home/Index';
})

//点击退出按钮（右上）
$("#exit").click(function () {
    $.ajax({
        type: "Get", //提交方式 
        url: "/Home/Logout",//路径 
        data: {},//数据，这里使用的是Json格式进行传输 
        beforeSend: function () {
        },
        success: function (result) {//返回数据根据结果进行相应的处理 
            if (result == "True") {
                layer.msg("退出成功！");
                $.session.remove('user');
                window.location.href = '/Home/Index';
            } else {
                layer.msg("退出失败");
            }

        },
        error: function () {
            layer.msg("网络错误！！！");
        },
        complete: function () {
            $btn.button('reset')
        }
    }); 
})

//点击注册按钮（右上）
$("#pregister").click(function () {
    $('#registerModal').modal('show')
})