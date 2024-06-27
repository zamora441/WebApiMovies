namespace WebApiMovies.Data_Access
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository GenreRepository { get; }
        ICountryRepository CountryRepository { get; }
        IActorRepository ActorRepository { get; }
        IMovieRepository MovieRepository { get; }
        IMovieReviewRepository MovieReviewRepository { get; }
        Task SaveChangesAsync();
    }
}
