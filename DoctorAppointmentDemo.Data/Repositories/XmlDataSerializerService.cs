using DocumentFormat.OpenXml.Bibliography;
using MyDoctorAppointment.Data.Interfaces;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public class XmlDataSerializerService
    {
        public static Tsource Deserialize<Tsource>(string path)
        {
            XmlSerializer serializer = new (typeof(Tsource));

            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (Tsource)serializer.Deserialize(stream)!;
            }
        }

        public void Serialize<Tsource>(string path, Tsource data)
        {
            XmlSerializer formatter = new (typeof(Tsource));

            using (FileStream fs = new (path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
            }
        }
    }
}