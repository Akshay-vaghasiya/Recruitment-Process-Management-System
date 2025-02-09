using Backend.Models;

namespace Backend.Repository
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> AddJobApplication(JobApplication jobApplication);
        Task<List<JobApplication>> GetApplicationsAsync();
        Task<JobApplication?> GetJobApplicationById(int id);
        Task<JobApplication> UpdateJobAppliction(JobApplication jobApplication);
        Task DeleteJobAppliction(int id);

        Task<JobApplication?> GetJobApplicationByJobAndCandidate(int? jobId, int? candidateId);
    }
}
