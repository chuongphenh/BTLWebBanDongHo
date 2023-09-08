var homeconfig = {
    pageSize: 9,
    pageIndex: 1,
}

var myController = {
    init: function () {
        myController.LoadThuongHieu();
        myController.LoadData();
    },

    LoadThuongHieu: function () {
        $.ajax({
            url: '/SanPham/LoadThuongHieu',
            type: 'Get',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstThuongHieu = response.lstThuongHieu;

                    if (lstThuongHieu != null) {
                        var html = '';
                        var template = $('#data-thuong-hieu').html();

                        $.each(lstThuongHieu, function (i, item) {
                            html += Mustache.render(template, {
                                TenThuongHieu: item.tenDanhMuc,
                                SoLuong: item.soLuong,
                            });
                        });

                        $('#lstThuongHieu').html(html);
                    }
                }
            }
        })
    },

    CTSanPham: function (TenThuongHieu,changePageSize) {
        $.ajax({
            url: '/SanPham/LoadChiTietSanPham',
            type: 'Get',
            data: {
                TenThuongHieu: TenThuongHieu,
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstChiTietDongHo = response.lstChiTietDongHoNam;

                    if (lstChiTietDongHo != null) {
                        var html = '';
                        var template = $('#data-dong-ho').html();

                        $.each(lstChiTietDongHo, function (i, item) {
                            html += Mustache.render(template, {
                                IdDongHo: item.idDongHo,
                                urlFile: item.urlAnh,
                                TenDongHo: item.tenLoai,
                                GiaBan: item.giaBan,
                                GiaSale: item.giaSale,
                            });
                        });

                        $('#lstChiTietDongHo').html(html);
                        myController.paging(response.total, function () {
                            myController.LoadChiTietSanPham();
                        }, changePageSize);
                    }
                }
            }
        })
    },


    LoadData: function (changePageSize) {
        $.ajax({
            url: '/SanPham/LoadData',
            type: 'Get',
            data: {
                page: homeconfig.pageIndex,
                pageSize: homeconfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstChiTietDongHo = response.lstChiTietDongHoNam;

                    if (lstChiTietDongHo != null) {
                        var html = '';
                        var template = $('#data-dong-ho').html();

                        $.each(lstChiTietDongHo, function (i, item) {
                            html += Mustache.render(template, {
                                IdDongHo: item.idDongHo,
                                urlFile: item.urlAnh,
                                TenDongHo: item.tenLoai,
                                GiaBan: item.giaBan,
                                GiaSale: item.giaSale,
                            });
                        });

                        $('#lstChiTietDongHo').html(html);
                        myController.paging(response.total, function () {
                            myController.LoadData();
                        }, changePageSize);
                    }
                }
            }
        })
    },

    DetailSanPham: function (IdDongHo) {
        window.open('/SanPhamDetail/SanPhamDetail?' + IdDongHo, "_self");
    },

    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
            $('#pagination').attr('margin','0 auto')
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                homeconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }


};
myController.init();