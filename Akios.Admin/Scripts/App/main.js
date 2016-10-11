
/******* COMMON FUNCTIONS *************/

$(document).bind("ajaxSend", function () {
    ShowLoadingAnimation();
}).bind("ajaxComplete", function () {
    HideLoadingAnimation();
}).bind("ajaxError", function (event, jqxhr, settings, thrownError) {
    HideLoadingAnimation();
    ShowAjaxError(event, jqxhr, settings, thrownError);
});


function ShowLoadingAnimation() {
    $('#LoadingDiv').modal({ position: 'center' });
}

function HideLoadingAnimation() {
    $("#LoadingDiv").modal('hide');
}

function ShowAjaxError(event, jqxhr, settings, thrownError) {
    MessageBox(thrownError + " " + jqxhr.responseText, "Error");
}

function MessageBox(msj, title) {
    if (title == "Info") {
        SetMessageBoxValues("Bilgi", msj, "box box-solid box-primary", "icon fa fa-info", "btn btn-primary");
    }
    else if (title == "Error") {
        SetMessageBoxValues("Hata", msj, "box box-solid box-danger", "icon fa fa-ban", "btn btn-danger");
    }
    else if (title == "Warning") {
        SetMessageBoxValues("Uyarı", msj, "box box-solid box-warning", "icon fa fa-warning", "btn btn-warning");
    }
    else if (title == "Success") {
        SetMessageBoxValues("Başarı", msj, "box box-solid box-success", "icon fa fa-check", "btn btn-success");
    }

    $("#MessageBoxDiv").modal({ position: 'center' });
}

function SetMessageBoxValues(headerText, msj, solidClass, iconClass, buttonClass) {
    $('#MessageBoxHeaderContent').text(headerText);
    $('#MessageBoxBodyContent').text(msj);
    $('#MessageBoxSolid').addClass(solidClass);
    $('#MessageBoxIcon').addClass(iconClass);
    $('#MessageBoxCloseButton').addClass(buttonClass);
}

function AuthenticationKontrolu() {
    $.ajax({
        type: 'GET',
        cache: false,
        dataType: "json",
        url: 'Account/IsAuthenticated',
        success: function (msg) {
            if (msg.Action == "Fail") {
                FailAuthentication();
                return false;
            }
            else { return true; }
        },
        error: function () {
            FailAuthentication();
            return false;
        }
    });

    return true;
}

function FailAuthentication() {
    window.location.href = 'Account/Login';
}

function PageLoad(url) {

    if (!AuthenticationKontrolu())
        return;

    $.ajax({
        type: 'GET',
        async: true,
        url: url,
        cache: false,
        success: function (msg) {
            $("#mainContentDiv").html(msg);
        }
    });
}


/********** JQUERY DATATABLES DEFAULTS  **********/

$.extend(true, $.fn.dataTable.defaults, {
    "oLanguage": {
        "sSearch": "Arama ",
        "sProcessing": "Yükleniyor...",
        "sLengthMenu": "<span class='showentries'>Satır Sayısı:</span> _MENU_  ",
        "sZeroRecords": "Hiç kayıt bulunamadı",
        "sEmptyTable": "Hiç kayıt bulunamadı",
        "sLoadingRecords": "Bekleyin...",
        "sInfo": "Toplam _TOTAL_ Kayıt İçinden _START_ ile _END_ Arası Kayıtlar Gösteriliyor.",
        "sInfoEmpty": "Gösterilen Kayıt Yoktur.",
        "sInfoFiltered": "(Filtreleme Dışında _MAX_ Toplam Kayıt Vardır)",
        "sInfoPostFix": "",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "İlk",
            "sPrevious": "Geri",
            "sNext": "İleri",
            "sLast": "Son"
        }
    },
    "paging": true,
    "lengthChange": false,
    "searching": true,
    "ordering": true,
    "info": true,
    "autoWidth": false,
    "sPaginationType": "full_numbers",
    'bServerSide': true,
    'bProcessing': true,
    "sDom": '<"tablePars"fl>t<"tableFooter"ip>',
    "fnPreDrawCallback": function () { ShowLoadingAnimation(); },
    "fnDrawCallback": function () { HideLoadingAnimation(); }
});



/********** COMMON FUNCTIONS *************/


/********** Document READY **********/

$(document).ready(function () {
    //$("#TestButton").click(function () {
    //    //MessageBox("Test 23423 234234", "Warning");
    //    AuthenticationKontrolu();
    //});
});

