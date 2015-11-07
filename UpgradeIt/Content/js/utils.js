// IE10 viewport hack for Surface/desktop Windows 8 bug
(function () {
    'use strict';
    if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
        var msViewportStyle = document.createElement('style')
        msViewportStyle.appendChild(
          document.createTextNode(
            '@-ms-viewport{width:auto!important}'
          )
        )
        document.querySelector('head').appendChild(msViewportStyle)
    }
})();

// Smart resize
(function ($, sr) {
    var debounce = function (func, threshold, execAsap) {
        var timeout;
        return function debounced() {
            var obj = this, args = arguments;
            function delayed() {
                if (!execAsap)
                    func.apply(obj, args);
                timeout = null;
            };
            if (timeout)
                clearTimeout(timeout);
            else if (execAsap)
                func.apply(obj, args);
            timeout = setTimeout(delayed, threshold || 100);
        };
    }
    jQuery.fn[sr] = function (fn) { return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };
})(jQuery, 'smartresize');

// Detect viewport
function detectView() {
    var view;

    function detect() {
        if (Modernizr.mq('(max-width : 767px)') && view != 'mobile') {
            view = 'mobile';
        } else if (Modernizr.mq('(min-width : 768px) and (max-width : 1023px)') && view != 'tablet') {
            view = 'tablet';
        } else if (view != 'desktop') {
            view = 'desktop';
        }
    }
    return view;
}