using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public class InstantArticle : IInstantArticle
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IEnumerable<IInstantArticleAuthor> Authors { get; set; }
        public string Kicker { get; set; }
        public ContentReference Image { get; set; }
        public string ImageCaption { get; set; }
        public XhtmlString Body { get; set; }
        public bool ExcludeFromFacebook { get; set; }
        public PageReference PageLink { get; set; }
        public Guid ContentGuid { get; set; }
        public DateTime StartPublish { get; set; }
        public DateTime Changed { get; set; }
    }
}