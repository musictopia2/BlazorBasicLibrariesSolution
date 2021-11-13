using BasicBlazorLibrary.Components.AutoCompleteHelpers;
using CommonBasicLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System;
namespace BasicBlazorLibrary.Components.ComboTextboxes
{
    public partial class ComboBoxGenericList<TValue>
    {
        [Parameter]
        public BasicList<TValue>? ItemList { get; set; }
        [Parameter]
        public TValue? Value { get; set; }
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter]
        public Func<TValue, string>? RetrieveValue { get; set; }
        [Parameter]
        public EventCallback ComboEnterPressed { get; set; }
        [Parameter]
        public AutoCompleteStyleModel Style { get; set; } = new AutoCompleteStyleModel();
        [Parameter]
        public bool Virtualized { get; set; } = false;
        [Parameter]
        public string Placeholder { get; set; } = "";
        public ElementReference? TextReference => _combo!.GetTextBox;
        private ComboBoxStringList? _combo;
        private string _textDisplay = "";
        private readonly BasicList<string> _list = new ();
        protected override void OnInitialized()
        {
            _combo = null;
        }
        protected override void OnParametersSet()
        {
            _list.Clear();
            ItemList!.ForEach(item =>
            {
                _list.Add(RetrieveValue!.Invoke(item));
            });
            int index = ItemList.IndexOf(Value!);
            if (index == -1)
            {
                _textDisplay = "";
            }
            else
            {
                _textDisplay = _list[index];
            }
            base.OnParametersSet();
        }
        private void TextChanged(string value)
        {
            var index = _list.IndexOf(value);
            if (index == -1)
            {
                _textDisplay = "";
                ValueChanged.InvokeAsync(); //try to send null because not selected anymore.
                return; //because not there.
            }
            ValueChanged.InvokeAsync(ItemList![index]); //hopefully this simple (?)
        }
    }
}