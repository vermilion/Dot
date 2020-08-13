﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;

namespace Web.Service.BusinessLogic
{
    public class CreateHandler : EntityCreateHandlerBase<MyEntity, MyEntityModel, CreateRequest>
    {
        public CreateHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
