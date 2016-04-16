using System.Collections.Generic;
using BVNetwork.InstantArticles.Models.Pages;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleService
    {
        IEnumerable<IInstantArticlePage> GetAllInstantArticlePages();

        InstantArticleRssPage GetInstantArticleRssPage();
    }
}
