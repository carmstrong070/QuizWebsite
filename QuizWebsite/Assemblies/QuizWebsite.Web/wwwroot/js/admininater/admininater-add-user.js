//import searchUser from "./admininater-utilities"
(function (admininater, $, undefined) {
admininater.createNewUser = function() {
    let params = {
        username: document.getElementById("new-username").value,
        email: document.getElementById("new-email").value,
        password: document.getElementById("new-password").value,
        isAdmininater: document.getElementById("new-admininater").value
    };
    $.ajax({
        url: "/CreateNewUser",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            if (data) {
                admininater.searchUser()
                $("#add-user-modal").modal("hide");
            }
            else {
                admininater.searchUser()
                $("#add-user-modal").modal("hide");
                alert("Save did not work")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    }
}(window.admininater = window.admininater || {}, jQuery));

