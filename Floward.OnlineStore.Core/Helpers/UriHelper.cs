using Floward.OnlineStore.Core.Helpers.Interfaces;
using Floward.OnlineStore.Core.Requests.Filters;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Floward.OnlineStore.Core.Helpers
{
    public class UriHelper : IUriHelper
    {
        private readonly string _baseUri;
        public UriHelper(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
