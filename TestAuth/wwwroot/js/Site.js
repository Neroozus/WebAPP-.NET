
$(function () {
    $('button[id="AddButton"]').click(function (event) {
        event.preventDefault();

        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.ajax({
            async: true,
            type: 'GET',
            url: decodedUrl,
            cache: false,
        })
            .done(function (part) {

                $('#dialogContent').html(part);
                $('#modDialog').modal('show');
            })
            .fail(function (part) {
                var deliveryMessage = "Товар невозможно добавить, произошли некоторые неполадки.";
                updateTableFail(deliveryMessage);
            })
        });
});
//нужное
$(function () {


    $('button[id="EditButton"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.ajax({
            async: true,
            type: 'GET',
            url: decodedUrl,
            cache: false,
        })
            .done(function (part) {

                $('#dialogContent').html(part);
                $('#modDialog').modal('show');
            })
            .fail(function (part) {
                var deliveryMessage = "Товар не существует, возможно его удалили.";
                updateTableFail(deliveryMessage);
            })
        });
});

$(function () {
    $('button[id="DeleteButton"]').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.ajax({
            async: true,
            type: 'GET',
            url: decodedUrl,
            cache: false,
        })
            .done(function (part) {

                $('#dialogContent').html(part);
                $('#modDialog').modal('show');
            })
            .fail(function (part) {
                var deliveryMessage = "Товар не существует, возможно его удалили.";
                updateTableFail(deliveryMessage);
            })
        });
});


function updateTableFail(message) {
    var deliveryMessage = message;
    var isSuccess = false;
    $.ajax({
        async: true,
        type: 'POST',
        url: '/Home/UpdateTable',
        cache: false,
        data: { message: deliveryMessage, isSuccess: isSuccess },
    })
        .done(function (data) {
            $("#ComputerPartsTable").html(data);
            $('#modDialog').modal('hide');
            $("#dangerPart").fadeTo(3000, 100).slideUp(500, function () {
                $("#dangerPart").slideUp(500);
            })

        });
}