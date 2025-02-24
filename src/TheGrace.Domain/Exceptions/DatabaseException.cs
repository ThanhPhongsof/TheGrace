using TheGrace.Domain.Exceptions.Commons;

namespace TheGrace.Domain.Exceptions;

public static class DatabaseException
{
    public class ConnectionStringNotFound : NotFoundException
    {
        public ConnectionStringNotFound() : base("The database connection string could not be found.")
        {
        }
    }

    public class ConnectionDatabaseFail : BadRequestException
    {
        public ConnectionDatabaseFail() : base("Encountered an issue while attempting to establish a connection with the database.")
        {
        }
    }
}
