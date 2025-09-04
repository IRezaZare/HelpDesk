using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.Interfaces;
using HelpDesk.ViewModels.TicketsDto;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Services
{
    public class TicketRepository(ApplicationDbContext context) : ITicketRepository
    {

        public async Task Create(Ticket ticket)
        {
            context.Add(ticket);
            await Save();
        }

        public async Task<bool> Delete(Ticket ticket)
        {
            ticket.IsDeleted = true;
            await Update(ticket);
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            var ticket = await GetTicketById(Id);
            return await Delete(ticket);
        }

        public async Task Update(Ticket ticket)
        {
            context.Update(ticket);
            await Save();

        }

        public async Task<IReadOnlyList<Ticket>> GetAll()
        {
            return await context.Ticket
                .Include(t => t.CreatedBy)
                .Include(t => t.Comments)
                .ToListAsync();

        }

        public async Task<Ticket> GetTicketById(int Id)
        {
            return await context.Ticket.FindAsync(Id);
        }   

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Ticket>> MyTickets(string currentUserId)
        {
            return await context.Ticket
                .Where(t => t.CreatedById == currentUserId)
                .Include(t => t.CreatedBy)
                .Include(t => t.Comments)
                .ToListAsync();
        }

        public async Task<List<TicketHstory>> GetTemporalHistory(int Id)
        {
            return await context.Ticket
                .TemporalAll()
                .Where(t => t.Id == Id)
                .Select(t => new TicketHstory
                {
                    Form = EF.Property<DateTime>(t,"From").ToLocalTime(),
                    To = EF.Property<DateTime>(t,"To").ToLocalTime(),
                    Title = t.Title
                })
                .ToListAsync();
        }
    }
}
