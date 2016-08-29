var viewportWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
var viewportHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
var headerHeight = $("header").outerHeight(true);
var footerheight = 135;

function calcuteContentHeight() {
    calculateContactHeight();
    calculateItemsBoxHeight();
}

function calculateContactHeight() {
    var contentDiv = $(".contact-page");
    var footerheight = 70;

    var contentHeight = viewportHeight - headerHeight - footerheight;
    contentDiv.height(contentHeight);
}

function calculateItemsBoxHeight() {
    var contentDiv = $(".swiper-container");

    var contentHeight = viewportHeight - headerHeight - footerheight;
    contentDiv.height(contentHeight);
}