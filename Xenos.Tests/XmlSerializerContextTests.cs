using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Xenos.Configuration;
using Xenos.Tests.TestEntities;

namespace Xenos.Tests
{
    [TestClass]
    public class XmlSerializerContextTests : XmlSerializerContext
    {
        [TestMethod]
        public void TestSerializer()
        {
            try
            {
                var obj = new PoHeader
                {
                    LineItems = new[]
                    {
                        new PoLine
                        {
                            Quantity = 1,
                            Price = 50,
                            Tax = null
                        },
                        new PoLine
                        {
                            Quantity = 2,
                            Price = 25,
                            Tax = 33.0m
                        }
                    }
                };

                using (var tw = new StringWriter())
                {
                    this.Serialize(tw, obj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected override void BuildSerializerContext(SerializerConfigurationContext configurationContext)
        {
            configurationContext.Entity<PoHeader>().HasArray(e => e.LineItems).WithElementName("lines").WithArrayItemName("line");
            configurationContext.Entity<PoLine>().HasAttribute(e => e.Tax).WithName("tax");
            configurationContext.Entity<PoLine>().HasAttribute(e => e.Quantity).WithName("qty");
        }
    }
}
