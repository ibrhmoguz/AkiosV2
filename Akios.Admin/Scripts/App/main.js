
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
    $('#LoadingDiv').modal();
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

    $('#MessageBoxDiv').modal();
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

    //if (!AuthenticationKontrolu())
    //    return;

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
        "sProcessing": "&#304;&#351;lem yap&#305;l&#305;yor...",
        "sLengthMenu": "<span class='showentries'>Satır Sayısı:</span> _MENU_  ",
        "sZeroRecords": "Hi&#231; kay&#305;t bulunamad&#305;",
        "sEmptyTable": "Hi&#231; kay&#305;t bulunamad&#305;",
        "sLoadingRecords": "Bekleyin...",
        "sInfo": "G&#246;sterilen _START_ ile _END_ Toplam _TOTAL_  Kay&#305;t Vard&#305;r",
        "sInfoEmpty": "G&#246;sterilen 0 to 0 of 0 Kay&#305;t",
        "sInfoFiltered": "(Filtreleme d&#305;&#351;&#305;nda _MAX_ Toplam Kay&#305;t Vard&#305;r)",
        "sInfoPostFix": "",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "&#304;lk",
            "sPrevious": "Geri",
            "sNext": "&#304;leri",
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
    "bFilter": false,
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

