using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentScheduling.Helpers
{
    public class UserTypes
    {
        public static readonly string Admin = "Admin";
        public static readonly string Patient = "Patient";
        public static readonly string Doctor = "Doctor";

        public static readonly List<SelectListItem> UTypes = new List<SelectListItem>()
        {
            new SelectListItem(UserTypes.Admin, UserTypes.Admin),
            new SelectListItem(UserTypes.Doctor, UserTypes.Doctor),
            new SelectListItem(UserTypes.Patient, UserTypes.Patient)
        };
    }
}
