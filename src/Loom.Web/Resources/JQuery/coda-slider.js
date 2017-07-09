/*
	jQuery Coda-Slider v1.1 - http://www.ndoherty.com/coda-slider
	
	Copyright (c) 2007 Niall Doherty
	
	Inspired by the clever folks at http://www.panic.com/coda
	Many thanks to Gian Carlo Mingati. Coda-Slider is a heavily modified version of his slideViewer, which can be found at  http://www.gcmingati.net/wordpress/wp-content/lab/jquery/imagestrip/imageslide-plugin.html
*/
jQuery(function() {});
var j = 0;
jQuery.fn.codaSlider = function(settings) {
    settings = jQuery.extend({ easeFunc: "expoinout", easeTime: 750, toolTip: false }, settings);
    return this.each(function() {
        var container = jQuery(this);
        container.removeClass("csw").addClass("stripViewer");
        var panelWidth = container.find("div.panel").width();
        var panelCount = container.find("div.panel").size();
        var stripViewerWidth = panelWidth * panelCount;
        container.find("div.panelContainer").css("width", stripViewerWidth);
        var navWidth = panelCount * 2;
        if (location.hash && parseInt(location.hash.slice(1)) <= panelCount) {
            var cPanel = parseInt(location.hash.slice(1));
            var cnt = -(panelWidth * (cPanel - 1));
            jQuery(this).find("div.panelContainer").css({ left: cnt });
        } else {
            var cPanel = 1;
        };
        container.each(function(i) {
            jQuery(this).before("<div class='stripNavL' id='stripNavL" + j + "'><a href='#'>Left</a><\/div>");
            jQuery(this).after("<div class='stripNavR' id='stripNavR" + j + "'><a href='#'>Right</a><\/div>");
            jQuery(this).before("<div class='stripNav' id='stripNav" + j + "'><ul><\/ul><\/div>");
            jQuery(this).find("div.panel").each(function(n) {
                jQuery("div#stripNav" + j + " ul").append("<li class='tab" +
                    (n + 1) +
                    "'><a href='#" +
                    (n + 1) +
                    "'>" +
                    jQuery(this).attr("title") +
                    "<\/a><\/li>");
            });
            jQuery("div#stripNav" + j + " a").each(function(z) {
                navWidth += jQuery(this).parent().width();
                jQuery(this).bind("click",
                    function() {
                        jQuery(this).addClass("current").parent().parent().find("a").not(jQuery(this))
                            .removeClass("current");
                        var cnt = -(panelWidth * z);
                        cPanel = z + 1;
                        jQuery(this).parent().parent().parent().next().find("div.panelContainer")
                            .animate({ left: cnt }, settings.easeTime, settings.easeFunc);
                    });
            });
            jQuery("div#stripNavL" + j + " a").click(function() {
                if (cPanel == 1) {
                    var cnt = -(panelWidth * (panelCount - 1));
                    cPanel = panelCount;
                    jQuery(this).parent().parent().find("div.stripNav a.current").removeClass("current").parent()
                        .parent().find("li:last a").addClass("current");
                } else {
                    cPanel -= 1;
                    var cnt = -(panelWidth * (cPanel - 1));
                    jQuery(this).parent().parent().find("div.stripNav a.current").removeClass("current").parent().prev()
                        .find("a").addClass("current");
                };
                jQuery(this).parent().parent().find("div.panelContainer")
                    .animate({ left: cnt }, settings.easeTime, settings.easeFunc);
                location.hash = cPanel;
                return false;
            });
            jQuery("div#stripNavR" + j + " a").click(function() {
                if (cPanel == panelCount) {
                    var cnt = 0;
                    cPanel = 1;
                    jQuery(this).parent().parent().find("div.stripNav a.current").removeClass("current").parent()
                        .parent().find("a:eq(0)").addClass("current");
                } else {
                    var cnt = -(panelWidth * cPanel);
                    cPanel += 1;
                    jQuery(this).parent().parent().find("div.stripNav a.current").removeClass("current").parent().next()
                        .find("a").addClass("current");
                };
                jQuery(this).parent().parent().find("div.panelContainer")
                    .animate({ left: cnt }, settings.easeTime, settings.easeFunc);
                location.hash = cPanel;
                return false;
            });
            jQuery("a.cross-link").click(function() {
                jQuery(this).parents()
                    .find(".stripNav ul li a:eq(" + (parseInt(jQuery(this).attr("href").slice(1)) - 1) + ")")
                    .trigger("click");
            });
            jQuery("div#stripNav" + j).css("width", navWidth);
            if (location.hash && parseInt(location.hash.slice(1)) <= panelCount) {
                jQuery("div#stripNav" + j + " a:eq(" + (location.hash.slice(1) - 1) + ")").addClass("current");
            } else {
                jQuery("div#stripNav" + j + " a:eq(0)").addClass("current");
            }
        });
        j++;
    });
};