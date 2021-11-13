export function starty(dotnetRef, element) {
	if (element == undefined || element == null) {
		return;
	}
	var isScrolling;
	element.addEventListener('scroll', function () {
		dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);

		//clearTimeout(isScrolling);
		//isScrolling = setTimeout(function () {

		//	dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);
		//}, 66);
	}, false);
}
var timer = null;
export function startx(dotnetRef, element) {
	if (element == undefined || element == null) {
		return;
	}
	var isScrolling;
	element.addEventListener('touchmove', function (event) {
		event.preventDefault();
		//figure out what else i can do.
		//good news is scrolling not allowed anymore.
		//bad news is not sure what else i can do.
	}, false);


	//$('window').on('touchmove', function (event) {
	//	//Prevent the window from being scrolled.
	//	event.preventDefault();

	//	//Do something like call window.scrollTo to mimic the scrolling
	//	//request the user made.
	//});


	element.addEventListener('scroll', function () {

		//dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollLeft);


		if (timer !== null) {
			clearTimeout(timer);
		}
		timer = setTimeout(function () {
			console.log(element.scrollLeft);
			dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollLeft);
		}, 150);


		//console.log(element.scrollLeft);
		

		//clearTimeout(isScrolling);
		//isScrolling = setTimeout(function () {

		//	dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);
		//}, 66);
	}, false);
}
