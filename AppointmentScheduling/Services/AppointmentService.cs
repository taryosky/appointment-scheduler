using System;
using System.Collections.Generic;
using System.Linq;

using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;

namespace AppointmentScheduling.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<DoctorVM> GetDoctors()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x=>x.Name == Helpers.UserTypes.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM { Id = user.Id, Name = user.Name }
                           ).ToList();
            return doctors;
        }

        public List<PatientVM> GetPatients()
        {
            var patients = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helpers.UserTypes.Patient) on userRoles.RoleId equals roles.Id
                           select new PatientVM { Id = user.Id, Name = user.Name }
                           ).ToList();
            return patients;
        }
    }
}
