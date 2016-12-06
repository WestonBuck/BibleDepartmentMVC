//Keenan Gates and Emily Pielemeier
//11/07/16
//MVC Badge System: Sprint 1

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