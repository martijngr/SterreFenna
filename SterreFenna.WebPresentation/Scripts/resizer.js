var resizer = function () {
    var viewportWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
    
    return {
        calcuteContentHeight: calcuteContentHeight,
    };

    function calcuteContentHeight() {
        calculateContactHeight();
        calculateItemsBoxHeight();

        refreshWhenNecessary();
    }

    function calculateContactHeight() {
        var contentDiv = $(".contact-page");
        var footerheight = 70;
        var headerHeight = getHeaderHeight();
        var viewportHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
        var contentHeight = viewportHeight - headerHeight - footerheight;

        contentDiv.height(contentHeight);
    }

    function calculateItemsBoxHeight() {
        var contentDiv = $(".swiper-container");
        var footerheight = 135;
        var headerHeight = getHeaderHeight();
        var viewportHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
        var contentHeight = viewportHeight - headerHeight - footerheight;

        contentDiv.height(contentHeight);
        window.swiperInstance.update();
    }

    function getHeaderHeight() {
        return $("header").outerHeight(true);
    }

    function refreshWhenNecessary() {
        var currentViewportWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
        // < 992 = small

        if (window.prevViewportWidth) {
            if (window.prevViewportWidth > 991 && currentViewportWidth <= 991) {
                location.reload();
            }
            else if (window.prevViewportWidth <= 991 && currentViewportWidth > 991) {
                location.reload();
            }
        }
        
        window.prevViewportWidth = currentViewportWidth;
    }
}();