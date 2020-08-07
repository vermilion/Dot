using MediatR;
using PlatformFramework.Eventing.Requests;
using System.Collections.Generic;

namespace Web.Service.BusinessLogic
{
    public class GetAllRequest : PagingRequest, IRequest<IEnumerable<MyEntityModel>>
    {
    }
}
