using System;
using System.Linq.Expressions;

namespace FRISS.Components.Utilities.Async
{
    public interface IAsyncActionQueue
    {
        void FireAndForget(Expression<Action> Job);
    }
}