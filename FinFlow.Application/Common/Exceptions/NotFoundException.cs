namespace FinFlow.Application.Common.Exceptions;

public class NotFoundException(string entity, Guid id)
    : Exception($"Entity \"{entity}\" ({id}) was not found.");