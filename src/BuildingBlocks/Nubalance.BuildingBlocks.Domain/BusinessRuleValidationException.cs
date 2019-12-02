﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nubalance.BuildingBlocks.Domain
{
    public class BusinessRuleValidationException : Exception
    {
        private readonly IBusinessRule _brokenRule;

        public string Details { get; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            _brokenRule = brokenRule;
            this.Details = brokenRule.Message;
        }

        public override string ToString()
        {
            return $"{_brokenRule.GetType().FullName}: {_brokenRule.Message}";
        }
    }
}
