using System.Collections.Generic;

namespace FRISS.Common.Models
{
    public interface IMatchable
    {
        IEnumerable<string> OrderedPropertiesToMatch { get; }
    }
}