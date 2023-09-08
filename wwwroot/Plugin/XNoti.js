var XNoti_ThanhCong = function (noidung, title) {
    toastr.options = {
        "closeButton": true,
        "timeOut": "3000",
    };
    toastr.success(noidung, title);
};
var XNoti_ThongTin = function (noidung, title) {
    toastr.options = {
        "closeButton": true,
        "timeOut": "30000",
    };
    toastr.info(noidung, title);
};
var XNoti_CanhBao = function (noidung, title) {
    toastr.options = {
        "closeButton": true,
        "timeOut": "30000",

    };
    toastr.warning(noidung, title);
};
var XNoti_Loi = function (noidung, title) {
    toastr.options = {
        "closeButton": true,
        "timeOut": "30000",
    };
    toastr.error(noidung, title);
};

var XCheckNull = function(value, message) {


    if (value === null || value === undefined || value === "") {

        if (message) {


            toastr.options = {
                "closeButton": true,
                "timeOut": "30000",

            };
            toastr.warning(message);


        }

        return true;
    } else {

        var obj = String(value);
        if (obj.match(/^ *$/) !== null) {
            toastr.options = {
                "closeButton": true,
                "timeOut": "30000",

            };
            toastr.warning(message);

            return true;
        } else {
            return false;
        }

    }

};

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};


