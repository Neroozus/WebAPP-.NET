$(function () {
    $('form').submit(function (event) {


        disableButton($('button[type = "submit"]'));
        return true;
    });
});

function disableButton(button) {
    button.attr('disabled', true);
}