// Portal Setup
//
(function($, window, undefined) {
    // Fix IE in Windows 8 Sidebar.
    if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
        var msViewportStyle = document.createElement("style");
        msViewportStyle.appendChild(
            document.createTextNode(
                "@-ms-viewport{width:auto!important}"
            )
        );
        document.getElementsByTagName("head")[0].appendChild(msViewportStyle);
    }

    registerJQueryPlugins();

    var portal = (function() {
        var p = function() {
            return new p.fn.Init();
        };

        p.fn = p.prototype = {
            constructor: p,
            Init: function() {
                return p;
            }
        };

        p.fn.Init.prototype = p.fn;

        var getSelector = function(setting) {
            return typeof (setting) === "string" ? $(setting) : setting;
        };

        var ajaxQueue = $({});

        p.extend = p.fn.extend = function() {
            var options,
                name,
                src,
                copy,
                copyIsArray = false,
                clone,
                target = arguments[0] || {},
                i = 1,
                length = arguments.length,
                deep = false;

            // Handle a deep copy situation
            if (typeof target === "boolean") {
                deep = true;
                target = arguments[1] || {};
                // skip the boolean and the target
                i = 2;
            }

            // Handle case when target is a string or something (possible in deep copy)
            if (typeof target !== "object" && !$.isFunction(target)) {
                target = {};
            }

            // extend cPortal itself if only one argument is passed
            if (length === i) {
                target = this;
                --i;
            }

            for (; i < length; i++) {
                // Only deal with non-null/undefined values
                if ((options = arguments[i]) != null) {
                    // Extend the base object
                    for (name in options) {
                        src = target[name];
                        copy = options[name];

                        // Prevent never-ending loop
                        if (target === copy) {
                            continue;
                        }

                        // Recurs if we're merging plain objects or arrays
                        if (deep && copy && ($.isPlainObject(copy) || (copyIsArray = $.isArray(copy)))) {
                            if (copyIsArray) {
                                copyIsArray = false;
                                clone = src && $.isArray(src) ? src : [];

                            } else {
                                clone = src && $.isPlainObject(src) ? src : {};
                            }

                            // Never move original objects, clone them
                            target[name] = $.extend(true, clone, copy);

                            // Don't bring in undefined values
                        } else if (copy !== undefined) {
                            target[name] = copy;
                        }
                    }
                }
            }

            return target;
        };

        p.extend({
            relativeAppPath: "/",
            mainContent: $.firstExists("#mainContent", "#content"),
            getTargetData: function($start) {
                var $nav = $start.closest(".nav");
                var $context = $start.closest(".context");
                var $target = $.firstExists($start.data("loomTarget"),
                    $context.data("loomTarget"),
                    $nav.data("loomTarget"),
                    $start.closest(".dynamic"),
                    p.mainContent);
                return { nav: $nav, context: $context, target: $target };
            },
            run: function() {
                var anchorActions = {
                    selector: "a:not(.dropdown-toggle)",
                    events: {
                        click: function(e) {
                            // Bind async link clicks
                            e.preventDefault();
                            p.debug.log("async anchor clicked");
                            var $anchor = $(e.currentTarget);
                            var loc = $anchor.attr("href");
                            if ($anchor.data("isModal") === true) {
                                p.debug.log("isModal");
                                p.loadModal(loc);
                                return;
                            }
                            var targetData = p.getTargetData($anchor);
                            if (targetData.target != p.mainContent) {
                                p.debug.log("loomTarget", targetData.target);
                                p.loadView(targetData.target, loc);
                                return;
                            }
                            if (loc.indexOf("#!", 0) > 0) {
                                document.location = loc;
                                return;
                            }

                            var split = loc.split("#");
                            if (split.length === 1) {
                                document.location = split[0];
                                return;
                            }

                            loc = split[1];
                            if (!loc || loc.length === 0 || loc === "index") {
                                document.location = split[0];
                                return;
                            }

                            p.debug.log("NavigateTo", loc);
                            document.location.hash = "!" + loc;
                        }
                    }
                };
                var dynamicActions = {
                    selector: ".dynamic",
                    events: {
                        updated: function(e) {
                            var $this = $(e.target);
                            $this.find(".loom-templated").each(function() {
                                var $templated = $(this);
                                var uri = p$.resolveUrl($templated.data("loomDatasource"));
                                if (uri) {
                                    $.getJSON(uri,
                                        null,
                                        function(data) {
                                            $templated.dataTemplate().dataBind(data.d);
                                        });
                                }
                            });

                            $this.find(".autocomplete").each(function() {
                                var $autocomplete = $(this);
                                var options = $autocomplete.data("loomOptions");
                                var autoCompleteOptions = {};
                                if (options && options.autocomplete) {
                                    autoCompleteOptions = options.autocomplete;
                                }
                                $autocomplete.autocomplete(autoCompleteOptions);
                            });

                            $this.find(".json-post input[type=submit]").jsonPost();
                        }
                    }
                };
                var windowActions = {
                    events: {
                        hashchange: function() {
                            var hash = window.location.hash;
                            if (!hash)
                                return;
                            p.debug.log("hashchange", hash);
                            var $anchor = $("body").find('a[href="' + hash.replace("!", "") + '"]');
                            var data = p.getTargetData($anchor);
                            p.navigateTo(hash, data.target);
                            p.debug.log("target data", data);
                            if (data.nav.exists()) {
                                data.nav.find("li.active").removeClass("active");
                                $anchor.closest(".nav li").addClass("active");
                            }
                        }
                    }
                };

                $("body.spa")
                    .on(anchorActions.events, anchorActions.selector)
                    .on(dynamicActions.events, dynamicActions.selector);
                $(window)
                    .on(windowActions.events)
                    .trigger("hashchange");
            },
            redirectIfRequired: function(xhr) {
                var loc = xhr.getResponseHeader("X-Portal-Location");
                if (loc && loc.length > 0) {
                    p.debug.log("Portal redirect to", loc);
                    top.location.href = p.resolveUrl(loc);
                    return true;
                }
                return false;
            },
            resolveUrl: function(virtualPath, relativeAppPath) {
                var combinePaths = function(appPath, vPath) {
                    if (vPath && vPath.length > 0 && vPath.substr(0, 1) === "~")
                        vPath = vPath.substr(1, vPath.length - 1);

                    if (vPath && vPath.length > 0 && vPath.substr(0, 1) !== "/")
                        vPath = "/" + vPath;

                    if (!appPath || appPath === "/")
                        return vPath;

                    if (appPath.length > 1 && appPath.substr(appPath.length - 1, 1) === "/")
                        appPath = appPath.substr(0, appPath.length - 1);

                    return appPath + vPath;
                };

                var isRelativeUrl = function(url) {
                    if (url.indexOf(":") !== -1)
                        return false;

                    return !isRooted(url);
                };

                var isRooted = function(url) {
                    if (url && url.length > 0 && url.substr(0, 1) !== "/")
                        return url.substr(0, 1) === "\\";
                    return true;
                };

                if (!virtualPath)
                    return virtualPath;

                if (virtualPath.length === 0 || !isRelativeUrl(virtualPath))
                    return virtualPath;

                return combinePaths(relativeAppPath || p.relativeAppPath, virtualPath);
            },
            loadView: function(target, loc) {
                if (loc && loc.length > 0) {
                    loc = loc.replace(/^#!/, "").replace(/^#/, "");
                } else {
                    loc = document.location.hash.replace(/^#!/, "");
                }

                $.ajax(loc + (loc.indexOf("/") > 0 ? "" : "/index"),
                    {
                        type: "GET",
                        dataType: "html",
                        success: function(data, status, xhr) {
                            p.debug.log("Load view success", xhr);
                            if (!p.redirectIfRequired(xhr))
                                target.html(data).trigger("updated").show();
                        }
                    });
            },
            loadModal: function(loc) {
                if (loc && loc.length > 0) {
                    loc = loc.replace(/^#!/, "").replace(/^#/, "");
                } else {
                    loc = document.location.hash.replace(/^#!/, "");
                }

                var $modal = $("#modal");
                $modal.on("click",
                    ".closer",
                    function() {
                        $modal.hide();
                    });
                $.ajax(loc + (loc.indexOf("/") > 0 ? "" : "/index"),
                    {
                        type: "GET",
                        cache: false,
                        dataType: "html",
                        success: function(data) {
                            $modal.children(".content").html(data);
                            $modal.trigger("updated").show();
                        }
                    });
            },
            throwError: function(ex) {
                var err = new Error();
                err.message = ex.message;
                err.name = ex.name;

                if (err.stack) {
                    var callerLine = err.stack.split("\n")[2];
                    var index = callerLine.lastIndexOf(":");
                    var clean = callerLine.slice(index + 1, callerLine.length);
                    err.lineNumber = clean;
                }
                throw err;
            },
            navigateTo: function(loc, target) {
                if (!loc || loc.length === 0 || loc === "#") {
                    p.throwError({
                        message:
                            "portal.navigateTo(loc): The navigate location was not supplied. Possible causes include a blank href attribute. (Parameter: loc).",
                        name: "navigationException"
                    });
                }

                p.loadView(target);
            },
            ajaxQueue: function(ajaxOpts) {
                // Hold the original complete function.
                var oldComplete = ajaxOpts.complete;

                // Queue the Ajax request.
                ajaxQueue.queue(function(next) {
                    // Create a complete callback to fire the next event in the queue.
                    ajaxOpts.complete = function() {
                        // Fire the original complete if it was there.
                        if (oldComplete) {
                            oldComplete.apply(this, arguments);
                        }

                        next(); // Run the next query in the queue.
                    };

                    // Run the query.
                    $.ajax(ajaxOpts);
                });
            },
            jsonPost: function(options) {
                var settings = $.extend({
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        cache: false,
                        serializeEmpty: false
                    },
                    options);

                var bindResponse = function(a) {
                    var r = a.response;
                    if (!r) return;

                    if (typeof (r) === "string") {
                        if (!a.target) return;
                        a.target.html(r);
                    } else if (typeof (r) === "object") {
                        if (r.hasOwnProperty("messageBox")) {
                            var t = a.target || window.messageBox;
                            if (!t) return;
                            t.dataBind(r.messageBox).show();
                        } else {
                            if (!a.target) return;
                            a.target.dataBind(r);
                        }
                    }
                };
                var json;

                settings.form = getSelector(settings.form);

                if (settings.form) {
                    settings.url = settings.url || settings.form.attr("action");
                    settings.dtoName = settings.dtoName || settings.form.attr("name");
                }

                if (settings.dtoName) {
                    if (settings.form)
                        json = '{"' +
                            settings.dtoName +
                            '":' +
                            JSON.stringify(settings.form.find(":input[name]")
                                .serializeObject(settings.data, settings.serializeEmpty)) +
                            "}";
                    else
                        json = '{"' + settings.dtoName + '":' + JSON.stringify(settings.data) + "}";
                } else {
                    if (settings.form)
                        json = JSON.stringify(settings.form.find(":input[name]")
                            .serializeObject(settings.data, settings.serializeEmpty));
                    else
                        json = JSON.stringify(settings.data);
                }

                settings.target = getSelector(settings.target);
                p.debug.log("jsonPost", settings);
                $.ajax({
                    url: p.resolveUrl((settings.url ? settings.url : location.href) +
                        (settings.method ? "/" + settings.method : "")),
                    type: "POST",
                    data: json,
                    dataType: settings.dataType,
                    contentType: settings.contentType,
                    cache: settings.cache,
                    beforeSend: function(xhr) {
                        var args = new Object();
                        args.ajaxRequest = this;
                        args.XmlHttpRequest = xhr;
                        args.target = settings.target;
                        args.form = settings.form;
                        if (typeof settings.beforeSend != "function")
                            return true;
                        return settings.beforeSend(args);
                    },
                    success: function(response, statusText) {
                        var args = new Object();
                        var s = true;
                        args.ajaxRequest = this;
                        args.response = response;
                        args.statusText = statusText;
                        args.target = settings.target;
                        args.form = settings.form;
                        if (settings.success)
                            s = settings.success(args);
                        if (s !== false)
                            bindResponse(args);
                    },
                    error: function(xhr, errorStatus, thrownError) {
                        if (errorStatus === "parsererror") {
                            alert(errorStatus);
                            return;
                        }

                        var args = new Object();
                        var serverError = JSON.parse(xhr.responseText);
                        args.ajaxRequest = this;
                        args.XmlHttpRequest = xhr;
                        args.errorStatus = errorStatus;
                        args.scriptError = thrownError;
                        args.serverError = serverError;
                        args.target = settings.target;
                        args.form = settings.form;
                        if (settings.error) {
                            settings.error(args);
                        } else {
                            if (serverError && serverError.hasOwnProperty("Message"))
                                alert(serverError.Message);
                            else
                                alert(errorStatus);
                        }
                    },
                    complete: function(xhr, statusText) {
                        var args = new Object();
                        args.ajaxRequest = this;
                        args.XmlHttpRequest = xhr;
                        args.statusText = statusText;
                        args.target = settings.target;
                        args.form = settings.form;
                        var redirect = null;
                        if (settings.complete)
                            redirect = settings.complete(args);
                        if (redirect !== false)
                            p.redirectIfRequired(xhr);
                    }
                });
                return false;
            },
            toFormParams: function(obj) {
                var s = [];

                function add(key, value) {
                    s[s.length] = { name: key, value: value };
                }

                if ($.isArray(obj) || obj.jquery) {
                    $.each(obj,
                        function() {
                            add(this.name, this.value);
                        });
                } else {
                    for (var j in obj) {
                        if ($.isArray(obj[j])) {
                            $.each(obj[j],
                                function() {
                                    add(j, this);
                                });
                        } else {
                            add(j, $.isFunction(obj[j]) ? obj[j]() : obj[j]);
                        }
                    }
                }
                return s;
            },
            dataToList: function(data, options) {
                var settings = $.extend({}, options);
                var ulAttr = (settings.listId ? (' id="' + settings.listId + '"') : "") +
                    (settings.listClass ? (' class="' + settings.listClass + '"') : "");
                var liAttr = (settings.itemClass ? (' class="' + settings.itemClass + '"') : "");

                var list = "<ul" + ulAttr + ">";

                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    if (!item)
                        continue;

                    list += "<li" + liAttr + ">" + item + "</li>";
                }

                list += "</ul>";
                return list;
            },
            routes: function() {
            },
            models: function() {
            },
            views: function() {
            },
            controllers: function() {
            },
            debug: {
                log: function(msg, data, name) {
                    if (window.console && window.console.log) {
                        if (data) {
                            if (name) {
                                window.console.log("%s: %o %o", msg, name, data);
                            } else {
                                window.console.log("%s: %o", msg, data);
                            }
                        } else {
                            window.console.log("%s", msg);
                        }

                    }
                },
                warn: function(msg, data, name) {
                    if (window.console && window.console.warn) {
                        if (data) {
                            if (name) {
                                window.console.warn("%s: %o %o", msg, name, data);
                            } else {
                                window.console.warn("%s: %o", msg, data);
                            }
                        } else {
                            window.console.warn("%s", msg);
                        }

                    }
                },
                info: function(msg, data, name) {
                    if (window.console && window.console.info) {
                        if (data) {
                            if (name) {
                                window.console.info("%s: %o %o", msg, name, data);
                            } else {
                                window.console.info("%s: %o", msg, data);
                            }
                        } else {
                            window.console.info("%s", msg);
                        }

                    }
                },
                group: function(msg, data, name) {
                    if (window.console && window.console.group) {
                        if (data) {
                            if (name) {
                                window.console.group("%s: %o %o", msg, name, data);
                            } else {
                                window.console.group("%s: %o", msg, data);
                            }
                        } else {
                            window.console.group("%s", msg);
                        }

                    }
                },
                groupCollapsed: function(msg, data, name) {
                    if (window.console && window.console.groupCollapsed) {
                        if (data) {
                            if (name) {
                                window.console.groupCollapsed("%s: %o %o", msg, name, data);
                            } else {
                                window.console.groupCollapsed("%s: %o", msg, data);
                            }
                        } else {
                            window.console.groupCollapsed("%s", msg);
                        }

                    }
                },
                groupEnd: function() {
                    if (window.console && window.console.groupEnd) {
                        window.console.groupEnd();
                    }
                },
                assert: function(b) {
                    if (window.console && window.console.assert) {
                        window.console.assert(b);
                    }
                },
                logIf: function(msg, data) {
                    if (!data) {
                        return;
                    }
                    p.debug.log(msg, data);
                }
            }
        });

        return p;
    })();

    var tokenRegEx = /\$\{\w+\}/g;

    window.portal = window.p$ = new portal();

    function registerJQueryPlugins() {
        $.extend({
            firstExists: function() {
                var $first;
                $.each(arguments,
                    function(index, item) {
                        if (!item) {
                            return true;
                        }

                        var $this = (typeof (item) === "string") ? $(item) : item;
                        if ($this.exists()) {
                            $first = $this;
                            return false;
                        }
                        return true;
                    });
                return $first || $("body");
            }
        });

        $.fn.extend({
            autocomplete: function(options) {
                var $results = null,
                    timeout = null,
                    lastVal = null,
                    prevVal = "",
                    prevData = [],
                    settings = $.extend({
                            source: [],
                            minLength: 2,
                            maxResults: 10,
                            resultsClass: "loom-autocomplete-results",
                            delay: 200,
                        },
                        options);

                this.attr("autocomplete", "off");

                $results = $('<div class="dropdown"></div>')
                    .addClass(settings.resultsClass)
                    .css("position", "absolute")
                    .appendTo(this.parent())
                    .on({
                            click: function() {
                                $results.hide().data("loomOwner").val($(this).text()).focus();
                            },
                            mouseover: function() {
                                $results.find("li.active").removeClass("active");
                            }
                        },
                        "li a");

                var showResults = function($input, data) {
                    if (!data) {
                        return;
                    }

                    var dataLength = data.length;
                    if (dataLength === 0) {
                        $results.empty().hide();
                        return;
                    }

                    var list =
                        '<ul class="dropdown-menu autocomplete-menu" role="menu" style="position:relative; overflow-y:auto;">';
                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        if (!item)
                            continue;
                        list += '<li role="presentation"><a href="javascript:void(0);">' + item + "</a></li>";
                    }
                    list += "</ul>";

                    $results.html(list).show().find(".dropdown-menu").show()
                        .height($results.find("li:first").height() * 7).find("li:first").addClass("active");
                };

                var getData = function($input, uri, callback) {
                    var val = $input.val();
                    if (val === prevVal) {
                        callback($input, prevData);
                        return;
                    }
                    prevVal = val;

                    var params = { term: $input.val() };
                    $.getJSON(uri,
                        params,
                        function(data) {
                            prevData = data;
                            if (typeof (callback) === "function") {
                                callback($input, data);
                            }
                        });
                };

                var hasScroll = function(el) {
                    return el.height() < el[$.fn.prop ? "prop" : "attr"]("scrollHeight");
                };

                var verifySearchableValue = function(val) {
                    if (val === lastVal) {
                        return false;
                    }

                    if (val.length < settings.minLength) {
                        $results.hide().empty();
                        lastVal = val;
                        return false;
                    }
                    lastVal = val;
                    return true;
                };

                var handleNewValue = function($input) {
                    if (timeout) {
                        clearTimeout(timeout);
                    }

                    if (typeof (settings.source) === "string") {
                        timeout = setTimeout(function() {
                                if (!verifySearchableValue($input.val())) {
                                    return;
                                }
                                getData($input, p$.resolveUrl(settings.source), showResults);
                            },
                            settings.delay);
                    } else {
                        showResults($input, settings.source);
                    }
                };

                var changeSelection = function(step) {
                    var $selected = $results.find("li.active").removeClass("active");

                    if (step > 0) {
                        $selected = $selected.next();
                        if ($selected.length === 0) {
                            $selected = $results.find("li:first");
                        }
                    } else {
                        $selected = $selected.prev();
                        if ($selected.length === 0) {
                            $selected = $results.find("li:last");
                        }
                    }

                    var $parent = $selected.parent();
                    $parent.height(($selected.height() * 7));

                    if (hasScroll($parent)) {
                        var offset = $selected.offset().top - $parent.offset().top,
                            scroll = $parent.scrollTop(),
                            elementHeight = $parent.height();

                        if (offset < 0) {
                            $parent.scrollTop((scroll + offset) - 1);
                        } else if (offset >= elementHeight) {
                            $parent.scrollTop((scroll + offset - elementHeight + $selected.height()) - 1);
                        }
                    }

                    if ($selected.length > 0) {
                        $selected.addClass("active");
                        $results.show();
                    }
                };

                var selectCurrentResult = function($input) {
                    var $selected = $results.find("li.active a");
                    if ($selected.length > 0) {
                        $input.val($selected.text());
                    }
                };

                this.on({
                    keydown: function(e) {
                        var $input = $(e.target);
                        switch (e.keyCode) {
                        case 38:
                            // Up Arrow 
                            e.preventDefault();
                            changeSelection(-1);
                            break;
                        case 40:
                            // Down Arrow
                            e.preventDefault();
                            changeSelection(1);
                            break;
                        case 9:
                        case 13:
                            // Tab & Return
                            selectCurrentResult($input);
                            $results.hide();
                            break;
                        case 27:
                            // Esc
                            $results.hide();
                        default:
                            handleNewValue($input);
                            break;
                        }
                    },
                    blur: function() {
                        setTimeout(function() { $results.hide().empty(); }, 200);
                    },
                    focus: function(e) {
                        var $input = $(e.target);
                        var owner = $results.data("loomOwner");
                        if (owner && owner[0] != $input[0]) {
                            $results.empty();
                        }
                        $results.data("loomOwner", $input);
                    }
                });

                return this;
            },
            dataBind: function(data, option) {
                function bindItem(index, data2, item, itemTemplate) {
                    var formatData = function(target, tmpl, data3, idx, itm, attr) {
                        var setText = function(targetElem, text) {
                            if (attr && attr !== "text")
                                targetElem.attr(attr, text);
                            else
                                targetElem.html(text);
                        };

                        var formatText = function(s) {
                            var match = s.match(tokenRegEx);
                            if (match) {
                                for (var k = 0; k < match.length; k++) {
                                    var token = match[k];
                                    s = s.replace(token, data3[token.substring(2, token.length - 1)]);
                                }
                            }
                            return s;
                        };

                        if (typeof (tmpl) === "string") {
                            setText(target, formatText(tmpl));
                        } else if (typeof (tmpl) === "object") {
                            var currentTmpl;
                            for (var a in tmpl) {
                                currentTmpl = tmpl[a];
                                if (a === "style") {
                                    if (typeof (currentTmpl) === "object")
                                        target.css(currentTmpl);
                                    else if (typeof (currentTmpl) === "function")
                                        target.css(currentTmpl({ data: data3, elem: target, item: itm, i: idx }));
                                    continue;
                                }
                                formatData(target, currentTmpl, data3, idx, itm, a);
                            }
                        } else if (typeof (tmpl) === "function")
                            setText(target, tmpl({ data: data3, elem: target, item: itm, i: idx }));
                        else
                            setText(target, tmpl);
                    };

                    item.data("loomData", data2);
                    item.addClass("loomDataItem");

                    // Look through the items in the data object.
                    for (var p in data2) {
                        if (!itemTemplate || !itemTemplate[p]) {
                            var t1 = item.find("." + p);
                            if (t1.length === 0 && item.hasClass(p)) {
                                t1 = item;
                            }

                            var tmplText = t1.text();
                            if (tmplText.length > 0) {
                                formatData(t1, tmplText, data2, index, item);
                            } else {
                                t1.text(data2[p]);
                            }
                        }
                    }

                    // Look through the items defined in the template.
                    for (var j in itemTemplate) {
                        var t2 = item.find("." + j);
                        if (t2.length === 0 && item.hasClass(j)) {
                            t2 = item;
                        }
                        formatData(t2, itemTemplate[j], data2, index, item);
                    }

                    delete formatData;
                }

                if (data.length && data.length === 0)
                    return this;

                var domParent = this.data("loomParent");
                if (!domParent) {
                    this.dataTemplate();
                    domParent = this.data("loomParent");
                }

                if (!data ||
                    (!data.length && $.isEmptyObject(data)) ||
                    data.length === 0 ||
                    (data.length > 0 && $.isEmptyObject(data[0]))) {
                    domParent.empty();
                    return this;
                }

                var templateHtml = this.data("loomTemplateHtml");
                var seperatorTemplate = this.data("loomSeperatorHtml");
                //var eh = this.data('loomEmptyHtml');
                //var ah = this.data('loomAlternatingHtml');
                var templateOptions = this.data("loomTemplateOptions");

                var current;
                var parent;

                if (!data.length || data.length === 1)
                    parent = domParent;
                else
                    parent = $("<div></div>");

                if (!option) {
                    // replacing current items
                    current = $(parent.html(templateHtml).children()[0]);
                } else if (option === "append") {
                    // adding to end of items
                    current = parent.append(templateHtml).children(":last");
                } else if (option === "prepend") {
                    // adding to beginning of items
                    current = $(parent.prepend(templateHtml).children()[0]);
                } else {
                    throw "Invalid dataBind option";
                }

                var callback = (templateOptions.itemBound && typeof (templateOptions.itemBound) === "function")
                    ? templateOptions.itemBound
                    : null;

                if (!data.length) {
                    bindItem(0, data, current, templateOptions.format);
                    if (callback)
                        callback(current, data, 0);
                } else {
                    var currentData;
                    for (var i = 0; i < data.length; i++) {
                        currentData = data[i];
                        bindItem(i, currentData, current, templateOptions.format);
                        if (callback)
                            callback(current, currentData, i);
                        if (i < data.length - 1) {
                            if (seperatorTemplate)
                                current = current.after(seperatorTemplate).next();
                            current = current.after(templateHtml).next();
                        }
                    }
                }

                if (data.length && data.length > 1) {
                    if (!option) {
                        // replacing current items
                        domParent.empty().append(parent.children());
                    } else if (option === "append") {
                        // adding to end of items
                        domParent.append(parent.children());
                    } else if (option === "prepend") {
                        // adding to beginning of items
                        domParent.prepend(parent.children());
                    }
                }
                return this.show();
            },
            dataTemplate: function(o) {
                var s = $.extend({ itemTemplate: $(this.children(":not(thead, tfoot)")[0]) }, o);
                var domParent = this.data("loomParent");
                if (domParent) {
                    return this;
                }

                var template = s.itemTemplate instanceof jQuery ? s.itemTemplate : this.find(s.itemTemplate);
                if (template && template.is("tbody")) {
                    template = template.find("tr:first");
                }

                var seperatorTemplate = this.find(s.seperatorTemplate);
                var emptyTemplate = this.find(s.emptyTemplate);
                var alternatingTemplate = this.find(s.alternatingTemplate);

                if (template.length === 0)
                    throw this.selector + ": No itemTemplate specified and/or element has no children";

                domParent = template.parent();
                this.data("loomParent", domParent);
                this.data("loomTemplateOptions", s);

                this.data("loomTemplateHtml", template.wrap("<div></div>").parent().html());
                if (seperatorTemplate.length > 0)
                    this.data("loomSeperatorHtml", seperatorTemplate.wrap("<div></div>").parent().html());
                if (emptyTemplate.length > 0)
                    this.data("loomEmptyHtml", emptyTemplate.wrap("<div></div>").parent().html());
                if (alternatingTemplate.length > 0)
                    this.data("loomAlternatingHtml", alternatingTemplate.wrap("<div></div>").parent().html());

                if (s.events) {
                    for (var event in s.events) {
                        domParent.bind(event,
                            function(e) {
                                var di = $(e.target).closest(".loomDataItem");
                                var l = s.events[event];
                                if (di.length === 0)
                                    return true;
                                return l(e, di.data("loomData"), di);
                            });
                    }
                }

                domParent.empty();
                return this;
            },
            exists: function() {
                return this.length !== 0;
            },
            findOrDefault: function(sel) {
                return sel instanceof jQuery ? sel : this.find(sel);
            },
            jsonPost: function(options, callback) {
                return this.each(function() {
                    var $this = $(this);
                    var settings = $.extend({
                            event: "click",
                            form: "form",
                            targetOrigin: false,
                            url: $this.attr("href")
                        },
                        options);
                    $this.bind(settings.event,
                        function(e) {
                            e.preventDefault();
                            e.stopPropagation();
                            if (!callback || (callback && callback(e) !== false)) {
                                var form = (typeof (settings.form) === "string"
                                    ? (settings.targetOrigin ? $(e.target) : $(this)).closest(settings.form)
                                    : settings.form);
                                settings.form = form instanceof jQuery ? form : $(form);
                                p$.jsonPost(settings);
                            }
                        });
                });
            },
            log: function(msg) {
                return this.each(function() {
                    p$.debug.log(msg, $(this));
                });
            },
            messageBox: function() {
                this.hide();
                window.messageBox = this;
                if (!this.html().length == 0) {
                    this.append("<div></div>")
                        .find("div")
                        .append('<span class="title"></span>')
                        .append('<p class="message"></p>');
                }

                this.dataTemplate({
                    itemBound: function(item, data) {
                        item.attr("class", data.type);
                    },
                    format: {
                        message: {
                            text: function(e) {
                                return e.data.message;
                            },
                            href: function(e) {
                                return e.data.message;
                            }
                        }
                    }
                });

                return this;
            },
            scrollView: function(speed) {
                return this.each(function() {
                    $("html, body").animate({
                            scrollTop: $(this).offset().top
                        },
                        speed || 1000);
                });
            },
            serializeObject: function(overrides, serializeEmpty) {

                var getValue = function(t) {
                    if (overrides && overrides[t.name]) {
                        return $.isFunction(overrides[t.name]) ? overrides[t.name](t.value) : overrides[t.name];
                    }
                    return t.value;
                };

                function serializeSingleProperty(obj, name, value) {
                    if (obj[name] !== undefined) {
                        if (!obj[name].push) {
                            obj[name] = [obj[name]];
                        }
                        if (value || serializeEmpty) {
                            obj[name].push(value);
                        }

                    } else {
                        if (value || serializeEmpty) {
                            obj[name] = value;
                        }
                    }
                }

                var o = {};
                var a = this.serializeArray();

                $.each(a,
                    function() {

                        var h = this.name.split(".");
                        var value = getValue(this);

                        if (h.length > 1) {
                            if (o[h[0]] === undefined) {
                                var obj = {};
                                if (value || serializeEmpty) {
                                    obj[h[1]] = value;
                                }
                                o[h[0]] = obj;
                            } else {
                                serializeSingleProperty(o[h[0]], h[1], value);
                            }
                        } else {
                            serializeSingleProperty(o, this.name, value);
                        }
                    });

                for (var p in overrides) {
                    if (!o[p])
                        o[p] = $.isFunction(overrides[p]) ? overrides[p]() : overrides[p];
                }

                return o;
            },
            selectRange: function(start, end) {
                return this.each(function() {
                    if (this.createTextRange) {
                        var range = this.createTextRange();
                        range.collapse(true);
                        range.moveStart("character", start);
                        range.moveEnd("character", end);
                        range.select();
                    } else if (this.setSelectionRange) {
                        this.setSelectionRange(start, end);
                    } else if (this.selectionStart) {
                        this.selectionStart = start;
                        this.selectionEnd = end;
                    }
                    this.focus();
                });
            },
            fadeRemove: function(speed, callback) {
                $(this).fadeOut(speed,
                    function() {
                        $(this).remove(callback);
                    });
                return this;
            },
        });

        $.ajaxSetup({
            converters: {
                'text msjson': function(msg) {
                    try {
                        var json = $.parseJSON(msg);
                        return (json.hasOwnProperty("d") ? json.d : json);
                    } catch (e) {
                        return {
                            messageBox: {
                                type: "error",
                                title: "Error Parsing Response",
                                message: "Could not parse the server response."
                            }
                        };
                    }
                }
            }
        });
    }

    //Start the application.
    //
    $(function() {
        portal.run();
    });

    return portal;

})(jQuery, window);

// Events
//
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
            if (e.which === 13) {
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

// Prototypes
//
if (!String.prototype.startsWith) {
    String.prototype.startsWith = function(str) {
        return (this.lastIndexOf(str, 0) === 0);
    };
}

if (!String.prototype.endsWith) {
    String.prototype.endsWith = function(suffix) {
        return this.indexOf(suffix, this.length - suffix.length) !== -1;
    };
}

if (!Array.prototype.remove) {
    Array.prototype.remove = function(from, to) {
        this.splice(from, !to || 1 + to - from + (!(to < 0 ^ from >= 0) && (to < 0 || -1) * this.length));
        return this.length;
    };
}