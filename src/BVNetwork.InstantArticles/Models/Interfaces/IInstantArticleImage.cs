using EPiServer.Core;

namespace BVNetwork.InstantArticles.Models.Interfaces
{
    public interface IInstantArticleImage
    {
        ContentReference Image { get; set; }

        string ImageText { get; set; }
    }
}
