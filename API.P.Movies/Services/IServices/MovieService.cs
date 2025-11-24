using API.P.Movies.DAL.Models;
using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Repository.IRepository;
using AutoMapper;

namespace API.P.Movies.Services.IServices
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }
        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            return _mapper.Map<MovieDto>(movie);
        }
        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto)
        {
            var movieExists = await _movieRepository.MovieExistsByNameAsync(movieCreateUpdateDto.Name);
            //Validar con un if
            if (movieExists)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre {movieCreateUpdateDto.Name}");
            }
            //Mapear copiando los valores del DTO al modelo
            var movie = _mapper.Map<Movie>(movieCreateUpdateDto);
            //Crear la pelicula en la DB
            var movieCreated = await _movieRepository.CreateMovieAsync(movie);
            //Validar si se creo
            if (!movieCreated)
            {
                throw new InvalidOperationException("Ocurrio un error al crear la pelicula");
            }
            //Mapear copiando los valores del createupdatedto al dto regular
            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }
        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto movieCreateUpdateDto, int id)
        {
            //Consultar si existe la pelicula
            var movieExists = await _movieRepository.GetMovieAsync(id);
            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontro la pelicula con el id:{id}");
            }
            //Verificar si el nombre no esta en uso ya
            var movieExistsByName = await _movieRepository.MovieExistsByNameAsync(movieCreateUpdateDto.Name);
            if (movieExistsByName)
            {
                throw new InvalidOperationException($"Ya existe una pelicula con el nombre:{movieCreateUpdateDto.Name}");
            }
            //Mapear los cambios del DTO al modelo
            _mapper.Map(movieCreateUpdateDto, movieExists);
            //Crear la actualizacion
            var movieUpdated = await _movieRepository.UpdateMovieAsync(movieExists);
            //Validar si se actualizó
            if (!movieUpdated)
            { 
                throw new InvalidOperationException("Ocurrio un error al actualizar la pelicula");            
            }
            //retornar el dto
            return _mapper.Map<MovieDto>(movieExists);
        }
        public async Task<bool> DeleteMovieAsync(int id)
        {
            //Verificar la existencia
            var movieExists = await _movieRepository.GetMovieAsync(id);
            if (movieExists == null)
            {
                throw new InvalidOperationException($"No se encontro una pelicula con el id:{id}");
            }
            //Borrar la pelicula en la DB
            var movieDeleted = await _movieRepository.DeleteMovieAsync(id);
            //Validar si NO se borró
            if (!movieDeleted)
            {
                throw new InvalidOperationException("Ocurrio un error al eliminar la pelicula");
            }
            return movieDeleted;
        }
        public Task<bool> MovieExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> MovieExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}
