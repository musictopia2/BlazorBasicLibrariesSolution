let closeHandler = null;
export function detectclose(dotnetRef) {
    closeHandler = () => {
        dotnetRef.invokeMethodAsync("OnBrowserClosing");
    };
    window.addEventListener("beforeunload", closeHandler);
}

export function removeclose() {
    if (closeHandler) {
        window.removeEventListener("beforeunload", closeHandler);
        closeHandler = null;
    }
}