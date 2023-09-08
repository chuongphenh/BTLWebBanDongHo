var myController = {
    init: function () {
        myController.LoadData();
        myController.registerEvent();
    },

    registerEvent: function () {

     
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/DuyetDonHang/LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {

                    var lstTableDonGui = response.lstTableDonGui;

                    if (lstTableDonGui != null) {
                        $("#tblDonDat").bootstrapTable('load', lstTableDonGui);
                    } else {
                        $("#tblDonDat").bootstrapTable('removeAll');
                    }

                    $('#modalDonDatHang').modal('show');

                    myController.registerEvent();
                }
            }
        })
    },


    LoadTableDonHang: function (idDonDat) {
        $.ajax({
            url: '/Admin/DuyetDonHang/LoadTableDonHang',
            type: 'GET',
            dataType: 'json',
            data: {
                IdDonDat: idDonDat
            },
            success: function (response) {
                if (response.status) {

                    var lstTableDonGui = response.lstTableDonGui;

                    if (lstTableDonGui != null) {
                        $("#tblChiTietDonDat").bootstrapTable('load', lstTableDonGui);
                    } else {
                        $("#tblChiTietDonDat").bootstrapTable('removeAll');
                    }
                    myController.registerEvent();
                }
            }
        })
    },

}
myController.init();


function TenKhachHang(e, value, row, index) {

    return [
        '<div>',
        '<a class="hvr-shrink" href="javascript:myController.LoadTableDonHang(' + value.idDonDat + ')"  title="Xem thông tin" style="color:#448aff" >' + value.hoTen + '</a>',
        '</div>'
    ].join('');
};
Anh
function Anh(e, value, row, index) {

    return [
        '<div style="width:115px">',
        '<a class="hvr-shrink" href="' + value.urlAnh + '"  title="Xem thông tin" style="color:#448aff" >' + '<img src = "' + value.urlAnh + '" class= "img-responsive" style = "width:115px;"/>'+'</a>',
        '</div>'
    ].join('');
};
function ChucNang(e, value, row, index) {

    return [
        '<div style="width:85px">',
        '<a class="btn btn-outline btn-success dim" title="Xem thông tin" href="javascript:myController.Duyet(' + value.idDongHo + ')" ><i class="fa fa-check"></i></a>',
        '<a class="btn btn-outline btn-danger  dim" title="Xem thông tin" style="margin-left: 5px;" href="javascript:myController.TuChoi(' + value.idDongHo + ')"><i class="fa fa-remove"></i></a>',
        '</div>'
    ].join('');
};