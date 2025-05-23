﻿using MediatR;

namespace LoLTeamSorter.Application.Contracts.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
    {
    }
}
