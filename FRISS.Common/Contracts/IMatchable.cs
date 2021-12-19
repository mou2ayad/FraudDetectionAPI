using System.Collections.Generic;

namespace FRISS.Common.Contracts
{
    public interface IMatchable
    {
        IEnumerable<string> OrderedPropertiesToMatch { get; }
    }
}