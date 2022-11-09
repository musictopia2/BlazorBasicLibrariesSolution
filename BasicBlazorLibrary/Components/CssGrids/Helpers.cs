﻿using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc;
using aa1 = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BasicBlazorLibrary.Components.CssGrids;
public static class Helpers
{
    public static string RepeatAuto(this int times)
    {
        return RepeatContentLength(times, aa1.Auto);
    }
    public static string RepeatMinimum(this int times)
    {
        return RepeatContentLength(times, aa1.MinContent);
    }
    public static string RepeatMaximum(this int times)
    {
        return RepeatContentLength(times, aa1.MaxContent);
    }
    public static string RepeatContentLength(this int times, string content)
    {
        StrCat cats = new();
        for (int i = 0; i < times; i++)
        {
            cats.AddToString(content, " ");
        }
        string output = cats.GetInfo();
        return output;
    }
    public static string RepeatSpreadOut(this int times)
    {
        return RepeatContentLength(times, "1fr"); //this means spread out.
    }
}