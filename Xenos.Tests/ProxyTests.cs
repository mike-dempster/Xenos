using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xenos.Proxy;
using Xenos.Tests.TestEntities;

namespace Xenos.Tests
{
    [TestClass]
    public class ProxyTests
    {
        [TestMethod]
        public void TestProxyGenerator()
        {
            try
            {
                var moduleBuilder = ProxyModuleBuilder.GetInstance<XmlSerializerContextTests>();
                var pg = ProxyTypeImplementer.GetInstance<PoLine>(moduleBuilder);
                var pi = new ProxyPropertyInfo
                {
                    PropertyName = "Tax",
                    BasePropertyInfo = typeof(PoLine).GetProperty("Tax"),
                    ProxyPropertyType = typeof(string),
                    ConversionToProxyDelegate = ((MethodCallExpression) ((Expression<Func<decimal, string>>) (d => NullableDecimalToString(d))).Body).Method,
                    ConversionFromProxyDelegate = ((MethodCallExpression) ((Expression<Func<string, decimal?>>) (s => ParseToNullableDecimal(s))).Body).Method
                };
                pg.AddPropertyInfo(pi);
                var t = pg.BuildProxyType();
                var i = Activator.CreateInstance(t);
                var tp = t.GetProperty("Tax");

                var ov = tp.GetValue(i);
                // tp.SetValue(i, new Nullable<decimal>(1107.0m));
                tp.SetValue(i, "1107.0");
                // var op0 = t.GetMethod("op_Explicit", new[] { t });
                var op0 = t.GetMethod("op_Implicit", new[] { t });
                // var x = op0.Invoke(null, new[] { i });
                // var op1 = t.GetMethod("op_Explicit", new[] { typeof(PoLine) });
                var op1 = t.GetMethod("op_Implicit", new[] { typeof(PoLine) });
                var x = new PoLine { Tax = 33.0m };
                var y = op1.Invoke(null, new[] { x });
                var z = new PoLine[1];

                var bi = (PoLine) i;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static decimal? ParseToNullableDecimal(string s)
        {
            if (null == s)
            {
                return null;
            }

            return new Nullable<decimal>(decimal.Parse(s));
        }

        public static string NullableDecimalToString(Nullable<decimal> d)
        {
            if (null == d)
            {
                return null;
            }

            return d.ToString();
        }
    }
}
