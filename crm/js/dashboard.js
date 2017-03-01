
$(function () {
    inicializaTables();
})

function inicializaTables() {

    $("#etapas").html('');

    $.ajax({
        url: "proxy/Dashboard.ashx?metodo=getEtapas",
        type: "POST", 
        success: function (data) {
            $("#etapas").html(data);
        },
        error: function (error) {
            console.log("test");
            console.log(error);
        }
    });
}