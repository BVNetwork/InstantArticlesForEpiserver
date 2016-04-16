using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public class InstantArticleImage : IInstantArticleImage
    {
        public ContentReference Image { get; set; }
        public string ImageCaption { get; set; }
    }
}