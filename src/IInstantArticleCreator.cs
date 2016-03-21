namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleCreator<T>
    {
        IInstantArticle CreateInstantArticle(T page);
    }
}
