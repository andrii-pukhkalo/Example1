using System.Collections.Generic;

namespace Cup.Domain.Core.AggregatesModel.CupAggregate
{
    public class PayoutSpecification
    {
        public List<PayoutRule> PayoutRules { get; private set; }
    }
}