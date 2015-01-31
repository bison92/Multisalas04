

$(document).ready(function () {


		$.get("http://localhost:49208/api/venta/1", function(data) {
                $("#p1").text(data.ID),
                $("#p2").text(data.SesionID),
                $("#p3").text(data.NEntradas),
                $("#p4").text(data.NEntradasJoven),
                $("#p5").text(data.Precio)
        });







}());