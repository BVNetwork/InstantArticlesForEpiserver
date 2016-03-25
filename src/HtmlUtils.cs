using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Castle.Core.Internal;
using EPiServer;
using EPiServer.Core;
using HtmlAgilityPack;

namespace BVNetwork.InstantArticles
{
    public static class HtmlUtils
    {

        private static readonly Dictionary<string, string[]> ValidHtmlTags =
    new Dictionary<string, string[]>
    {
            {"p", new string[]            {}},
            {"div", new string[]          {"*"}},
            {"h1", new string[]           {}},
            {"h2", new string[]           {}},
            {"h3", new string[]           {}},
            {"h4", new string[]           {}},
            {"h5", new string[]           {}},
            {"h6", new string[]           {}},
            {"ol", new string[]           {}},
            {"ul", new string[]           {}},
            {"li", new string[]           {}},
            {"blockquote", new string[]   {}},
            {"a", new string[]            {}},
            {"cite", new string[]         {}},
            {"aside", new string[]        {}},

    };

        /// <summary>
        /// Sanitizes all artticles to HTML that Facebook can accept.
        /// </summary>
        /// <param name="allInstantArticles"></param>
        public static void SanitizeBodyHtml(IEnumerable<IInstantArticle> allInstantArticles)
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
                        if (tag.Value.Contains("*")) continue;
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
            RemoveNestedParagraphElements(allNodes);

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

        /// <summary>
        /// Removed nested <p></p>-elements
        /// </summary>
        /// <param name="allNodes"></param>
        private static void RemoveNestedParagraphElements(HtmlNode allNodes)
        {

            var pTags = allNodes.SelectNodes("//p");
            if (pTags != null)
            {
                foreach (var tag in pTags)
                {
                    if (tag.InnerText.IsNullOrEmpty())
                    {
                        tag.ParentNode.RemoveChild(tag, true);
                    }
                }
            }
        }
    }
}