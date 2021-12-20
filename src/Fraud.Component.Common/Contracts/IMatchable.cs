using System;
using System.Collections.Generic;

namespace Fraud.Component.Common.Contracts
{
    public interface IMatchable<T> : IEquatable<T>
    {
        IEnumerable<string> OrderedPropertiesToMatch { get; }
    }
}