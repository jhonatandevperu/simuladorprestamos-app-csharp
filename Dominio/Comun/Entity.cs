namespace Dominio.Comun
{
    public abstract class Entity : IDomainObject
    {
        public int Id {get; protected set;}
    }
}