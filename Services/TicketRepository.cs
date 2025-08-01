﻿using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.Interfaces;
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
    }
}
