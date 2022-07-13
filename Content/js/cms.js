﻿/*! MenuIfy by Templateify | v1.0.0 - https://www.templateify.com */
!function (a) { a.fn.menuify = function () { return this.each(function () { var $t = a(this), b = $t.find('.LinkList ul > li').children('a'), c = b.length; for (var i = 0; i < c; i++) { var d = b.eq(i), h = d.text(); if (h.charAt(0) !== '_') { var e = b.eq(i + 1), j = e.text(); if (j.charAt(0) === '_') { var m = d.parent(); m.append('<ul class="sub-menu m-sub"/>'); } } if (h.charAt(0) === '_') { d.text(h.replace('_', '')); d.parent().appendTo(m.children('.sub-menu')); } } for (var i = 0; i < c; i++) { var f = b.eq(i), k = f.text(); if (k.charAt(0) !== '_') { var g = b.eq(i + 1), l = g.text(); if (l.charAt(0) === '_') { var n = f.parent(); n.append('<ul class="sub-menu2 m-sub"/>'); } } if (k.charAt(0) === '_') { f.text(k.replace('_', '')); f.parent().appendTo(n.children('.sub-menu2')); } } $t.find('.LinkList ul li ul').parent('li').addClass('has-sub'); }); } }(jQuery);

/*! Tabify by Templateify | v1.0.0 - https://www.templateify.com */
!function (a) { a.fn.tabify = function (b) { b = jQuery.extend({ onHover: false, animated: true, transition: 'fadeInUp' }, b); return this.each(function () { var e = a(this), c = e.children('[tab-ify]'), d = 0, n = 'tab-animated', k = 'tab-active'; if (b.onHover == true) { var event = 'mouseenter' } else { var event = 'click' } e.prepend('<ul class="select-tab"></ul>'); c.each(function () { if (b.animated == true) { a(this).addClass(n) } e.find('.select-tab').append('<li><a href="javascript:;">' + a(this).attr('tab-ify') + '</a></li>') }).eq(d).addClass(k).addClass('tab-' + b.transition); e.find('.select-tab a').on(event, function () { var f = a(this).parent().index(); a(this).closest('.select-tab').find('.active').removeClass('active'); a(this).parent().addClass('active'); c.removeClass(k).removeClass('tab-' + b.transition).eq(f).addClass(k).addClass('tab-' + b.transition); return false }).eq(d).parent().addClass('active') }) } }(jQuery);

/*! jQuery replaceText by "Cowboy" Ben Alman | v1.1.0 - http://benalman.com/projects/jquery-replacetext-plugin/ */
(function ($) { $.fn.replaceText = function (b, a, c) { return this.each(function () { var f = this.firstChild, g, e, d = []; if (f) { do { if (f.nodeType === 3) { g = f.nodeValue; e = g.replace(b, a); if (e !== g) { if (!c && /</.test(e)) { $(f).before(e); d.push(f) } else { f.nodeValue = e } } } } while (f = f.nextSibling) } d.length && $(d).remove() }) } })(jQuery);

/*! Theia Sticky Sidebar | v1.9.0 - https://github.com/WeCodePixels/theia-sticky-sidebar */
(function ($) { $.fn.theiaStickySidebar = function (options) { var defaults = { 'containerSelector': '', 'additionalMarginTop': 0, 'additionalMarginBottom': 0, 'updateSidebarHeight': true, 'minWidth': 0, 'disableOnResponsiveLayouts': true, 'sidebarBehavior': 'modern', 'defaultPosition': 'relative', 'namespace': 'TSS' }; options = $.extend(defaults, options); options.additionalMarginTop = parseInt(options.additionalMarginTop) || 0; options.additionalMarginBottom = parseInt(options.additionalMarginBottom) || 0; tryInitOrHookIntoEvents(options, this); function tryInitOrHookIntoEvents(options, $that) { var success = tryInit(options, $that); if (!success) { console.log('TSS: Body width smaller than options.minWidth. Init is delayed.'); $(document).on('scroll.' + options.namespace, function (options, $that) { return function (evt) { var success = tryInit(options, $that); if (success) { $(this).unbind(evt) } } }(options, $that)); $(window).on('resize.' + options.namespace, function (options, $that) { return function (evt) { var success = tryInit(options, $that); if (success) { $(this).unbind(evt) } } }(options, $that)) } } function tryInit(options, $that) { if (options.initialized === true) { return true } if ($('body').width() < options.minWidth) { return false } init(options, $that); return true } function init(options, $that) { options.initialized = true; var existingStylesheet = $('#theia-sticky-sidebar-stylesheet-' + options.namespace); if (existingStylesheet.length === 0) { $('head').append($('<style id="theia-sticky-sidebar-stylesheet-' + options.namespace + '">.theiaStickySidebar:after {content: ""; display: table; clear: both;}</style>')) } $that.each(function () { var o = {}; o.sidebar = $(this); o.options = options || {}; o.container = $(o.options.containerSelector); if (o.container.length == 0) { o.container = o.sidebar.parent() } o.sidebar.parents().css('-webkit-transform', 'none'); o.sidebar.css({ 'position': o.options.defaultPosition, 'overflow': 'visible', '-webkit-box-sizing': 'border-box', '-moz-box-sizing': 'border-box', 'box-sizing': 'border-box' }); o.stickySidebar = o.sidebar.find('.theiaStickySidebar'); if (o.stickySidebar.length == 0) { var javaScriptMIMETypes = /(?:text|application)\/(?:x-)?(?:javascript|ecmascript)/i; o.sidebar.find('script').filter(function (index, script) { return script.type.length === 0 || script.type.match(javaScriptMIMETypes) }).remove(); o.stickySidebar = $('<div>').addClass('theiaStickySidebar').append(o.sidebar.children()); o.sidebar.append(o.stickySidebar) } o.marginBottom = parseInt(o.sidebar.css('margin-bottom')); o.paddingTop = parseInt(o.sidebar.css('padding-top')); o.paddingBottom = parseInt(o.sidebar.css('padding-bottom')); var collapsedTopHeight = o.stickySidebar.offset().top; var collapsedBottomHeight = o.stickySidebar.outerHeight(); o.stickySidebar.css('padding-top', 1); o.stickySidebar.css('padding-bottom', 1); collapsedTopHeight -= o.stickySidebar.offset().top; collapsedBottomHeight = o.stickySidebar.outerHeight() - collapsedBottomHeight - collapsedTopHeight; if (collapsedTopHeight == 0) { o.stickySidebar.css('padding-top', 0); o.stickySidebarPaddingTop = 0 } else { o.stickySidebarPaddingTop = 1 } if (collapsedBottomHeight == 0) { o.stickySidebar.css('padding-bottom', 0); o.stickySidebarPaddingBottom = 0 } else { o.stickySidebarPaddingBottom = 1 } o.previousScrollTop = null; o.fixedScrollTop = 0; resetSidebar(); o.onScroll = function (o) { if (!o.stickySidebar.is(":visible")) { return } if ($('body').width() < o.options.minWidth) { resetSidebar(); return } if (o.options.disableOnResponsiveLayouts) { var sidebarWidth = o.sidebar.outerWidth(o.sidebar.css('float') == 'none'); if (sidebarWidth + 50 > o.container.width()) { resetSidebar(); return } } var scrollTop = $(document).scrollTop(); var position = 'static'; if (scrollTop >= o.sidebar.offset().top + (o.paddingTop - o.options.additionalMarginTop)) { var offsetTop = o.paddingTop + options.additionalMarginTop; var offsetBottom = o.paddingBottom + o.marginBottom + options.additionalMarginBottom; var containerTop = o.sidebar.offset().top; var containerBottom = o.sidebar.offset().top + getClearedHeight(o.container); var windowOffsetTop = 0 + options.additionalMarginTop; var windowOffsetBottom; var sidebarSmallerThanWindow = (o.stickySidebar.outerHeight() + offsetTop + offsetBottom) < $(window).height(); if (sidebarSmallerThanWindow) { windowOffsetBottom = windowOffsetTop + o.stickySidebar.outerHeight() } else { windowOffsetBottom = $(window).height() - o.marginBottom - o.paddingBottom - options.additionalMarginBottom } var staticLimitTop = containerTop - scrollTop + o.paddingTop; var staticLimitBottom = containerBottom - scrollTop - o.paddingBottom - o.marginBottom; var top = o.stickySidebar.offset().top - scrollTop; var scrollTopDiff = o.previousScrollTop - scrollTop; if (o.stickySidebar.css('position') == 'fixed') { if (o.options.sidebarBehavior == 'modern') { top += scrollTopDiff } } if (o.options.sidebarBehavior == 'stick-to-top') { top = options.additionalMarginTop } if (o.options.sidebarBehavior == 'stick-to-bottom') { top = windowOffsetBottom - o.stickySidebar.outerHeight() } if (scrollTopDiff > 0) { top = Math.min(top, windowOffsetTop) } else { top = Math.max(top, windowOffsetBottom - o.stickySidebar.outerHeight()) } top = Math.max(top, staticLimitTop); top = Math.min(top, staticLimitBottom - o.stickySidebar.outerHeight()); var sidebarSameHeightAsContainer = o.container.height() == o.stickySidebar.outerHeight(); if (!sidebarSameHeightAsContainer && top == windowOffsetTop) { position = 'fixed' } else if (!sidebarSameHeightAsContainer && top == windowOffsetBottom - o.stickySidebar.outerHeight()) { position = 'fixed' } else if (scrollTop + top - o.sidebar.offset().top - o.paddingTop <= options.additionalMarginTop) { position = 'static' } else { position = 'absolute' } } if (position == 'fixed') { var scrollLeft = $(document).scrollLeft(); o.stickySidebar.css({ 'position': 'fixed', 'width': getWidthForObject(o.stickySidebar) + 'px', 'transform': 'translateY(' + top + 'px)', 'left': (o.sidebar.offset().left + parseInt(o.sidebar.css('padding-left')) - scrollLeft) + 'px', 'top': '0px' }) } else if (position == 'absolute') { var css = {}; if (o.stickySidebar.css('position') != 'absolute') { css.position = 'absolute'; css.transform = 'translateY(' + (scrollTop + top - o.sidebar.offset().top - o.stickySidebarPaddingTop - o.stickySidebarPaddingBottom) + 'px)'; css.top = '0px' } css.width = getWidthForObject(o.stickySidebar) + 'px'; css.left = ''; o.stickySidebar.css(css) } else if (position == 'static') { resetSidebar() } if (position != 'static') { if (o.options.updateSidebarHeight == true) { o.sidebar.css({ 'min-height': o.stickySidebar.outerHeight() + o.stickySidebar.offset().top - o.sidebar.offset().top + o.paddingBottom }) } } o.previousScrollTop = scrollTop }; o.onScroll(o); $(document).on('scroll.' + o.options.namespace, function (o) { return function () { o.onScroll(o) } }(o)); $(window).on('resize.' + o.options.namespace, function (o) { return function () { o.stickySidebar.css({ 'position': 'static' }); o.onScroll(o) } }(o)); if (typeof ResizeSensor !== 'undefined') { new ResizeSensor(o.stickySidebar[0], function (o) { return function () { o.onScroll(o) } }(o)) } function resetSidebar() { o.fixedScrollTop = 0; o.sidebar.css({ 'min-height': '1px' }); o.stickySidebar.css({ 'position': 'static', 'width': '', 'transform': 'none' }) } function getClearedHeight(e) { var height = e.height(); e.children().each(function () { height = Math.max(height, $(this).height()) }); return height } }) } function getWidthForObject(object) { var width; try { width = object[0].getBoundingClientRect().width } catch (err) { } if (typeof width === "undefined") { width = object.width() } return width } return this } })(jQuery);

