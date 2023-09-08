var myController = {
    init: function () {
        myController.LoadData();
        myController.registerEvent();
    },

    registerEvent: function () {
        $('#btnMuaHang').off('click').on('click', function () {
            myController.GuiDuyet();
        });
    },

    GuiDuyet: function () {
        $.ajax({
            url: '/Admin/DatHang/GuiDuyet',
            type: 'Post',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    myController.LoadData();
                }
            }
        })
    },


    LoadData: function () {
        $.ajax({
            url: '/Admin/DatHang/LoadData',
            type: 'Get',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstDonHang = response.lstDonHang;

                    if (lstDonHang != null) {
                        var html = '';
                        var template = $('#data-datHang').html();

                        $.each(lstDonHang, function (i, item) {
                            html += Mustache.render(template, {
                                urlFile: item.urlAnh,
                                TenLoai: item.tenLoai,
                                IdChiTietDonDat: item.idChiTietDonDat,
                                GiaBan: item.giaBanT,
                                GiaSale: item.giaSale,
                            });
                        });

                        $('#lstDonHang').html(html);
                        $('#TongTien').html(response.tong);
                    }
                }
            }
        })
    },



    DeleateData: function (IdChiTietDonDat) {
        $.ajax({
            url: '/Admin/DatHang/DeleateData',
            type: 'Post',
            data: {
                IdChiTietDonDat: IdChiTietDonDat,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    myController.LoadData();
                }
            }
        })
    }

   
};
myController.init();