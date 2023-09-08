$(function () {
    $('#btn_LogOut').off('click').on('click', function () {


        $.ajax({
            cache: false,
            type: "POST",
            url: "../Accounts/LogOff",
            success: function (data) {
                if (data === 1) {
                    window.location.href = '../Accounts/Login';
                }
            },
            error: function () {
                bootbox.alert('Có lỗi khi đăng xuất');

            }
        });

    });
    
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $.ajax({
        url: "../Layout/GetNoti",
        type: "GET",
        data: "",
        dataType: "json",
        success: function (response) {
            if (response.status) {
                
                if (response.roles =="DVHC") {
                    $('#number_noti').text(parseInt(response.dang_cho) + parseInt(response.dang_xu_ly));
                    $('#div_dang_cho').html("Có " + response.dang_cho + " hồ sơ cần tiếp nhận và duyệt");
                    $('#div_dang_xu_ly').html("Có " + response.dang_xu_ly + " hồ sơ cần xử lý");
                }else if (response.roles =="DN") {
                    $('#number_noti').text(response.bi_tu_choi);
                    $('#div_dang_cho').html("Có " + response.dang_cho + " hồ sơ đang chờ tiếp nhận và duyệt");
                    $('#div_dang_xu_ly').html("Có " + response.dang_xu_ly + " hồ sơ đang xử lý");
                    $('#div_da_hoan_thanh').html("Có " + response.da_hoan_thanh + " hồ sơ đã hoàn thành");
                    $('#div_bi_tu_choi').html("Có " + response.bi_tu_choi + " hồ sơ bị từ chối");
                }
                

            }
        }
    });


});