using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static  class DatTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            //var today =  DateTime.Now;
            //var age =  today.Year - dob.Year;
            ////20,11,2005




            //if(dob.Date > today.AddYears(-age)) age --;

            //return age;   
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - dob.Year;

            if (dob > today.AddYears(-age)) age--;

            return age;
        }
    }
}