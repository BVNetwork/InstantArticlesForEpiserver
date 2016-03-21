namespace BVNetwork.InstantArticles.Business.Interfaces
{
    interface IInstantArticleCreator<T>
    {
        IInstantArticle CreateInstantArticle(T page);
    }
}
