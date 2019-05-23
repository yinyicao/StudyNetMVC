function addUser(emailForReg, phoneForReg, passForReg) {
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
                layer.msg('添加成功！');
            } else {
                layer.msg('添加失败！');
            }

        },
        error: function () {
            layer.msg('网络错误！', { icon: 5 });
        },
        complete: function () {
            $('#addOrModifyModal').modal('hide')//手动隐藏该窗口
            $("#tb_users").bootstrapTable('refresh');
        }
    });
}

function editUser(userId,userName,emailForReg, phoneForReg, passForReg) {
    $.ajax({
        type: "Post", //提交方式 
        url: "/Home/EditUser",//路径 
        data: {
            "id": userId,
            "username":userName,
            "email": emailForReg,
            "phone": phoneForReg,
            "pass": passForReg
        },//数据，这里使用的是Json格式进行传输 
        beforeSend: function () {

        },
        success: function (result) {//返回数据根据结果进行相应的处理 
            if (result == "True") {
                layer.msg('编辑成功！');
            } else {
                layer.msg('编辑失败！');
            }

        },
        error: function () {
            layer.msg('网络错误！', { icon: 5 });
        },
        complete: function () {
            $('#addOrModifyModal').modal('hide')//手动隐藏该窗口
            $("#tb_users").bootstrapTable('refresh');
        }
    });
}

function delUser(arrselections) {
    layer.confirm('确定要删除选择的数据吗？', {
        btn: ['是', '否'] //按钮
    }, function (index) {
        var ids = "";
        for (var i = 0; i < arrselections.length; i++) {
            ids += arrselections[i]['Id'];
            if (i < arrselections.length - 1) {
                ids += ",";
            }
        }
        $.ajax({
            type: "Post", //提交方式 
            url: "/Home/Delete",//路径 
            data: {
                "ids": ids
            },//数据，这里使用的是Json格式进行传输 
            beforeSend: function () {

            },
            success: function (result) {//返回数据根据结果进行相应的处理 
                layer.msg("共删除" + result + "条数据！");

            },
            error: function () {
                layer.msg('网络错误！', { icon: 5 });
            },
            complete: function () {
                $("#tb_users").bootstrapTable('refresh');
                layer.close(index);
            }
        });
    }, function () {

    });
}





//验证非空
function Verify() {
    var len = arguments.length;
    for (var i = 0; i < len; i++) {
        if (arguments[i].length == 0)
            return false;
    }
    return true;
}


$(function () {

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

});


var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_users').bootstrapTable({
            url: '/Home/GetAllUserInfo',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [5, 10, 15, 20],        //可供选择的每页的行数（*）
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }
                , {
                field: 'Id',
                title: '用户编号'
            }, {
                field: 'Username',
                title: '用户名'
            },
               {
                field: 'Email',
                title: '电子邮件'
            }, {
                field: 'Password',
                title: '密码'
            }, {
                field: 'PhoneNumber',
                title: '电话'
            },{
                field: 'CreateDate',
                title: '创建时间'
            }
            ]


        });
    };

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一致，这边改动，控制器也需要改成一样的
            limit: params.limit,   //页面大小
            offset: params.offset,  //页码
            username: $("#txt_search_username").val(),
            phone: $("#txt_search_phone").val()
        };
        return temp;
    };
    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
        $("#btn_add").click(function () {
            $("#addOrModifyModalLabel").text("新增");
            $("#addOrModifyModal").find(".form-control").val("");
            $('#addOrModifyModal').modal('show')

            postdata.DEPARTMENT_ID = "";
        });

        $("#btn_edit").click(function () {
            var arrselections = $("#tb_users").bootstrapTable('getSelections');
            if (arrselections.length > 1) {
                layer.msg('只能选择一行进行编辑');

                return;
            }
            if (arrselections.length <= 0) {
                layer.msg('请选择有效数据');

                return;
            }
            $("#addOrModifyModalLabel").text("编辑");
            $("#userNameDiv").css("display", "block");
            $("#userId").val(arrselections[0].Id);
            $("#userName").val(arrselections[0].Username);
            $("#emailForReg").val(arrselections[0].Email);
            $("#phoneForReg").val(arrselections[0].PhoneNumber);
            $("#passForReg1").val(arrselections[0].Password);
            $("#passForReg2").val(arrselections[0].Password);

            postdata.DEPARTMENT_ID = arrselections[0].Id;
            $('#addOrModifyModal').modal('show')
        });

        $("#btn_delete").click(function () {
            var arrselections = $("#tb_users").bootstrapTable('getSelections');
            if (arrselections.length <= 0) {
                layer.msg('请选择有效数据');
                return;
            }
            delUser(arrselections);

        });

        $("#btn_submit").click(function () {
            var title = $("#addOrModifyModalLabel").text();
            var userId = $("#userId").val();
            var userName = $("#userName").val();
            var emailForReg = $("#emailForReg").val();
            var phoneForReg = $("#phoneForReg").val();
            var passForReg1 = $("#passForReg1").val();
            var passForReg2 = $("#passForReg2").val();
            var vRes;
            if (title === "新增") {
                vRes = Verify(emailForReg, phoneForReg, passForReg1, passForReg2);
                if (vRes == false) {
                    layer.msg("请填写完整！");//弹出提示信息
                    return;
                }
                if (passForReg1 != passForReg2) {
                    layer.msg("两次密码不同！");//弹出提示信息
                    return;
                }
                addUser(emailForReg, phoneForReg, passForReg1);
            } else if (title === "编辑"){
                vRes = Verify(userName, emailForReg, phoneForReg, passForReg1, passForReg2);
                if (vRes == false) {
                    layer.msg("请填写完整！");//弹出提示信息
                    return;
                }
                if (passForReg1 != passForReg2) {
                    layer.msg("两次密码不同！");//弹出提示信息
                    return;
                }
                editUser(userId,userName,emailForReg, phoneForReg, passForReg1);
            }
            
        });

        $("#btn_query").click(function () {
            $("#tb_users").bootstrapTable('refresh');
        });
    };

    return oInit;
};