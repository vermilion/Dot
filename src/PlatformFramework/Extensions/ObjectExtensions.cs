using System.ComponentModel;

namespace PlatformFramework.Shared.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the provided <paramref name="value"/> to a strongly typed value object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An instance of <typeparamref name="T"/> representing the provided <paramref name="value"/>.</returns>
        public static T FromString<T>(this string value)
        {
            if (value == null)
            {
                return default;
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
        }

        /// <summary>
        /// Converts the provided <paramref name="value"/> to a strongly typed value object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An instance of <typeparamref name="T"/> representing the provided <paramref name="value"/>.</returns>
        public static T To<T>(this string value)
        {
            return FromString<T>(value);
        }
    }
}