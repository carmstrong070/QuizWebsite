//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap.bundle"); //-- I commented it out because some stuff broke for me - JB

function searchUser() {
    let params = { searchedUser: document.getElementById("txtSearch").value };
    $.ajax({
        url: "/UpdateTable",
        type: "POST",
        dataType: "html",
        data: params,
        success: function (data, textStatus, jqXHR) {
            $("#result-table tbody").replaceWith(data)
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}
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
function createNewUser() {
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
                searchUser()
                $("#add-user-modal").modal("hide");
            }
            else {
                searchUser()
                $("#add-user-modal").modal("hide");
                alert("Save did not work")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

function showUserModal() {
    $.ajax({
        url: "/ShowUserModal",
        type: "GET",
        dataType: "html",
        success: function (data, textStatus, jqXHR) {
            $("#add-container").html(data);
            $("#add-user-modal").modal("show");
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

var clockInterval;
var clockStartTime;
var $clock;

function startClock(elementSelector) {
    $clock = $("#" + elementSelector);

    if ($clock.length === 0)
        return;
        
    clockStartTime = new Date();

    clockInterval = setInterval(function () {
        let now = Date.now();
        let delta = now - clockStartTime;
        $clock.text(delta / 1000);
    }, 33.33);
}