var myController = {
    init: function () {
        $('#IdDanhMuc').val(0);
        myController.LoadTable();
        myController.registerEvent();
    },

    registerEvent: function () {

        $('#btnTaoMoi').off('click').on('click', function () {
            myController.Reset();
            XNoti_ThanhCong('Tạo mới thành công');
        });

        $('#tblDanhMuc').off('click-row.bs.table').on('click-row.bs.table', function (e, row, $element) {
            $('.table-warning').removeClass('table-warning');
            $($element).addClass('table-warning');
            myController.LoadDetail(row.idDanhMuc);
        });

        $('#btnLuu').off('click').on('click', function () {
            myController.SaveData();
        });

        $('#btnXoaDanhMuc').off('click').on('click', function () {
            var IdDanhMuc = $("#IdDanhMuc").val();
            myController.DeleteData(IdDanhMuc);
        });

        $('#cbLoaiDanhMuc').off('change').on('change', function () {
            myController.LoadTable();
        });

    },

    Reset: function () {
        $('#IdDanhMuc').val(0);
        $('#cbLoaiDanhMuc').val("");
        $('#MaDanhMuc').val("");
        $('#TenDanhMuc').val("");
        $("#tblDanhMuc").bootstrapTable('removeAll');
    },

    SaveData: function () {

        var IdDanhMuc = $("#IdDanhMuc").val();
        var TenDanhMuc = $("#TenDanhMuc").val();
        var MaDanhMuc = $("#MaDanhMuc").val();
        var cbLoaiDanhMuc = $("#cbLoaiDanhMuc").val();

        if (cbLoaiDanhMuc == "" || cbLoaiDanhMuc == null || cbLoaiDanhMuc == undefined) {
            XNoti_CanhBao('Bạn chưa chọn phân loại danh mục', 'Cảnh Báo');
            $("#cbLoaiDanhMuc").focus();
            return;
        }

        if (MaDanhMuc == "" || MaDanhMuc == null || MaDanhMuc == undefined) {
            XNoti_CanhBao('Bạn chưa nhập mã danh mục', 'Cảnh Báo');
            return;
        }

        var objData = {
            IdDanhMuc: IdDanhMuc,
            TenDanhMuc: TenDanhMuc,
            MaDanhMuc: MaDanhMuc,
            cbLoaiDanhMuc: cbLoaiDanhMuc,
        }

        $.ajax({

            url: "/Admin/DanhMucHeThong/SaveData",
            type: "Post",
            dataType: "json",
            data: {
                data: JSON.stringify(objData),
            },
            success: function (response) {
                    if (response.status = true) {
                        XNoti_ThanhCong('Cập nhâp thành công');
                        myController.LoadTable();
                    }
                    else {
                        XNoti_CanhBao('Mã danh mục đã tồn tại !', 'Cảnh Báo');
                    }
                    myController.registerEvent();
            },
        })
    },

    DeleteData: function (idDanhMuc) {

        if (idDanhMuc <= 0) {
            XNoti_CanhBao('Bạn chưa chọn bản ghi để xóa !', 'Cảnh Báo');
            return 0;
        }

        $.ajax({
            url: '/Admin/DanhMucHeThong/DeleteData',
            type: 'Post',
            data: {
                idDanhMuc: idDanhMuc,
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
        var cbLoaiDanhMuc = $('#cbLoaiDanhMuc').val();
        $.ajax({
            url: '/Admin/DanhMucHeThong/LoadTable',
            type: 'GET',
            data: {
                cbLoaiDanhMuc: cbLoaiDanhMuc,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTable = response.lstTable;

                    if (lstTable != null) {
                        $("#tblDanhMuc").bootstrapTable('load', lstTable);
                    } else {
                        $("#tblDanhMuc").bootstrapTable('removeAll');
                    }

                    myController.registerEvent();
                }
            }
        })
    },

    LoadDetail: function (idDanhMuc) {

        if (idDanhMuc <= 0) {
            return 0;
        }

        $.ajax({
            url: '/Admin/DanhMucHeThong/LoadDetail',
            type: 'Get',
            data: {
                IdDanhMuc: idDanhMuc,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var data = response.data;

                    $("#IdDanhMuc").val(data.idDanhMuc);
                    $("#TenDanhMuc").val(data.tenDanhMuc);
                    $("#MaDanhMuc").val(data.maDanhMuc);
                    $("#cbLoaiDanhMuc").val(data.cbLoaiDanhMuc);
                    myController.registerEvent();
                }
            }
        })
    },

}
myController.init();

