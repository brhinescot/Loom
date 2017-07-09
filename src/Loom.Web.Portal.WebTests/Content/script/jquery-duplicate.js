(function($, undefined) {

    /*  
    Plug-in used to duplicate elements on an event trigger of another element.
    The new element is placed below the previous element. Only the newest element 
    retains the duplicate ability.
    */
    $.fn.duplicate = function(options) {
        var opts = $.extend({}, $.fn.duplicate.defaults, options);

        function initCss() {
            this.firstClass = "duplicate-first";
            this.sourceClass = "duplicate-source";
            this.insertedClass = "duplicate-inserted";
            this.insertedSelector = "." + this.insertedClass;
            this.activeClass = "duplicate-active";
            this.activeSelector = "." + this.activeClass;
            this.focusClass = "duplicate-focus";
            this.focusSelector = "." + this.focusClass;
            this.removerClass = "duplicate-remover";
            this.removerSelector = "." + this.removerClass;
        }

        var css = new initCss(), cloneKey = "duplicate-clone";

        function duplicate(e) {
            var $this = $(this);
            var $currTmpl = $this.closest(css.activeSelector);
            var $clone = $currTmpl.data(cloneKey);
            var dupOpts = e.data.options;

            $clone.data(cloneKey, $clone.clone(true));

            var count = $currTmpl.siblings(css.insertedSelector).length;
            if (dupOpts.duplicating.call(this, $clone, count)) {
                $this.unbind(e.data.options.eventType);

                if (dupOpts.maxDuplicates && count >= dupOpts.maxDuplicates - 1) {
                    $clone.hide();
                }

                if (dupOpts.remover) {
                    $currTmpl.find(css.removerSelector).show().click({ options: dupOpts }, assasinate);
                }

                $currTmpl.removeClass(css.activeClass).addClass(css.insertedClass).removeData(cloneKey).after($clone);

                if (dupOpts.duplicated) {
                    dupOpts.duplicated.call(this, $clone, count + 1);
                }

                if (dupOpts.autoFocus) {
                    $clone.find(css.focusSelector).focus();
                }
            }
        }

        function assasinate(e) {
            e.preventDefault();

            var $killer = $(this).unbind("click");
            var $mark = $killer.closest(css.insertedSelector);
            var $zomb = $mark.siblings(css.activeSelector);
            var assOpts = e.data.options;

            $mark.fadeOut("fast",
                function() {
                    var $this = $(this);
                    if ($this.hasClass(css.firstClass)) {
                        $this.next().addClass(css.firstClass);
                    }

                    if ($this.siblings(css.insertedSelector).length >= (assOpts.maxDuplicates - 1)) {
                        $zomb.show();
                    }
                    $zomb.find(css.focusSelector).focus();

                    $this.empty().remove();
                });
        }

        return this.each(function() {
            var $this = $(this);

            var localOpts = {
                template: $this.parent(),
                autoFocus: $this
            };

            // Support the meta plug-in. Merge it's options if available.
            var o = $.meta ? $.extend(localOpts, opts, $this.data()) : $.extend(localOpts, opts);

            if (!o.template) {
                var err = new Error();
                err.name = "Missing Template Exception";
                err.message =
                    "The element to use as a template is not defined. Possible causes include inadvertently overriding the setting with an undefined object.";
                throw (err);
            }

            if (o.remover) {
                var rem = ($.isFunction(o.remover) ? o.remover(o.template) : o.remover);
                rem.hide().addClass(css.removerClass);
                console.log(rem);
            }

            $this.addClass(css.sourceClass).bind(o.eventType, { options: o }, duplicate);

            if (o.autoFocus)
                o.autoFocus.addClass(css.focusClass);

            if (o.setup)
                o.setup.call(this, o.template);

            o.template.addClass(css.activeClass)
                .data(cloneKey, o.template.clone(true))
                .addClass(css.firstClass);
        });
    };

    $.fn.duplicate.defaults = {
        eventType: "tabpress enterpress",
        autoDuplicate: true,
        setup: function() {
            $(this).val(null);
        },
        remover: undefined,
        maxDuplicates: 0,
        duplicated: undefined,
        duplicating: function() {
            // The default behavior is to duplicate only when the element has a value.
            return $(this).val().length > 0;
        }
    };

})(jQuery);


if (!jQuery.event.special.enterpress) {
    jQuery.event.special.enterpress = {
        setup: function() {
            $(this).bind("keydown", jQuery.event.special.enterpress.handler);
        },
        teardown: function() {
            $(this).unbind("keydown", jQuery.event.special.enterpress.handler);
            return;
        },
        handler: function(e) {
            var keycode = e.keyCode || e.which;
            if (keycode === 13) {
                // set event type to "enterpress"
                e.type = "enterpress";
                // let jQuery handle the triggering of "enterPress" event handlers
                jQuery.event.dispatch.apply(this, arguments);
            }
        }
    };
}

if (!jQuery.event.special.tabpress) {
    jQuery.event.special.tabpress = {
        setup: function() {
            $(this).bind("keydown", jQuery.event.special.tabpress.handler);
        },
        teardown: function() {
            $(this).unbind("keydown", jQuery.event.special.tabpress.handler);
            return;
        },
        handler: function(e) {
            var keycode = e.keyCode || e.which;
            if (keycode === 9) {
                if ($(this).val().length > 0) {
                    e.preventDefault();
                }
                // set event type to "tabpress"
                e.type = "tabpress";
                // let jQuery handle the triggering of "tabpress" event handlers
                jQuery.event.dispatch.apply(this, arguments);
            }
        }
    };
}

if (!jQuery.event.special.escpress) {
    jQuery.event.special.escpress = {
        setup: function() {
            $(this).bind("keydown", jQuery.event.special.escpress.handler);
        },
        teardown: function() {
            $(this).unbind("keydown", jQuery.event.special.escpress.handler);
            return;
        },
        handler: function(e) {
            var keycode = e.keyCode || e.which;
            if (keycode === 27) {
                // set event type to "escpress"
                e.type = "escpress";
                // let jQuery handle the triggering of "escpress" event handlers
                jQuery.event.dispatch.apply(this, arguments);
            }
        }
    };
}

Array.prototype.remove = function(from, to) {
    this.splice(from, !to || 1 + to - from + (!(to < 0 ^ from >= 0) && (to < 0 || -1) * this.length));
    return this.length;
};