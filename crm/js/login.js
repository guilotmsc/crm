
$(function () {
    inicializaBotoes();
});

function inicializaBotoes() {
    $("#btn_logar").click(function(){
        $.ajax({
            url: "proxy/Login.ashx?metodo=logar",
            type: "POST",
            data: {
                "usuario": $("#usuario").val(),
                "senha": $("#senha").val()
            },
            success: function (data) {
                window.location = "dashboard.aspx";
            },
            error: function (err) {

            }
        });
    });
}