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
            Response.AddHeader("meta charset", "utf-8"); 
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
    }
}