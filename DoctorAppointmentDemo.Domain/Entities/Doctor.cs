using MyDoctorAppointment.Domain.Enums;

namespace MyDoctorAppointment.Domain.Entities
{
    public class Doctor : UserBase//доктора
    {
        public DoctorTypes DoctorType { get; set; }

        public byte Experience { get; set; }

        public decimal Salary { get; set; }
    }
}
