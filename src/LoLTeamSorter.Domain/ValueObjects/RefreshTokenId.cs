﻿using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record RefreshTokenId
    {
        public Guid Value { get; }
        private RefreshTokenId(Guid value) => Value = value;
        public static RefreshTokenId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("RefreshTokenId cannot be empty.");
            }
            return new RefreshTokenId(value);
        }
    }
}
