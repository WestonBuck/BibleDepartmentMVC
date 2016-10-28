window.GoogleAuthModule = (function ($) {

    var getCity = function (query) {
        query = (typeof query === 'undefined' || query === null) ? $('#txtSearch').val() : query;
        $.ajax({
            url: '/Home/GetCity',
            type: 'POST',
            dataType: 'html',
            data: { filter: query },
            beforeSend: function () {
                $('#spinner').css('display', 'block');
            },
            success: function (data) {
                $('#results').html(data);
            },
            error: function () { },
            complete: function () {
                $('#spinner').css('display', 'none');
            }
        });
    }

    return {
        init: function () {
            $('#txtSearch').keypress(function (e) {
                if (e.which === 13) {
                    getCity();
                }
            });
        },
        getCity: function (query) {
            getCity(query);
        },
        getArtist: function () {
            getArtist();
        },
        show500: function () {
            show500();
        },
        show304: function () {
            show304();
        },
        show404: function () {
            show404();
        },
        show301: function () {
            show301();
        },
    }
})(jQuery);