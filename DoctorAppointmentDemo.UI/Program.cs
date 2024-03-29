﻿using DocumentFormat.OpenXml.Drawing.Diagrams;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    enum Filter
    {
        Xml = 1,
        Json 
    }
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public DoctorAppointment(int filter)
        {
            if (filter == (int)Filter.Json)
            {
                _doctorService = new DoctorService(new DoctorRepositoryJson());
            }
            else if (filter == (int)Filter.Xml)
            {
                _doctorService = new DoctorService(new DoctorRepositoryXml());
            }
            
            
        }
        enum Select
        {
            AddDoctors = 1,
            AddPatients,
            Exit

        }

        public void Menu()
        {
            //while (true)// add Enum for menu items and describe menu
            //{
            //}

            Console.WriteLine("Current doctors list: ");
            var docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }

            Console.WriteLine("Adding doctor: ");

            var newDoctor = new Doctor
            {
                Name = "Vasya",
                Surname = "Petrov",
                Experience = 20,
                DoctorType = Domain.Enums.DoctorTypes.Dentist
            };

            _doctorService.Create(newDoctor);

            Console.WriteLine("Current doctors list: ");
            docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Сhoose what type to save in:");
            Console.WriteLine("1 to save as Xml");
            Console.WriteLine("2 to save as Json");
            int typeToSave = Convert.ToInt32(Console.ReadLine());
            DoctorAppointment doctorAppointment = null;

            while (true)
            {
                if (typeToSave.Equals(1))
                {
                    doctorAppointment = new DoctorAppointment(typeToSave);
                    break;
                }
                else if (typeToSave.Equals(2))
                {
                    doctorAppointment = new DoctorAppointment(typeToSave);
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong choice.");
                    typeToSave = Convert.ToInt32(Console.ReadLine());
                }
            }

            doctorAppointment.Menu();
        }
    }
}