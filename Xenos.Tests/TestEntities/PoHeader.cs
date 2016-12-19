namespace Xenos.Tests.TestEntities
{
    public class PoHeader
    {
        public string Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public PoLine[] LineItems { get; set; }
    }
}
