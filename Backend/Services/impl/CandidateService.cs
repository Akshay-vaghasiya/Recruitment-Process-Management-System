using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dtos;
using Backend.Models;
using Backend.Repository;
using Backend.Repository.impl;
using ExcelDataReader;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.impl
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly ICandidateSkillRepository _candidateskillRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IConfiguration _configuration;

        public CandidateService(ICandidateRepository repository, ICandidateSkillRepository skillRepository, ISkillRepository skillRepository1, IConfiguration configuration)
        {
            _repository = repository;
            _candidateskillRepository = skillRepository;
            _skillRepository = skillRepository1;
            _configuration = configuration;
        }

        public async Task<Candidate> AddCandidate(CandidateDto candidateDto)
        {
            Candidate? candidate1 = await _repository.GetCandidateByEmail(candidateDto?.Email);
            if (candidate1 != null) throw new Exception("candidate already exist");

            string uploadsFolder = Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Resume\\" + candidateDto?.Email;

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = $"{candidateDto?.Resume?.FileName}";
            string fileExtention = Path.GetExtension(fileName);

            if(fileExtention != ".pdf")
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
            candidate.ResumeUrl = "../../public/Resume/" + candidate?.Email + "/" + fileName;
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
                string uploadsFolder = Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Resume\\" + candidate?.Email;

                if (candidate?.ResumeUrl != null && !candidate.ResumeUrl.Equals(""))
                {
                    File.Delete(uploadsFolder + "\\" + candidate?.ResumeUrl?.Substring(candidate.ResumeUrl.LastIndexOf("/") + 1));
                }

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = $"{candidateDto?.Resume?.FileName}";
                string fileExtention = Path.GetExtension(fileName);

                if (fileExtention != ".pdf")
                {
                    throw new Exception("file extention is not valid.");
                }

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await candidateDto?.Resume?.CopyToAsync(stream);
                }

                candidate.ResumeUrl = "../../public/Resume/" + candidate?.Email + "/" + fileName;
            }

            if(candidateDto?.Password != null)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(candidateDto?.Password);
                candidate.Password = hashedPassword ?? candidate.Password;
            }
            candidate.UpdatedAt = DateTime.Now;

            var result = await _repository.UpdateCandidate(candidate);

            return result;
        }

        public async Task DeleteCandidate(int candidateId)
        {
            Candidate? candidate = await _repository.GetCandidateById(candidateId);
            if (candidate == null) throw new Exception("candidate not exist with given id");

            if(candidate.ResumeUrl != null && candidate.ResumeUrl != "")
            {
                Directory.Delete(Directory.GetCurrentDirectory().Replace("Backend", "Frontend") + "\\public\\Resume\\" + candidate?.Email, true);
            }

            await _repository.DeleteCandidate(candidateId);
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

        public async Task<Object> AuthenticateCandidate(LoginDto loginDto)
        {
            var candidate1 = await _repository.GetCandidateByEmail(loginDto.Email);
            if (candidate1 == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, candidate1.Password))
                return null;

            return new { token = await GenerateJwtToken(candidate1), candidate = candidate1 };
        }

        private async Task<string> GenerateJwtToken(Candidate candidate)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, candidate.Email),
            new Claim(ClaimTypes.Role, "CANDIDATE")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task BulkAddCandidate(IFormFile file)
        {
            var candidates = new List<Candidate>();

            using (var stream = file.OpenReadStream())
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool isFirstRow = true;
                    while (reader.Read())
                    {
                        if (isFirstRow)
                        {
                            isFirstRow = false;
                            continue;
                        }

                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(reader.GetString(1));
                        candidates.Add(new Candidate
                        {
                            FullName = reader.GetString(0),
                            Password = hashedPassword,
                            Email = reader.GetString(2),
                            Phone = Convert.ToString(reader.GetValue(3)),
                            YearsOfExperience = Convert.ToInt16(reader.GetValue(4).ToString())
                        });
                    }
                }
            }

            await _repository.BulkAddCandidateAsync(candidates);
        }
    }
}
