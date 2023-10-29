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

        public Task<Course> Delete(Course course)
        {
            throw new NotImplementedException();
        }
        public async Task<Course> GetById(Guid CourseId)
        {
            var course = _dbContext.Course.Find(CourseId);

            if (course == null)
            {
                return null;
            }

            return course;

        }

        public async Task<List<Course>> GetByUserId(Guid driverId)
        {
            var courses = _dbContext.Course.Where(c => c.UserVehicle.UserId == driverId).ToList();

            if (courses == null || courses.Count == 0)
            {
                return null;
            }
                
            return courses;

        }

        public Task<List<Course>> GetByUserId(Guid driver, DateTime StartSearch, DateTime EndSearch)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> Register(Course course)
        {

            UserVehicle userVehicle = _dbContext.UserVehicles.FirstOrDefault(uv => uv.UserVehicleId == course.UserVehicleId);

            course.UserVehicle = userVehicle;

            var setCourse = _dbContext.Course.Add(course);

            if (setCourse == null) // || vehicle == null)
            {
                return null;
            }

            _dbContext.SaveChanges();

            return course;
        }

        public Task<Course> Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
