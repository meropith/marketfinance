using System.Collections.Generic;
using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication
{
    internal class ApplicationResult : IApplicationResult
    {
        public int? ApplicationId { get; set; }
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}