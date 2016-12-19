using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Xenos.Conversion
{
    /// <summary>
    /// Helper methods for working with delegate methods and properties.
    /// </summary>
    internal static class ReflectionHelpers
    {
        /// <summary>
        /// Gets the MethodInfo of the given method delegate.
        /// </summary>
        /// <typeparam name="T">Type of the delegate's input parameter.</typeparam>
        /// <typeparam name="TReturn">Return type of the delegate method.</typeparam>
        /// <param name="methodCallExpression">Delegate method to get the MethodInfo from.</param>
        /// <returns>MethodInfo for the given delegate method.</returns>
        internal static MethodInfo GetDelegateMethodInfo<T, TReturn>(Func<T, TReturn> methodCallExpression)
        {
            return methodCallExpression.Method;
        }

        /// <summary>
        /// Gets the propertyInfo for a property on an object.
        /// </summary>
        /// <typeparam name="TIn">Type of the class that declares the property.</typeparam>
        /// <typeparam name="TOut">Type of the property to get.</typeparam>
        /// <param name="propertyExpression">Property access expression.</param>
        /// <returns>PropertyInfo of the property selected by the expression.</returns>
        internal static PropertyInfo GetPropertyInfo<TIn, TOut>(Expression<Func<TIn, TOut>> propertyExpression)
        {
            var memberAccessExpression = propertyExpression.Body as MemberExpression;

            if (null == memberAccessExpression)
            {
                throw new Exception("Expression must select a property.");
            }

            var propertyInfo = memberAccessExpression.Member as PropertyInfo;

            if (null == propertyInfo)
            {
                throw new Exception($"Selected member '{memberAccessExpression.Member.Name}' is not a property.");
            }

            return propertyInfo;
        }
    }
}
