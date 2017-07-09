$(document).ready(function() {
    $("ul.items>li.active>ul").slideDown();

    $(function() {
        $("ul.items>li>span").click(clickFn);
    });

    function clickFn(e) {
        var slideUpSpeed = "medium";
        var slideDownSpeed = "medium";
        var $el = $(e.target);
        var $parent = $el.parent();
        var $slider = $parent.children("ul");
        if (!$slider.is(":visible")) {
            if ($parent.parent().is("ul.items")) {
                var $visibles = $("ul.items>li>ul:visible");
                if ($visibles.length > 0) {
                    $visibles.slideUp(slideUpSpeed);
                    $slider.slideDown(slideDownSpeed);
                } else {
                    $slider.slideDown(slideDownSpeed);
                }
            }
        }
    }

    $(".close").click(function() {
        $(this).parents(".alert").animate({ opacity: "hide" }, "medium");
        return false;
    });

    $(".submit").click(function() {
        $(this).parents("form").submit();
        return false;
    });
});