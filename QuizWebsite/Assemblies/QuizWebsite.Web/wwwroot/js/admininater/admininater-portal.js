(function (admininater, $, undefined) {

    admininater.showUserModal = function() {
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
}(window.admininater = window.admininater || {}, jQuery));