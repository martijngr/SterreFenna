$(document).ready(function () {
    //to enable checking what resolution is used by bootstrap, add these empty divs    //use with isBreakpoint('xs')
    var bootstrapSizes = ["xs", "sm", "md", "lg"];
    for (var i = 0; i < bootstrapSizes.length; i++) {
        $("<div />", {
            class: 'device-' + bootstrapSizes[i] + ' visible-' + bootstrapSizes[i] + '-block'
        }).appendTo("body");
    }

    //menu responsive
    if (isBreakpoint('xs') || isBreakpoint('sm')) {
        //custom collapse for responsive menu
        $(".navbar-toggle").on("click", function () {
            if ($(".main-menu").hasClass("open")) {
                $(".navbar-collapse").css("min-height", "0px");
                $(".navbar-collapse").css("max-height", "0px");
                $(".main-menu").toggleClass("open");
            }
            else {
                //height equal to height of menu
                $(".navbar-collapse").css("min-height", "0px");

                //this number is an estimate of the max height the menu can be.
                //Change this by calculating the height of the total menu for each combination of opened submenu items
                //and replace 450px with the heighest possible value from that calculation.
                $('.navbar-collapse').css('max-height', calculateMainMenuHeight() + 'px');
                $(".main-menu").toggleClass("open");
            }
        });

        //animate chevron behind menu item in responsive menu
        $(".main-menu__link").on("click", function () {
            //open clicked dropdown triangle
            if ($("i", this).hasClass("list__triangle--down")) {
                //close open triangles
                $.each($(".list__triangle--up"), function (key, value) {
                    $($(".list__triangle--up")[key]).addClass("list__triangle--down");
                    $($(".list__triangle--up")[key]).removeClass("list__triangle--up");
                });
                //open clicked item triangle
                $("i", this).removeClass("list__triangle--down");
                $("i", this).addClass("list__triangle--up");
            }//if already open (clicked item triangle), close
            else {
                $("i", this).addClass("list__triangle--down");
                $("i", this).removeClass("list__triangle--up");
            }
        });
    }

    //menu large position submenu's centered below triangle
    if (isBreakpoint('md') || isBreakpoint('lg')) {
        var mainMenuItems = $(".main-menu__link");
        $.each(mainMenuItems, function (key, value) {
            if ($(mainMenuItems[key]).siblings(".main-menu__submenu").length > 0) {
                //get main menu item
                var mainMenuItem = $('.main-menu__link')[key];
                var submenuItem = $(mainMenuItems[key]).siblings(".main-menu__submenu");

                //also get the first submenu item width
                var submenu = $(submenuItem.children(".main-menu__submenu__item")[0]);
                var submenuWidth = submenu.children("a").innerWidth();

                //get the width of the triangle (=border) and its position from the left side (calculated in css)
                var leftTriangle = parseFloat(window.getComputedStyle(mainMenuItem, ':before').getPropertyValue('left'));
                var widthTriangle = parseFloat(window.getComputedStyle(mainMenuItem, ':before').getPropertyValue('border-left'));

                //position submenu centered below triangle
                submenuItem.css("margin-left", leftTriangle - widthTriangle);
            }
        });
    }
    //menu handlers
    $(".main-menu__link").on("click", function () {
        toggleMainMenuItem(this);
    });

    function calculateMainMenuHeight() {
        var height = 0;
        var totalItems = $('.navbar-collapse .main-menu__item').each(function (index, item) {
            height += $(item).outerHeight(true);
        });

        return height;
    }
});

//functions
function isBreakpoint(alias) { //required for checking bootstrap breakpoint on the page in js
    return $('.device-' + alias).is(':visible');
}

function toggleMainMenuItem(menuItem) {
    //get dropdown belonging to clicked menu item
    var link = $(menuItem); //clicked anchor
    var dropdown = link.siblings(".main-menu__submenu"); //dropdown to show
    var dropdownItemHeight = 0;

    //close if already open, otherwise open
    var dropdownHeight = 0;

    if (dropdown.hasClass("dropdown-open")) {
        dropdown.css("height", dropdownHeight);
    }
    else {
        //close other dropdowns (main menu item) first (if open)
        $.each($(".triangle-open"), function () {
            $(this).removeClass("triangle-open");
        });

        //close other dropdowns (sub menu item) first (if open)
        $.each($(".dropdown-open"), function () {
            $(this).css("height", 0);
            $(this).removeClass("dropdown-open");
        });
        $(dropdown).children().each(function () {
            //set height of highest element as submenu height (for animation)
            dropdownItemHeight += $(this).outerHeight(true);
            if (dropdownHeight < $(this).outerHeight(true)) {
                dropdownHeight = $(this).outerHeight(true);
            }
        });
        if (isBreakpoint("xs") || isBreakpoint("sm")) {
            if (dropdownItemHeight > 100) {
                dropdown.css("height", "100px");
                dropdown.css("overflow", "hidden");
                dropdown.css("overflow-y", "scroll");
            }
            else {
                dropdown.css("height", dropdownItemHeight);
            }
        } else {
            dropdown.css("height", dropdownHeight + "px");
        }
    }

    //toggle triangle and dropdown visibility
    link.toggleClass("triangle-open");
    dropdown.toggleClass("dropdown-open");
}