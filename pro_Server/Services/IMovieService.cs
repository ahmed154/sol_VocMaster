using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IMovieService
    {
        Task<List<MovieVM>> GetMovies();
        Task<MovieVM> GetMovie(int id);
        Task<MovieVM> UpdateMovie(int id, MovieVM movieVM);
        Task<MovieVM> CreateMovie(MovieVM movieVM);
        Task<MovieVM> DeleteMovie(int id);
    }
}
