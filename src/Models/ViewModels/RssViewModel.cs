using System.Collections.Generic;
using BVNetwork.InstantArticles.Models.Pages;

namespace BVNetwork.InstantArticles.Models.ViewModels
{
    public class RssViewModel
    {
        public RssPage CurrentPage;
        public IEnumerable<IInstantArticle> InstantArticles { get; set; }

        public RssViewModel(RssPage rssPage)
        {
            CurrentPage = rssPage;
        }

    }
}