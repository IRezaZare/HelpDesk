using HelpDesk.Entities;

namespace HelpDesk.Interfaces
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);
        Task Update(Ticket ticket);
        Task<bool> Delete(Ticket ticket);
        Task<bool> Delete(int Id);
        Task<IReadOnlyList<Ticket>> GetAll();
        Task Save();
        Task<Ticket> GetTicketById(int Id);
    }
}
