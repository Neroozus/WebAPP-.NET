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
            success: function (part) {

                $('#dialogContent').html(part);
                $('#modDialog').modal('show');
            }
        });
    });
});
$(function () {
    $('button[id="confirmDeleteBtn"]').click(function (event) {
        event.preventDefault();
        alert("авыаыв");
        // var placeholderElement = $('#placeholderElement');
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
                enableButton($('button[id="confirmDeleteBtn"]'));
                
                        $('#modDialog').modal('hide');
                       // $(document.body).removeClass('modal-open');
                        //$('.modal-backdrop').remove();
                        $("#successPart").fadeTo(3000, 100).slideUp(500, function () {
                            $("#successPart").slideUp(500);
                        })
                    });
            });
    });