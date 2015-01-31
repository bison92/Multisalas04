
    var controladorCalculaTotales = {

        total: function () {

            var recaudacion = null;
            //var parameters=null;
            //$.ajax({
            //    type: "GET",
            //    url: "http://localhost:49208/api/venta/1",
            //    data: parameters,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (msg) {
            //        recaudacion=msg.d;
            //    },
            //    error: function (e) {
            //        recaudacion = "Error";
            //    }
            //});

            $.get("http://localhost:49208/api/venta/1", function (data) {

                recaudacion=data;

                alert("Load was performed.");

            });




        return recaudacion;
    },
   
};
