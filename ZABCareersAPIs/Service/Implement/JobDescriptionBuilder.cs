using ZABCareersAPIs.Models;

namespace ZABCareersAPIs.Service.Implement
{
    public static class JobDescriptionBuilder
    {
        public static string Build(Job job)
        {
            return $"""
                JOB TITLE: {job.JobTitle}
                REQUIRED EXPERIENCE: {job.Experience}
                EMPLOYMENT STATUS: {job.EmploymentStatus}
                LOCATION: {job.JobLocation}

                JOB DESCRIPTION:
                {job.JobDescription}

                RESPONSIBILITIES:
                {job.Responsibilities}

                EDUCATION AND EXPERIENCE REQUIREMENTS:
                {job.EducationAndExperience}

                OTHER BENEFITS:
                {job.OtherBenefits}
                """;
        }
    }
}
