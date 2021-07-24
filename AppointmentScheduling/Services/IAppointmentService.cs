﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppointmentScheduling.Models.ViewModels;

namespace AppointmentScheduling.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctors();
        public List<PatientVM> GetPatients();

    }
}
