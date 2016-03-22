namespace BVNetwork.InstantArticles
{
    public interface IInstantArticlePage
    {
        bool ExcludeFromFacebook { get; set; }
        IInstantArticle CreateInstantArticle();
       
    }
}
