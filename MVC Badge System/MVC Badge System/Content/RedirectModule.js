
window.RedirectModule = (function ($) {
    var getShareableLink = function (studentID, callback) {
        $.ajax({
            url: '/User/GetShareableLink',
            type: 'POST',
            dataType: 'html',
            data: { studentId: studentID },
            success: function (data) {
                callback(data);
           },
            error: function () { }
        });
    };
    return {
        
        getShareableLink: function (studentId, callback) {
            if (typeof callback === 'function') {
                getShareableLink(studentId, callback);
            }
        }
    };
})(jQuery);