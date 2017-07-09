navHover = function() {
    var navMenu = document.getElementById("navmenu");
    if (!navMenu)
        return;

    var lis = navMenu.getElementsByTagName("LI");
    for (var i = 0; i < lis.length; i++) {
        lis[i].onmouseover = function() {
            this.className += " iehover";
        };
        lis[i].onmouseout = function() {
            this.className = this.className.replace(new RegExp(" iehover\\b"), "");
        };
    }
};
if (window.attachEvent) window.attachEvent("onload", navHover);
navHover = function() {
    var subMenu = document.getElementById("submenu");
    if (!subMenu)
        return;

    var lis = subMenu.getElementsByTagName("LI");
    for (var i = 0; i < lis.length; i++) {
        lis[i].onmouseover = function() {
            this.className += " iehoversm";
        };
        lis[i].onmouseout = function() {
            this.className = this.className.replace(new RegExp(" iehoversm\\b"), "");
        };
    }
};
if (window.attachEvent) window.attachEvent("onload", navHover);