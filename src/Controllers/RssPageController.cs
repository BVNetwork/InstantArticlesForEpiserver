using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class RssPageController : PageController<RssPage>
    {
        private static readonly ILogger logger = LogManager.GetLogger();
        private IContentLoader _contentLoader;
        private IInstantArticleService _instantAricleService;

        public ActionResult Index(RssPage currentPage)
        {
            Response.AddHeader("Content-Type", "application/rss+xml");
            Response.AddHeader("meta charset", "utf-8");

            var model = new RssViewModel(currentPage);
            var allInstantArticlePages = _instantAricleService.GetAllInstantArticlePages();

            var allInstantArticles = new List<IInstantArticle>();

            foreach (var instantArticlePage in allInstantArticlePages)
            {
                allInstantArticles.Add(instantArticlePage.CreateInstantArticle());
            }

            logger.Debug("Found {0} instant articles", allInstantArticles.Count());

            SanitizeBodyHtml(allInstantArticles);
            model.InstantArticles = allInstantArticles;

            return View(Paths.PublicRootPath + "BVNetwork.InstantArticles/Views/RssPage/Index.cshtml", model);
        }

        private void SanitizeBodyHtml(IEnumerable<IInstantArticle> allInstantArticles)
        {
            foreach (var article in allInstantArticles)
            {
                if (article.Body != null)
                {
                    var rawHtml = article.Body.ToEditString();
                    var sanitizedHtml = SanitizeHtml(rawHtml);
                    article.Body = new XhtmlString(sanitizedHtml);
                }
            }
        }


        public RssPageController(IContentLoader contentLoader, IInstantArticleService instantArticleService)
        {
            _contentLoader = contentLoader;
            _instantAricleService = instantArticleService;
        }

        private static readonly Dictionary<string, string[]> ValidHtmlTags =
            new Dictionary<string, string[]>
            {
            {"p", new string[]            {}},
            {"div", new string[]          {"*"}},
            //{"span", new string[]       {"style", "class", }},
            //{"br", new string[]         {"style", "class"}},
            //{"hr", new string[]         {"style", "class"}},
            //{"label", new string[]      {"style", "class"}},

            {"h1", new string[]           {}},
            {"h2", new string[]           {}},
            {"h3", new string[]           {}},
            {"h4", new string[]           {}},
            {"h5", new string[]           {}},
            {"h6", new string[]           {}},

            //{"font", new string[]       {"style", "class", "color", "face", "size"}},
            //{"strong", new string[]     {"style", "class"}},
            //{"b", new string[]          {"style", "class"}},
            //{"em", new string[]         {"style", "class"}},
            //{"i", new string[]          {"style", "class"}},
            //{"u", new string[]          {"style", "class"}},
            //{"strike", new string[]     {"style", "class"}},
            {"ol", new string[]           {}},
            {"ul", new string[]           {}},
            {"li", new string[]           {}},
            {"blockquote", new string[]   {}},
            //{"code", new string[]       {"style", "class"}},

            {"a", new string[]            {}},
            //{"img", new string[]        {"style", "class", "src", "height", "width",
            //    "alt", "title", "hspace", "vspace", "border"}},

            //{"table", new string[]      {"style", "class"}},
            //{"thead", new string[]      {"style", "class"}},
            //{"tbody", new string[]      {"style", "class"}},
            //{"tfoot", new string[]      {"style", "class"}},
            //{"th", new string[]         {"style", "class", "scope"}},
            //{"tr", new string[]         {"style", "class"}},
            //{"td", new string[]         {"style", "class", "colspan"}},

            //{"q", new string[]          {"style", "class", "cite"}},
            {"cite", new string[]         {}},
            {"aside", new string[]        {}},
            //{"abbr", new string[]       {"style", "class"}},
            //{"acronym", new string[]    {"style", "class"}},
            //{"del", new string[]        {"style", "class"}},
            //{"ins", new string[]        {"style", "class"}}
            };

        /// <summary>
        /// Takes raw HTML input and cleans against a whitelist
        /// </summary>
        /// <param name="source">Html source</param>
        /// <returns>Clean output</returns>
        private static string SanitizeHtml(string source)
        {
            if (source == null)
                return null;
            source = source.Replace("<p>&nbsp;</p>", "");

            HtmlDocument html = GetHtml(source);
            if (html == null) return String.Empty;

            // All the nodes
            HtmlNode allNodes = html.DocumentNode;

            // Select whitelist tag names
            string[] whitelist = (from kv in ValidHtmlTags
                                  select kv.Key).ToArray();

            // Scrub tags not in whitelist
            CleanNodes(allNodes, whitelist);

            // Filter the attributes of the remaining
            foreach (KeyValuePair<string, string[]> tag in ValidHtmlTags)
            {
                IEnumerable<HtmlNode> nodes = (from n in allNodes.DescendantsAndSelf()
                                               where n.Name == tag.Key
                                               select n);

                if (nodes == null) continue;

                foreach (var n in nodes)
                {
                    if (!n.HasAttributes) continue;

                    // Get all the allowed attributes for this tag
                    HtmlAttribute[] attr = n.Attributes.ToArray();
                 

                    foreach (HtmlAttribute a in attr)
                    {
                        if(tag.Value.Contains("*")) continue;
                        if (!tag.Value.Contains(a.Name))
                        {
                            a.Remove(); // Wasn't in the list
                        }
                        //else
                        //{
                        //    // AntiXss
                        //    a.Value =
                        //        Microsoft.Security.Application.Encoder.UrlPathEncode(a.Value);
                        //}
                    }
                }
            }

            return allNodes.InnerHtml;
        }

        /// <summary>
        /// Recursively delete nodes not in the whitelist
        /// </summary>
        private static void CleanNodes(HtmlNode node, string[] whitelist)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                if (!whitelist.Contains(node.Name))
                {
                    node.ParentNode.RemoveChild(node);
                    return; // We're done
                }
            }

            if (node.HasChildNodes)
                CleanChildren(node, whitelist);
        }

        /// <summary>
        /// Apply CleanNodes to each of the child nodes
        /// </summary>
        private static void CleanChildren(HtmlNode parent, string[] whitelist)
        {
            for (int i = parent.ChildNodes.Count - 1; i >= 0; i--)
                CleanNodes(parent.ChildNodes[i], whitelist);
        }

        /// <summary>
        /// Helper function that returns an HTML document from text
        /// </summary>
        private static HtmlDocument GetHtml(string source)
        {
            HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;
            html.OptionAutoCloseOnEnd = true;
            html.OptionDefaultStreamEncoding = Encoding.UTF8;

            html.LoadHtml(source);

            return html;
        }
    }
}