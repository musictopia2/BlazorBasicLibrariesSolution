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
        // Get the initial inner width and height of the window
        const width = window.innerWidth;
        const height = window.innerHeight;

        // Get the screen's physical size
        const screenWidth = window.screen.width;
        const screenHeight = window.screen.height;

        // Check if zoom is applied by comparing screen width and inner width
        const zoomFactor = screenWidth / width;

        // If zoom is applied, adjust the width and height accordingly
        const adjustedWidth = width * zoomFactor;
        const adjustedHeight = height * zoomFactor;

        // Return the adjusted width and height
        return {
            width: adjustedWidth,
            height: adjustedHeight
        };
    }
}