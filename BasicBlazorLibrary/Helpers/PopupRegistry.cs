using BasicBlazorLibrary.Components.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBlazorLibrary.Helpers;

public sealed class PopupRegistry
{
    private readonly HashSet<PopupBase> _popups = new();

    public void Register(PopupBase popup) => _popups.Add(popup);

    public void Unregister(PopupBase popup) => _popups.Remove(popup);

    public async Task CloseAllAsync()
    {
        // Snapshot so we don’t break enumeration if popups unregister during closing
        var snapshot = _popups.ToArray();

        foreach (var popup in snapshot)
        {
            try
            {
                await popup.RequestCloseAsync();
            }
            catch
            {
                // ignore: popup may already be disposed or mid-render
            }
        }
    }
}
