

var Module = (function (my) {

    var btnCancelar = function () {
        $(".btn-danger").click(function () {
            $.Reload();
        });
    };

    my.totales = {

        //"apagado", "inicio", "total_dinero", "total_dinero_sesion", "total_dinero_sala", "total_entradas", "total_entradas_sesion", "total_entradas_sala"],
        totales_estado : "apagado",


        //acciones
        totales_iniciar: function () {
            this.totales_estado= "inicio";
            this.totales_botoneras(this.totales_estado);
            $("#content").append("<button type='button' style='margin-bottom: 10px; margin-left: 10px;float:right;' class='btn btn-danger'>Cancelar</button>");
            btnCancelar();

        },
        totales_total_dinero: function () {
            this.totales_estado = "total_dinero";
            this.totales_botoneras(this.totales_estado);
            

        },
        totales_total_dinero_sesion: function () {
            this.totales_estado = "total_dinero_sesion";
            this.totales_botoneras(this.totales_estado);

        },
        totales_total_dinero_sala: function () {
            this.totales_estado = "total_dinero_sala";
            this.totales_botoneras(this.totales_estado);

        },
        totales_total_entradas: function () {
            this.totales_estado = "total_entradas";
            this.totales_botoneras(this.totales_estado);

        },
        totales_total_entradas_sesion: function () {
            this.totales_estado = "total_entradas_sesion";
            this.totales_botoneras(this.totales_estado);

        },
        totales_total_entradas_sala: function () {
            this.totales_estado = "total_entradas_sala";
            this.totales_botoneras(this.totales_estado);

        },



        //botones
        totales_btn_total_dinero: function () {            
            totales_total_dinero();
        },
        totales_btn_total_dinero_sesion: function () {
            totales_total_dinero_sesion();

        },
        totales_btn_total_dinero_sala: function () {
            totales_total_dinero_sala();

        },
        totales_btn_total_entradas: function () {
            totales_total_entradas();

        },
        totales_btn_total_entradas_sesion: function () {
            totales_total_entradas_sesion();

        },
        totales_btn_total_entradas_sala: function () {
            totales_total_entradas_sesion();

        },
        //boton calculador
        totales_btn_calcula: function () {
            switch(this.totales_estado){
                case "total_dinero":
                    var ano =2015;
                    var mes=5;
                    var dia=12;
                    $.get("http://localhost:49208/api/totalventa/"+ano+"/"+mes+"/"+dia, function (data) {
                        $(".totales_resultado").text(data+" Euros");
                    });

                    break;
                case "total_dinero_sesion":
                    $.get("http://localhost:49208/api/totalventasesion/"+$(".dato").val(), function (data) {
                        $(".totales_resultado").text(data + " Euros");
                    });
                    break;
                case "total_dinero_sala":
                    var ano = 2015;
                    var mes = 5;
                    var dia = 12;
                    $.get("http://localhost:49208/api/totalventasala/" + $(".dato").val() + "/" + ano + "/" + mes + "/" + dia, function (data) {
                        $(".totales_resultado").text(data + " Euros");
                    });
                    break;
                case "total_entradas":
                    var ano = 2015;
                    var mes = 5;
                    var dia = 12;
                    $.get("http://localhost:49208/api/totalentrada/" +ano+"/"+mes+"/"+dia, function (data) {
                        $(".totales_resultado").text(data + " Entradas");
                    });
                    break;
                case "total_entradas_sesion":
                    $.get("http://localhost:49208/api/totalentradasesion/" + $(".dato").val(), function (data) {
                        $(".totales_resultado").text(data + " Entradas");
                    });
                    break;
                case "total_entradas_sala":
                    var ano = 2015;
                    var mes = 5;
                    var dia = 12;
                    $.get("http://localhost:49208/api/totalentradasala/" + $(".dato").val() + "/" + ano+"/"+mes+"/"+dia, function (data) {
                        $(".totales_resultado").text(data + " Entradas");
                    });
                    break;
                   

            }

        },



        //botoneras
        totales_botoneras: function (aux) {
            switch (aux) {
                case "inicio":
                    var texto = '<div class="totales_contenedor"><div class="totales_nav"></div><div class="totales_article"></div><div class="totales_footer"></div></div>';
                    $("#content").html(texto);
                    texto = '<div class="btn_totales_dinero">Calcular Dinero</div><div class="btn_totales_dinero_sesion">Calcular Dinero Sesión</div><div class="btn_totales_dinero_sala">Calcular Dinero Sala</div><div class="btn_totales_entradas">Entradas totales</div><div class="btn_totales_entradas_sesion">Entradas por sesión</div><div class="btn_totales_entradas_sala">Entradas por sala</div>';
                    $(".totales_nav").html(texto);

                    break;
                case "apagado":
                    break;
                case "total_dinero":
                    var texto = '<div class="btn_totales_calcula">Calcular Dinero</div>';
                    $(".totales_footer").html(texto);
                    $(".totales_article").html('<div class="totales_resultado"></div>');
                    break;
                case "total_dinero_sesion":
                    $(".totales_footer").html('<div class="btn_totales_calcula">Calcular Dinero Sesión</div>');
                    $(".totales_article").html('<input type="text" class="dato"><div class="totales_resultado"></div>');
                    break;
                case "total_dinero_sala":
                    $(".totales_footer").html('<div class="btn_totales_calcula">Calcular Dinero Sala</div>');
                    $(".totales_article").html('<input type="text" class="dato"><div class="totales_resultado"></div>');
                    break;
                case "total_entradas":
                    $(".totales_footer").html('<div class="btn_totales_calcula">Entradas totales</div>');
                    $(".totales_article").html('<div class="totales_resultado"></div>');
                    break;
                case "total_entradas_sesion":
                    $(".totales_footer").html('<div class="btn_totales_calcula">Entradas por sesión</div>');
                    $(".totales_article").html('<input type="text" class="dato"><div class="totales_resultado"></div>');
                    break;
                case "total_entradas_sala":
                    $(".totales_footer").html('<div class="btn_totales_calcula">Entradas por sala</div>');
                    $(".totales_article").html('<input type="text" class="dato"><div class="totales_resultado"></div>');
                    break;



            }



            $(".btn_totales_dinero").click(function () {


                Module.totales.totales_total_dinero();

            });
            $(".btn_totales_dinero_sesion").click(function () {
                Module.totales.totales_total_dinero_sesion();
            });
            $(".btn_totales_dinero_sala").click(function () {
                Module.totales.totales_total_dinero_sala();
            });
            $(".btn_totales_entradas").click(function () {
                Module.totales.totales_total_entradas();
            });
            $(".btn_totales_entradas_sesion").click(function () {
                Module.totales.totales_total_entradas_sesion();
            });
            $(".btn_totales_entradas_sala").click(function () {
                Module.totales.totales_total_entradas_sala();
            });
            $(".btn_totales_calcula").click(function () {
                Module.totales.totales_btn_calcula();
            });

        }
    };
    return my;

}(Module || {}));