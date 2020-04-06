using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Aliera.Utilities.Encoding
{
    public class HtmlEncodeContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Override string properties
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            foreach (var property in properties.Where(p => p.PropertyType == typeof(string)))
            {
                var propertyInfo = type.GetProperty(property.UnderlyingName);
                if (propertyInfo != null)
                {
                    property.ValueProvider = new HtmlEncodingValueProvider(propertyInfo);
                }
            }

            return properties;
        }

        /// <summary>
        /// Class Html Encoding Value Provider
        /// </summary>
        protected class HtmlEncodingValueProvider : IValueProvider
        {
            private readonly PropertyInfo _targetProperty;

            public HtmlEncodingValueProvider(PropertyInfo targetProperty)
            {
                this._targetProperty = targetProperty;
            }

            public void SetValue(object target, object value)
            {
                var s = value as string;
                if (s != null)
                {
                    var encodedString = HtmlEncoder.Default.Encode(s);
                    _targetProperty.SetValue(target, encodedString);
                }
                else
                {
                    _targetProperty.SetValue(target, value);
                }
            }

            public object GetValue(object target)
            {
                return _targetProperty.GetValue(target);
            }
        }
    }
}
