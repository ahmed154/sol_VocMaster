using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IMovieRepository
    {
        Task<List<MovieVM>> Search(string name);
        Task<List<MovieVM>> GetMovies();
        Task<MovieVM> GetMovie(int movieId);
        Task<MovieVM> CreateMovie(MovieVM movieVM);
        Task<MovieVM> UpdateMovie(MovieVM movie);
        Task<MovieVM> DeleteMovie(int movieId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Movie> GetMovieByName(string name);
        Task<Movie> GetMovieByname(Movie movie);
    }
}
