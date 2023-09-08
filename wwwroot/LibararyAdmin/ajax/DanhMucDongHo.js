var myController = {
    init: function () {
        myController.LoadData();
        myController.registerEvent();
    },

    registerEvent: function () {
        $(".chosen").chosen();
        $(".chosen-results").css('font-size', '14px');
        $(".chosen-results").css('font-family', 'FontAwesome');
        $(".chosen-container").css('width', '100%');


        $('#btnTaoMoi').off('click').on('click', function () {
            myController.Reset();
            XNoti_ThanhCong('Tạo mới thành công');
        });

        $('#tblDanhMucDongHo').off('click-row.bs.table').on('click-row.bs.table', function (e, row, $element) {
            $('.table-warning').removeClass('table-warning');
            $($element).addClass('table-warning');
            myController.LoadDetail(row.idLoaiDongHo);
        });

        $('#btnLuu').off('click').on('click', function () {
            myController.SaveData();
        });

        $('#btnXoaDanhMuc').off('click').on('click', function () {
            var IdLoaiDongHo = $("#IdLoaiDongHo").val();
            myController.DeleteData(IdLoaiDongHo);
        });

        $('#tkHangSanXuat').off('change').on('change', function () {
            myController.LoadTable();
        });

        $('#tkThuongHieu').off('change').on('change', function () {
            myController.LoadTable();
        });
    },
    Reset: function () {
        $('#IdLoaiDongHo').val(0);
        $('#TenLoai').val("");
        $('#HangSanXuat').val("").trigger('chosen:updated');;
        $('#ThuongHieu').val("").trigger('chosen:updated');;
        $("#IsTrangThai").prop('checked', false).iCheck('update');
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/DanhMucDongHo/LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstThuongHieu = response.lstThuongHieu;
                    var lstHangSx = response.lstHangSx;

                    if (lstThuongHieu != null) {
                        var optionX = "<option value=''>-- Chọn thương hiệu --</option>";
                        $.each(lstThuongHieu, function (index, val) {
                            optionX += "<option value='" + val.tenDanhMuc + "'>" + val.tenDanhMuc + "</option>";
                        });
                        $("#ThuongHieu").html(optionX).trigger('chosen:updated');
                        $("#tkThuongHieu").html(optionX).trigger('chosen:updated');
                    }

                    if (lstHangSx != null) {
                        var optionH = "<option value=''>-- Chọn hãng sản xuất --</option>";
                        $.each(lstHangSx, function (index, val) {
                            optionH += "<option value='" + val.tenDanhMuc + "'>" + val.tenDanhMuc + "</option>";
                        });
                        $("#HangSanXuat").html(optionH).trigger('chosen:updated');
                        $("#tkHangSanXuat").html(optionH).trigger('chosen:updated');
                    }

                    myController.Reset();
                    myController.LoadTable();
                    myController.registerEvent();
                }
            }
        })
    },

    SaveData: function () {

        var IdLoaiDongHo = $("#IdLoaiDongHo").val();
        var TenLoai = $("#TenLoai").val();
        var HangSanXuat = $("#HangSanXuat").val();
        var ThuongHieu = $("#ThuongHieu").val();
        var IsTrangThai = $("#IsTrangThai").prop('checked')

        if (TenLoai == "" || TenLoai == null || TenLoai == undefined) {
            XNoti_CanhBao('Bạn chưa chọn tên đồng hồ', 'Cảnh Báo');
            $("#TenLoai").focus();
            return;
        }


        var objData = {
            IdLoaiDongHo: IdLoaiDongHo,
            TenLoai: TenLoai,
            HangSanXuat: HangSanXuat,
            ThuongHieu: ThuongHieu,
            IsTrangThai: IsTrangThai,
        }

        $.ajax({

            url: "/Admin/DanhMucDongHo/SaveData",
            type: "Post",
            dataType: "json",
            data: {
                data: JSON.stringify(objData),
            },
            success: function (response) {

                if (response.status) {
                    if (response.status = true) {
                        XNoti_ThanhCong('Cập nhâp thành công');
                        myController.LoadTable();
                    }
                    else {
                        XNoti_CanhBao('có lỗi !', 'Cảnh Báo');
                    }
                    myController.registerEvent();
                }
            },
        })
    },

    DeleteData: function (idLoaiDongHo) {

        if (idLoaiDongHo <= 0) {
            XNoti_CanhBao('Bạn chưa chọn bản ghi để xóa !', 'Cảnh Báo');
            return 0;
        }

        $.ajax({
            url: '/Admin/DanhMucDongHo/DeleteData',
            type: 'Post',
            data: {
                idLoaiDongHo: idLoaiDongHo,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    XNoti_ThanhCong('Đã xóa thành công bản ghi');
                    myController.LoadTable();
                    myController.registerEvent();
                }
            }
        })
    },


    LoadTable: function () {
        var tkHangSanXuat = $('#tkHangSanXuat').val();
        var tkThuongHieu = $('#tkThuongHieu').val();
        $.ajax({
            url: '/Admin/DanhMucDongHo/LoadTable',
            type: 'GET',
            data: {
                tkHangSanXuat: tkHangSanXuat,
                tkThuongHieu: tkThuongHieu,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTable = response.lstTable;

                    if (lstTable != null) {
                        $("#tblDanhMucDongHo").bootstrapTable('load', lstTable);
                    } else {
                        $("#tblDanhMucDongHo").bootstrapTable('removeAll');
                    }

                    myController.registerEvent();
                }
            }
        })
    },

    LoadDetail: function (idLoaiDongHo) {

        if (idLoaiDongHo <= 0) {
            return 0;
        }

        $.ajax({
            url: '/Admin/DanhMucDongHo/LoadDetail',
            type: 'Get',
            data: {
                idLoaiDongHo: idLoaiDongHo,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var data = response.data;

                    $("#IdLoaiDongHo").val(data.idLoaiDongHo);
                    $("#TenLoai").val(data.tenLoai);
                    $("#HangSanXuat").val(data.hangSanXuat).trigger('chosen:updated');
                    $("#ThuongHieu").val(data.thuongHieu).trigger('chosen:updated');
                    $("#IsTrangThai").prop('checked', data.isTrangThai).iCheck('update');

                    myController.registerEvent();
                }
            }
        })
    },

}
myController.init();

