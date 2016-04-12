using System.Collections.Generic;
using BVNetwork.InstantArticles.Models.Pages;

namespace BVNetwork.InstantArticles.Models.ViewModels
{
    public class InstantArticleRssViewModel
    {
        public InstantArticleRssPage CurrentPage;
        public IEnumerable<IInstantArticle> InstantArticles { get; set; }

        public InstantArticleRssViewModel(InstantArticleRssPage rssPage)
        {
            CurrentPage = rssPage;
        }

    }
}