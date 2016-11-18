window.BadgeModalModule = (function ($) {
    
    //get each instance this badge has been gifted to this student
    var getGifts = function (studentId, badgeId) {

        $.ajax({
            url: '/Gift/GetGiftsReceived',
            type: 'GET',
            dataType: 'html',
            data: { studentId: studentId, badgeId: badgeId },
            success: function (data) {                
                $('#gst-bmm-modal').html(data);
                $('#gifts-modal').modal('show');
           },
            error: function (xhr, ajaxOptions, thrownError) { 
                alert(xhr.status + ': ' + thrownError);
            }
        });
    };
    
    return {
        init: function () {
            //whenever a badge gets clicked
            $('.gst-bmm').click(function (e) {
                //if the badge isn't disabled, and the badge has a badge-id and a student-id, show the modal popup
                if (!$(this).hasClass('gst-bmm-disabled')) {
                    var studentId = $('#gst-bmm-student-id').val();
                    var badgeId = $(this).data('badge-id');
                    if (studentId && badgeId) {
                        console.log('getting gifts for student: ' + studentId + ' with badge: ' + badgeId);
                        getGifts(studentId, badgeId);
                    } else {//missing required values
                        console.log('popup failed: missing student (' + studentId + ') or badge id (' + badgeId + ')');
                    }
                }
            });
        },
    };
})(jQuery);