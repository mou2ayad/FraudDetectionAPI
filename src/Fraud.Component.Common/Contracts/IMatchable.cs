using System.Collections.Generic;

namespace Fraud.Component.Common.Contracts
{
    public interface IMatchable
    {
        IEnumerable<string> OrderedPropertiesToMatch { get; }
    }
}