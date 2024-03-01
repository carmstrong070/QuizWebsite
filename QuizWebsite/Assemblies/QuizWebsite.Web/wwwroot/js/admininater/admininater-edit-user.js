//import searchUser from "./admininater-utilities"
(function (admininater, $, undefined) {
admininater.saveUserChanges = function(id) {
    let params = {
        id: id,
        username: document.getElementById("edit-username").value,
        email: document.getElementById("edit-email").value,
        password: document.getElementById("edit-password").value
    };
    $.ajax({
        url: "/SaveUserChanges",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            if (data) {
                admininater.searchUser()
                $("#edit-user-modal").modal("hide");
            }
            else {
                admininater.searchUser()
                $("#edit-user-modal").modal("hide");
                alert("Save did not work")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
    }
}(window.admininater = window.admininater || {}, jQuery));
