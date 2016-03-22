using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BVNetwork.InstantArticles.Models.Pages;
using BVNetwork.InstantArticles.Models.ViewModels;
using Castle.Core.Internal;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web.Mvc;

namespace BVNetwork.InstantArticles.Controllers
{
    public class RssPageController : PageController<RssPage>
    {
        private IContentLoader _contentLoader;
        private IInstantArticleService _instantAricleService;

        public ActionResult Index(RssPage currentPage)
        {
            Response.AddHeader("Content-Type", "application/rss+xml");
            var model = new RssViewModel(currentPage);
            var allInstantArticles = _instantAricleService.GetAllInstantArticles();
            model.InstantArticles = allInstantArticles;
            return View(Paths.PublicRootPath + "BVNetwork.InstantArticles/Views/RssPage/Index.cshtml", model);
        }

        public RssPageController(IContentLoader contentLoader, IInstantArticleService instantArticleService)
        {
            _contentLoader = contentLoader;
            _instantAricleService = instantArticleService;
        }

        private void FindAllInstantArticles(List<PageData> list, ContentReference parentPage)
        {
            var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var children = loader.GetChildren<PageData>(parentPage);

            children.ForEach(pg =>
            {
                if (pg is IInstantArticle)
                {
                    list.Add(pg);
                }

                FindAllInstantArticles(list, pg.ContentLink);
            });
        }
    }
}