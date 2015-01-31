

$(document).ready(function () {



	$.get("http://localhost:49208/api/sesion/", function (data) {
		for (i = 0; i <= data.length; i++) {
			var success = "";
			if (data[i].Abierto) {
				success = "class ='success'";
			}
			else {
				success = "class = 'danger'";
			};
			$("#tabla").append("<tr " + success + "><th scope='row'>" + data[i].ID + "</th>" +
							  "<td>" + data[i].SalaID + "</td>" +
							  "<td>" + data[i].fecha + "</td>" +
							  "<td>" + data[i].Abierto + "</td></tr>");
		}
	});





}());