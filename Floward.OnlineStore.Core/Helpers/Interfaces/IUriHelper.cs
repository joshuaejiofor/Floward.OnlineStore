using Floward.OnlineStore.Core.Requests.Filters;
using System;

namespace Floward.OnlineStore.Core.Helpers.Interfaces
{
    public interface IUriHelper
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
