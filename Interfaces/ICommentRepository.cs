using HelpDesk.Entities;

namespace HelpDesk.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
        Task<List<Comment>> GetCommentsByTicketId(int ticketid);
    }
}
