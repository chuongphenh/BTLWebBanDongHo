var files;  
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

        $('#btnLuuDongHo').off('click').on('click', function () {
            myController.SaveData();
        });

        $('#inputFileUpload').filestyle({
            text: '',
            dragdrop: false,
            btnClass: 'btn-success',
            htmlIcon: '<span class="fa fa-upload" style="height: 20px;"></span> ',
        });

        $('#inputFileUpload2').filestyle({
            text: '',
            dragdrop: false,
            btnClass: 'btn-success',
            htmlIcon: '<span class="fa fa-upload" style="height: 20px;"></span> ',
        });

        $('#btn_remove_file').off('click').on('click', function () {
            $('#is_thay_doi').val('true');
            $("#inputFileUpload").filestyle('clear');
        });

        $('#btn_remove_file2').off('click').on('click', function () {
            $('#is_thay_doi').val('true');
            $("#inputFileUpload2").filestyle('clear');
        });

        $('#btnTaoMoi').off('click').on('click', function () {
            myController.ResetDongHo();
            XNoti_ThanhCong('Tạo mới thành công mời bạn nhập dữ liệu');
        });

        $("#inputFileUpload2").on('change', function (e) {
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

        $('#btnUploadFileDH').off('click').on('click', function () {
            myController.SaveFileAnhCon();
        });
    },
    ResetDongHo: function () {
        $('#IdDongHo').val(0);
        $('#MaCode').val("");
        $('#TenLoai').val("").trigger('chosen:updated');;
        $('#dtNgayTao').val("");
        $('#GiaBan').val("");
        $('#Sale').val("");
        $('#cbChatLieuMat').val("").trigger('chosen:updated');
        $('#cbChatLieuDay').val("").trigger('chosen:updated');
        $('#cbChatLieuVo').val("").trigger('chosen:updated');
        $('#SoLuong').val("");
        $('#KieuDang').val("");
        $('#DungLuong').val("");
        $('#DuongkinhMat').val("");
        $('#KichThuocDay').val("");
        $('#KichThuocVo').val("");
        $('#BaoHanh').val("");
        $("#ChiuNuoc").prop('checked', false).iCheck('update');
        $("#image_HoSoX").attr('src', "/DocumentImage/user-286.png");
        $("#inputFileUpload").val("");
        $("#image_HoSoX").css('width', '115px')
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/C_QuanLyDongHo/LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTenDongHo = response.lstTenDongHo;
                    var lstChatLieuMat = response.lstChatLieuMat;
                    var lstChatLieuDay = response.lstChatLieuDay;
                    var lstChatLieuVo = response.lstChatLieuVo;

                    if (lstTenDongHo != null) {
                        var optionX = "<option value=''>-- Chọn tên đồng hồ --</option>";
                        $.each(lstTenDongHo, function (index, val) {
                            optionX += "<option value='" + val.idLoaiDongHo + "'>" + val.tenLoai + "</option>";
                        });
                        $("#TenLoai").html(optionX).trigger('chosen:updated');
                    }

                    if (lstChatLieuMat != null) {
                        var optiona = "<option value=''>-- Chọn chất liệu mặt --</option>";
                        $.each(lstChatLieuMat, function (index, val) {
                            optiona += "<option value='" + val.tenDanhMuc + "'>" + val.tenDanhMuc + "</option>";
                        });
                        $("#cbChatLieuMat").html(optiona).trigger('chosen:updated');
                    }


                    if (lstChatLieuDay != null) {
                        var optionZ = "<option value=''>-- Chọn chất liệu dây --</option>";
                        $.each(lstChatLieuDay, function (index, val) {
                            optionZ += "<option value='" + val.tenDanhMuc + "'>" + val.tenDanhMuc + "</option>";
                        });
                        $("#cbChatLieuDay").html(optionZ).trigger('chosen:updated');
                    }

                    if (lstChatLieuVo != null) {
                        var optiony = "<option value=''>-- Chọn chất liệu vỏ --</option>";
                        $.each(lstChatLieuVo, function (index, val) {
                            optiony += "<option value='" + val.tenDanhMuc + "'>" + val.tenDanhMuc + "</option>";
                        });
                        $("#cbChatLieuVo").html(optiony).trigger('chosen:updated');
                    }


                    myController.LoadTable();
                    myController.registerEvent();
                }
            }
        })
    },

    SaveData: function () {
        var IdDongHo = $('#IdDongHo').val();
        var MaCode = $('#MaCode').val();
        var TenLoai = $('#TenLoai').val();
        var dtNgayTao = $('#dtNgayTao').val();
        var GiaBan = $('#GiaBan').val();
        var Sale = $('#Sale').val();
        var cbChatLieuMat = $('#cbChatLieuMat').val();
        var cbChatLieuDay = $('#cbChatLieuDay').val();
        var cbChatLieuVo = $('#cbChatLieuVo').val();
        var SoLuong = $('#SoLuong').val();
        var KieuDang = $('#KieuDang').val();
        var DungLuong = $('#DungLuong').val();
        var DuongkinhMat = $('#DuongkinhMat').val();
        var KichThuocDay = $('#KichThuocDay').val();
        var KichThuocVo = $('#KichThuocVo').val();
        var BaoHanh = $('#BaoHanh').val();
        var ChiuNuoc = $('#ChiuNuoc').prop('checked');

        if (MaCode == "" || MaCode == null || MaCode == undefined) {
            XNoti_CanhBao('Bạn chưa nhập mã code', 'Cảnh Báo');
            $("#MaCode").focus();
            return;
        }

        if (TenLoai == "" || TenLoai == null || TenLoai == undefined) {
            XNoti_CanhBao('Bạn chưa chọn tên đồng hồ', 'Cảnh Báo');
            $("#TenLoai").focus();
            return;
        }

        var objData =
        {
            IdDongHo: IdDongHo,
            MaCode: MaCode,
            IdLoaiDongHo: TenLoai,
            dtNgayTao: dtNgayTao,
            GiaBan: GiaBan,
            cbChatLieuMat: cbChatLieuMat,
            cbChatLieuDay: cbChatLieuDay,
            cbChatLieuVo: cbChatLieuVo,
            Sale: Sale,
            SoLuong: SoLuong,
            KieuDang: KieuDang,
            DungLuong: DungLuong,
            DuongkinhMat: DuongkinhMat,
            KichThuocDay: KichThuocDay,
            KichThuocVo: KichThuocVo,
            BaoHanh: BaoHanh,
            ChiuNuoc: ChiuNuoc,
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
                    url: '/Admin/C_QuanLyDongHo/SaveData',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            XNoti_ThanhCong('Cập nhập thành công');
                            myController.LoadTable();
                            $("#modal-form").modal('hide');

                        } else {
                            XNoti_CanhBao('Upload thất bại', 'Cảnh Báo');
                        }
                    },
                    error: function (response) {
                        XNoti_CanhBao('Upload thất bại', 'Cảnh Báo');
                    }
                });
            } else {
                alert("Upload file thất bại!");
            }
        }

        else {

            $.ajax({
                type: "POST",
                url: '/Admin/C_QuanLyDongHo/SaveData',
                dataType: 'json',
                data: {
                    str_JSON: JSON.stringify(objData),
                    is_thay_doi: $('#is_thay_doi').val()
                },
                success: function (response) {
                    if (response.status == true) {
                        XNoti_ThanhCong('Cập nhập thành công');
                        myController.LoadTable();
                        $("#modal-form").modal('hide');
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

    SaveFileAnhCon: function () {
        var IdDongHo = $('#IdDongHo').val();
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
                data.append("IdDongHo", IdDongHo);
                $.ajax({
                    type: "POST",
                    url: '/Admin/C_QuanLyDongHo/SaveFileAnhCon',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            XNoti_ThanhCong('Cập nhập thành công');
                            myController.LoadFile();
                        } else {
                            XNoti_CanhBao('Upload thất bại', 'Cảnh Báo');
                        }
                    },
                    error: function (response) {
                        XNoti_CanhBao('Upload thất bại', 'Cảnh Báo');
                    }
                });
            } else {
                alert("Upload file thất bại!");
            }
        }
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/C_QuanLyDongHo/LoadTable',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTableDongHo = response.lstTableDongHo;

                    if (lstTableDongHo != null) {
                        $("#tblDongHo").bootstrapTable('load', lstTableDongHo);
                    } else {
                        $("#tblDongHo").bootstrapTable('removeAll');
                    }

                    var html = '';
                    var template = $('#data-dong-ho').html();
                    $.each(lstTableDongHo, function (i, item) {
                        html += Mustache.render(template, {
                            urlFile: item.urlAnh,
                            TenDongHo: item.tenLoai,
                            idDongHo: item.idDongHo,
                        });
                    });

                    $('#lstChiTietDongHo').html(html);

                    myController.registerEvent();
                }
            }
        })
    },

    DeleteData: function (idDongHo) {
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
                        url: '/Admin/C_QuanLyDongHo/DeleteData',
                        type: 'Post',
                        data: {
                            idDongHo: idDongHo,
                        },
                        dataType: 'json',
                        success: function (response) {
                            if (response.status) {
                                XNoti_ThanhCong('Đã xóa thành công 1 bản ghi');
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



    LoadDetail: function (idDongHo) {

        if (idDongHo < 0) {
            return 0;
        }

        $.ajax({
            url: '/Admin/C_QuanLyDongHo/LoadDetail',
            type: 'Get',
            data: {
                idDongHo: idDongHo,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var data = response.data;

                    $('#IdDongHo').val(data.idDongHo);
                    $('#TenLoai').val(data.idLoaiDongHo).trigger('chosen:updated');
                    $('#Sale').val(data.sale);
                    $('#MaCode').val(data.maCode);
                    $('#SoLuong').val(data.soLuong);
                    $('#KieuDang').val(data.kieuDang);
                    $('#DungLuong').val(data.dungLuong);
                    $('#cbChatLieuMat').val(data.cbChatLieuMat).trigger('chosen:updated');
                    $('#DuongkinhMat').val(data.duongKinhMat);
                    $('#cbChatLieuDay').val(data.cbChatLieuDay).trigger('chosen:updated');
                    $('#KichThuocDay').val(data.kichThuocDay);
                    $('#cbChatLieuVo').val(data.cbChatLieuVo).trigger('chosen:updated');
                    $('#KichThuocVo').val(data.kichThuocVo);
                    $('#BaoHanh').val(data.baoHanh);
                    $('#GiaBan').val(data.giaBan);
                    $('#dtNgayTao').val(data.dtNgayTao);
                    $("#ChiuNuoc").prop('checked', data.chiuNuoc).iCheck('update');


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
                    $("#modal-form").modal('show');
                    myController.registerEvent();
                }
            }
        })
    },

    ThemChiTietAnh: function (idDongHo) {

        if (idDongHo < 0) {
            return 0;
        }

        $.ajax({
            url: '/Admin/C_QuanLyDongHo/ThemChiTietAnh',
            type: 'Get',
            data: {
                idDongHo: idDongHo,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstAnh = response.lstAnh;

                    if (lstAnh != null) {
                        var html = '';
                        var template = $('#data-dong-ho-con').html();
                        $.each(lstAnh, function (i, item) {
                            html += Mustache.render(template, {
                                urlFile_Con: item.urlAnh,
                                IdDocument: item.idDocument,
                            });
                        });

                        $('#lstChiTietDongHoCon').html(html);
                    }

                    $("#modal-form2").modal('show');
                    $('#IdDongHo').val(idDongHo);
                    myController.registerEvent();
                }
            }
        })
    },

    DeleteAnhCon: function (IdDocument) {
        $.ajax({
            url: '/Admin/C_QuanLyDongHo/DeleteAnhCon',
            type: 'Post',
            data: {
                IdDocument: IdDocument,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    XNoti_ThanhCong('Đã xóa thành công 1 bản ghi');
                    myController.LoadFile();
                }
            }
        })
    },

    LoadFile: function () {
        var IdDongHo = $('#IdDongHo').val();
        $.ajax({
            url: '/Admin/C_QuanLyDongHo/LoadFile',
            type: 'Get',
            data: {
                IdDongHo: IdDongHo,
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var lstAnh = response.lstAnh;

                    if (lstAnh != null) {
                        var html = '';
                        var template = $('#data-dong-ho-con').html();
                        $.each(lstAnh, function (i, item) {
                            html += Mustache.render(template, {
                                urlFile_Con: item.urlAnh,
                                IdDocument: item.idDocument,
                            });
                        });

                        $('#lstChiTietDongHoCon').html(html);
                    }
                }
            }
        })
    }

}
myController.init();


function ChucNang(e, value, row, index) {

    return [
        '<div style="width:85px">',
        '<a class="btn btn-outline btn-warning dim" title="Xem thông tin" href="javascript:myController.LoadDetail(' + value.idDongHo + ')" ><i class="fa fa-pencil"></i></a>',
        '<a class="btn btn-outline btn-danger  dim" title="Xem thông tin" style="margin-left: 5px;" href="javascript:myController.DeleteData(' + value.idDongHo + ')"><i class="fa fa-remove"></i></a>',
        '</div>'
    ].join('');
};