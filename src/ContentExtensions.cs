using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;

namespace BVNetwork.InstantArticles
{
    /// <summary>
    /// Extension methods for content
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Shorthand for DataFactory.Instance.Get
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="contentLink"></param>
        /// <returns></returns>
        public static IContent Get<TContent>(this ContentReference contentLink) where TContent : IContent
        {
            return DataFactory.Instance.Get<TContent>(contentLink);
        }

        public static IInstantArticle CreateInstantArticleBase(this IContent content)
        {
            var page = content as PageData;
            return new InstantArticle()
            {
                Title = page.PageName,
                PageLink = page.PageLink,
                ContentGuid = page.ContentGuid,
                StartPublish = page.StartPublish,
                Changed = page.Changed
            };
        }

        public static MvcHtmlString GetCanonicalLink(this ContentReference contentRef)
        {
            return CanonicalLinkExtensions.CanonicalLink(null, (ContentReference) contentRef, (string) null, (string) null);
        }
    }
}
