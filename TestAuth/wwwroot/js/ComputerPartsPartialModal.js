$(function () {
    $('button[data-save="addComputerPart"]').click(function (event) {
        event.preventDefault();
        disableButton($('button[data-save="addComputerPart"]'));
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();

        $.ajax({
            async: true,
            type: 'POST',
            url: actionUrl,
            data: sendData,
            cache: false,

        })
            .done(function (data) {
                var newBody = $('.modal-body', data);
                var isValid = newBody.find('[name="IsValid"]').val() == 'True';
                if (isValid) {
                    var deliveryMessage = "Товар: " + $('#manufacturer').val() + " " + $('#computerPart').val() + " был добавлен.";
                    updateTableDone(deliveryMessage);
                   
                }
                else {
                    enableButton($('button[data-save="addComputerPart"]'));
                    $('#modDialog').find('.modal-body').replaceWith(newBody);

                }
            })
            .fail(function () {
                var deliveryMessage = "Товар не был добавлен.";
                updateTableFail(deliveryMessage);
            })
            

    });
});
$(function () {
$('button[id="confirmDeleteBtn"]').click(function (event) {
    event.preventDefault();
    disableButton($('button[id="confirmDeleteBtn"]'));
    var form = $(this).parents('.modal').find('form');
    var actionUrl = form.attr('action');
    var sendData = form.serialize();
    $.ajax({
        async: true,
        type: 'POST',
        url: actionUrl,
        data: sendData,
        cache: false,

    })
        .done(function (data) {
            var deliveryMessage = "Товар: " + $('#manufacturer').val() + " " + $('#computerPart').val() + " был удален.";
            updateTableDone(deliveryMessage);
        })
        .fail(function () {
            var deliveryMessage = "Товар не был удален, так как его уже не существует.";
            updateTableFail(deliveryMessage);
        })
        
});
});
$(function () {

    $('button[data-save="editComputerPart"]').click(function (event) {
        event.preventDefault();
    disableButton($('button[data-save="editComputerPart"]'));

    var form = $(this).parents('.modal').find('form');
    var actionUrl = form.attr('action');
    var sendData = form.serialize();
    $.ajax({
        async: true,
        type: 'POST',
        url: actionUrl,
        data: sendData,
        cache: false,

    })
        .done(function (data) {
            var newBody = $('.modal-body', data);
            var isValid = newBody.find('[name="IsValid"]').val() == 'True';

            if (isValid) {
                var deliveryMessage = "Товар был изменен.";
                updateTableDone(deliveryMessage);
            }
            else {
                enableButton($('button[data-save="editComputerPart"]'));
                $('#modDialog').find('.modal-body').replaceWith(newBody);

            }
        })
        .fail(function () {
            var deliveryMessage = "Товар не был изменен";
            updateTableFail(deliveryMessage);
        })
        
});
});

function updateTableDone(message) {
    event.preventDefault();
deliveryMessage = message 
var isSuccess = true;
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
        $("#successPart").fadeTo(3000, 100).slideUp(500, function () {
            $("#successPart").slideUp(500);
        })

    });
  
};
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

function disableButton(button) {
    button.prop("disabled", true);
}
function enableButton(button) {
    button.prop("disabled", false);
}