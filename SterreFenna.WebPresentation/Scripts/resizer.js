function calcuteContentHeight() {
    var viewportWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0);
    var viewportHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);
    var headerHeight = $("header").outerHeight(true);
    var footerheight = 135;
    var contentDiv = $(".swiper-container");

    var contentHeight = viewportHeight - headerHeight - footerheight;
    contentDiv.height(contentHeight);
}