using System.Collections.Generic;
using Fraud.Component.DataAccessLayer.Context;

namespace Fraud.Component.DataAccessLayer.Database
{
    internal static class RunTimePersonsDatabase
    {
        public static readonly Dictionary<string, PersonDao> Db = new Dictionary<string, PersonDao>();
    }
}
