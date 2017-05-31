using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShopXSLib.ViewModels;

using Microsoft.EntityFrameworkCore;
using MovieShopXS_LeplaeKevin.Repositories;
using MovieShopXS_LeplaeKevin.Entities;

// /For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopXS_LeplaeKevin.Controllers
{

    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private MovieRepository repository;

        public MovieController(MovieRepository movieRepository)
        {
            repository = movieRepository;
        }

        [HttpGet("",Name = "GetMovies")]
        public IActionResult Get()
        {
            try
            {
                List<MovieVM> movies = repository.Movies().ToList();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]Movie movie)
        {
            try
            {
                if (movie != null)
                {
                    repository.AddMovie(movie);
                    return Created(Url.Link("GetMovies",new { }), movie);
                }
                else
                {
                    return NotFound("No Customer info retrieved...");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Movie oldMovie = repository.Movie(id);
                if (oldMovie != null)
                {
                    repository.RemoveMovie(oldMovie);
                    return Ok($"Customer with id {id} has been succesfully deleted");
                }
                else
                {
                    return NotFound($"No Customer found with id {id}");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }

    }
}
