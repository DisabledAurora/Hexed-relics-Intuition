// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#Stocks').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true
    });
});


function redirectToPage(location) {
    switch (location) {
        case "main":
            document.location = "/Banking/MainScreen";
            break;
        case "stocks":
            document.location = "/Banking/Stocks";
            break;
        case "partners":
            document.location = "/Banking/Partners";
            break;
        case "login":
            document.location = "/login";
            break;
        case "home":
            document.location = "/";
            break;
        case "transaction":
            document.location = "/Banking/Transactions";
            break;
        default:
            document.location = "/";
            break;
    }
}