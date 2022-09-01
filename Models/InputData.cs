using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aYoUnitConversion.Models
{
    public class ConversionInput
    {
        public string SourceUnit { get; set; }
        public string ConversionUnit { get; set; }
        public double InputValue { get; set; }
    }

    public class ConversionResult : ConversionInput
    {
        public double ConvertedValue { get; set; }
    }
}
