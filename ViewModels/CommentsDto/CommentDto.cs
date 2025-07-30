using AutoMapper;
using HelpDesk.Entities;
using HelpDesk.Mapping;

namespace HelpDesk.ViewModels.CommentsDto
{
    public class CommentDto : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string Description { get; set; }
        public int TicketId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDto>()
                .ForMember(x => x.CreatedBy, y => y.MapFrom(z => z.CreatedBy.UserName));
                
        }
    }
}
