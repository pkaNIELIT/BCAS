// ASP.NET WebForms client-side functions — fallback for WebResource.axd failures

// Required by WebForm_AutoFocus in ASP.NET's ScriptResource.axd — true for all non-IE browsers
if (typeof __nonMSDOMBrowser === 'undefined') {
    var __nonMSDOMBrowser = (window.navigator.appName.toLowerCase().indexOf('microsoft') == -1);
}
if (typeof WebForm_PostBackOptions === 'undefined') {
    function WebForm_PostBackOptions(eventTarget, eventArgument, validation, validationGroup, actionUrl, trackFocus, clientSubmit) {
        this.eventTarget = eventTarget;
        this.eventArgument = eventArgument;
        this.validation = validation;
        this.validationGroup = validationGroup;
        this.actionUrl = actionUrl;
        this.trackFocus = trackFocus;
        this.clientSubmit = clientSubmit;
    }
}

if (typeof WebForm_DoPostBackWithOptions === 'undefined') {
    function WebForm_DoPostBackWithOptions(options) {
        var validationResult = true;
        if (options.validation) {
            if (typeof (Page_ClientValidate) == 'function') {
                validationResult = Page_ClientValidate(options.validationGroup);
            }
        }
        if (validationResult) {
            if ((typeof (options.actionUrl) != "undefined") && (options.actionUrl != null) && (options.actionUrl.length > 0)) {
                theForm.action = options.actionUrl;
            }
            if (options.trackFocus) {
                var lastFocus = theForm.elements["__LASTFOCUS"];
                if ((typeof (lastFocus) != "undefined") && (lastFocus != null)) {
                    if (typeof (document.activeElement) == "undefined") {
                        lastFocus.value = options.eventTarget;
                    } else {
                        var active = document.activeElement;
                        if ((typeof (active) != "undefined") && (active != null)) {
                            if ((typeof (active.id) != "undefined") && (active.id != null) && (active.id.length > 0)) {
                                lastFocus.value = active.id;
                            } else if (typeof (active.name) != "undefined") {
                                lastFocus.value = active.name;
                            }
                        }
                    }
                }
            }
            if (options.clientSubmit) {
                __doPostBack(options.eventTarget, options.eventArgument);
            }
        }
    }
}

if (typeof WebForm_AutoFocus === 'undefined') {
    function WebForm_AutoFocus(element) {
        var el = (typeof (element) == "string") ? document.getElementById(element) : element;
        if (el != null && typeof (el) != "undefined") {
            if (typeof (el.focus) != "undefined") {
                try { el.focus(); } catch (e) { }
            }
            if (el.type == "text") { el.select(); }
        }
    }
}

if (typeof WebForm_FireDefaultButton === 'undefined') {
    function WebForm_FireDefaultButton(event, target) {
        var element = event.srcElement ? event.srcElement : event.target;
        if (element && element.tagName.toLowerCase() == "input") {
            var keyCode = event.charCode ? event.charCode : event.keyCode;
            if (keyCode == 13) {
                var defaultButton = document.getElementById(target);
                if (defaultButton) {
                    defaultButton.click();
                    event.cancelBubble = true;
                    if (event.stopPropagation) event.stopPropagation();
                    return false;
                }
            }
        }
        return true;
    }
}

if (typeof WebForm_GetScrollX === 'undefined') {
    function WebForm_GetScrollX() {
        if (window.pageXOffset) return window.pageXOffset;
        else if (document.documentElement && document.documentElement.scrollLeft) return document.documentElement.scrollLeft;
        else if (document.body) return document.body.scrollLeft;
        return 0;
    }
    function WebForm_GetScrollY() {
        if (window.pageYOffset) return window.pageYOffset;
        else if (document.documentElement && document.documentElement.scrollTop) return document.documentElement.scrollTop;
        else if (document.body) return document.body.scrollTop;
        return 0;
    }
}

if (typeof WebForm_InitCallback === 'undefined') {
    function WebForm_InitCallback() { }
}



