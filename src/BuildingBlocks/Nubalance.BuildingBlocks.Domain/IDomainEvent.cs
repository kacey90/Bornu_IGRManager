﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
