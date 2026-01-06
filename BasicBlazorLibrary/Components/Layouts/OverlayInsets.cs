using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBlazorLibrary.Components.Layouts;
public sealed class OverlayInsets
{
    public int TopPx { get; set; }     // nav height
    public int BottomPx { get; set; }  // optional future (iphone bar, etc.)
}