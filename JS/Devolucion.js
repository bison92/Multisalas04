
var venta={
	ventaID: "1",
	devolucion:"7",
};

var controlador={
	id:1,
	datos:venta,
	descarga: function(d){ 
			 var temp1= $('#'+ this.id);
			 var temp2= temp1.find("input[name='idVenta']").val(d.ventaID);
			 var temp3=temp1.find("input[name='precioDevolucion']").val(d.devolucion)
			},
			
	carga: function(){ 
			 var temp1= $('#'+ this.id);
			 var temp2= temp1.find("input[name='idVenta']");
			 this.datos.ventaID = temp2.val();
			 var temp3= temp1.find("input[name='precioDevolucion']");
			 this.datos.devolucion = temp3.val()
			}			
};

$(document).ready(function(){
	
	controlador.carga();
	console.log("Cargando valores:"+ venta.ventaID + " " + venta.devolucion);
	
	venta.ventaID="2";
	venta.devolucion="14";
	controlador.descarga(venta);
	console.log("Descargando valores del modelo a formulario:" + venta.ventaID + " " + venta.devolucion);
	
	controlador.carga();
	console.log("Comprobando que se han cargado en el formulario:" + venta.ventaID + " " + venta.devolucion);
			
});




