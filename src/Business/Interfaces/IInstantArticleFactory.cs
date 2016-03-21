namespace BVNetwork.InstantArticles.Business.Interfaces
{
    public interface IInstantArticleFactory<T>
    {
        IInstantArticle CreateInstantArticle(T page);
    }
}