// // ASP.NET WebForms client-side functions — fallback for WebResource.axd failures
// if (typeof WebForm_PostBackOptions === 'undefined') {
//     function WebForm_PostBackOptions(eventTarget, eventArgument, validation, validationGroup, actionUrl, trackFocus, clientSubmit) {
//         this.eventTarget = eventTarget;
//         this.eventArgument = eventArgument;
//         this.validation = validation;
//         this.validationGroup = validationGroup;
//         this.actionUrl = actionUrl;
//         this.trackFocus = trackFocus;
//         this.clientSubmit = clientSubmit;
//     }
// }

// if (typeof WebForm_DoPostBackWithOptions === 'undefined') {
//     function WebForm_DoPostBackWithOptions(options) {
//         var validationResult = true;
//         if (options.validation) {
//             if (typeof (Page_ClientValidate) == 'function') {
//                 validationResult = Page_ClientValidate(options.validationGroup);
//             }
//         }
//         if (validationResult) {
//             if ((typeof (options.actionUrl) != "undefined") && (options.actionUrl != null) && (options.actionUrl.length > 0)) {
//                 theForm.action = options.actionUrl;
//             }
//             if (options.trackFocus) {
//                 var lastFocus = theForm.elements["__LASTFOCUS"];
//                 if ((typeof (lastFocus) != "undefined") && (lastFocus != null)) {
//                     if (typeof (document.activeElement) == "undefined") {
//                         lastFocus.value = options.eventTarget;
//                     } else {
//                         var active = document.activeElement;
//                         if ((typeof (active) != "undefined") && (active != null)) {
//                             if ((typeof (active.id) != "undefined") && (active.id != null) && (active.id.length > 0)) {
//                                 lastFocus.value = active.id;
//                             } else if (typeof (active.name) != "undefined") {
//                                 lastFocus.value = active.name;
//                             }
//                         }
//                     }
//                 }
//             }
//             if (options.clientSubmit) {
//                 __doPostBack(options.eventTarget, options.eventArgument);
//             }
//         }
//     }
// }

// if (typeof WebForm_AutoFocus === 'undefined') {
//     function WebForm_AutoFocus(element) {
//         var el = (typeof (element) == "string") ? document.getElementById(element) : element;
//         if (el != null && typeof (el) != "undefined") {
//             if (typeof (el.focus) != "undefined") {
//                 try { el.focus(); } catch (e) { }
//             }
//             if (el.type == "text") { el.select(); }
//         }
//     }
// }

// if (typeof WebForm_FireDefaultButton === 'undefined') {
//     function WebForm_FireDefaultButton(event, target) {
//         var element = event.srcElement ? event.srcElement : event.target;
//         if (element && element.tagName.toLowerCase() == "input") {
//             var keyCode = event.charCode ? event.charCode : event.keyCode;
//             if (keyCode == 13) {
//                 var defaultButton = document.getElementById(target);
//                 if (defaultButton) {
//                     defaultButton.click();
//                     event.cancelBubble = true;
//                     if (event.stopPropagation) event.stopPropagation();
//                     return false;
//                 }
//             }
//         }
//         return true;
//     }
// }

// if (typeof WebForm_GetScrollX === 'undefined') {
//     function WebForm_GetScrollX() {
//         if (window.pageXOffset) return window.pageXOffset;
//         else if (document.documentElement && document.documentElement.scrollLeft) return document.documentElement.scrollLeft;
//         else if (document.body) return document.body.scrollLeft;
//         return 0;
//     }
//     function WebForm_GetScrollY() {
//         if (window.pageYOffset) return window.pageYOffset;
//         else if (document.documentElement && document.documentElement.scrollTop) return document.documentElement.scrollTop;
//         else if (document.body) return document.body.scrollTop;
//         return 0;
//     }
// }

// if (typeof WebForm_InitCallback === 'undefined') {
//     function WebForm_InitCallback() { }
// }
