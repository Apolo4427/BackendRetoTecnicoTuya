namespace BackendTuya.src.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, Guid key)
            : base ($"{entity} con id '{key}' no se ha encontrado."){}
    }
}