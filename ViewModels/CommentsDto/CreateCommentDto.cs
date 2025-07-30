using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Mapping;

namespace HelpDesk.ViewModels.CommentsDto
{
    public class CreateCommentDto : IMapFrom<Comment>
    {
        public int TicketId { get; set; }
        public string Description { get; set; }

        public List<CommentDto> CommentsOfTicket { get; set; } = new();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommentDto, Comment>();
        }
    }
}
