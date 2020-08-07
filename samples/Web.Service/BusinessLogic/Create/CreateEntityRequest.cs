using MediatR;

namespace Web.Service.BusinessLogic
{
    public class CreateRequest : IRequest<MyEntityModel>
    {
        public string? Title { get; set; }
    }
}
