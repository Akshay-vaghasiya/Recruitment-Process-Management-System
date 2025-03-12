using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;

namespace Backend.Services.impl
{
    public class JobStatusService : IJobStatusService
    {
        private readonly IJobStatusRepository _repository;

        public JobStatusService(IJobStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<JobStatus>> GetAllJobStatusesAsync()
        {
            return await _repository.GetAllJobStatusesAsync();
        }

        public async Task<JobStatus?> GetJobStatusByIdAsync(int id)
        {
            return await _repository.GetJobStatusByIdAsync(id);
        }

        public async Task<JobStatus?> AddJobStatusAsync(JobStatus? jobStatus)
        {
            JobStatus? jobStatus1 = await _repository.GetJobStatusByNameAsync(jobStatus?.Name);
            if (jobStatus1 != null) throw new Exception("jobstatus already exist!");

            return await _repository.AddJobStatusAsync(jobStatus);
        }

        public async Task<JobStatus> UpdateJobStatusAsync(int id,JobStatus jobStatus)
        {
            JobStatus? jobStatus1 = await _repository.GetJobStatusByIdAsync(id);
            if (jobStatus1 == null) throw new Exception("status with given id is not exist!");

            JobStatus? jobStatus2 = await _repository.GetJobStatusByNameAsync(jobStatus.Name);
            if (jobStatus2 != null) throw new Exception("status already exist!");

            jobStatus1.Name = jobStatus.Name;

            return await _repository.UpdateJobStatusAsync(jobStatus1);
        }

        public async Task DeleteJobStatusAsync(int id)
        {
            JobStatus? jobStatus = await _repository.GetJobStatusByIdAsync(id);
            if (jobStatus == null) throw new Exception("job status not found by given id");

            if(jobStatus.JobPositions.Count() > 0)
            {
                throw new Exception("This job status is assign to many jobs.");
            }

            await _repository.DeleteJobStatusAsync(id);
        }
    }
}
