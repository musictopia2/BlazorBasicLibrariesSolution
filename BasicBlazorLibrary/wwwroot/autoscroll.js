export function scrolltoelement(element) {
    if (element == undefined || element == null) {
        return;
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