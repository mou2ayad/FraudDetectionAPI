using System;
using System.Collections.Generic;
using System.Text;
using Fraud.Component.Matching.Models;

namespace Fraud.Component.Matching.Configuration
{
    public class MatchingConfig
    {
        public bool EnableCache { set; get; }

        public int ExpireAfterInMinutes { set; get; }

        public List<MatchingRule> MatchingRules { set; get; }
    }
}
