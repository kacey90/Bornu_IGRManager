﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid CorrelationId { get; }

        bool IsAvailable { get; }
    }
}
