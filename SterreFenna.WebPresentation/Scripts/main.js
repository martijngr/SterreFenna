$(document).ready(function () {
    //to enable checking what resolution is used by bootstrap, add these empty divs
    //use with isBreakpoint('xs')
    var bootstrapSizes = ["xs", "sm", "md", "lg"];
    for (var i = 0; i < bootstrapSizes.length; i++) {
        $("<div />", {
            class: 'device-' + bootstrapSizes[i] + ' visible-' + bootstrapSizes[i] + '-block'
        }).appendTo("body");
    }

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
                //and replace 350px with the heighest possible value from that calculation.
                $('.navbar-collapse').css('max-height', '350px');
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


    //position submenu's centered below triangle
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
        //get dropdown belonging to clicked menu item
        var link = $(this); //clicked anchor
        var dropdown = $(this).siblings(".main-menu__submenu"); //dropdown to show
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
                dropdown.css("height", dropdownItemHeight);
                //count submenu items and give that as height
            } else {
                dropdown.css("height", dropdownHeight);
            }
        }

        //toggle triangle and dropdown visibility
        link.toggleClass("triangle-open");
        dropdown.toggleClass("dropdown-open");
    });
});

//functions
function isBreakpoint(alias) { //required for checking bootstrap breakpoint on the page in js
    return $('.device-' + alias).is(':visible');
}
