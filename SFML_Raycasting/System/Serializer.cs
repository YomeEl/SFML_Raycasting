using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Raycasting
{
    /// <summary>
    /// Serializer class allows us to save/load objects to/from filesystem
    /// </summary>
    static class Serializer
    {
        /// <summary>
        /// Serializes object
        /// </summary>
        /// <typeparam name="T">Target object type</typeparam>
        /// <param name="target">Targer object to serialize</param>
        /// <param name="filename">Destination path</param>
        public static void Serialize<T>(T target, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(filename, FileMode.Create);
            formatter.Serialize(f, target);
            f.Close();
        }

        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <typeparam name="T">Target object type</typeparam>
        /// <param name="target">Targer object to deserialize</param>
        /// <param name="filename">Destination path</param>
        public static void Deserialize<T>(out T target, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(filename, FileMode.Open);
            target = (T)formatter.Deserialize(f);
            f.Close();
        }
    }
}
