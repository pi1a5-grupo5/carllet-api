namespace Application.ViewModels.Course
{
    public class NewCourseRequest
    {
        public Guid? OwnerId { get; set; }
        public float CourseLength { get; set; }
        public DateTime CourseEndTime { get; set; }
    }
}
