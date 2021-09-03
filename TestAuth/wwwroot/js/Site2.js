$(async function () {
    //var PlaceHolderElement = $('#PlaceHolderHere');
    //var modals = PlaceHolderElement.find('#AddPartModal2');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.ajax({
            async: true,
            type: 'GET',
            url: decodedUrl,
            cache: false,
            success: function (data) {
                $('#dialogContent').html(data);
                $('#modDialog').modal('show');
            }
        });
        $('#modDialog').on('hidden.bs.modal', function () {
            //var count = $('.modal-body').find('[name="count"]').val() == 0;
            $('input[type=text]').val('');
        });
        //  var url = $(this).data('url');
        // var decodedUrl = decodeURIComponent(url);
        //$.get(decodedUrl).done(function (data) {
        //  PlaceHolderElement.html(data);
        //modals.html(data);
        //  modals.modal('show');
        //      modal.modal('show');

    });
});
    //modals.on('hidden.bs.modal', function () {
    // PlaceHolderElement = $('#PlaceHolderHere');
    //  PlaceHolderElement.html(null);
    // modal.modal('hide');

$(function () {
    $('button[id="addButton"]').click(function (event) {
        //var placeholderElement = PlaceHolderElement.find('form');
        // var form = PlaceHolderElement;
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
              //  var placeholderElement = $('#addPartModal');
                var newBody = $('.modal-body', data);
                var isValid = newBody.find('[name="IsValid"]').val() == 'True';

                if (isValid) {
                //    placeholderElement.modal('hide');
                    location.href = "/Home/MainPage";
                }
                else {
                  //  placeholderElement.find('.modal-body').replaceWith(newBody);

                }
            });
    });
})
$(function () {

    $('button[id="confirmDeleteBtn"]').click(function (event) {
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
                $('#modDialog').modal('hide');
            });
    });
});
