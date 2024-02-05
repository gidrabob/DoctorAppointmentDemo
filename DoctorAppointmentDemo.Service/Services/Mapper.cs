using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Service.ViewModels;

namespace MyDoctorAppointment.Service
{

    public static class Mapper
    {
        public static DoctorViewModel ConvertTo(this Doctor doctor)
        {
            if (doctor == null)
            {
                return null;
            }

            string doctorType;

            switch (doctor.DoctorType)
            {
                case DoctorTypes.Dentist:
                    doctorType = "Dentist";
                    break;
                case DoctorTypes.Dermatologist:
                    doctorType = "Dermatologist";
                    break;
                case DoctorTypes.FamilyDoctor:
                    doctorType = "FamilyDoctor";
                    break;
                case DoctorTypes.Paramedic:
                    doctorType = "Paramedic";
                    break;
                default:
                    doctorType = "Unknown";
                    break;
            }

            return new DoctorViewModel()
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                Phone = doctor.Phone,
                Email = doctor.Email,
                DoctorType = doctorType,
                Experience = doctor.Experience,
                Salary = doctor.Salary
            };
        }
    }
}