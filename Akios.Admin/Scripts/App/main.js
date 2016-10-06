
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

/******* COMMON FUNCTIONS *************/


/****** Document READY **********/

$(document).ready(function () {
    $("#TestButton").click(function () {
        //MessageBox("Test 23423 234234", "Warning");
        AuthenticationKontrolu();
    });
});

function AuthenticationKontrolu() {
    $.ajax({
        type: 'GET',
        cache: false,
        dataType: "json",
        url: 'Account/IsAuthenticated',
        success: function (msg) {
            if (msg.Action == "Fail") {
                //FailAuthentication();
                return false;
            }
            else { return true; }
        },
        error: function (req, status, error) {
            //FailAuthentication();
            return false;
        }
    });

    return true;
}

function FailAuthentication() {
    window.location.href = 'Account/Login';
}