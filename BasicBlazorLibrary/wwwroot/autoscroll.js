var wasrippedoff = true;
function start() {
    document.onkeydown = function (evt) {
        if (evt == null) evt = event; if (testKeyCode(evt, 38)) //up arrow     
        {
            cancelKeyEvent(evt); //up arrow can't do default action which is scroll
            return false;
        }
        if (evt == null) evt = event; if (testKeyCode(evt, 40)) //down arrow     
        {
            cancelKeyEvent(evt); //down arrow can't do default action which is scroll.
            return false;
        }
        function cancelKeyEvent(evt) {
            if (window.createPopup) evt.keyCode = 0;
            else evt.preventDefault();
        }
        function testKeyCode(evt, intKeyCode) {
            if (window.createPopup) return evt.keyCode == intKeyCode;
            else return evt.which == intKeyCode;
        }
    }
    wasrippedoff = false; //no longer ripped off because now the arrow up and arrow down won't do default functions which is scroll up/down (since i am handling it via code)
}
export function scrolltoelement(element) {
    if (element == undefined || element == null) {
        return;
    }
    if (wasrippedoff) {
        start();
    }
    element.scrollIntoView();
}
export function setscroll(element, pixels) {
    element.scrollTop = pixels;
}
export function getscrollleftvalue(element) {
    var value = element.scrollLeft;
    console.log(value);
    return element.scrollLeft; //hopefully this simple
}