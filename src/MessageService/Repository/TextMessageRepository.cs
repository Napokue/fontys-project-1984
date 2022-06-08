using DatabaseLib;
using MessageService.Models;

namespace MessageService.Repository;

internal class TextMessageRepository : IRepository<TextMessage>
{
    public Task<bool> AddAsync(TextMessage model)
    {
        throw new NotImplementedException();
    }

    public Task<TextMessage?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TextMessage>> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(TextMessage model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}