/*! Shortcode.js by @nicinabox | v1.1.0 - https://github.com/nicinabox/shortcode.js */
var Shortcode = function (el, tags) { if (!el) { return } this.el = el; this.tags = tags; this.matches = []; this.regex = '\\[{name}(\\s[\\s\\S]*?)?\\]' + '(?:((?!\\s*?(?:\\[{name}|\\[\\/(?!{name})))[\\s\\S]*?)' + '(\\[\/{name}\\]))?'; if (this.el.jquery) { this.el = this.el[0] } this.matchTags(); this.convertMatchesToNodes(); this.replaceNodes() }; Shortcode.prototype.matchTags = function () { var html = this.el.outerHTML, instances, match, re, contents, regex, tag, options; for (var key in this.tags) { if (!this.tags.hasOwnProperty(key)) { return } re = this.template(this.regex, { name: key }); instances = html.match(new RegExp(re, 'g')) || []; for (var i = 0, len = instances.length; i < len; i++) { match = instances[i].match(new RegExp(re)); contents = match[3] ? '' : undefined; tag = match[0]; regex = this.escapeTagRegExp(tag); options = this.parseOptions(match[1]); if (match[2]) { contents = match[2].trim(); tag = tag.replace(contents, '').replace(/\n\s*/g, ''); regex = this.escapeTagRegExp(tag).replace('\\]\\[', '\\]([\\s\\S]*?)\\[') } this.matches.push({ name: key, tag: tag, regex: regex, options: options, contents: contents }) } } }; Shortcode.prototype.convertMatchesToNodes = function () { var html = this.el.innerHTML, excludes, re, replacer; replacer = function (match, p1, p2, p3, p4, offset, string) { if (p1) { return match } else { var node = document.createElement('span'); node.setAttribute('data-sc-tag', this.tag); node.className = 'templateify-sc-node templateify-sc-node-' + this.name; return node.outerHTML } }; for (var i = 0, len = this.matches.length; i < len; i++) { excludes = '((data-sc-tag=")|(<pre.*)|(<code.*))?'; re = new RegExp(excludes + this.matches[i].regex, 'g'); html = html.replace(re, replacer.bind(this.matches[i])) } this.el.innerHTML = html }; Shortcode.prototype.replaceNodes = function () { var self = this, html, match, result, done, node, fn, replacer, nodes = this.el.querySelectorAll('.templateify-sc-node'); replacer = function (result) { if (result.jquery) { result = result[0] } result = self.parseCallbackResult(result); node.parentNode.replaceChild(result, node) }; for (var i = 0, len = this.matches.length; i < len; i++) { match = this.matches[i]; node = this.el.querySelector('.templateify-sc-node-' + match.name); if (node && node.dataset.scTag === match.tag) { fn = this.tags[match.name].bind(match); done = replacer.bind(match); result = fn(done); if (result !== undefined) { done(result) } } } }; Shortcode.prototype.parseCallbackResult = function (result) { var container, fragment, children; switch (typeof result) { case 'function': result = document.createTextNode(result()); break; case 'string': container = document.createElement('div'); fragment = document.createDocumentFragment(); container.innerHTML = result; children = container.childNodes; if (children.length) { for (var i = 0, len = children.length; i < len; i++) { fragment.appendChild(children[i].cloneNode(true)) } result = fragment } else { result = document.createTextNode(result) } break; case 'object': if (!result.nodeType) { result = JSON.stringify(result); result = document.createTextNode(result) } break; case 'default': break }return result }; Shortcode.prototype.parseOptions = function (stringOptions) { var options = {}, set; if (!stringOptions) { return } set = stringOptions.replace(/(\w+=)/g, '\n$1').split('\n'); set.shift(); for (var i = 0; i < set.length; i++) { var kv = set[i].split('='); options[kv[0]] = kv[1].replace(/\'|\"/g, '').trim() } return options }; Shortcode.prototype.escapeTagRegExp = function (regex) { return regex.replace(/[\[\]\/]/g, '\\$&') }; Shortcode.prototype.template = function (s, d) { for (var p in d) { s = s.replace(new RegExp('{' + p + '}', 'g'), d[p]) } return s }; String.prototype.trim = String.prototype.trim || function () { return this.replace(/^\s+|\s+$/g, '') }; if (window.jQuery) { var pluginName = 'shortcode'; $.fn[pluginName] = function (tags) { this.each(function () { if (!$.data(this, pluginName)) { $.data(this, pluginName, new Shortcode(this, tags)) } }); return this } }

$("#post-body").find("img").addClass("img-fluid lozad");

var fixedMenu = true, fixedSidebar = true;

$("#main-wrapper, #sidebar-wrapper").each(function () {
    if (fixedSidebar == true) {
        $(this).theiaStickySidebar({ additionalMarginTop: 30, additionalMarginBottom: 30 });
    }
});
$(".back-top").each(function () {
    var $this = $(this);
    $(window).on("scroll", function () {
        $(this).scrollTop() >= 100 ? $this.fadeIn(250) : $this.fadeOut(250);
    }),
        $this.click(function () {
            $("html, body").animate({ scrollTop: 0 }, 500);
        });
});

$(function () {
    
});

$(function () {
    $(".index-post .entry-image-link .entry-thumb, .PopularPosts .entry-image-link .entry-thumb, .FeaturedPost .entry-image-link .entry-thumb,.about-author .author-avatar").lazyify();
    $(".mobile-logo").each(function () {
        var $t = $(this),
            $l = $("#main-logo .header-widget a").clone();
        $l.find("#h1-tag").remove();
        $l.appendTo($t);
    });
    $("#mobile-menu").each(function () {
        var $t = $(this),
            $m = $("#article-main-menu-nav").clone();
        $m.attr("id", "main-mobile-nav");
        $m.find(".getMega, .mega-widget, .mega-tab").remove();
        $m.find("li.mega-tabs .complex-tabs").each(function () {
            var $eq = $(this);
            $eq.replaceWith($eq.find("> ul.select-tab").attr("class", "sub-menu m-sub"));
        });
        $m.find(".mega-menu:not(.mega-tabs) > a").each(function () {
            var $t = $(this),
                $h = $t.attr("href").trim(),
                $tlc = $h.toLowerCase();
            if ($tlc.match("getmega")) {
                $t.append('<div class="getMega">' + $h + "</div>");
                $t.find(".getMega").shortcode({
                    getMega: function ($u, $l) {
                        $l = this.options.label;
                        $l == "recent" ? ($u = "/search") : ($u = "/search/label/" + $l);
                        $t.attr("href", $u);
                    },
                });
            }
        });
        $m.find(".mega-tabs ul li > a").each(function () {
            var $a = $(this),
                $l = $a.text().trim();
            $a.attr("href", "/search/label/" + $l);
        });
        $m.appendTo($t);
        $(".show-mobile-menu, .hide-mobile-menu, .overlay").on("click", function () {
            $("body").toggleClass("nav-active");
        });
        $(".mobile-menu .has-sub").append('<div class="submenu-toggle"/>');
        $(".mobile-menu .mega-menu").find(".submenu-toggle").remove();
        $(".mobile-menu .mega-tabs").append('<div class="submenu-toggle"/>');
        $(".mobile-menu ul li .submenu-toggle").on("click", function ($this) {
            if ($(this).parent().hasClass("has-sub")) {
                $this.preventDefault();
                if (!$(this).parent().hasClass("show")) {
                    $(this).parent().addClass("show").children(".m-sub").slideToggle(170);
                } else {
                    $(this).parent().removeClass("show").find("> .m-sub").slideToggle(170);
                }
            }
        });
    });
    $(".social-mobile").each(function () {
        var $t = $(this),
            $l = $("#about-section .social-footer").clone();
        $l.removeClass("social-color");
        $l.appendTo($t);
    });
    $(".navbar-wrap .navbar").each(function () {
        var $this = $(this);
        if (fixedMenu == true) {
            if ($this.length > 0) {
                var t = $(document).scrollTop(),
                    w = $this.offset().top,
                    s = $this.height(),
                    h = w + s;
                $(window).scroll(function () {
                    var n = $(document).scrollTop(),
                        f = $("#footer-wrapper").offset().top,
                        m = f - s;
                    if (n < m) {
                        if (n > h) {
                            $this.addClass("is-fixed");
                        } else if (n <= 0) {
                            $this.removeClass("is-fixed");
                        }
                        if (n > t) {
                            $this.removeClass("show");
                        } else {
                            $this.addClass("show");
                        }
                        t = $(document).scrollTop();
                    }
                });
            }
        }
    });
    
    $("p.comment-content").each(function () {
        var $t = $(this);
        $t.replaceText(/(https:\/\/\S+(\.png|\.jpeg|\.jpg|\.gif))/g, '<img src="$1"/>');
        $t.replaceText(
            /(?:https:\/\/)?(?:www\.)?(?:youtube\.com)\/(?:watch\?v=)?(.+)/g,
            '<iframe id="youtube" width="100%" height="330" src="https://www.youtube.com/embed/$1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>'
        );
    });
    $("#load-more-link").each(function () {
        var $this = $(this),
            $loadLink = $this.data("load");
        if ($loadLink) {
            $("#load-more-link").show();
        }
        $("#load-more-link").on("click", function (a) {
            $("#load-more-link").hide();
            $.ajax({
                url: $loadLink,
                success: function (data) {
                    var $p = $(data).find(".blog-posts");
                    $p.find(".index-post").addClass("post-animated post-fadeInUp");
                    $(".blog-posts").append($p.html());
                    $loadLink = $(data).find("#load-more-link").data("load");
                    if ($loadLink) {
                        $("#load-more-link").show();
                    } else {
                        $("#load-more-link").hide();
                        $("#blog-pager .no-more").addClass("show");
                    }
                    $(".index-post .entry-image-link .entry-thumb").lazyify();
                },
                beforeSend: function () {
                    $("#blog-pager .loading").show();
                },
                complete: function () {
                    $("#blog-pager .loading").hide();
                },
            });
            a.preventDefault();
        });
    });
});


!(function (a) {
    a.fn.lazyify = function () {
        return this.each(function () {
            var t = a(this),
                dImg = t.attr("data-image"),
                iWid = Math.round(t.width()),
                iHei = Math.round(t.height()),
                iSiz = "/w" + iWid + "-h" + iHei + "-p-k-no-nu",
                img = "";
            if (dImg.match("s72-c")) {
                img = dImg.replace("/s72-c", iSiz);
            } else if (dImg.match("w72-h")) {
                img = dImg.replace("/w72-h72-p-k-no-nu", iSiz);
            } else {
                img = dImg;
            }
            a(window).on("resize scroll", lazyOnScroll);
            function lazyOnScroll() {
                var wHeight = a(window).height(),
                    scrTop = a(window).scrollTop(),
                    offTop = t.offset().top;
                if (scrTop + wHeight > offTop) {
                    var n = new Image();
                    (n.onload = function () {
                        t.attr("style", "background-image:url(" + this.src + ")").addClass("lazy-ify");
                    }),
                        (n.src = img);
                }
            }
            lazyOnScroll();
        });
    };
})(jQuery);
$("#article-main-menu").menuify();
$("#article-main-menu .widget").addClass("show-menu");
$(".show-search").on("click", function () {
    $("#nav-search").fadeIn(170).find("input").focus();
});
$(".hide-search").on("click", function () {
    $("#nav-search").fadeOut(170).find("input").blur();
});
//$(".blog-posts-headline,.related-title").each(function () {
//    var $t = $(this),
//        $m = $t.find(".more"),
//        $mT = showMoreText;
//    if ($mT != "") {
//        $m.text($mT);
//    }
//});
$(".follow-by-email-text").each(function () {
    var $t = $(this),
        $fbet = followByEmailText;
    if ($fbet != "") {
        $t.text($fbet);
    }
});
$("#post-body").shortcode({
    ads: function () {
        if (this.options != undefined) {
            var i = this.options.id;
            switch (i) {
                case "ads1":
                    return '<div id="new-before-ad"/>';
                    break;
                case "ads2":
                    return '<div id="new-after-ad"/>';
                    break;
                default:
                    return "";
                    break;
            }
        }
    },
});
$("#new-before-ad").each(function () {
    var $t = $(this);
    if ($t.length) {
        $("#before-ad").appendTo($t);
    }
});
$("#new-after-ad").each(function () {
    var $t = $(this);
    if ($t.length) {
        $("#after-ad").appendTo($t);
    }
});
$("#main-before-ad .widget").each(function () {
    var $t = $(this);
    if ($t.length) {
        $t.appendTo($("#before-ad"));
    }
});
$("#main-after-ad .widget").each(function () {
    var $t = $(this);
    if ($t.length) {
        $t.appendTo($("#after-ad"));
    }
});
$("#social-counter ul.social-icons li a").each(function () {
    var $t = $(this),
        $a = $t.find(".count"),
        $d = $t.data("content").trim(),
        $s = $d.split("$"),
        $u = $s[0],
        $c = $s[1];
    $t.attr("href", $u);
    $a.text($c);
});
$("#sidebar-tabs").each(function () {
    $("#sidebar-tabs .widget").each(function () {
        var textTab = $(this).find(".widget-title > h3").text().trim();
        $(this).attr("tab-ify", textTab);
    });
    $("#sidebar-tabs").tabify();
    var wCount = $("#sidebar-tabs .widget").length;
    if (wCount >= 1) {
        $(this)
            .addClass("tabs-" + wCount)
            .show();
    }
});
$(".avatar-image-container img").attr("src", function ($this, i) {
    i = i.replace("//resources.blogblog.com/img/blank.gif", "//1.bp.blogspot.com/-oSjP8F09qxo/Wy1J9dp7b0I/AAAAAAAACF0/ggcRfLCFQ9s2SSaeL9BFSE2wyTYzQaTyQCK4BGAYYCw/s35-r/avatar.jpg");
    i = i.replace("//img1.blogblog.com/img/blank.gif", "//1.bp.blogspot.com/-oSjP8F09qxo/Wy1J9dp7b0I/AAAAAAAACF0/ggcRfLCFQ9s2SSaeL9BFSE2wyTYzQaTyQCK4BGAYYCw/s35-r/avatar.jpg");
    return i;
});
$(".post-body a").each(function () {
    var $this = $(this),
        type = $this.text().trim(),
        sp = type.split("/"),
        txt = sp[0],
        ico = sp[1],
        color = sp.pop();
    if (type.match("button")) {
        $this.addClass("button").text(txt);
        if (ico != "button") {
            $this.addClass(ico);
        }
        if (color != "button") {
            $this.addClass("colored-button").css({ "background-color": color });
        }
    }
});
$(".post-body strike").each(function () {
    var $this = $(this),
        type = $this.text().trim(),
        html = $this.html();
    if (type.match("contact-form")) {
        $this.replaceWith('<div class="contact-form"/>');
        $(".contact-form").append($("#ContactForm1"));
    }
    if (type.match("alert-success")) {
        $this.replaceWith('<div class="alert-message alert-success short-b">' + html + "</div>");
    }
    if (type.match("alert-info")) {
        $this.replaceWith('<div class="alert-message alert-info short-b">' + html + "</div>");
    }
    if (type.match("alert-warning")) {
        $this.replaceWith('<div class="alert-message alert-warning short-b">' + html + "</div>");
    }
    if (type.match("alert-error")) {
        $this.replaceWith('<div class="alert-message alert-error short-b">' + html + "</div>");
    }
    if (type.match("left-sidebar")) {
        $this.replaceWith("<style>.item #main-wrapper{float:right}.item #sidebar-wrapper{float:left}</style>");
    }
    if (type.match("right-sidebar")) {
        $this.replaceWith("<style>.item #main-wrapper{float:left}.item #sidebar-wrapper{float:right}</style>");
    }
    if (type.match("full-width")) {
        $this.replaceWith("<style>.item #main-wrapper{width:100%}.item #sidebar-wrapper{display:none}</style>");
    }
    if (type.match("code-box")) {
        $this.replaceWith('<pre class="code-box short-b">' + html + "</pre>");
    }
    var $sb = $(".post-body .short-b").find("b");
    $sb.each(function () {
        var $b = $(this),
            $t = $b.text().trim();
        if ($t.match("alert-success") || $t.match("alert-info") || $t.match("alert-warning") || $t.match("alert-error") || $t.match("code-box")) {
            $b.replaceWith("");
        }
    });
});
$(".share-links .window-ify").on("click", function () {
    var $this = $(this),
        url = $this.data("url"),
        wid = $this.data("width"),
        hei = $this.data("height"),
        wsw = window.screen.width,
        wsh = window.screen.height,
        mrl = Math.round(wsw / 2 - wid / 2),
        mrt = Math.round(wsh / 2 - hei / 2),
        win = window.open(url, "_blank", "scrollbars=yes,resizable=yes,toolbar=no,location=yes,width=" + wid + ",height=" + hei + ",left=" + mrl + ",top=" + mrt);
    win.focus();
});
$(".share-links").each(function () {
    var $t = $(this),
        $b = $t.find(".show-hid a");
    $b.on("click", function () {
        $t.toggleClass("show-hidden");
    });
});
$(".about-author .author-description span a").each(function () {
    var $this = $(this),
        cls = $this.text().trim(),
        url = $this.attr("href");
    $this.replaceWith('<li class="' + cls + '"><a href="' + url + '" title="' + cls + '" target="_blank"/></li>');
    $(".author-description").append($(".author-description span li"));
    $(".author-description").addClass("show-icons");
});
$(".footer-widgets-wrap").each(function () {
    var $t = $(this),
        $n = $t.find(".no-items").length;
    if ($n == 3) {
        $("#footer-wrapper").addClass("compact-footer");
    }
});
$("#article-main-menu li").each(function () {
    var lc = $(this),
        ltx = lc.find("a"),
        am = ltx.attr("href"),
        st = am.toLowerCase(),
        $this = lc,
        li = $this,
        text = st;
    if (st.match("getmega")) {
        $this.addClass("has-sub mega-menu").append('<div class="getMega">' + am + "</div>");
    }
    $this.find(".getMega").shortcode({
        getMega: function () {
            var label = this.options.label,
                type = this.options.type,
                num = 5;
            ajaxMega($this, type, num, label, text);
            if (type == "mtabs") {
                if (label != undefined) {
                    label = label.split("/");
                }
                megaTabs(li, type, label);
            }
        },
    });
});
function megaTabs(li, type, label) {
    if (type == "mtabs") {
        if (label != undefined) {
            var lLen = label.length,
                code = '<ul class="complex-tabs">';
            for (var i = 0; i < lLen; i++) {
                var tag = label[i];
                if (tag) {
                    code += '<div class="mega-tab" tab-ify="' + tag + '"/>';
                }
            }
            code += "</ul>";
            li.addClass("mega-tabs mtabs").append(code);
            li.find("a:first").attr("href", "javascript:;");
            $(".mega-tab").each(function () {
                var $this = $(this),
                    label = $this.attr("tab-ify");
                ajaxMega($this, "megatabs", 4, label, "getmega");
            });
            li.find("ul.complex-tabs").tabify({ onHover: true });
        } else {
            li.addClass("mega-tabs").append('<ul class="mega-widget">' + msgError() + "</ul>");
        }
    }
}
$("#breaking-sec .HTML .widget-content").each(function () {
    var $this = $(this),
        text = $this.text().trim().toLowerCase();
    $this.shortcode({
        getBreaking: function () {
            var num = this.options.results,
                label = this.options.label;
            ajaxBreaking($this, "breaking", num, label, text);
        },
    });
});
$("#featured-sec .HTML .widget-content").each(function () {
    var $this = $(this),
        text = $this.text().trim().toLowerCase();
    $this.shortcode({
        getFeatured: function () {
            var label = this.options.label,
                type = this.options.type;
            switch (type) {
                case "featured1":
                    var num = 4;
                    break;
                case "featured3":
                    num = 6;
                    break;
                case "featured6":
                    num = 3;
                    break;
                default:
                    num = 5;
                    break;
            }
            ajaxFeatured($this, type, num, label, text);
        },
    });
});

$(".block-posts .HTML .widget-content").each(function () {
    var $this = $(this),
        text = $this.text().trim().toLowerCase();
    $this.shortcode({
        getBlock: function () {
            var num = this.options.results,
                label = this.options.label,
                type = this.options.type;
            ajaxBlock($this, type, num, label, text);
        },
    });
});
$(".article-widget-ready .HTML .widget-content").each(function () {
    var $this = $(this),
        text = $this.text().trim().toLowerCase();
    $this.shortcode({
        getWidget: function () {
            var num = this.options.results,
                label = this.options.label,
                type = this.options.type;
            ajaxWidget($this, type, num, label, text);
        },
    });
});
$(".article-related-content").each(function () {
    var $this = $(this),
        label = $this.find(".related-tag").attr("data-label"),
        num = relatedPostsNum;
    if (num >= 6) {
        num = 6;
    } else {
        num = 3;
    }
    ajaxRelated($this, "related", num, label, "getrelated");
});
function msgError() {
    return '<span class="no-posts"><b>Error:</b> No Results Found</span>';
}
function msgServerError() {
    return '<div class="no-posts error-503"><b>Failed to load resource:</b> the server responded with a status of 503</div>';
}
function beforeLoader() {
    return '<div class="loader"/>';
}
function getFeedUrl(type, num, label) {
    var furl = "";
    switch (label) {
        case "recent":
            furl = "/feeds/posts/summary?alt=json&max-results=" + num;
            break;
        case "comments":
            if (type == "list") {
                furl = "/feeds/comments/summary?alt=json&max-results=" + num;
            } else {
                furl = "/feeds/posts/summary/-/" + label + "?alt=json&max-results=" + num;
            }
            break;
        default:
            furl = "/feeds/posts/summary/-/" + label + "?alt=json&max-results=" + num;
            break;
    }
    return furl;
}
function getPostLink(feed, i) {
    for (var x = 0; x < feed[i].link.length; x++)
        if (feed[i].link[x].rel == "alternate") {
            var link = feed[i].link[x].href;
            break;
        }
    return link;
}
function getPostTitle(feed, i) {
    var n = feed[i].title.$t;
    return n;
}
function getPostImage(feed, i) {
    if ("media$thumbnail" in feed[i]) {
        var src = feed[i].media$thumbnail.url;
        if (src.match("img.youtube.com")) {
            src = src.replace("/default.", "/0.");
        }
        var img = src;
    } else {
        img = "https://4.bp.blogspot.com/-eALXtf-Ljts/WrQYAbzcPUI/AAAAAAAABjY/vptx-N2H46oFbiCqbSe2JgVSlHhyl0MwQCK4BGAYYCw/s72-c/nth-ify.png";
    }
    return img;
}
function getPostAuthor(feed, i) {
    var n = feed[i].author[0].name.$t,
        by = messages.postedBy,
        em = "";
    if (by != "") {
        em = "<em>" + by + "</em>";
    } else {
        em = "";
    }
    var code = '<span class="entry-author">' + em + '<span class="by">' + n + "</span></span>";
    return code;
}
function getPostDate(feed, i) {
    var c = feed[i].published.$t,
        d = c.substring(0, 4),
        f = c.substring(5, 7),
        m = c.substring(8, 10),
        h = monthFormat[parseInt(f, 10) - 1] + " " + m + ", " + d;
    var on = messages.postedOn,
        em = "";
    if (on != "") {
        em = "<em>" + on + "</em>";
    } else {
        em = "";
    }
    var code = ['<span class="entry-time">' + em + '<time class="published" datetime="' + c + '">' + h + "</time></span>", '<span class="entry-time"><time class="published" datetime="' + c + '">' + h + "</time></span>"];
    return code;
}
function getPostLabel(feed, i) {
    if (feed[i].category != undefined) {
        var tag = feed[i].category[0].term,
            code = '<span class="entry-category">' + tag + "</span>";
    } else {
        code = "";
    }
    return code;
}
function getPostComments(feed, i, link) {
    var n = feed[i].author[0].name.$t,
        e = feed[i].author[0].gd$image.src.replace("/s113", "/w55-h55-p-k-no-nu"),
        h = feed[i].title.$t;
    if (e.match("//img1.blogblog.com/img/blank.gif")) {
        var img = "//4.bp.blogspot.com/-oSjP8F09qxo/Wy1J9dp7b0I/AAAAAAAACF0/ggcRfLCFQ9s2SSaeL9BFSE2wyTYzQaTyQCK4BGAYYCw/w55-h55-p-k-no-nu/avatar.jpg";
    } else {
        var img = e;
    }
    var code =
        '<article class="custom-item item-' +
        i +
        '"><a class="entry-image-link cmm-avatar" href="' +
        link +
        '"><span class="entry-thumb" data-image="' +
        img +
        '"/></a><h2 class="entry-title"><a href="' +
        link +
        '">' +
        n +
        '</a></h2><span class="cmm-snippet excerpt">' +
        h +
        "</span></article>";
    return code;
}
function getFeatMeta(type, i, author, date) {
    var code = '<div class="entry-meta">' + date[1] + "</div>";
    switch (type) {
        case "featured1":
        case "featured2":
        case "featured3":
        case "featured4":
        case "featured5":
        case "featured6":
            switch (i) {
                case 0:
                    switch (type) {
                        case "featured1":
                        case "featured2":
                        case "featured4":
                            code = '<div class="entry-meta">' + author + date[0] + "</div>";
                            break;
                    }
                    break;
                case 1:
                    switch (type) {
                        case "featured4":
                            code = '<div class="entry-meta">' + author + date[0] + "</div>";
                            break;
                    }
                    break;
            }
            break;
    }
    return code;
}
function getAjax($this, type, num, label) {
    switch (type) {
        case "msimple":
        case "megatabs":
        case "breaking":
        case "featured1":
        case "featured2":
        case "featured3":
        case "featured4":
        case "featured5":
        case "featured6":
        case "block1":
        case "block2":
        case "col-left":
        case "col-right":
        case "grid1":
        case "grid2":
        case "carousel":
        case "videos":
        case "list":
        case "related":
            if (label == undefined) {
                label = "geterror404";
            }
            var furl = getFeedUrl(type, num, label);
            $.ajax({
                url: furl,
                type: "GET",
                dataType: "json",
                cache: true,
                beforeSend: function (data) {
                    switch (type) {
                        case "featured1":
                        case "featured2":
                        case "featured3":
                        case "featured4":
                        case "featured5":
                        case "featured6":
                            $this
                                .html(beforeLoader())
                                .parent()
                                .addClass("show-ify show-" + type + "");
                            break;
                        case "block1":
                        case "block2":
                        case "grid1":
                        case "grid2":
                        case "videos":
                        case "carousel":
                        case "related":
                            $this.html(beforeLoader()).parent().addClass("show-ify");
                            break;
                        case "col-left":
                            $this.html(beforeLoader()).parent().addClass("column-left block-column show-ify");
                            break;
                        case "col-right":
                            $this.html(beforeLoader()).parent().addClass("column-right block-column show-ify");
                            break;
                        case "list":
                            $this.html(beforeLoader());
                            break;
                    }
                },
                success: function (data) {
                    var html = "";
                    switch (type) {
                        case "msimple":
                        case "megatabs":
                            html = '<ul class="mega-widget">';
                            break;
                        case "breaking":
                            html = '<div class="breaking-news">';
                            break;
                        case "featured1":
                        case "featured2":
                        case "featured3":
                        case "featured4":
                        case "featured5":
                        case "featured6":
                            html = '<div class="featured-grid ' + type + '">';
                            break;
                        case "block1":
                            html = '<div class="block-posts-1">';
                            break;
                        case "block2":
                            html = '<div class="block-posts-2 total-' + num + '">';
                            break;
                        case "col-left":
                        case "col-right":
                            html = '<div class="column-posts">';
                            break;
                        case "grid1":
                            html = '<div class="grid-posts-1 total-' + num + '">';
                            break;
                        case "grid2":
                            html = '<div class="grid-posts-2">';
                            break;
                        case "carousel":
                            html = '<div class="block-carousel">';
                            break;
                        case "videos":
                            html = '<div class="block-videos total-' + num + '">';
                            break;
                        case "list":
                            html = '<div class="custom-widget">';
                            break;
                        case "related":
                            html = '<div class="related-posts total-' + num + '">';
                            break;
                    }
                    var entry = data.feed.entry;
                    if (entry != undefined) {
                        for (var i = 0, feed = entry; i < feed.length; i++) {
                            var link = getPostLink(feed, i),
                                title = getPostTitle(feed, i, link),
                                image = getPostImage(feed, i, link),
                                author = getPostAuthor(feed, i),
                                date = getPostDate(feed, i),
                                tag = getPostLabel(feed, i),
                                feat_meta = getFeatMeta(type, i, author, date);
                            var content = "";
                            switch (type) {
                                case "msimple":
                                case "megatabs":
                                    content +=
                                        '<article class="mega-item"><div class="mega-content"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/></a><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        date[1] +
                                        "</div></div></article>";
                                    break;
                                case "breaking":
                                    content += '<article class="breaking-item"><h2 class="entry-title"><a href="' + link + '">' + title + "</a></h2></article>";
                                    break;
                                case "featured1":
                                case "featured2":
                                case "featured3":
                                case "featured4":
                                case "featured5":
                                case "featured6":
                                    switch (i) {
                                        case 0:
                                            content +=
                                                '<article class="featured-item item-' +
                                                i +
                                                '"><div class="featured-item-inner"><a class="entry-image-link before-mask" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-info">' +
                                                tag +
                                                '<h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                "</a></h2>" +
                                                feat_meta +
                                                '</div></div></article><div class="featured-scroll">';
                                            break;
                                        default:
                                            content +=
                                                '<article class="featured-item item-' +
                                                i +
                                                '"><div class="featured-item-inner"><a class="entry-image-link before-mask" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-info">' +
                                                tag +
                                                '<h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                "</a></h2>" +
                                                feat_meta +
                                                "</div></div></article>";
                                            break;
                                    }
                                    break;
                                case "block1":
                                    switch (i) {
                                        case 0:
                                            content +=
                                                '<article class="block-item item-' +
                                                i +
                                                '"><div class="block-inner"><a class="entry-image-link before-mask" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-info">' +
                                                tag +
                                                '<h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                author +
                                                date[0] +
                                                "</div></div></div></article>";
                                            break;
                                        default:
                                            content +=
                                                '<article class="block-item item-' +
                                                i +
                                                '"><a class="entry-image-link" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-header"><h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                date[1] +
                                                "</div></div></article>";
                                            break;
                                    }
                                    break;
                                case "block2":
                                    switch (i) {
                                        case 0:
                                            content +=
                                                '<article class="block-item item-' +
                                                i +
                                                '"><div class="block-inner"><a class="entry-image-link before-mask" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-info">' +
                                                tag +
                                                '<h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                author +
                                                date[0] +
                                                '</div></div></div></article><div class="block-grid">';
                                            break;
                                        default:
                                            content +=
                                                '<article class="block-item item-' +
                                                i +
                                                '"><div class="entry-image"><a class="entry-image-link" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a></div><h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                date[1] +
                                                "</div></article>";
                                            break;
                                    }
                                    break;
                                case "col-left":
                                case "col-right":
                                    switch (i) {
                                        case 0:
                                            content +=
                                                '<article class="column-item item-' +
                                                i +
                                                '"><div class="column-inner"><a class="entry-image-link before-mask" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-info">' +
                                                tag +
                                                '<h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                author +
                                                date[0] +
                                                "</div></div></div></article>";
                                            break;
                                        default:
                                            content +=
                                                '<article class="column-item item-' +
                                                i +
                                                '"><a class="entry-image-link" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-header"><h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                date[1] +
                                                "</div></div></article>";
                                            break;
                                    }
                                    break;
                                case "grid1":
                                    content +=
                                        '<article class="grid-item item-' +
                                        i +
                                        '"><div class="entry-image"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/></a></div><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        date[1] +
                                        "</div></article>";
                                    break;
                                case "grid2":
                                    content +=
                                        '<article class="grid-item item-' +
                                        i +
                                        '"><div class="entry-image"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/></a>' +
                                        tag +
                                        '</div><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        author +
                                        date[0] +
                                        "</div></article>";
                                    break;
                                case "carousel":
                                    content +=
                                        '<article class="carousel-item item-' +
                                        i +
                                        '"><div class="entry-image"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/></a></div><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        date[1] +
                                        "</div></article>";
                                    break;
                                case "videos":
                                    content +=
                                        '<article class="videos-item item-' +
                                        i +
                                        '"><div class="entry-image"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/><span class="video-icon"/></a></div><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        date[1] +
                                        "</div></article>";
                                    break;
                                case "list":
                                    switch (label) {
                                        case "comments":
                                            var code = getPostComments(feed, i, link);
                                            content += code;
                                            break;
                                        default:
                                            content +=
                                                '<article class="custom-item item-' +
                                                i +
                                                '"><a class="entry-image-link" href="' +
                                                link +
                                                '"><span class="entry-thumb" data-image="' +
                                                image +
                                                '"/></a><div class="entry-header"><h2 class="entry-title"><a href="' +
                                                link +
                                                '">' +
                                                title +
                                                '</a></h2><div class="entry-meta">' +
                                                date[1] +
                                                "</div></div></article>";
                                            break;
                                    }
                                    break;
                                case "related":
                                    content +=
                                        '<article class="related-item item-' +
                                        i +
                                        '"><div class="related-item-inner"><div class="entry-image"><a class="entry-image-link" href="' +
                                        link +
                                        '"><span class="entry-thumb" data-image="' +
                                        image +
                                        '"/></a></div><h2 class="entry-title"><a href="' +
                                        link +
                                        '">' +
                                        title +
                                        '</a></h2><div class="entry-meta">' +
                                        date[1] +
                                        "</div></div></article>";
                                    break;
                            }
                            html += content;
                        }
                    } else {
                        switch (type) {
                            case "msimple":
                            case "megatabs":
                                html = '<ul class="mega-widget">' + msgError() + "</ul>";
                                break;
                            default:
                                html = msgError();
                                break;
                        }
                    }
                    switch (type) {
                        case "msimple":
                            html += "</ul>";
                            $this.append(html).addClass("msimple");
                            $this.find("a:first").attr("href", function ($this, href) {
                                switch (label) {
                                    case "recent":
                                        href = href.replace(href, "/search");
                                        break;
                                    default:
                                        href = href.replace(href, "/search/label/" + label);
                                        break;
                                }
                                return href;
                            });
                            break;
                        case "breaking":
                            html += "</div></ul>";
                            $this.html(html).parent().addClass("show-ify");
                            var $slider = $this.find(".breaking-news");
                            $slider.owlCarousel({
                                items: 1,
                                animateOut: "fadeOutRight",
                                animateIn: "fadeInRight",
                                smartSpeed: 0,
                                rtl: slideRTL,
                                nav: true,
                                navText: ["", ""],
                                loop: true,
                                autoplay: true,
                                autoplayHoverPause: true,
                                dots: false,
                                mouseDrag: false,
                                touchDrag: false,
                                freeDrag: false,
                                pullDrag: false,
                            });
                            break;
                        case "featured1":
                        case "featured2":
                        case "featured3":
                        case "featured4":
                        case "featured5":
                        case "featured6":
                            html += "</div></div>";
                            $this.html(html);
                            break;
                        case "block1":
                        case "grid1":
                        case "grid2":
                        case "col-left":
                        case "col-right":
                        case "videos":
                            html += "</div>";
                            $this.html(html);
                            break;
                        case "block2":
                            html += "</div></div>";
                            $this.html(html);
                            break;
                        case "carousel":
                            html += "</div>";
                            $this.html(html);
                            var $slider = $this.find(".block-carousel");
                            $slider.owlCarousel({
                                items: 3,
                                slideBy: 3,
                                margin: 20,
                                smartSpeed: 150,
                                rtl: slideRTL,
                                nav: true,
                                navText: ["", ""],
                                loop: true,
                                autoHeight: true,
                                autoplay: false,
                                dots: false,
                                responsive: { 0: { items: 1 }, 541: { items: 2 }, 681: { items: 3 } },
                            });
                            break;
                        default:
                            html += "</div>";
                            $this.html(html);
                            break;
                    }
                    $this.find("span.entry-thumb").lazyify();
                },
                error: function () {
                    switch (type) {
                        case "msimple":
                            $this.append("<ul>" + msgServerError() + "</ul>");
                            break;
                        case "breaking":
                            $this.html(msgServerError()).parent().addClass("show-ify");
                            break;
                        default:
                            $this.html(msgServerError());
                            break;
                    }
                },
            });
    }
}
function ajaxMega($this, type, num, label, text) {
    if (text.match("getmega")) {
        if (type == "msimple" || type == "megatabs" || type == "mtabs") {
            return getAjax($this, type, num, label);
        } else {
            $this.addClass("has-sub mega-menu").append('<ul class="mega-widget">' + msgError() + "</ul>");
        }
    }
}
function ajaxBreaking($this, type, num, label, text) {
    if (text.match("getbreaking")) {
        if (type == "breaking") {
            return getAjax($this, type, num, label);
        } else {
            $this.html(msgError()).parent().addClass("show-ify");
        }
    }
}
function ajaxFeatured($this, type, num, label, text) {
    if (text.match("getfeatured")) {
        if (type == "featured1" || type == "featured2" || type == "featured3" || type == "featured4" || type == "featured5" || type == "featured6") {
            return getAjax($this, type, num, label);
        } else {
            $this.html(beforeLoader()).parent().addClass("show-ify");
            setTimeout(function () {
                $this.html(msgError());
            }, 500);
        }
    }
}
function ajaxBlock($this, type, num, label, text) {
    if (text.match("getblock")) {
        if (type == "block1" || type == "block2" || type == "col-left" || type == "col-right" || type == "grid1" || type == "grid2" || type == "carousel" || type == "videos") {
            var moreText = showMoreText,
                text = "";
            if (moreText != "") {
                text = moreText;
            } else {
                text = messages.showMore;
            }
            $this
                .parent()
                .find(".widget-title")
                .append('<a class="more" href="/search/label/' + label + '">' + text + "</a>");
            return getAjax($this, type, num, label);
        } else {
            $this.html(msgError()).parent().addClass("show-ify");
        }
    }
}
function ajaxWidget($this, type, num, label, text) {
    if (text.match("getwidget")) {
        if (type == "list") {
            return getAjax($this, type, num, label);
        } else {
            $this.html(msgError());
        }
    }
}
function ajaxRelated($this, type, num, label, text) {
    if (text.match("getrelated")) {
        return getAjax($this, type, num, label);
    }
}
$(".comments-title h3.title").each(function () {
    var $t = $(this),
        $tx = $t.text().trim(),
        $c = $t.attr("count").trim(),
        $m = $t.attr("message").trim(),
        $sp = $tx.split("/"),
        $r = "";
    if ($c == 0) {
        $r = $sp[1];
    } else {
        if ($sp[2] == undefined) {
            $r = $sp[0] + " " + $m;
        } else {
            $r = $sp[0] + " " + $sp[2];
        }
    }
    $t.text($r);
});

$(".article-blog-post-comments").each(function () {
    var $this = $(this),
        system = commentsSystem,
        facebook = '<div class="fb-comments" data-width="100%" data-href="' + disqus_blogger_current_url + '" order_by="time" data-numposts="5"></div>',
        sClass = "comments-system-" + system;

    var tabs = $("#comments-tabs");
    var comment_block = $(".article-blog-post-comments");
    function multiComment(value) {
        switch (value) {
            case "blogger":
                tabs.append("<div class='widget' id='blogger' tab-ify='Blogger'/>");
                comment_block.find("#def_comments").appendTo(tabs.find("#blogger"));
                tabs.find("div.comments-title").remove();
                tabs.find("#def_comments").addClass("mt-0");
                break;

            case "disqus":
                tabs.append("<div class='widget' id='disqus' tab-ify='Disqus'/>");
                tabs.find("#disqus").append("<div id='comments'/>").append("<div id='disqus_thread'/>");
                (function () {
                    var bloggerjs = document.createElement("script");
                    bloggerjs.type = "text/javascript";
                    bloggerjs.async = true;
                    bloggerjs.src = "//" + disqus_shortname + ".disqus.com/blogger_item.js";
                    (document.getElementsByTagName("head")[0] || document.getElementsByTagName("body")[0]).appendChild(bloggerjs);
                })();
                break;

            case "facebook":
                tabs.append("<div class='widget' id='facebook' tab-ify='Facebook'/>");
                tabs.find("#facebook").append(facebook);
                tabs.find("#facebook").find(".fb-comments").addClass("mt-0");
                break;

            case "hide":
                comment_block.hide();
                break;

            default:
                comment_block.addClass("comments-system-default").show();
                $(".entry-meta .entry-comments-link").addClass("show");
                break;
        }
    }

    if (system.length > 1) {
        system.forEach(multiComment);
        comment_block.find("#def_comments").remove();
        $('<div class="title-wrap related-title mt-5 mb-0"><h3>Post a Comment</h3></div>').insertBefore("#comments-tabs");
        $("#comments-tabs").tabify();
        if ($("#comments-tabs .widget").length > 1) {
            $("#comments-tabs")
                .addClass("tabs-" + $("#comments-tabs .widget").length)
                .show();
        }
    } else {
        switch (system[0]) {
            case "blogger":
                $this.addClass(sClass).show();
                $(".entry-meta .entry-comments-link").addClass("show");
                break;
            case "disqus":
                $this.addClass(sClass).show();
                $this.addClass(sClass).show().find("#def_comments").append("<div id='comments'/>");
                $this.addClass(sClass).show().find(".footer").remove();
                break;
            case "facebook":
                $this.addClass(sClass).show().find("#def_comments").find(".footer").replaceWith(facebook);
                break;
            case "hide":
                $this.hide();
                break;
            default:
                $this.addClass("comments-system-default").show();
                $(".entry-meta .entry-comments-link").addClass("show");
                break;
        }
    }

    var $r = $this.find(".comments .toplevel-thread > ol > .comment .comment-actions .comment-reply"),
        $c = $this.find(".comments .toplevel-thread > #top-continue");
    $r.on("click", function () {
        $c.show();
    });
    $c.on("click", function () {
        $c.hide();
    });
});

$(function () {
    $(".index-post .entry-image-link .entry-thumb, .PopularPosts .entry-image-link .entry-thumb, .FeaturedPost .entry-image-link .entry-thumb,.about-author .author-avatar").lazyify();
    $(".mobile-logo").each(function () {
        var $t = $(this),
            $l = $("#main-logo .header-widget a").clone();
        $l.find("#h1-tag").remove();
        $l.appendTo($t);
    });
    $("#mobile-menu").each(function () {
        var $t = $(this),
            $m = $("#article-main-menu-nav").clone();
        $m.attr("id", "main-mobile-nav");
        $m.find(".getMega, .mega-widget, .mega-tab").remove();
        $m.find("li.mega-tabs .complex-tabs").each(function () {
            var $eq = $(this);
            $eq.replaceWith($eq.find("> ul.select-tab").attr("class", "sub-menu m-sub"));
        });
        $m.find(".mega-menu:not(.mega-tabs) > a").each(function () {
            var $t = $(this),
                $h = $t.attr("href").trim(),
                $tlc = $h.toLowerCase();
            if ($tlc.match("getmega")) {
                $t.append('<div class="getMega">' + $h + "</div>");
                $t.find(".getMega").shortcode({
                    getMega: function ($u, $l) {
                        $l = this.options.label;
                        $l == "recent" ? ($u = "/search") : ($u = "/search/label/" + $l);
                        $t.attr("href", $u);
                    },
                });
            }
        });
        $m.find(".mega-tabs ul li > a").each(function () {
            var $a = $(this),
                $l = $a.text().trim();
            $a.attr("href", "/search/label/" + $l);
        });
        $m.appendTo($t);
        $(".show-mobile-menu, .hide-mobile-menu, .overlay").on("click", function () {
            $("body").toggleClass("nav-active");
        });
        $(".mobile-menu .has-sub").append('<div class="submenu-toggle"/>');
        $(".mobile-menu .mega-menu").find(".submenu-toggle").remove();
        $(".mobile-menu .mega-tabs").append('<div class="submenu-toggle"/>');
        $(".mobile-menu ul li .submenu-toggle").on("click", function ($this) {
            if ($(this).parent().hasClass("has-sub")) {
                $this.preventDefault();
                if (!$(this).parent().hasClass("show")) {
                    $(this).parent().addClass("show").children(".m-sub").slideToggle(170);
                } else {
                    $(this).parent().removeClass("show").find("> .m-sub").slideToggle(170);
                }
            }
        });
    });
    $(".social-mobile").each(function () {
        var $t = $(this),
            $l = $("#about-section .social-footer").clone();
        $l.removeClass("social-color");
        $l.appendTo($t);
    });
    $(".navbar-wrap .navbar").each(function () {
        var $this = $(this);
        if (fixedMenu == true) {
            if ($this.length > 0) {
                var t = $(document).scrollTop(),
                    w = $this.offset().top,
                    s = $this.height(),
                    h = w + s;
                $(window).scroll(function () {
                    var n = $(document).scrollTop(),
                        f = $("#footer-wrapper").offset().top,
                        m = f - s;
                    if (n < m) {
                        if (n > h) {
                            $this.addClass("is-fixed");
                        } else if (n <= 0) {
                            $this.removeClass("is-fixed");
                        }
                        if (n > t) {
                            $this.removeClass("show");
                        } else {
                            $this.addClass("show");
                        }
                        t = $(document).scrollTop();
                    }
                });
            }
        }
    });
    $("#main-wrapper, #sidebar-wrapper").each(function () {
        if (fixedSidebar == true) {
            $(this).theiaStickySidebar({ additionalMarginTop: 30, additionalMarginBottom: 30 });
        }
    });
    $(".back-top").each(function () {
        var $this = $(this);
        $(window).on("scroll", function () {
            $(this).scrollTop() >= 100 ? $this.fadeIn(250) : $this.fadeOut(250);
        }),
            $this.click(function () {
                $("html, body").animate({ scrollTop: 0 }, 500);
            });
    });
    $("p.comment-content").each(function () {
        var $t = $(this);
        $t.replaceText(/(https:\/\/\S+(\.png|\.jpeg|\.jpg|\.gif))/g, '<img src="$1"/>');
        $t.replaceText(
            /(?:https:\/\/)?(?:www\.)?(?:youtube\.com)\/(?:watch\?v=)?(.+)/g,
            '<iframe id="youtube" width="100%" height="330" src="https://www.youtube.com/embed/$1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>'
        );
    });
    $("#load-more-link").each(function () {
        var $this = $(this),
            $loadLink = $this.data("load");
        if ($loadLink) {
            $("#load-more-link").show();
        }
        $("#load-more-link").on("click", function (a) {
            $("#load-more-link").hide();
            $.ajax({
                url: $loadLink,
                success: function (data) {
                    var $p = $(data).find(".blog-posts");
                    $p.find(".index-post").addClass("post-animated post-fadeInUp");
                    $(".blog-posts").append($p.html());
                    $loadLink = $(data).find("#load-more-link").data("load");
                    if ($loadLink) {
                        $("#load-more-link").show();
                    } else {
                        $("#load-more-link").hide();
                        $("#blog-pager .no-more").addClass("show");
                    }
                    $(".index-post .entry-image-link .entry-thumb").lazyify();
                },
                beforeSend: function () {
                    $("#blog-pager .loading").show();
                },
                complete: function () {
                    $("#blog-pager .loading").hide();
                },
            });
            a.preventDefault();
        });
    });
});

$(function () {
    $(".index-post .entry-image-link .entry-thumb, .PopularPosts .entry-image-link .entry-thumb, .FeaturedPost .entry-image-link .entry-thumb,.about-author .author-avatar").lazyify();
    $(".mobile-logo").each(function () {
        var $t = $(this),
            $l = $("#main-logo .header-widget a").clone();
        $l.find("#h1-tag").remove();
        $l.appendTo($t);
    });
    $("#mobile-menu").each(function () {
        var $t = $(this),
            $m = $("#article-main-menu-nav").clone();
        $m.attr("id", "main-mobile-nav");
        $m.find(".getMega, .mega-widget, .mega-tab").remove();
        $m.find("li.mega-tabs .complex-tabs").each(function () {
            var $eq = $(this);
            $eq.replaceWith($eq.find("> ul.select-tab").attr("class", "sub-menu m-sub"));
        });
        $m.find(".mega-menu:not(.mega-tabs) > a").each(function () {
            var $t = $(this),
                $h = $t.attr("href").trim(),
                $tlc = $h.toLowerCase();
            if ($tlc.match("getmega")) {
                $t.append('<div class="getMega">' + $h + "</div>");
                $t.find(".getMega").shortcode({
                    getMega: function ($u, $l) {
                        $l = this.options.label;
                        $l == "recent" ? ($u = "/search") : ($u = "/search/label/" + $l);
                        $t.attr("href", $u);
                    },
                });
            }
        });
        $m.find(".mega-tabs ul li > a").each(function () {
            var $a = $(this),
                $l = $a.text().trim();
            $a.attr("href", "/search/label/" + $l);
        });
        $m.appendTo($t);
        $(".show-mobile-menu, .hide-mobile-menu, .overlay").on("click", function () {
            $("body").toggleClass("nav-active");
        });
        $(".mobile-menu .has-sub").append('<div class="submenu-toggle"/>');
        $(".mobile-menu .mega-menu").find(".submenu-toggle").remove();
        $(".mobile-menu .mega-tabs").append('<div class="submenu-toggle"/>');
        $(".mobile-menu ul li .submenu-toggle").on("click", function ($this) {
            if ($(this).parent().hasClass("has-sub")) {
                $this.preventDefault();
                if (!$(this).parent().hasClass("show")) {
                    $(this).parent().addClass("show").children(".m-sub").slideToggle(170);
                } else {
                    $(this).parent().removeClass("show").find("> .m-sub").slideToggle(170);
                }
            }
        });
    });
    $(".social-mobile").each(function () {
        var $t = $(this),
            $l = $("#about-section .social-footer").clone();
        $l.removeClass("social-color");
        $l.appendTo($t);
    });
    $(".navbar-wrap .navbar").each(function () {
        var $this = $(this);
        if (fixedMenu == true) {
            if ($this.length > 0) {
                var t = $(document).scrollTop(),
                    w = $this.offset().top,
                    s = $this.height(),
                    h = w + s;
                $(window).scroll(function () {
                    var n = $(document).scrollTop(),
                        f = $("#footer-wrapper").offset().top,
                        m = f - s;
                    if (n < m) {
                        if (n > h) {
                            $this.addClass("is-fixed");
                        } else if (n <= 0) {
                            $this.removeClass("is-fixed");
                        }
                        if (n > t) {
                            $this.removeClass("show");
                        } else {
                            $this.addClass("show");
                        }
                        t = $(document).scrollTop();
                    }
                });
            }
        }
    });
    $("#main-wrapper, #sidebar-wrapper").each(function () {
        if (fixedSidebar == true) {
            $(this).theiaStickySidebar({ additionalMarginTop: 30, additionalMarginBottom: 30 });
        }
    });
    $(".back-top").each(function () {
        var $this = $(this);
        $(window).on("scroll", function () {
            $(this).scrollTop() >= 100 ? $this.fadeIn(250) : $this.fadeOut(250);
        }),
            $this.click(function () {
                $("html, body").animate({ scrollTop: 0 }, 500);
            });
    });
    $("p.comment-content").each(function () {
        var $t = $(this);
        $t.replaceText(/(https:\/\/\S+(\.png|\.jpeg|\.jpg|\.gif))/g, '<img src="$1"/>');
        $t.replaceText(
            /(?:https:\/\/)?(?:www\.)?(?:youtube\.com)\/(?:watch\?v=)?(.+)/g,
            '<iframe id="youtube" width="100%" height="330" src="https://www.youtube.com/embed/$1" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>'
        );
    });
    $("#load-more-link").each(function () {
        var $this = $(this),
            $loadLink = $this.data("load");
        if ($loadLink) {
            $("#load-more-link").show();
        }
        $("#load-more-link").on("click", function (a) {
            $("#load-more-link").hide();
            $.ajax({
                url: $loadLink,
                success: function (data) {
                    var $p = $(data).find(".blog-posts");
                    $p.find(".index-post").addClass("post-animated post-fadeInUp");
                    $(".blog-posts").append($p.html());
                    $loadLink = $(data).find("#load-more-link").data("load");
                    if ($loadLink) {
                        $("#load-more-link").show();
                    } else {
                        $("#load-more-link").hide();
                        $("#blog-pager .no-more").addClass("show");
                    }
                    $(".index-post .entry-image-link .entry-thumb").lazyify();
                },
                beforeSend: function () {
                    $("#blog-pager .loading").show();
                },
                complete: function () {
                    $("#blog-pager .loading").hide();
                },
            });
            a.preventDefault();
        });
    });
});
