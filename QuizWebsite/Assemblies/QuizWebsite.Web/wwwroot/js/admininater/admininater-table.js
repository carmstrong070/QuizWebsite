//import searchUser from "./admininater-utilities"
(function (admininater, $, undefined) {
admininater.selectUser = function(id) {
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

    admininater.banUser = function(id) {
    let params = {
        id: id
    };
    $.ajax({
        url: "/Ban",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            admininater.searchUser()
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

admininater.unbanUser = function(id) {
    let params = {
        id: id
    };
    $.ajax({
        url: "/Unban",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            admininater.searchUser()
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
}(window.admininater = window.admininater || {}, jQuery));