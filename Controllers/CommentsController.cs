﻿using AutoMapper;
using HelpDesk.Data;
using HelpDesk.Entities;
using HelpDesk.Extensions;
using HelpDesk.Interfaces;
using HelpDesk.ViewModels.CommentsDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class CommentsController(ICommentRepository commentRepository , IMapper mapper) : AuthorizeBaseController
    {
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Create(int ticketId)
        {
            var comments = await commentRepository.GetCommentsByTicketId(ticketId);
            return PartialView(new CreateCommentDto
            {
                TicketId = ticketId,
                CommentsOfTicket = mapper.Map<List<CommentDto>>(comments)
            });
        }
        [HttpPost("{ticketId}")]
        public async Task<IActionResult> Create(CreateCommentDto model)
        {
            var entity = mapper.Map<Comment>(model);
            entity.CreatedById = User.GetId();
            await commentRepository.CreateComment(entity);
            return RedirectToAction("Index", "Tickets");
        }
    }
}
