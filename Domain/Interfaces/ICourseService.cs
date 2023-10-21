using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<Course> Register(Course course);
        Task<Course> Update(Course course);
        Task<Course> Delete(Course course);
        Task<List<Course>> GetByUserId(Guid driver);
        Task<List<Course>> GetByUserId(Guid driver, DateTime StartSearch, DateTime EndSearch);

    }
}
