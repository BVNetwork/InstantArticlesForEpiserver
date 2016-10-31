using System;
using EPiServer;
using EPiServer.Core;

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

        public static IInstantArticle CreateInstantArticleBase(this IInstantArticlePage content)
        {
            var page = content as PageData;
            return new InstantArticle()
            {
                Title = page.PageName,
                PageLink = page.PageLink,
                ContentGuid = page.ContentGuid,
                StartPublish = page.StartPublish ?? DateTime.MinValue,
                Changed = page.Changed
            };
        }
    }
}
