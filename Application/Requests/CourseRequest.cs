using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Requests
{
    public class CourseRequest
    { 
        public int VehicleId { get; set; }
        public char type { get; set; }

        public int CourseLength { get; set; }

        public DateTime CourseStartTime { get; set; }
        public DateTime CourseEndTime { get; set; }
    }
}
