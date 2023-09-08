var myController = {
    init: function () {
        myController.registerEvent();
    },

    registerEvent: function () {
        $('#btnDangKy').off('click').on('click', function () {
            myController.SaveData();
        });
    },

    SaveData: function () {
        var UserName = $('#UserName').val();
        var PassWord = $('#PassWord2').val();
        var HoTen = $('#HoTen').val();
        var DiaChi = $('#DiaChi').val();
        var Email = $('#Email').val();
        var SoDienThoai = $('#SoDienThoai').val();

        if (UserName == "" || UserName == null || UserName == undefined) {
            XNoti_CanhBao('Bạn phải nhập tài khoản');
            $("#UserName").focus();
            return;
        }

        if (PassWord == "" || PassWord == null || PassWord == undefined) {
            XNoti_CanhBao('Bạn phải nhập mật khẩu');
            return;
        }

        var objData =
        {
            UserId: 0,
            UserName: UserName,
            PassWord: PassWord,
            Email: Email,
            HoTen: HoTen,
            DiaChi: DiaChi,
            SoDienThoai: SoDienThoai,
        };

            $.ajax({
                type: "POST",
                url: '/RegesterClient/SaveData',
                dataType: 'json',
                data: {
                    str_JSON: JSON.stringify(objData),
                },
                success: function (response) {
                    if (response.status == true) {
                        XNoti_ThanhCong('Đăng ký thành công');
                        window.open('/LoginClient/LoginClient');
                    } else {
                        $.notify("Có lỗi xảy ra.", "warn");
                    }
                },
                error: function (response) {
                    $.notify("Có lỗi.", "warn");
                }
            });
    },


}
myController.init();
