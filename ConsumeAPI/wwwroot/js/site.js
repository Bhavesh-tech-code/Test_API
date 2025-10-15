$(function () {
    if ($('div.alert.notification').length) {
        setTimeout(() => {

            $('div.alert.notification').fadeOut();
        }, 3000)
    }
});


function DisplayBusyIndiacation{
    $('.loading').show();
}

function HideBusyIndiacation() {
    $('.loading').hide();
}

$(window).on('beforeunload', function () {

    DisplayBusyIndiacation();
});

document.on('submit', 'form', function () {
    DisplayBusyIndiacation();
});
window.setTimeout(function () {
    HideBusyIndiacation();
}, 2000);