using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BVNetwork.InstantArticles.Models.Pages;
using BVNetwork.InstantArticles.Models.ViewModels;
using Castle.Core.Internal;
using EPiServer;
using EPiServer.Core;
using EPiServer.Logging;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Web.Mvc;
using HtmlAgilityPack;

namespace BVNetwork.InstantArticles.Controllers
{
   // [ContentOutputCache]
    public class InstantArticleRssPageController : PageController<InstantArticleRssPage>
    {
        private static readonly ILogger logger = LogManager.GetLogger();
        private IContentLoader _contentLoader;
        private IInstantArticleService _instantAricleService;

        public ActionResult Index(InstantArticleRssPage currentPage)
        {
            var model = new InstantArticleRssViewModel(currentPage);
            var allInstantArticlePages = _instantAricleService.GetAllInstantArticlePages();

            var allInstantArticles = new List<IInstantArticle>();

            foreach (var instantArticlePage in allInstantArticlePages)
            {
                allInstantArticles.Add(instantArticlePage.CreateInstantArticle(currentPage));
            }

            logger.Debug("Found {0} instant articles", allInstantArticles.Count());

            HtmlUtils.SanitizeBodyHtml(allInstantArticles);
            model.InstantArticles = allInstantArticles;

            SetResposneHeaders();

            var viewResult = View(Paths.PublicRootPath + "BVNetwork.InstantArticles/Views/RssPage/Index.cshtml", model);


            return viewResult;
        }

        public void SetResposneHeaders()
        {
            Response.AddHeader("Content-Type", "application/rss+xml");
            //HttpContext.Response.Cache.SetExpires(DateTime.Now.AddMinutes(3.0));
            //HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
            //HttpContext.Response.Cache.SetValidUntilExpires(true);
        }

        public InstantArticleRssPageController(IContentLoader contentLoader, IInstantArticleService instantArticleService)
        {
            _contentLoader = contentLoader;
            _instantAricleService = instantArticleService;
        }
    }
}