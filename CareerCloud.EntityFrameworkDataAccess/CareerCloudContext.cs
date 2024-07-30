using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        protected readonly string _connStr = string.Empty;
        public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }

        public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }

        public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }

        public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }

        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }

        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }

        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }

        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }

        public DbSet<CompanyJobPoco> CompanyJob{get; set; }

        public DbSet<CompanyJobSkillPoco> CompanyJobSkill{get; set; }

        public DbSet<CompanyLocationPoco> CompanyLocation{get; set; } 

        public DbSet<CompanyProfilePoco> CompanyProfile{get; set; }

        public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }

        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog{get; set; } 

        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRole{get; set; }

        public DbSet<SecurityRolePoco> SecurityRole{get; set; } 

        public DbSet<SystemCountryCodePoco> SystemCountryCode{get; set; }

        public DbSet<SystemLanguageCodePoco> SystemLanguageCode{get; set; }
         
        public CareerCloudContext()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(),"appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemLanguageCodePoco>().Ignore(e => e.Id);
            modelBuilder.Entity<SystemCountryCodePoco>().Ignore(e => e.Id);



            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantEducations)
                .HasForeignKey(a => a.Applicant);



            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(a => a.CompanyJob)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Job);




            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantEducations)
                .WithOne(a => a.ApplicantProfile)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantJobApplications)
               .WithOne(a => a.ApplicantProfile)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantResumes)
               .WithOne(a => a.ApplicantProfile)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantSkills)
               .WithOne(a => a.ApplicantProfile)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                          .HasMany(a => a.ApplicantWorkHistorys)
                          .WithOne(a => a.ApplicantProfile)
                          .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
              .HasOne(a => a.SystemCountryCode)
              .WithMany(a => a.ApplicantProfiles)
              .HasForeignKey(a => a.Country);



            modelBuilder.Entity<ApplicantResumePoco>()
               .HasOne(a => a.ApplicantProfile)
               .WithMany(a => a.ApplicantResumes)
               .HasForeignKey(a => a.Applicant);




            modelBuilder.Entity<ApplicantSkillPoco>()
               .HasOne(a => a.ApplicantProfile)
               .WithMany(a => a.ApplicantSkills)
               .HasForeignKey(a => a.Applicant);



            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
               .HasOne(a => a.ApplicantProfile)
               .WithMany(a => a.ApplicantWorkHistorys)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
               .HasOne(a => a.SystemCountryCode)
               .WithMany(a => a.ApplicantWorkHistorys)
               .HasForeignKey(a => a.CountryCode);



            modelBuilder.Entity<CompanyDescriptionPoco>()
              .HasOne(a => a.SystemLanguageCode)
              .WithMany(a => a.CompanyDescriptions)
              .HasForeignKey(a => a.LanguageId);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                         .HasOne(a => a.CompanyProfile)
                         .WithMany(a => a.CompanyDescriptions)
                         .HasForeignKey(a => a.Company);



            modelBuilder.Entity<CompanyJobDescriptionPoco>()
           .HasOne(a => a.CompanyJob)
           .WithMany(a => a.CompanyJobDescriptions)
           .HasForeignKey(a => a.Job);


            modelBuilder.Entity<CompanyJobEducationPoco>()
           .HasOne(a => a.CompanyJob)
           .WithMany(a => a.CompanyJobEducations)
           .HasForeignKey(a => a.Job);



            modelBuilder.Entity<CompanyJobPoco>()
           .HasMany(a => a.CompanyJobDescriptions)
           .WithOne(a => a.CompanyJob)
           .HasForeignKey(a => a.Job);

            modelBuilder.Entity<CompanyJobPoco>()
           .HasMany(a => a.CompanyJobEducations)
           .WithOne(a => a.CompanyJob)
           .HasForeignKey(a => a.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                      .HasMany(a => a.CompanyJobSkills)
                      .WithOne(a => a.CompanyJob)
                      .HasForeignKey(a => a.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                      .HasMany(a => a.ApplicantJobApplications)
                      .WithOne(a => a.CompanyJob)
                      .HasForeignKey(a => a.Job);




            modelBuilder.Entity<CompanyJobSkillPoco>()
               .HasOne(a => a.CompanyJob)
               .WithMany(a => a.CompanyJobSkills)
               .HasForeignKey(a => a.Job);



            modelBuilder.Entity<CompanyLocationPoco>()
               .HasOne(a => a.CompanyProfile)
               .WithMany(a => a.CompanyLocations)
               .HasForeignKey(a => a.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
               .HasOne(a => a.SystemCountryCode)
               .WithMany(a => a.CompanyLocations)
               .HasForeignKey(a => a.CountryCode);




            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyDescriptions)
               .WithOne(a => a.CompanyProfile)
               .HasForeignKey(a => a.Company);

            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyLocations)
               .WithOne(a => a.CompanyProfile)
               .HasForeignKey(a => a.Company);


            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyJobs)
               .WithOne(a => a.CompanyProfile)
               .HasForeignKey(a => a.Company);



            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(a => a.ApplicantProfiles)
               .WithOne(a => a.SecurityLogin)
               .HasForeignKey(a => a.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(a => a.SecurityLoginsLogs)
               .WithOne(a => a.SecurityLogin)
               .HasForeignKey(a => a.Login);
            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(a => a.SecurityLoginsRoles)
               .WithOne(a => a.SecurityLogin)
               .HasForeignKey(a => a.Login);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
               .HasOne(a => a.SecurityLogin)
               .WithMany(a => a.SecurityLoginsLogs)
               .HasForeignKey(a => a.Login);



            modelBuilder.Entity<SecurityLoginsRolePoco>()
               .HasOne(a => a.SecurityLogin)
               .WithMany(a => a.SecurityLoginsRoles)
               .HasForeignKey(a => a.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
               .HasOne(a => a.SecurityRole)
               .WithMany(a => a.SecurityLoginsRoles)
               .HasForeignKey(a => a.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                          .HasOne(a => a.SecurityLoginsLog)
                          .WithMany(a => a.SecurityLoginsRoles)
                          .HasForeignKey(a => a.Login);

        }


    }
}
