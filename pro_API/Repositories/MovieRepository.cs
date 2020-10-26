using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;
using AutoMapper;
using pro_API.Migrations;
using Movie = pro_Models.Models.Movie;
using Subtitle = pro_Models.Models.Subtitle;
using VocTest = pro_Models.Models.VocTest;

namespace pro_API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        List<Voc> vocList;

        public MovieRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;

            vocList = appDbContext.Vocs.ToList();
        }

        #region Pro
        string Reformat(string txt)
        {
            txt = txt.Replace("<font face=\"comic sans ms\">", "");
            txt = txt.Replace("</font>", "");
            txt = txt.Replace("<i>", "");
            txt = txt.Replace("</i>", "");
            txt = txt.Replace("- ", " ");
            txt = txt.Replace("?", "");
            txt = txt.Replace("!", "");
            txt = txt.Replace(", ", " ");
            txt = txt.Replace(".", "");
            txt = txt.Replace("<i>", "");
            txt = txt.Replace("</i>", "");
            txt = txt.Replace("-", "");
            txt = txt.Replace(" '", " ");

            return txt;
        }
        async Task<int> GetSubtitleRank(string txt)
        {
            int rank = 0;
            Voc voc;
            foreach (string word in txt.Split(" "))
            {
                voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Text == word);
                rank += (voc == null) ? 5000 : voc.Id;
            }
            return rank;
        }
        async Task<List<pro_Models.Models.VocTest>> GetVocTests(string txt, pro_Models.Models.Movie movie)
        {
            List<VocTest> vocTests = new List<VocTest>();
            foreach (string word in txt.Split(" "))
            {
                vocTests.Add(new VocTest 
                { 
                    Text = word,
                    Movie = movie
                });
            }
            return vocTests;
        }
        async Task<List<pro_Models.Models.VocSubtitle>> GetVocSubtitles(string txt)
        {
            List<pro_Models.Models.VocSubtitle> vocSubtitles = new List<pro_Models.Models.VocSubtitle>();           
            Voc dbVoc;

            foreach (string word in txt.Split(" "))
            {
                dbVoc = vocList.FirstOrDefault(x => x.Text == word);
                if (dbVoc != null) vocSubtitles.Add(new pro_Models.Models.VocSubtitle { VocId = dbVoc.Id });                 
            }
            
            return vocSubtitles;
        }
        #endregion
        async Task<List<MovieVM>> IMovieRepository.Search(string name)
        {
            List<MovieVM> movieVMs = new List<MovieVM>();

            IQueryable<Movie> query = appDbContext.Movies;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.MovieId.Contains(name));
            }

            var movies = await query.ToListAsync();

            foreach (var movie in movies)
            {
                movieVMs.Add(new MovieVM { Movie = movie });
            }
            return movieVMs;
        }
        public async Task<List<MovieVM>> GetMovies()
        {
            List<MovieVM> movieVMs = new List<MovieVM>();
            List<Movie> movies = await appDbContext.Movies.ToListAsync();
            foreach (var movie in movies)
            {
                movieVMs.Add(new MovieVM { Movie = movie});
            }
            return movieVMs; 
        }
        public async Task<MovieVM> GetMovie(int id)
        {
            MovieVM movieVM = new MovieVM();
            movieVM.Movie = await appDbContext.Movies.FirstOrDefaultAsync(e => e.Id == id);
            return movieVM;
        }
        public async Task<MovieVM> CreateMovie(MovieVM movieVM)
        {
            movieVM.Movie.Text = movieVM.Movie.Text.ToLower();
            movieVM.Movie.Text = Reformat(movieVM.Movie.Text);

            //string[] MovieContentList = movieVM.Movie.Text.Split("\r\n\r\n");
            string[] MovieContentList = movieVM.Movie.Text.Split("\n\n");

            try
            {
                movieVM.Movie.Subtitles = new List<Subtitle>();
                foreach (var subtitle in MovieContentList)
                {
                    if (subtitle.Trim() == "") continue;
                    string[] line = subtitle.Split("\n");
                    string[] time = line[1].Split(" ");
                    string txt = line[2] + ((line.Length > 3) ? " " + line[3] : "");

                    movieVM.Movie.Subtitles.Add(new pro_Models.Models.Subtitle
                    {
                        Index = line[0],
                        Text = txt,
                        StartTime = TimeSpan.Parse(time[0].Replace(",", ".")).ToString(),
                        EndtTime = TimeSpan.Parse(time[2].Replace(",", ".")).ToString(),
                        //Rank = await GetSubtitleRank(txt),
                        Movie = movieVM.Movie,
                        //VocTests = await GetVocTests(txt, movieVM.Movie),
                        VocSubtitles = await GetVocSubtitles(txt)
                    });
                }
       

                    var result = await appDbContext.Movies.AddAsync(movieVM.Movie);
                    await appDbContext.SaveChangesAsync();

                    movieVM.Movie = result.Entity;
                    movieVM.Movie.Subtitles = null;
                    movieVM.Movie.Text = null;
            }
            catch (Exception ex)
            {

            }



            return movieVM;
        }
        public async Task<MovieVM> UpdateMovie(MovieVM movieVM)
        {
            pro_Models.Models.Movie result = await appDbContext.Movies
                .FirstOrDefaultAsync(e => e.Id == movieVM.Movie.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(movieVM.Movie, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new MovieVM { Movie = result };
            }

            return null;
        }
        public async Task<MovieVM> DeleteMovie(int movieId)
        {
            var result = await appDbContext.Movies
                .FirstOrDefaultAsync(e => e.Id == movieId);
            if (result != null)
            {
                appDbContext.Movies.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new MovieVM { Movie = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<pro_Models.Models.Movie> GetMovieByname(pro_Models.Models.Movie movie)
        {
            return await appDbContext.Movies.Where(n => n.MovieId == movie.MovieId && n.Id != movie.Id)
                .FirstOrDefaultAsync();
        }
    }
}
