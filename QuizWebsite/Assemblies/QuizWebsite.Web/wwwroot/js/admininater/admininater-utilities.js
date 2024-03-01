(function (admininater, $, undefined) {
admininater.searchUser = function() {
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
}(window.admininater = window.admininater || {}, jQuery));