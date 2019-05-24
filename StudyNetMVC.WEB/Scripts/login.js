

//验证非空
function Verify() {
    var len = arguments.length;
    for (var i = 0; i < len; i++) {
        if (arguments[i].length == 0)
            return false;
    }
    return true;
}

//登录
function Login(context,username,pass,loginType) {
    var $btn;
    var thisBtn = $(context);
    $.ajax({
        type: "Post", //提交方式 
        url: "/Home/Login",//路径 
        data: {
            "username": username,
            "pass": pass,
            "loginType": loginType
        },//数据，这里使用的是Json格式进行传输 
        beforeSend: function () {
            $btn = thisBtn.button('loading')
        },
        success: function (result) {//返回数据根据结果进行相应的处理 
            if (result == "True") {
                layer.msg("登录成功！");
                $.session.set('user', username)
                window.location.href = '/Home/Main';
            } else {
                layer.msg("登录失败！用户名或密码错误！");
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


//点击邮箱登录或手机号登录tab
$(".myTabs a").click(function (e) {
    e.preventDefault()
    $(this).tab('show')
})


//点击登录页面登录按钮
$("#loginByEmail").click(function () {
    var username = $("#email").val();
    var pass = $("#passForEmail").val();
    var loginType = "email";
    var vRes = Verify(username, pass);
    if (vRes == false) {
        layer.msg("用户名或密码为空");//弹出提示信息
        return;
    }
    Login(this, username, pass, loginType);
})

$("#loginByPhone").click(function () {

    var username = $("#phone").val();
    var pass = $("#passForPhone").val();
    var loginType = "phone";
    Login(this, username, pass, loginType);
})

//注册
function Register(emailForReg, phoneForReg, passForReg) {
    $.ajax({
        type: "Post", //提交方式 
        url: "/Home/Register",//路径 
        data: {
            "email": emailForReg,
            "phone": phoneForReg,
            "pass": passForReg
        },//数据，这里使用的是Json格式进行传输 
        beforeSend: function () {

        },
        success: function (result) {//返回数据根据结果进行相应的处理 
            if (result == "True") {
                alert("注册成功！");
                //$("#tipMsg").text("删除数据成功");
                //tree.deleteItem("${org.id}", true);
            } else {
                alert("注册失败,请重试！！！");
                //$("#tipMsg").text("删除数据失败");
            }

        },
        error: function () {
            alert("网络错误！！！");
        },
        complete: function () {
            $('#registerModal').modal('hide')//手动隐藏该窗口
        }
    }); 
}


//点击注册按钮
$("#btn_register").click(function () {
    var emailForReg = $("#emailForReg").val();
    var phoneForReg = $("#phoneForReg").val();
    var passForReg1 = $("#passForReg1").val();
    var passForReg2 = $("#passForReg2").val();
    var vRes = Verify(emailForReg, phoneForReg, passForReg1, passForReg2);
    if (vRes == false) {
        layer.msg("请填写完整！");//弹出提示信息
        return;
    }
    if (passForReg1 != passForReg2) {
        layer.msg("两次密码不同！");//弹出提示信息
        return;
    }
    Register(emailForReg, phoneForReg, passForReg1);

})