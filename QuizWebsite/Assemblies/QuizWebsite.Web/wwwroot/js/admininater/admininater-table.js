//import searchUser from "./admininater-utilities"

function selectUser(id) {
    let params = { id: id };
    $.ajax({
        url: "/SelectUser",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            $("#edit-container").html(data);
            $("#edit-user-modal").modal("show");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

function banUser(id) {
    let params = {
        id: id
    };
    $.ajax({
        url: "/Ban",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            searchUser()
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

function unbanUser(id) {
    let params = {
        id: id
    };
    $.ajax({
        url: "/Unban",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            searchUser()
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}