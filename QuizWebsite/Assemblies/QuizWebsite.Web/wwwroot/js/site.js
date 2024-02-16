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
}function selectUser(id) {
    let params = { id : id};
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