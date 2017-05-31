using Microsoft.EntityFrameworkCore;
using MovieShopXS_LeplaeKevin.Entities;
using MovieShopXSLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopXS_LeplaeKevin.Repositories
{
    
    public class MovieRepository
    {
        private MovieContext db;

        public MovieRepository(MovieContext context)
        {
            db = context;

        }

        public ICollection<MovieVM> Movies()
        {

            List<MovieVM> movies =
                db.Movie
                .Include(d => d.MovieActor)
                .ThenInclude(d => d.Actor)
                .Include(d => d.Genre)


                .Select(p => new MovieVM()
                {
                    Title = p.Title,
                    Year = p.Year,
                    Director = p.Director.FirstName,
                    Genre = p.Genre.Description,
                    Stars = p.Stars,
                    MovieId = p.MovieId


                }).ToList();
            return movies;


        }

        public Movie Movie(int id)
        {

            Movie movie =
                db.Movie
                .Include(d => d.MovieActor)
                .ThenInclude(d => d.Actor)
                .Include(d => d.Genre)
                .Where(d => d.MovieId == id)


                .Select(p => new Movie()
                {
                    Title = p.Title,
                    Year = p.Year,
                    Description = p.Description,
                    Director = p.Director,
                    MovieActor = p.MovieActor.Select(
                    ma => ma).ToList(),
                    Stars = p.Stars,
                    MovieId = p.MovieId



                }).FirstOrDefault();
            return movie;


        }
        /*
        public ICollection<MovieVM> MoviesOfYear(int year)
        {
            List<MovieVM> movies =
                db.Movie
                .Include(d => d.MovieActor)
                .ThenInclude(d => d.Actor)
                .Include(d => d.Genre)
                .Where(d => d.Year == year)

                .Select(p => new MovieVM()
                {
                    Title = p.Title,
                    Year = p.Year,
                    Director = p.Director.FirstName,
                    Genre = p.Genre.Description,
                    Stars = p.Stars,
                    MovieId = p.MovieId



                }).ToList();
            return movies;
        }
        */
        internal void RemoveMovie(Movie oldMovie)
        {
            db.Movie.Remove(oldMovie);
            db.SaveChanges();
        }

        internal void AddMovie(Movie movie)
        {
            Movie newMovie = new Movie()
            {
                Description = movie.Description,
                Genre = movie.Genre,
                Director = movie.Director,
                Stars = movie.Stars,
                Year = movie.Year,
                Title = movie.Title
            };
            db.Movie.Add(newMovie);
            db.SaveChanges();
        }
        /*
        public void UpdateMovieUp(int id,bool val)
        {



            Movie movie =
                db.Movie
                .Where(m => m.MovieId == id)
                .Select(m => m)
                .FirstOrDefault();
            if (val) {
                movie.Stars = (byte)(movie.Stars + 1);

            }
            else
            {
                movie.Stars = (byte)(movie.Stars - 1);

            }
            db.Update(movie);
            db.SaveChanges();


        }

*/
    }
    

    }
