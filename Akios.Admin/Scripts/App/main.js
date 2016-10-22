
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

function HideAddItemModalDialog() {
    $("#addItemModalDialogDiv").modal('hide');
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

function ModalLoad(url, baslik) {

    if (!AuthenticationKontrolu())
        return;

    $.ajax({
        type: 'GET',
        async: true,
        url: url,
        cache: false,
        success: function (msg) {
            $("#addItemModalDialogBodyDiv").html(msg);
            $("#addItemModalDialogHeader").html(baslik);
            $("#addItemModalDialogDiv").modal({ position: 'center' });
        }
    });
}

function DropDownLoad(dropDownListId, url, selected) {
    $.ajax({
        type: 'GET',
        url: url,
        data: selected,
        cache: false,
        dataType: "json",
        async: true,
        success: function (msg) {
            $("#" + dropDownListId).html(msg.Data);
        }
    });
}


/********** JQUERY DATATABLES DEFAULTS  **********/

$.extend(true, $.fn.dataTable.defaults, {
    "oLanguage": {
        "sSearch": "Arama ",
        "sProcessing": "Yükleniyor...",
        "sLengthMenu": "<span class='showentries'>Satır Sayısı:</span> _MENU_  ",
        "sZeroRecords": "",
        "sEmptyTable": "Hiç kayıt bulunamadı.",
        "sLoadingRecords": "Yükleniyor...",
        "sInfo": "Toplam _TOTAL_ Kayıt İçinden _START_ ile _END_ Arası Kayıtlar Gösteriliyor.",
        "sInfoEmpty": "",
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
    "bSort": true,
    "processing": true,
    "serverSide": true,
    "filter": true,
    "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "Hepsi"]],
    "pageLength": 10,
    "searching": true,
    "info": true,
    "lengthChange": true,
    "paging": true,
    "bAutoWidth": false,
    "pagingType": "full_numbers",
    "fnPreDrawCallback": function () { ShowLoadingAnimation(); },
    "fnDrawCallback": function () { HideLoadingAnimation(); }
});


/********** JQUERY FILE UPLOAD  **********/

function FileUpload(url, id) {
    var uploadButton = $('<button/>')
        .addClass('btn btn-primary')
        .prop('disabled', true)
        .text('Processing...')
        .on('click', function () {
            var $this = $(this),
                data = $this.data();
            $this
                .off('click')
                .text('Abort')
                .on('click', function () {
                    $this.remove();
                    data.abort();
                });
            data.submit().always(function () {
                $this.remove();
            });
        });
    $('#' + id).fileupload({
        url: url,
        dataType: 'json',
        autoUpload: true,
        singleFileUploads: true,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        maxFileSize: 999000,
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        previewMaxWidth: 100,
        previewMaxHeight: 100,
        previewCrop: false
    }).on('fileuploadadd', function (e, data) {
        data.context = $('<div/>').appendTo('#files');
        $.each(data.files, function (index, file) {
            var node = $('<p/>')
                .append($('<span/>').text(file.name));
            if (!index) {
                node
                    .append('<br>')
                    .append(uploadButton.clone(true).data(data));
            }
            node.appendTo(data.context);
        });
    }).on('fileuploadprocessalways', function (e, data) {
        var index = data.index,
            file = data.files[index],
            node = $(data.context.children()[index]);
        if (file.preview) {
            node
                .prepend('<br>')
                .prepend(file.preview);
        }
        if (file.error) {
            node
                .append('<br>')
                .append($('<span class="text-danger"/>').text(file.error));
        }
        if (index + 1 === data.files.length) {
            data.context.find('button')
                .text('Upload')
                .prop('disabled', !!data.files.error);
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('#progress .progress-bar').css(
            'width',
            progress + '%'
        );
    }).on('fileuploaddone', function (e, data) {
        $.each(data.result.files, function (index, file) {
            if (file.url) {
                var link = $('<a>')
                    .attr('target', '_blank')
                    .prop('href', file.url);
                $(data.context.children()[index])
                    .wrap(link);
            } else if (file.error) {
                var error = $('<span class="text-danger"/>').text(file.error);
                $(data.context.children()[index])
                    .append('<br>')
                    .append(error);
            }
        });
    }).on('fileuploadfail', function (e, data) {
        $.each(data.files, function (index) {
            var error = $('<span class="text-danger"/>').text('File upload failed.');
            $(data.context.children()[index])
                .append('<br>')
                .append(error);
        });
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
}


/********** COMMON FUNCTIONS *************/


/********** Document READY **********/

$(document).ready(function () {
    //$("#TestButton").click(function () {
    //    //MessageBox("Test 23423 234234", "Warning");
    //    AuthenticationKontrolu();
    //});
});

