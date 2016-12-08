window.ShareableLinkModule = (function($) {
    var regenerateShareableLink = function() {
        $.ajax({
            url: '/ShareableLink',
            type: 'GET',
            dataType: 'json',
            success: function(data) {
                $("#shareable-link").html(data.Url);
            },
            error: function(xhr, ajaxOptions, thrownError) {
                alert(xhr.status + ": " + thrownError);
            }
        });
    };

    return {
        init: function() {
            $("#regen-shareable-link-button")
                .click(function(e) {
                    regenerateShareableLink();
                });
        }
    }
})(jQuery);