
$(document).ready(function () {
    Module.totales.totales_iniciar();

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
});
