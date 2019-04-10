namespace Infraestructura.Persistencia.Vanilla
{
    public class Repository
    {
        protected readonly string ConnectionString;

        protected Repository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}