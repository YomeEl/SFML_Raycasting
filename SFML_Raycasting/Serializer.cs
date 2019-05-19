using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Raycasting
{
    static class Serializer
    {
        public static void Serialize<T>(T target, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream f = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(f, target);
                f.Close();
            }
        }

        public static void Deserialize<T>(out T target, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream f = new FileStream(filename, FileMode.Open))
            {
                target = (T)formatter.Deserialize(f);
                f.Close();
            }
        }
    }
}
