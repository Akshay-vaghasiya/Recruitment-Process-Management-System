using Backend.Dtos;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services.impl
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly ICandidateSkillRepository _candidateskillRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateService(ICandidateRepository repository, ICandidateSkillRepository skillRepository, ISkillRepository skillRepository1)
        {
            _repository = repository;
            _candidateskillRepository = skillRepository;
            _skillRepository = skillRepository1;
        }

        public async Task<Candidate> AddCandidate(CandidateDto candidateDto)
        {
            Candidate? candidate1 = await _repository.GetCandidateByEmail(candidateDto?.Email);
            if (candidate1 != null) throw new Exception("candidate already exist");

            string uploadsFolder = Path.Combine("C:", "Resumes");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = $"{candidateDto?.Resume?.FileName}";
            string fileExtention = Path.GetExtension(fileName);

            if(fileExtention != ".pdf" && fileExtention != ".docx")
            {
                throw new Exception("file extention is not valid.");
            }

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await candidateDto?.Resume?.CopyToAsync(stream);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(candidateDto.Password);
            Candidate candidate = new Candidate();
            candidate.FullName = candidateDto.FullName;
            candidate.Phone = candidateDto.Phone;
            candidate.Email = candidateDto.Email;
            candidate.Password = hashedPassword;
            candidate.ResumeUrl = filePath;
            candidate.YearsOfExperience = candidateDto.YearsOfExperience;

            var result = await _repository.AddCandidate(candidate);
            return result;
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _repository.GetCandidatesAsync();
        }

        public async Task<Candidate?> UpdateCandidate(int id ,CandidateDto candidateDto)
        {
            Candidate? candidate = await _repository.GetCandidateById(id);
            if (candidate == null) throw new Exception("candidate not found with give id");

            candidate.Email = candidateDto.Email ?? candidate.Email;
            candidate.Phone = candidateDto.Phone ?? candidate.Phone;
            candidate.FullName=candidateDto.FullName ?? candidate.FullName;
            candidate.YearsOfExperience = candidateDto.YearsOfExperience ?? candidate.YearsOfExperience;

            if(candidateDto.Resume != null)
            {
                if(candidate.ResumeUrl != null && !candidate.ResumeUrl.Equals(""))
                {
                    File.Delete(candidate.ResumeUrl);
                }

                string uploadsFolder = Path.Combine("C:", "Resumes");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = $"{candidateDto?.Resume?.FileName}";
                string fileExtention = Path.GetExtension(fileName);

                if (fileExtention != ".pdf" && fileExtention != ".docx")
                {
                    throw new Exception("file extention is not valid.");
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await candidateDto?.Resume?.CopyToAsync(stream);
                }

                candidate.ResumeUrl = filePath;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(candidateDto.Password);
            candidate.Password = hashedPassword ?? candidate.Password;
            candidate.UpdatedAt = DateTime.Now;

            var result = await _repository.UpdateCandidate(candidate);

            return result;
        }


        public async Task<CandidateSkill> AddCandidateSkill(int candidateId, int skillId, int yearOfExp)
        {
            Candidate? candidate = await _repository.GetCandidateById(candidateId);
            if (candidate == null) throw new Exception("Candidate not exist");

            Skill? skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skill == null) throw new Exception("Skill not exist");

            CandidateSkill? candidateSkill = await _candidateskillRepository.GetCandidateSkillByCandidateAndSkill(candidateId, skillId);

            if (candidateSkill != null)
            {

                if (candidateSkill.YearsOfExperience != yearOfExp)
                {
                    candidate.YearsOfExperience = yearOfExp;
                    return await _candidateskillRepository.UpdateCandidateSkill(candidateSkill);
                }
                else
                {
                    return candidateSkill;
                }
            }

            CandidateSkill candidateSkill1 = new CandidateSkill();
            candidateSkill1.FkSkillId = skillId;
            candidateSkill1.FkCandidateId = candidateId;
            candidateSkill1.YearsOfExperience = yearOfExp;

            return await _candidateskillRepository.AddCandidateSkill(candidateSkill1);
        }

        public async Task DeleteCandidateSkill(int candidateSkillId)
        {
            CandidateSkill? candidateSkill = await _candidateskillRepository.GetCandidateSkillAsyncById(candidateSkillId);
            if (candidateSkill == null) throw new Exception("candidate skill not exist");

            await _candidateskillRepository.DeleteCandidateSkill(candidateSkill);
        }
    }
}
