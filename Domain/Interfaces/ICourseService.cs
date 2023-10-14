using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<Course> Register(Course course);

        Task<List<Course>> GetByUserId(Guid driver);
    }
}
