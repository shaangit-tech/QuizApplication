﻿using Microsoft.EntityFrameworkCore;

namespace QuizApplication.DataModels
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
