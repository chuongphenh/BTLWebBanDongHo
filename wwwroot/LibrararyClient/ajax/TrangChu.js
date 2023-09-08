var myController = {
    init: function () {
        myController.LoadData();
    },


    LoadData: function () {
        $.ajax({
            url: '/Product/LoadData',
            type: 'Get',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstChiTietDongHoNam = response.lstChiTietDongHoNam;

                    if (lstChiTietDongHoNam != null) {
                        var html = '';
                        var template = $('#data-dong-ho-nam').html();

                        $.each(lstChiTietDongHoNam, function (i, item) {
                            html += Mustache.render(template, {
                                IdDongHo: item.idDongHo,
                                urlFile: item.urlAnh,
                                TenDongHo: item.tenLoai,
                                GiaBan: item.giaBan,
                                GiaSale: item.giaSale,
                                Sale: item.sale,
                            });
                        });

                        $('#lstChiTietDongHoNam').html(html);
                    }
                }
            }
        })
    },

    DetailSanPham: function (IdDongHo) {
        window.open('/SanPhamDetail/SanPhamDetail?' + IdDongHo, "_self");
    },

};
myController.init();