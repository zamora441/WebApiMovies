using WebApiMovies.Data;

namespace WebApiMovies.Data_Access.Implements
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext _dbContext ,IGenreRepository genreRepository, ICountryRepository countryRepository, IActorRepository actorRepository, IMovieRepository movieRepository, IMovieReviewRepository movieReviewRepository)
        {
            this._dbContext = _dbContext;
            GenreRepository = genreRepository;
            CountryRepository = countryRepository;
            ActorRepository = actorRepository;
            MovieRepository = movieRepository;
            MovieReviewRepository = movieReviewRepository;
        }

        public IGenreRepository GenreRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IActorRepository ActorRepository { get; }
        public IMovieRepository MovieRepository { get; }
        public IMovieReviewRepository MovieReviewRepository { get; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
