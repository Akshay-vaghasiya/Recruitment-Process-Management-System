using Backend.Models;

namespace Backend.Services
{
    public interface IJobApplicationService
    {
        Task<JobApplication> AddJobApplication(int jobPositionId, int candidateId);
        Task<List<JobApplication>> GetJobApplicationsAsync();
        Task<JobApplication> UpdateJobApplicationStatus(int jobApplicationId, int jobApplicationStatusId);
        Task DeleteJobApplication(int jobApplicationId);
    }
}
