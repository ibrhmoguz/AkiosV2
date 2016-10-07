$('#tableMusteriler').dataTable({
    "fnDrawCallback": function () {

    },
    "sAjaxSource": 'Musteri/JsonList',
    "aoColumns": [{
        "sName": "Logo",
        "bSearchable": false,
        "bSortable": false,
        "fnRender": function () { return "<img class=\"img-circle\" width=\"50\" height=\"50\" src=\"/Musteri/LogoYukle?musteriId=" + oObj.aData[0] + "/>"; }
    },
    {
        "sName": "KodMusteri",
        "bSearchable": true,
        "bSortable": true,
        "fnRender": function (oObj) { return oObj.aData[1]; }
    },
     {
         "sName": "YetkiliKisi",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[2]; }
     },
     {
         "sName": "Adres",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[3]; }
     },
     {
         "sName": "Tel",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[4]; }
     },
     {
         "sName": "Faks",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[5]; }
     },
     {
         "sName": "Mobil",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[6]; }
     },
     {
         "sName": "Mail",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[7]; }
     },
     {
         "sName": "Web",
         "bSearchable": true,
         "bSortable": true,
         "fnRender": function (oObj) { return oObj.aData[8]; }
     },
     {
         "sName": "",
         "bSearchable": false,
         "bSortable": false,
         "fnRender": function (oObj) { return "<input type='button' class='btn btn-block btn-info' value='Düzenle' MusteriId = " + oObj.aData[0] + "/>"; }
     },
     {
         "sName": "",
         "bSearchable": false,
         "bSortable": false,
         "fnRender": function (oObj) { return "<input type='button' class='btn btn-block btn-danger' value='Sil' MusteriId = " + oObj.aData[0] + "/>"; }
     }]
});