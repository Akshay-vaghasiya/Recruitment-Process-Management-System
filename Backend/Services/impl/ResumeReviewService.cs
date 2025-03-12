using Backend.Dtos;
using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;

namespace Backend.Services.impl
{
    public class ResumeReviewService : IResumeReviewService
    {
        private readonly IResumeReviewRepository _repository;
        private readonly IJobPositionRepository _positionRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobApplicationRepository _applicationRepository;
        private readonly IApplicationStatusRepository _applicationStatusRepository;

        public ResumeReviewService(IResumeReviewRepository repository, IJobPositionRepository jobPositionRepository, ICandidateRepository candidateRepository, IJobApplicationRepository applicationRepository, IApplicationStatusRepository applicationStatusRepository)
        {
            _repository = repository;
            _candidateRepository = candidateRepository;
            _positionRepository = jobPositionRepository;
            _applicationRepository = applicationRepository;
            _applicationStatusRepository = applicationStatusRepository;
        }

        public async Task<ResumeReview> AddResumeReview(int userId, ResumeReview resumeReview)
        { 
         
            Candidate? candidate = await _candidateRepository.GetCandidateById(resumeReview.FkCandidateId);
            if (candidate == null) throw new Exception("candidate not exist in system");

            JobPosition? jobPosition = await _positionRepository.GetJobPositionByIdAsync(resumeReview.FkJobPositionId);
            if (jobPosition == null) throw new Exception("job position not exist in system");

            JobApplication? jobApplication = await _applicationRepository.GetJobApplicationByJobAndCandidate(jobPosition.PkJobPositionId, candidate.PkCandidateId);
            if (jobApplication == null) throw new Exception("application not found in system.");

            ApplicationStatus? applicationStatus = await _applicationStatusRepository.GetApplicationStatusByNameAsync("SCREENED");

            jobApplication.FkStatus = applicationStatus;

            await _applicationRepository.UpdateJobAppliction(jobApplication);

            if (jobPosition.FkReviewerId != userId)
            {
                throw new Exception("Your can't review this job candidate resume");
            }

            ResumeReview? resumeReview1 = await _repository.GetResumeReviewByJobAndCandidate(resumeReview.FkJobPositionId, resumeReview.FkCandidateId);
            if (resumeReview1 != null) throw new Exception("candidate resume already reviewed for this job position");

            return await _repository.AddResumeReview(resumeReview);

        }

        public async Task<List<ResumeReview>> GetResumeReviews()
        {
            return await _repository.GetResumeReviewsAsync();
        }

        public async Task<ResumeReview> UpdateResumeReview(int userId, int resumeReviewId, ResumeReview resumeReview)
        {
            ResumeReview? resumeReview1 = await _repository.GetResumeReviewById(resumeReviewId);
            if (resumeReview1 == null) throw new Exception("resume review not exist in system");

            Candidate? candidate = await _candidateRepository.GetCandidateById(resumeReview.FkCandidateId);
            if (candidate == null) throw new Exception("candidate not exist in system");

            JobPosition? jobPosition = await _positionRepository.GetJobPositionByIdAsync(resumeReview.FkJobPositionId);
            if (jobPosition == null) throw new Exception("job position not exist in system");

            if (jobPosition.FkReviewerId != userId)
            {
                throw new Exception("Your can't review this candidate resume");
            }

            resumeReview1.Comments = resumeReview.Comments;

            return await _repository.UpdateResumeReview(resumeReview1);
        }

        public async Task DeleteResumeReview(int id)
        {
            await _repository.DeleteResumeReview(id);
        }
    }
}
