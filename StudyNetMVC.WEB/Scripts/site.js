$().ready(function () {//页面加载事件（右上角的控制）
    var user = $.session.get('user');;
    if ((typeof (user) == undefined) || !user) {
        $('#personalCenter').css('display', 'none');
    } else {
        $('#plogin').css('display', 'none');
        $('#pregister').css('display', 'none');
    }
})


//点击登录按钮（右上）
$("#plogin").click(function () {
    window.location.href = '/Home/Index';
})

//点击退出按钮（右上）
$("#exit").click(function () {
    $.session.remove('user');
    window.location.href = '/Home/Index';
})

//点击注册按钮（右上）
$("#pregister").click(function () {
    $('#registerModal').modal('show')
})