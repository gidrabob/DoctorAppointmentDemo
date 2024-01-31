using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;
using System.Xml;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepositoryXml<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            XmlSerializer serializer = new XmlSerializer(typeof(TSource));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, source);
            }
            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;



            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }
            var xml = File.ReadAllText(Path);
            if (string.IsNullOrWhiteSpace(xml))
            {
                File.WriteAllText(Path, "[]");
                xml = "[]";
            }
            XmlSerializer formatter = new XmlSerializer(typeof(TSource[]));
            TSource[]? newpeople;
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                newpeople = formatter.Deserialize(fs) as TSource[];
            }
            return newpeople;
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.AppSettingsPath))!;
    }
}