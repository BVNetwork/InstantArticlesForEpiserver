using System.Web.Mvc;
using BVNetwork.InstantArticles.Models.Pages;
using BVNetwork.InstantArticles.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Shell;
using EPiServer.Web.Mvc;

namespace BVNetwork.InstantArticles.Controllers
{
    public class RssPageController : PageController<RssPage>
    {
        private IContentLoader _contentLoader;

        public ActionResult Index(RssPage currentPage)
        {
            Response.AddHeader("Content-Type", "application/rss+xml");
            var model = new RssViewModel(currentPage);

            //        var searchResult = SearchClient.Instance.Search<IInstantArticle>()
            //.For("John")
            //.InField(x => x.Name)
            //.GetResult();

            //var articles = SearchClient.Instance.Search<PageData>()
               
            //    .Filter(x => x.MatchTypeHierarchy(typeof(IInstantArticle)))
            //   // .Filter(x => ((IInstantArticle)x.ExcludeFromFacebook).Match(false))
            // .GetContentResult();
            ////model.InstantArticles = articles.Items
            //// .GetResult();
            //var instantArticles = _contentLoader.GetChildren<InstantArticlePage>(ContentReference.StartPage);
            //model.InstantArticles = articles.Cast<IInstantArticle>();
            return View(Paths.PublicRootPath + "BVNetwork.InstantArticles/Views/RssPage/Index.cshtml",model);
        }

        public RssPageController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

    }
}