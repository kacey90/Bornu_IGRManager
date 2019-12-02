using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
