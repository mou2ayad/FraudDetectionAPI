using System;
using System.Linq.Expressions;
using Hangfire;

namespace FRISS.Components.Utilities.Async
{    
    public class AsyncActionQueue : IAsyncActionQueue
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        public AsyncActionQueue( IBackgroundJobClient backgroundJobClient)
        {            
            _backgroundJobClient = backgroundJobClient;
        }
        public void FireAndForget(Expression<Action> Job)  =>
            _backgroundJobClient.Enqueue(Job);
        
    }
}
