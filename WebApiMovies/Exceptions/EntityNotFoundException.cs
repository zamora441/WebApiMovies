namespace WebApiMovies.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(Type entityType, int id) : base($"The {entityType.Name} with id {id} was not found in the system.")
        {
        }
    }
}
