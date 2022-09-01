using System;
using System.Collections.Generic;

#nullable disable

namespace aYoUnitConversion.Models
{
    public partial class UnitConversionFactor
    {
        public int Id { get; set; }
        public string SourceUnit { get; set; }
        public string ConversionUnit { get; set; }
        public double? ConversionFactor { get; set; }
    }
}
