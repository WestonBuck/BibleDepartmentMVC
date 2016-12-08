
window.PrerequisiteModule = (function ($) {
    var setPrerequisites = function (badgeID, prerequisiteIDs, callback) {
        $.ajax({
            url: '/Badge/SetPrerequisites',
            type: 'POST',
            dataType: 'html',
            async: false,
            data: { badgeId: badgeID, prerequisiteIds: prerequisiteIDs },
            success: function (data) {
                callback(data);
           },
            error: function () { }
        });
    };
    return {
        init: function() {
            $('form').submit(function () {

                var badgeId = $('#BadgeId').val();

                var prereqBadgeIds = []
                $('.prerequisite-select').find('option:selected').each(function (index, ele) {
                    prereqBadgeIds.push($(ele).val());
                });

                if (badgeId) {
                    setPrerequisites(badgeId, prereqBadgeIds);
                }
                return true;
            });
        },

        setPrerequisites: function (badgeID, prerequisiteIDs, callback) {
            if (typeof callback === 'function') {
                setPrerequisites(badgeID, prerequisiteIDs, callback);
            }
        }
    };
})(jQuery);