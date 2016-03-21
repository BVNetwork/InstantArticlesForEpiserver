namespace BVNetwork.InstantArticles.Business.Interfaces
{
    public interface IInstantArticleCreator<T>
    {
        IInstantArticle CreateInstantArticle(T page);
    }
}
