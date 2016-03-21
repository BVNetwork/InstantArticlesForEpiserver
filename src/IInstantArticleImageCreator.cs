using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleImageCreator<T>
    {
        IInstantArticleImage CreateInstantArticleImage(BlockData block);
    }
}
