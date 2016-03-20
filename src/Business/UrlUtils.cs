using System.Web.Routing;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace BVNetwork.InstantArticles.Business
{
    public static class UrlUtils
    {

        public static string GetExternalUrl(ContentReference pageReference)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            var requestContext = new RequestContext()
            {
                RouteData = new RouteData()
            };
            requestContext.RouteData.DataTokens["contextmode"] = ContextMode.Default;
            var url = urlResolver.GetVirtualPath(new ContentReference(pageReference.ID), ContentLanguage.PreferredCulture.Name, null, requestContext).GetUrl();
            return url;
        }
    }
}