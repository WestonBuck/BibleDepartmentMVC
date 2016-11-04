//Keenan Gates and Emily Pielemeier
//11/07/16
//MVC Badge System: Sprint 1

window.SearchModule = (function ($) {
    // Holds any functions needed to handle searches
    var selectHandler; // line 41
    var getUser = function (userName) {
        $.ajax({
            url: '/User/GetUser',
            type: 'POST',
            dataType: 'html',
            data: { filter: userName },
            success: function (data) {
                $('#results').html(data);
           },
            error: function () { }
        });
    };
    ////////////////////////////////////////////////////////////////////
    // returns the user ID for the selection in the search bar based on
    //the options in the drop down datalist
    ////////////////////////////////////////////////////////////////////
    var getSelectedUserID = function () {
        var val = $("#txtSearch").val();
        var userID = $('#user_list option').filter(function () {
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
    var setSelectHandler = function (handlerFunc) {
        selectHandler = handlerFunc;
    };
    return {
        init: function () {
            // when a user modifies the text in the search bar, it modifies
            //the datalist by calling the get user function
            $('#txtSearch').keyup(function (e) {
                if ((e.which >= 48 && e.which <= 90) || e.which === 8 || e.which === 109 || e.which === 110 || e.which === 189 || e.which === 190) {
                    var userName = $('#txtSearch').val();
                    getUser(userName);
                    // when an option is selected by mouseclick, it calls a different function
                    //depending on the requirements of the page
                    $('#user_list option').click(function (e) {
                        if (typeof selectHandler === 'function') {
                            selectHandler();
                        }
                    });
                }
            });

            // when an option is selected via enter, it calls a different function
            //depending on the requirements of the page
            $('#txtSearch').keypress(function (e) {
                if (e.which === 13 && typeof selectHandler === 'function') {
                    selectHandler();
                }
            });
        },
        // line 8
        getUser: function (userName) {
            getUser(userName);
        },
        // line 24
        getSelectedUserID: function (){
            getSelectedUserID();
        },
        // line 41
        setSelectHandler: function(handlerFunc){
            setSelectHandler(handlerFunc);
        }
    };
})(jQuery);