using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticle
    {
        /// <summary>
        /// The title of the article.
        /// </summary>
        string Title { get; set; }

        string Subtitle { get; set; }

        //string Description { get; set; }

        IEnumerable<IInstantArticleAuthor> Authors { get; set; }

        string Kicker { get; set; }

        ContentReference Image { get; set; }

        string ImageCaption { get; set; }

        XhtmlString Body { get; set; }

        /// <summary>
        /// Article style
        /// </summary>
        /// <remarks>
        /// The  style to be used for this article. Set up the style on your Facebook page.
        /// </remarks>
        string ArticleStyle { get; set; }

        bool ExcludeFromFacebook { get; set; }

        PageReference PageLink { get; set; }

        Guid ContentGuid { get; set; }

        DateTime StartPublish { get; set; }

        DateTime Changed { get; set; }

    }
}
