using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleImage
    {
        ContentReference Image { get; set; }

        string ImageCaption { get; set; }
    }
}
