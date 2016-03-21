using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business.Interfaces
{
    public interface IInstantArticleImage
    {
        ContentReference Image { get; set; }

        string ImageCaption { get; set; }
    }
}
