using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;

namespace Services
{
    public class CourseService : ICourseService
    {
        private readonly CarlletDbContext _dbContext;

        public CourseService(CarlletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Course>> GetByUserId(Guid driverId)
        {
            var courses = _dbContext.Course.Where(u => u.OwnerId == driverId).ToList();

            if (courses == null || courses.Count == 0)
            {
                return null;
            }

            return courses;

        }

        public async Task<Course> Register(Course course)
        {

            User user = _dbContext.User.FirstOrDefault(u => u.Id == course.OwnerId);

            course.Owner = user;

            var setCourse = _dbContext.Course.Add(course);

            if (setCourse == null) // || vehicle == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            return course;
        }


    }
}
