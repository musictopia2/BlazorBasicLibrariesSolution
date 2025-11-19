using System.Drawing; //not common enough.
namespace BasicBlazorLibrary.Helpers;
public static class PositionExtensions
{
    private readonly static BasicList<string> _units = new() { "rem", "em", "px", "vw", "vh", "vmin", "vmax", "%" };
    extension(string size)
    {
        public string UnitOfMeasureString()
        {
            foreach (var unit in _units)
            {
                if (size.EndsWith(unit))
                {
                    return unit;
                }
            }
            throw new CustomBasicException("No unit measure found");
        }
        public string GetLocation(float percents)
        {
            string unit = size.UnitOfMeasureString();
            var subs = size.SizeUsed(unit);
            var total = subs * percents;
            return $"{total}{unit}";
        }
        public float SizeUsed()
        {
            string unit = size.UnitOfMeasureString();
            return SizeUsed(unit);
        }
        private float SizeUsed(string unit)
        {
            string results = size.Replace(unit, "");
            bool rets = float.TryParse(results, out float output);
            if (rets == false)
            {
                throw new CustomBasicException($"Size {size} incorrect format");
            }
            return output;
        }
        public string ContainerSize(float howMany) //to be more flexible
        {
            string unit = size.UnitOfMeasureString();
            float used = size.SizeUsed(unit);
            float total = used * howMany;
            return $"{total}{unit}";
        }
        public string ElementLocation(int proposedLocation)
        {
            string unit = size.UnitOfMeasureString();
            return $"{proposedLocation}{unit}";
        }
        public string ContainerWidth(int columns, SizeF elementRatio)
        {
            string unit = size.UnitOfMeasureString();
            float height = size.SizeUsed(unit);
            float singleWidth = GetWidth(height, elementRatio);
            float totalWidth = singleWidth * columns;
            return $"{totalWidth}{unit}";
        }
        public string ContainerHeight(int rows, SizeF elementRatio)
        {
            string unit = size.UnitOfMeasureString();
            float width = size.SizeUsed(unit);
            float singleHeight = GetHeight(width, elementRatio);
            float totalHeight = singleHeight * rows;
            return $"{totalHeight}{unit}";
        }
    }
    //did not do extension style so this is fine this time.
    private static float GetWidth(float height, SizeF ratio)
    {
        float output = height * ratio.Width / ratio.Height;
        return output;
    }
    private static float GetHeight(float width, SizeF ratio)
    {
        float output = width * ratio.Height / ratio.Width;
        return output;
    }
}