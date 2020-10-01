using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace pro_API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Voc>().HasIndex(u => u.Text).IsUnique();
            builder.Entity<UserVoc>().HasKey(t => new { t.UserId, t.VocId });
            //builder.Entity<VocsQuotes>().HasKey(t => new { t.VocId, t.QuoteId });

            base.OnModelCreating(builder);  
        }

        public DbSet<Lang> Langs { get; set; }
        public DbSet<Voc> Vocs { get; set; }
        public DbSet<UserVoc> UserVocs { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<VocAudio> VocAudios { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Example> Examples { get; set; }
        public DbSet<Synonym> Synonyms { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<VocTest> VocTests { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VocSubtitle> VocsSubtitless { get; set; }
        public DbSet<Influencer> Influencers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Idiom> Idioms { get; set; }
    }
}
