using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using MyDoctorAppointment.Data.Repositories;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepositoryXml<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }
        public XmlDataSerializerService SerializerService { get; set; }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;
            var doctors = GetAll().Append(source).ToList();//добавляет в конец списка новый файл?

            SerializerService.Serialize(Path, doctors);
            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            
            if (GetById(id) is null)
            {
                return false;
            }
            SerializerService.Serialize(Path,GetAll().Where(x => x.Id !=id));

            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            return SerializerService.Deserialize<List<TSource>>(Path);
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;
            SerializerService.Serialize(Path, GetAll().Select(x => x.Id == id ? source : x));
            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected dynamic ReadFromAppSettings() => JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.AppSettingsPath))!;
    }
}