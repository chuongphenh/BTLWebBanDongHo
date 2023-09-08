var files;
var myController = {
    init: function () {
        myController.Reset();
        myController.registerEvent();
    },

    registerEvent: function () {

        $('.dataTables-example').DataTable({
            pageLength: 25,
            responsive: true,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                { extend: 'copy' },
                { extend: 'csv' },
                { extend: 'excel', title: 'ExampleFile' },
                { extend: 'pdf', title: 'ExampleFile' },

                {
                    extend: 'print',
                    customize: function (win) {
                        $(win.document.body).addClass('white-bg');
                        $(win.document.body).css('font-size', '10px');

                        $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                    }
                }
            ]
        });

        $('#btnLuu').off('click').on('click', function () {
            myController.SaveData();
        });

        $('#tblTaiKhoan').off('click-row.bs.table').on('click-row.bs.table', function (e, row, $element) {
            $('.table-warning').removeClass('table-warning');
            $($element).addClass('table-warning');
            myController.LoadDetail(row.userId);
        });

        $('#btnTaoMoi').off('click').on('click', function () {
            myController.Reset();
            $.notify("Tạo mới thành công.", "success");
        });
        $('#DanhSachTaiKhoan').off('click').on('click', function () {
            myController.LoadTable();
        });

        $("#inputFileUpload").on('change', function (e) {
            files = e.target.files;
            if (files.length > 0) {
                for (var x = 0; x < files.length; x++) {
                    if (files[x].size >= 52428800) {
                        $('#inputFileUpload').val(null);
                        $.notify("Bạn chỉ được tải file dưới 50 MB.", "warn");
                        $('#is_thay_doi').val('true');
                        return;
                    }
                    $('#is_thay_doi').val('true');
                }
            }
        });

    },
    Reset: function () {
        $('#UserId').val(0);
        $('#UserName').val("");
        $('#PassWord').val("");
        $('#NgaySinh').val("");
        $('#Email').val("");
        $('#GioiTinh').val("Nam");
        $('#SoDienThoai').val("");
        $("#isAdMin").prop('checked', false).iCheck('update');
        $("#image_HoSoX").attr('src', "/DocumentImage/user-286.png");
        $("#inputFileUpload").val("");
        $("#image_HoSoX").css('width', '115px')
    },

    SaveData: function () {
        var UserId = $('#UserId').val();
        var UserName = $('#UserName').val();
        var PassWord = $('#PassWord').val();
        var Email = $('#Email').val();
        var NgaySinh = $('#NgaySinh').val();
        var GioiTinh = $('#GioiTinh').val();
        var SoDienThoai = $('#SoDienThoai').val();
        var isAdMin = $('#isAdMin').prop('checked');

        if (UserName == "" || UserName == null || UserName == undefined) {
            $.notify("Bạn chưa nhập tài khoản", "warn");
            $("#CbDanhMuc").focus();
            return;
        }

        if (PassWord == "" || PassWord == null || PassWord == undefined) {
            $.notify("Bạn chưa nhập mật khẩu", "warn");
            return;
        }

        var objData =
        {
            UserId: UserId,
            UserName: UserName,
            PassWord: PassWord,
            Email: Email,
            NgaySinh: NgaySinh,
            GioiTinh: GioiTinh,
            SoDienThoai: SoDienThoai,
            IsAdmin: isAdMin,
        };

        if (files != undefined && files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();

                for (var x = 0; x < files.length; x++) {
                    if (files[x].size >= 52428800) {
                        $.notify("Bạn chỉ được tải file dưới 50 MB.", "warn");
                        $('#inputFileUpload').val("");
                        return;
                    }
                    data.append(files[x].name, files[x]);
                }
                data.append("str_JSON", JSON.stringify(objData));
                data.append("is_thay_doi", $('#is_thay_doi').val());
                $.ajax({
                    type: "POST",
                    url: '/Admin/AcountRegeterEvent/SaveData',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            $.notify("Cập nhập thành công.", "success");
                            myController.LoadTable();
                            $("#modal-form").modal('show');
                        } else {
                            $.notify(response.message, "warn");
                        }
                    },
                    error: function (response) {
                        $.notify("Upload thất bại.", "warn");
                    }
                });
            } else {
                alert("Upload file thất bại!");
            }
        }

        else {

            $.ajax({
                type: "POST",
                url: '/Admin/AcountRegeterEvent/SaveData',
                dataType: 'json',
                data: {
                    str_JSON: JSON.stringify(objData),
                    is_thay_doi: $('#is_thay_doi').val()
                },
                success: function (response) {
                    if (response.status == true) {
                        $.notify("Cập nhập thành công.", "success");
                        myController.LoadTable();
                        $("#modal-form").modal('show');
                    } else {
                        $.notify("Có lỗi xảy ra.", "warn");
                    }
                },
                error: function (response) {
                    $.notify("Có lỗi.", "warn");
                }
            });
        }
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/AcountRegeterEvent/LoadTable',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTable = response.lstTable;

                    if (lstTable != null) {
                        $("#tblTaiKhoan").bootstrapTable('load', lstTable);
                    } else {
                        $("#tblTaiKhoan").bootstrapTable('removeAll');
                    }

                    myController.registerEvent();
                }
            }
        })
    },

    deleteData: function (maBao) {
        swal({
            title: "Xác nhận xóa dữ liệu?",
            text: "Dữ liệu sẽ bị xóa!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Đồng ý!",
            cancelButtonText: "Thoát!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: '/admin/ChiTietSanPham/DeleteData',
                        type: 'Post',
                        data: {
                            maBao: maBao,
                        },
                        dataType: 'json',
                        success: function (response) {
                            if (response.status) {
                                $.notify("Đã xóa thành công bản ghi", "success");
                                myController.LoadTable();
                                myController.registerEvent();
                            }
                        }
                    })
                } else {
                    swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
    },



    LoadDetail: function (userId) {

        if (userId < 0) {
            return 0;
        }

        $.ajax({
            url: '/Admin/AcountRegeterEvent/LoadDetail',
            type: 'Get',
            data: {
                UserId: userId,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var data = response.data;

                    $('#UserId').val(data.userId);
                    $('#UserName').val(data.userName);
                    $('#PassWord').val(data.password);
                    $('#Email').val(data.email);
                    $('#NgaySinh').val(data.ngaySinh);
                    $('#GioiTinh').val(data.gioiTinh);
                    $('#SoDienThoai').val(data.soDienThoai);
                    $("#isAdMin").prop('checked', data.isAdmin).iCheck('update');

                    if (data.urlAnh != "") {
                        $("#image_HoSoX").css('width', '100%')
                        $("#image_HoSoX").css('border-radius', '0%')
                        $("#image_HoSoX").attr('src', data.urlAnh);
                        $("#image_HoSo2").attr('href', data.urlAnh);
                    }
                    else {
                        $("#image_HoSoX").attr('src', "/DocumentImage/user-286.png");
                        $("#inputFileUpload").val("");
                        $("#image_HoSoX").css('width', '115px')

                    }
                    $("#modal-form").modal('hide');
                    myController.registerEvent();
                }
            }
        })
    },

}
myController.init();
initThumbnail();


function TenTaiKhoan(e, value, row, index) {

    return [
        '<div style="width:85px">',
        '<a class="hvr-shrink" title="Xem thông tin" style="color:#448aff" >' + value.userName + '</a>',
        '</div>'
    ].join('');
};