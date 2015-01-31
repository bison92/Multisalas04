





    my.VentasModule = (function (my) {

         my.controladorCalculaEntradas = {

            totalEntradas: function () {
                var entradas = 1; //llamo al servidor
                return entradas;
            },
            totalEntradasSesion: function () {
                var id = $("#id_entrada_sesion").attr("value");
                var entradas = 2; //llamo al servidor
                return entradas;
            },
            totalEntradasSala: function () {
                var id = $("#id_entrada_sala").attr("value");
                var entradas = 3; //llamo al servidor
                return entradas;
            }
        };

    }(VentasModule || {}));

