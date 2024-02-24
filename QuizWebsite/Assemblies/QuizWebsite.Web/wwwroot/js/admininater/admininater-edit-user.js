//import searchUser from "./admininater-utilities"

function saveUserChanges(id) {
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
                searchUser()
                $("#edit-user-modal").modal("hide");
            }
            else {
                searchUser()
                $("#edit-user-modal").modal("hide");
                alert("Save did not work")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
