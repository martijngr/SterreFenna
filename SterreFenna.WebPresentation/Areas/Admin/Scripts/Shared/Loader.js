var Loader = function () {
    return {
        show: show,
        hide: hide
    };

    function show() {
        $("#loader").show();
    }

    function hide() {
        $("#loader").hide();
    }
}();