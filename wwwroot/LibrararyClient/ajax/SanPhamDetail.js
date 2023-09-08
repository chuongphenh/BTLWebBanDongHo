var currentURL = document.URL.split("?");
var getID = currentURL[currentURL.length - 1];

var myController = {

    init: function () {
        myController.LoadData();
    },


    LoadData: function () {
        $.ajax({
            url: '/SanPhamDetail/LoadData',
            type: 'Get',
            data: {
                IdDongHo: getID,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstChiTietDongHo = response.lstChiTietDongHo;
                    var lstChiTietDongHoCon = response.lstChiTietDongHoCon;

                    if (lstChiTietDongHo != null) {
                        var html = '';
                        var template = $('#data-chi-tiet-anh').html();

                        $.each(lstChiTietDongHoCon, function (i, item) {
                            html += Mustache.render(template, {
                                Url_File: item.urlAnh,
                            });
                        });

                        $('#lstSanPhamCT').html(html);

                        $('#TenSanPham').html(lstChiTietDongHo.tenLoai);

                        $("#imgAnhSanPham").attr('src', lstChiTietDongHo.urlAnh);

                    }
                }
            }
        })
    },

    DatHang: function () {
        $.ajax({
            url: '/SanPhamDetail/DatHang',
            type: 'Post',
            data: {
                IdDongHo: getID,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    window.open('/Admin/DatHang/DatHang?');
                } else {
                    XNoti_CanhBao('Bạn phải đăng nhập trước khi đặt hàng');
                }
            }
        });
    },


};
myController.init();