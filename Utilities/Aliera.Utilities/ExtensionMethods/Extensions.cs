using Newtonsoft.Json;

namespace Aliera.Utilities.ExtensionMethods
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to create a deep copy of a type by serializing and deserializing an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clonedObject"></param>
        /// <param name="inputObjectToClone"></param>
        /// <returns></returns>
        public static T Clone<T>(this T clonedObject, T inputObjectToClone)
        {
            var oldJsonObject = JsonConvert.SerializeObject(inputObjectToClone);
            clonedObject = JsonConvert.DeserializeObject<T>(oldJsonObject);

            return clonedObject;
        }
    }
}
