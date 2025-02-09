export class ResizeListener {
    throttleResizeHandlerId = -1;
    dotnet;
    resizeHandler = () => {
        this.dotnet.invokeMethodAsync(
            'RaiseOnResized', {
            height: window.innerHeight,
            width: window.innerWidth
        });
    }
    listenForResize(dotnetRef) {
        this.dotnet = dotnetRef;
        window.addEventListener("resize", this.throttleResizeHandler);
        this.resizeHandler();
    }
    throttleResizeHandler = () => {
        clearTimeout(this.throttleResizeHandlerId);
        this.throttleResizeHandlerId = window.setTimeout(this.resizeHandler, 300);
    }
    cancelListener() {
        window.removeEventListener("resize", this.throttleResizeHandler);
    }
    getBrowserWindowSize() {
        // Get the physical screen dimensions
        const screenWidth = window.screen.width;
        const screenHeight = window.screen.height;

        // Get the device pixel ratio (to account for higher DPI screens)
        const devicePixelRatio = window.devicePixelRatio || 1;

        // Calculate adjusted width and height (in actual pixels, not zoomed)
        const actualWidth = window.innerWidth * devicePixelRatio;
        const actualHeight = window.innerHeight * devicePixelRatio;

        // Return the adjusted width and height (in real pixels, accounting for DPI and zoom)
        return {
            height: actualHeight,
            width: actualWidth
        };
    }
}