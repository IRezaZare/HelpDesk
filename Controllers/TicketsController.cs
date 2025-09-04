using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Extensions;
using HelpDesk.Interfaces;
using HelpDesk.ViewModels.TicketsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HelpDesk.Controllers
{
    public class TicketsController(IMapper mapper, ITicketRepository ticketRepository, IMemoryCache cache) : AuthorizeBaseController
    {
        private const string _Key = "Product";

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var history = await ticketRepository.GetTemporalHistory();
            IReadOnlyList<Ticket> tickets;
            if (!cache.TryGetValue(_Key, out tickets)) return View(tickets);

            tickets = await ticketRepository.GetAll();
            if (tickets is { Count: > 0 })
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                cache.Set(_Key, tickets, cacheEntryOptions);
            }
            return View(tickets);

        }
        public async Task<IActionResult> MyTickets()
        {
            return View(await ticketRepository.MyTickets(User.GetId()));
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(mapper.Map<TicketDetailDto>(ticket));
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var id = User.GetId();
            var username = User.GetUsername();
            var email = User.GetEmail();
            //ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketDto ticket)
        {
            if (ModelState.IsValid)
            {
                //ticket.CreatedById = User.GetId();
                var entity = mapper.Map<Ticket>(ticket);
                entity.CreatedById = User.GetId();
                await ticketRepository.Create(entity);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CreatedById"] = new SelectList(context.Users, "Id", "Id", ticket.CreatedById);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(mapper.Map<EditTicketDto>(ticket));
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTicketDto model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var entity = await ticketRepository.GetTicketById(model.Id); // entity
            if (entity.Version == model.Version)
            {
                mapper.Map(model, entity); // model => entity : updated
                await ticketRepository.Update(entity);
                // await context.Ticket.ExecuteUpdateAsync(x =>
                //     x.SetProperty(c => c.Priority, 2)
                //         .SetProperty(c=>c.Status,TicketStatus.Close));
            }
            else
            {
                ModelState.AddModelError("", "شخص دیگری این بخش را ویرایش کرده است ");
                return View(model);
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await ticketRepository.GetTicketById(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await ticketRepository.GetTicketById(id);
            if (ticket != null)
            {
                await ticketRepository.Delete(ticket);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool TicketExists(int id)
        //{
        //    return context.Ticket.Any(e => e.Id == id);
        //}
    }
}
