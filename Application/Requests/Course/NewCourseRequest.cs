using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Course
{
    public class NewCourseRequest
    {
        public Guid? OwnerId { get; set; }
        public float CourseLength { get; set; }
        public DateTime CourseEndTime { get; set; }
    }
}
