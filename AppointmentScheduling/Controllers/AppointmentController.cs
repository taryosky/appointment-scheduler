using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppointmentScheduling.Services;

using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduling.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            var doctors = _appointmentService.GetDoctors();
            ViewBag.Doctors = doctors;
            return View();
        }
    }
}
