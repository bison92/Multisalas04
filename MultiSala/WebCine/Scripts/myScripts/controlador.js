$(document).ready(function () {
    $.Reload = function () {
        Module.handlers.cargaIndex();
        Module.Constantes.loadStatus();
    }
    $.Reload();
});