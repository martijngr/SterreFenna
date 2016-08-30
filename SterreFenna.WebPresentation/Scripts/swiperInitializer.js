
function initializeSwiper(totalItems) {

    var options = {
        pagination: '.swiper-pagination',
        slidesPerView: 'auto',
        paginationClickable: true,
        spaceBetween: 30,
        freeMode: true
        //lazyLoading: true
    };

    if (isBreakpoint('xs') || isBreakpoint('sm')) {
        options.direction = 'vertical';
    }
    else {
        options.direction = 'horizontal';
        options.centeredSlides = totalItems > 3 ? false : true;
        options.onInit = function (swiper) {
            if (totalItems > 1 && totalItems <= 3) {
                swiper.slideTo(1);
            }
        };
    }

    window.swiperInstance = new Swiper('.swiper-container', options);
}