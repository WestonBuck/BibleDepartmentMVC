//Keenan Gates and Emily Pielemeier
//11/07/16
//MVC Badge System: Sprint 1

window.SearchModule = (function ($) {
    // Holds any functions needed to handle searches
    var selectHandlers = []; // line 41
    var getUser = function (userName, callback) {
        $.ajax({
            url: '/User/GetStudent',
            type: 'POST',
            dataType: 'html',
            data: { filter: userName },
            success: function (data) {
                callback(data);
           },
            error: function () { }
        });
    };
    ////////////////////////////////////////////////////////////////////
    // returns the user ID for the selection in the search bar based on
    //the options in the drop down datalist
    ////////////////////////////////////////////////////////////////////
    var getSelectedUserID = function (parentDiv) {
        var val = parentDiv.chidren('input[type=search]').val();
        var userID = parentDiv.find('option').filter(function () {
            return this.value === val;
        }).first().attr("name");

        if (typeof userID !== 'undefined') {
            console.log("user id is " + userID);
            return userID;
        }
        else {
            return null;
        }
    };
    //////////////////////////////////////////////////////////////
    // Sets our placeholder function to the user's search function
    //////////////////////////////////////////////////////////////
    var setSelectHandler = function (parentDivID, handlerFunc) {
        selectHandlers[parentDivID] = handlerFunc;
    };
    return {
        init: function () {
            // when a user modifies the text in the search bar, it modifies
            //the datalist by calling the get user function
            $('.gst-studentsearch input[type=search]').keyup(function (e) {
                if ((e.which >= 48 && e.which <= 90) || e.which === 8 || e.which === 109 || e.which === 110 || e.which === 189 || e.which === 190) {
                    var thisBar = $(this);
                    var userName = thisBar.val();
                    
                    //AJAX get the users that match, and on success complete the function:
                    getUser(userName, function (postResults) {
                
                        var resultsList = thisBar.parent().children('.gst-ss-results');
                        if (resultsList) {
                            resultsList.html(postResults);
                        }
                    });
                }
            });

            // when an option is selected by mouseclick, it calls a different function
            //depending on the requirements of the page
            $('.gst-studentsearch input[type=search]').on('input', function (e) {
                var selected = $(this).val();
                var parentDiv = $(this).parent();
                var options = parentDiv.find('option');
                for (var i = 0; i < options.length; i++) {
                    //user selected a datalist option
                    if (options[i].value === selected) {
                        console.log('selected on click');
                        var parentDivID = parentDiv.attr('id');
                        var onSelectFunc = selectHandlers[parentDivID];
                        if (typeof onSelectFunc === 'function') {
                            onSelectFunc($(options[i]).attr('name'));
                        }
                    }
                }
            });
        },
        // line 24
        getSelectedUserID: function (studentSearchDiv) {
            getSelectedUserID(studentSearchDiv);
        },
        setSelectHandler: function (studentSearchDivID, handlerFunc) {
            setSelectHandler(studentSearchDivID, handlerFunc);
        }
    };
})(jQuery);