using System;

namespace Xenos.Tests.TestEntities
{
    public class PoLine
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public int Quantity { get; set; }
    }
}
