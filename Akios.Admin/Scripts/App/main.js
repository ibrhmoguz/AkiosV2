$(document).bind("ajaxSend", function () {
    ShowLoadingAnimation();
}).bind("ajaxComplete", function () {
    HideLoadingAnimation();
}).bind("ajaxError", function (event, jqxhr, settings, thrownError) {
    HideLoadingAnimation();
    ShowAjaxError(event, jqxhr, settings, thrownError);
});


function ShowLoadingAnimation() {
    var panel = document.createElement("div");
    $(panel).attr("id", "loadingDiv");
    $(panel).height(document.documentElement.clientHeight);
    $(panel).width(document.documentElement.clientWidth);
    $(panel).css("background-color", "black");
    $(panel).css("position", "fixed");
    $(panel).css("top", 0);
    $(panel).css("left", 0);
    $(panel).css("z-index", "1200");
    $(panel).css({ opacity: 0.4 });

    var animationPanel = document.createElement("div");
    animationPanel.innerHTML = "<img src='Content/Image/loader.gif' style='position:relative' alt='' />";
    $(animationPanel).css("position", "relative");
    var top = ($(panel).height()) / 2 - 200;
    var left = ($(panel).width()) / 2 - 150;
    $(animationPanel).css("top", top);
    $(animationPanel).css("left", left);
    $(panel).html(animationPanel);
    $("body").append($(panel));
}

function HideLoadingAnimation() {
    $("#loadingDiv").remove();
}

function ShowAjaxError(event, jqxhr, settings, thrownError) {
    alert(jqxhr.responseText);
}

/****** Document READY **********/

$(document).ready(function () {
    $("#TestButton").click(function () {
        AuthenticationKontrolu();
    });
});


/****** Document READY **********/

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