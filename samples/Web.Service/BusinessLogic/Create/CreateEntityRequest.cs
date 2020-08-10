using MediatR;

namespace Web.Service.BusinessLogic
{
    public class CreateRequest : IRequest<MyEntityModel>
    {
        public MyEntityModel? Model { get; set; }
    }
}
