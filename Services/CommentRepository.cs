using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services
{
    public class CommentRepository(ApplicationDbContext context) : ICommentRepository
    {
        public async Task CreateComment(Comment comment)
        {
            context.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByTicketId(int ticketid)
        {
            return await context.Comments.Include(c => c.CreatedBy)
                .Where(c => c.TicketId == ticketid)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}
