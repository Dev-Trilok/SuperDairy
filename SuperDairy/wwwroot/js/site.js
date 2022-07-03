// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showModal(title, onSave, onCancel ) {
    onCancel = onCancel != null ? onCancel : () => {
        $("#mainModal").modal('hide');
    };
    onSave = onSave != null ? onSave : () => {
        $("#mainModal").modal('hide');
    };
    $("#mainModal").modal('toggle');
    $("#mainModalLabel").html(title);
    $("#mainModalClose").click(onCancel);
    $("#mainModalAccept").click(onSave);
    return "#mainModalBody";
}
function hideModal() {
    $("#mainModal").modal('toggle');
}