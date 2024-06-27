namespace WebApiMovies.Parameters
{
    public class MovieQueryParameters : BaseQueryParameters
    {
        public MovieQueryParameters()
        {
            OrderBy = "Title";
        }

        public int? Genre { get; set; }
    }
}
