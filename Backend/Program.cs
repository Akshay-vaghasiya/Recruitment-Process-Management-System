using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.Services;
using Backend.Services.impl;
using Backend.Repository;
using Backend.Repository.impl;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<RecruitmentProcessManagementSystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IJobPositionRepository, JobPositionRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();
builder.Services.AddScoped<IJobStatusRepository, JobStatusRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateSkillRepository, CandidateSkillRepository>();
builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddScoped<IDocumentStatusRepository, DocumentStatusRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IApplicationStatusRepository, ApplicationStatusRepository>();
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
builder.Services.AddScoped<IResumeReviewRepository, ResumeReviewRepository>();
builder.Services.AddScoped<IInterviewStatusRepository, InterviewStatusRepository>();
builder.Services.AddScoped<IInterviewRoundRepository, InterviewRoundRepository>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewPanelRepository, InterviewPanelRepository>();
builder.Services.AddScoped<IReportTypeRepository, ReportTypeRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IInterviewFeedbackRepository, InterviewFeedbackRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ICandidateNotificationRepository, CandidateNotificationRepository>();




builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<IJobStatusService, JobStatusService>();
builder.Services.AddScoped<IJobSkillService, JobSkillService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IDocumentStatusService, DocumentStatusService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IApplicationStatusService, ApplicationStatusService>();
builder.Services.AddScoped<IJobApplicationService,  JobApplicationService>();
builder.Services.AddScoped<IResumeReviewService, ResumeReviewService>();
builder.Services.AddScoped<IInterviewStatusService, InterviewStatusService>();
builder.Services.AddScoped<IInterviewRoundService, InterviewRoundService>();
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IInterviewPanelService, InterviewPanelService>();
builder.Services.AddScoped<IReportTypeService, ReportTypeService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IInterviewFeedbackService, InterviewFeedbackService>();
builder.Services.AddScoped<INotificationService, NotificationService>();    
builder.Services.AddScoped<ICandidateNotificationService, CandidateNotificationService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();