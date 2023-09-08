var currentURL = document.domain;
var myController = {
    init: function () {
        myController.registerEvent();
    },

    registerEvent: function () {
        $('#btnDangNhap').off('click').on('click', function () {
            myController.SaveData();
        });
    },

    SaveData: function () {
        var UserName = $('#UserName').val();
        var PassWord = $('#Password').val();

        if (UserName == "" || UserName == null || UserName == undefined) {
            XNoti_CanhBao('Bạn phải nhập tài khoản');
            $("#UserName").focus();
            return;
        }

        if (PassWord == "" || PassWord == null || PassWord == undefined) {
            XNoti_CanhBao('Bạn phải nhập mật khẩu');
            return;
        }

        $.ajax({
            type: "POST",
            url: '/LoginClient/Login',
            dataType: 'json',
            data: {
                UserName: UserName,
                PassWord: PassWord,
            },
            success: function (response) {
                if (response.status == true) {
                    if (response.isAdmin == false) {
                        window.open('/Product/Product' , "_self" );
                        XNoti_ThanhCong('Đăng nhập thành công');
                    } else if (response.isAdmin == true){
                        window.open('/Admin/Home/DashBoard', "_self");
                        XNoti_ThanhCong('Đăng nhập thành công');
                    }
                    
                } else {
                    XNoti_CanhBao('Tài khoản hoặc mật khẩu không chính xác');
                }
            },
            error: function (response) {
                $.notify("Có lỗi.", "warn");
            }
        });
    },


}
myController.init();
