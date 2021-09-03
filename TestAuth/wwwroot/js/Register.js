function BlockResendButton() {

    var input = $("#ResendButton");
    input.attr("disabled", true);
    setTimeout(function () {

        input.attr("disabled", false);

    }, 61000);
    timer(60);
};

$(function () {
    $("#successResendEmail").hide();
    BlockResendButton();
    jQuery(document).ready(function () { 
        
        $('#ResendForm').submit(function (event) {
            BlockResendButton();
            event.preventDefault();           
            var actionUrl = $(this).attr('action');
            var sendData = $(this).serialize();
            $.ajax({
                async: true,
                type: 'POST',
                url: actionUrl,
                data: sendData,
                cache: false,

            })
                .done(function (data) {

                    $("#successResendEmail").fadeTo(3000, 100).slideUp(500, function () {
                        $("#successResendEmail").slideUp(500);
                    });
                });


        });
    });
    });
    function timer(seconds) {
        
        var seconds_timer_id = setInterval(function () {
            $("#timer").css('visibility', 'visible');
            if (seconds > 0) {              
                seconds--;
                $(".seconds").text(seconds);
            } else {
                clearInterval(seconds_timer_id);
                $("#timer").css('visibility', 'hidden');
            }
        }, 1000);

}
$(function () {
    $('form').submit(function (event) {
        

        disableButton($('button[type = "submit"]'));
        return true;
            });
    });

function disableButton(button) {
    button.attr('disabled', true);
}
function enableButton(button) {
    button.attr("disabled", false);
}