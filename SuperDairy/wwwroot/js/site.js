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

// Show Page loading panel
function showPageLoadingPanel() {
    $(".pageLoader").removeClass("display-none");
}

// to hide Page loading panel.
function hidePageLoadingPanel() {
    $(".pageLoader").addClass("display-none");
}

function closeModalDialog(modalId) {
    $(modalId).find('.modal-body').scrollTop(0)
    $(modalId).modal('hide').data('bs.modal', null);
}

function ValidateRequiredFieldTextBox(fieldName, errorFieldName, errorMessage) {
    var isValidData = true;
    fieldValue = trim($(fieldName).val());
    if (fieldValue != undefined && fieldValue == "") {
        isValidData = ShowErrorMessage(errorFieldName, errorMessage);
        $(fieldName).focus();
    }
    else {
        HideErrorMessage(errorFieldName);
    }
    return isValidData;
}

function addMaskingForField() {
    // $(".date-Control").mask("99/99/9999");
    $(".numeric-Control").numeric({ decimal: ".", negative: false, scale: 3 });
    $(".numeric-integer-control").numeric({ decimal: false, negative: false, scale: 3 });
}

//show error message
function ShowErrorMessage(errorFieldName, message, isSuccessMsg) {
    try {
        $(errorFieldName).html("");
        if (isSuccessMsg) {
            $(errorFieldName).removeClass("bg-danger").addClass("bg-success");
        } else {
            $(errorFieldName).removeClass("bg-success").addClass("bg-danger");
        }
        //$(errorFieldName)[0].style.visibility = 'visible';
        //$(errorFieldName).show();
        $(errorFieldName).html(message);
        if (message.length > 0) {
            $(errorFieldName).removeClass("hidden");
        }
        return false;
    } catch (er) { }
}

//hide Error Message
function HideErrorMessage(errorFieldName) {
    try {
        $(errorFieldName).text("");
        //$(errorFieldName)[0].style.visibility = 'hidden';
        //$(errorFieldName).hide();
        $(errorFieldName).addClass("hidden");
    } catch (er) { }
}


//email Address validation
function ValidateEmail(emailAddressField, errorFieldName) {
    try {
        var isValidData = true;
        var emailAddressValue = $(emailAddressField).val();
        var pos = emailAddressValue.indexOf('<');
        if (pos > 0) {
            emailAddressValue = emailAddressValue.substring(pos + 1, emailAddressValue.length - 1);
        }
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (!emailPattern.test(emailAddressValue)) {
            isValidData = ShowErrorMessage(errorFieldName, "Please Enter Valid Email Address.");
            $(emailAddressField).focus();
        }
        return isValidData;
    } catch (er) { }
}

//*************************************
// Check null,blank and undefined data
//*************************************
function isEmptyAndNullAndUndefined(data) {
    return (data == null || data == undefined || data == '');
}

//*****************************************
// Check Guid null,blank and undefined data
//*****************************************
function isGuidEmptyOrNullOrUndefined(guid) {
    return (guid == null || guid == "" || guid == undefined || guid == '00000000-0000-0000-0000-000000000000');
}



//**************************
// Show popup message
//**************************
function popupMessage(id, message) {
    $('#' + id).html(message);
    $('#' + id).dialog({
        height: 200
        , modal: true
        , title: 'UNICENTRIC'
        , buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });
}


function confirmCustomPopup(id, message, functionName) {
    $('#' + id).html(message);
    $('#' + id).dialog({ height: 200 }, { modal: true }, { title: 'UNICENTRIC' }, {
        buttons: [
            {
                text: "Cancel",
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "OK",
                click: function () {
                    $(this).dialog("close");
                    if (functionName)
                        eval(functionName + "()");
                }

            }]
    });
}