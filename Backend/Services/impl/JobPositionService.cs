﻿using Backend.Dtos;
using Backend.Models;
using Backend.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Services.impl
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository _repository;
        private readonly IJobStatusRepository _jobStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IJobSkillRepository _jobSkillRepository;
        private readonly ICandidateNotificationRepository _candidateNotificationRepository;

        public JobPositionService(IJobPositionRepository repository, IJobStatusRepository jobStatusRepository, IUserRepository userRepository, IJobSkillRepository jobSkillRepository, ISkillRepository skillRepository, ICandidateNotificationRepository candidateNotificationRepository)
        {
            _repository = repository;
            _jobStatusRepository = jobStatusRepository;
            _userRepository = userRepository;
            _skillRepository = skillRepository;
            _jobSkillRepository = jobSkillRepository;
            _candidateNotificationRepository = candidateNotificationRepository;
        }

        public async Task<List<JobPosition>> GetAllJobPositionsAsync()
        {
            return await _repository.GetAllJobPositionsAsync();
        }

        public async Task<JobPosition?> GetJobPositionByIdAsync(int id)
        {
            return await _repository.GetJobPositionByIdAsync(id);
        }

        public async Task<JobPosition> AddJobPositionAsync(JobPositionDto jobPositionDto)
        {
            JobPosition jobPosition = new JobPosition();
            jobPosition.Description = jobPositionDto.Description;
            jobPosition.Title = jobPositionDto.Title;

            JobStatus? jobStatus = await _jobStatusRepository.GetJobStatusByNameAsync("OPEN");
            if (jobStatus == null) throw new Exception("job status with "+ jobPositionDto.FkStatusId +" id not exist");

            jobPosition.FkStatus = jobStatus;
            
            User? user = await _userRepository.GetUserById(jobPositionDto.FkReviewerId);
            if (user == null) throw new Exception("user not found for reviewer");

            int flag = 0;

            if (user.UserRoles != null) {
                foreach (var userRole in user.UserRoles)
                {
                    if(userRole?.FkRole?.Name != null &&  userRole.FkRole.Name.Equals("REVIEWER"))
                    {
                        flag = 1;
                        break;
                    }
                }
            }

            if(flag == 0)
            {
                throw new Exception("User not have reviewer role");
            }

            jobPosition.FkReviewerId = user.PkUserId;

            JobPosition jobPosition1 = await _repository.AddJobPositionAsync(jobPosition);

            if (jobPositionDto.Skills != null) {


                foreach (var skillid in jobPositionDto.Skills) { 
                    
                    Skill? skill = await _skillRepository.GetSkillByIdAsync(skillid);

                    if (skill == null) throw new Exception("skill id " + skillid + " not exist");
                    
                    JobSkill jobSkill = new JobSkill();
                    jobSkill.FkSkillId = skillid;
                    jobSkill.FkJobPosition = jobPosition1;
                    jobSkill.IsRequired = false;

                    await _jobSkillRepository.AddJobSkillAsync(jobSkill);   
                }
            }

            if (jobPositionDto.RequireSkills != null) {
                foreach (var skillid in jobPositionDto.RequireSkills)
                {

                    Skill? skill = await _skillRepository.GetSkillByIdAsync(skillid);

                    if (skill == null) throw new Exception("skill id " + skillid + " not exist");

                    JobSkill jobSkill = new JobSkill();
                    jobSkill.FkSkillId = skillid;
                    jobSkill.FkJobPosition = jobPosition1;
                    jobSkill.IsRequired = true;

                    await _jobSkillRepository.AddJobSkillAsync(jobSkill);
                }
            }

            return jobPosition1;
        }

        public async Task<JobPosition> UpdateJobPositionAsync(int id, JobPositionDto jobPositionDto)
        {
            JobPosition? jobPosition =  await _repository.GetJobPositionByIdAsync(id);
            if (jobPosition == null) throw new Exception("job position not found!");

            JobStatus? jobStatus = await _jobStatusRepository.GetJobStatusByIdAsync(jobPositionDto?.FkStatusId);
            if (jobStatus == null) throw new Exception("job status not exist in system");

            jobPosition.UpdatedAt = DateTime.UtcNow;
            jobPosition.Title = jobPositionDto?.Title ?? jobPosition.Title;
            jobPosition.Description = jobPositionDto?.Description ?? jobPosition.Description;
            jobPosition.FkReviewerId = jobPositionDto?.FkReviewerId ?? jobPosition.FkReviewerId;
            jobPosition.ClosureReason = jobPositionDto?.ClosureReason ?? jobPosition.ClosureReason;


            if(jobPositionDto?.FkStatusId != null &&  jobPositionDto?.FkStatusId != jobPosition.FkStatusId)
            {
                jobPosition.FkStatusId = jobPositionDto?.FkStatusId ?? jobPosition.FkStatusId;

                foreach(var application in jobPosition.JobApplications)
                {
                    CandidateNotification candidateNotification = new CandidateNotification();
                    candidateNotification.FkCandidateId = application.FkCandidateId;
                    candidateNotification.Message = $"{jobPosition.Title} job position status is change and it was {jobStatus.Name}";
                    candidateNotification.IsRead = false;

                    await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);
                }
            }

            if(jobPositionDto?.FkSelectedCandidateId != null && jobPosition.FkSelectedCandidateId != jobPositionDto.FkSelectedCandidateId)
            {
                CandidateNotification candidateNotification = new CandidateNotification();
                candidateNotification.FkCandidateId = jobPositionDto.FkSelectedCandidateId;
                candidateNotification.Message = $"Congratulations, You are selected for {jobPosition.Title} job position";
                candidateNotification.IsRead = false;

                await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);

                CandidateNotification candidateNotification1 = new CandidateNotification();
                candidateNotification1.FkCandidateId = jobPosition.FkSelectedCandidateId;
                candidateNotification1.Message = $"Sorry, You are not selected for {jobPosition.Title} job position";
                candidateNotification1.IsRead = false;

                await _candidateNotificationRepository.AddCandidateNotification(candidateNotification1);

                jobPosition.FkSelectedCandidateId = jobPositionDto?.FkSelectedCandidateId ?? jobPosition.FkSelectedCandidateId;
            }
            
            if(jobPositionDto?.JoiningDate != null && jobPositionDto.JoiningDate != jobPosition.JoiningDate)
            {
                CandidateNotification candidateNotification = new CandidateNotification();
                candidateNotification.FkCandidateId = jobPositionDto.FkSelectedCandidateId;
                candidateNotification.Message = $"You can join us on {jobPositionDto.JoiningDate.ToString()} and offer letter will be send via email";
                candidateNotification.IsRead = false;

                await _candidateNotificationRepository.AddCandidateNotification(candidateNotification);
                jobPosition.JoiningDate = jobPositionDto.JoiningDate;
            }

            if(jobPositionDto?.RequireSkills != null && jobPositionDto.Skills != null)
            {
                foreach (var skill in jobPosition.JobSkills) { 
                    
                    bool inrequier = jobPositionDto.RequireSkills.Any(rs => rs == skill.FkSkillId);
                    bool notrequier = jobPositionDto.Skills.Any(rs => rs == skill.FkSkillId);

                    if(inrequier==false && notrequier==false)
                    {
                        await DeleteJobSkill(skill.PkJobSkillId);
                    }
                }

                foreach (var skillId in jobPositionDto.RequireSkills)
                {
                    await AddJobSkill(jobPosition.PkJobPositionId, skillId, 1);
                }

                foreach (var skillId in jobPositionDto.Skills)
                {
                    await AddJobSkill(jobPosition.PkJobPositionId, skillId, 0);
                }
            }
            
            var result = await _repository.UpdateJobPositionAsync(jobPosition);
            
            return result;
            
        }

        public async Task AddJobSkill(int jobid, int skillid, int isRequire)
        {
            Skill? skill = await _skillRepository.GetSkillByIdAsync(skillid);
            if (skill == null) throw new Exception("skill not exist with this id");

            JobPosition? jobPosition = await _repository.GetJobPositionByIdAsync(jobid);
            if (jobPosition == null) throw new Exception("job position not exist with this id");

            JobSkill? jobSkill = await _jobSkillRepository.GetJobSkillByJobAndSkill(jobid, skillid);

            if (jobSkill != null)
            {
                if(jobSkill.IsRequired == false && isRequire == 1)
                {
                    jobSkill.IsRequired = true;
                } else if (jobSkill.IsRequired == true &&  isRequire == 0)
                {
                    jobSkill.IsRequired = false;
                }

                await _jobSkillRepository.UpdateJobSkillAsync(jobSkill);
            } else
            {
                JobSkill jobSkill1 = new JobSkill();
                jobSkill1.FkSkillId = skillid;
                jobSkill1.FkJobPositionId = jobid;
                jobSkill1.IsRequired = isRequire==0?false:true;

                await _jobSkillRepository.AddJobSkillAsync(jobSkill1);
            }

        }

        public async Task DeleteJobSkill(int jobskillid)
        {
            JobSkill? jobskill = await _jobSkillRepository.GetJobSkillByIdAsync(jobskillid);
            if (jobskill == null) throw new Exception("jobskill not exist");

            await _jobSkillRepository.DeleteJobSkillAsync(jobskillid);
        }

        public async Task DeleteJobPositionAsync(int id)
        {
            await _repository.DeleteJobPositionAsync(id);
        }
    }
}
