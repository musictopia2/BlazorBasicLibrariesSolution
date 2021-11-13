using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxEnums<TValue>
        where TValue : Enum
    {
        [Parameter]
        public TValue? Value { get; set; }
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public string Placeholder { get; set; } = "";
        [Parameter]
        public bool RemoveFirstValue { get; set; }
        [Parameter]
        public BasicList<TValue> ItemList { get; set; } = new();
        public ElementReference? TextReference => _combo!.GetTextBox;
        private ComboBoxStringList? _combo;
        private string _textDisplay = "";
        private TValue FirstValue { get; set; } = default!;
        private readonly BasicList<string> _list = new();
        protected override void OnInitialized()
        {
            _combo = null;
            BasicList<TValue> list;
            if (ItemList.Count > 0)
            {
                list = ItemList.ToBasicList();
            }
            else
            {
                list = new();
                var firsts = Enum.GetValues(typeof(TValue));
                foreach (var item in firsts)
                {
                    list.Add((TValue) item);
                }
            }
            int x = 0;
            foreach (var item in list)
            {
                if (x == 0 && RemoveFirstValue)
                {
                    BindConverter.TryConvertTo<TValue>(item.ToString(), CultureInfo.CurrentCulture, out var ff);
                    FirstValue = ff!;
                    continue;
                }
                if (item.ToString() != "None" )
                {
                    _list.Add(item.ToString()!);
                }
                else
                {
                    BindConverter.TryConvertTo<TValue>(item.ToString(), CultureInfo.CurrentCulture, out var ff);
                    FirstValue = ff!;
                }
                x++;
            }
            _list.Sort();
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            if (Value!.Equals(FirstValue))
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = Value.ToString();
            }
            base.OnParametersSet();
        }
        private void TextChanged(string value)
        {
            var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
            if (success == false)
            {
                _textDisplay = "";
                return; //don't even change our stuff.
            }
            if (parsedValue!.Equals(FirstValue))
            {
                _textDisplay = "";
                return; 
            }
            ValueChanged.InvokeAsync(parsedValue); //let the child recall this again.
        }
    }
}