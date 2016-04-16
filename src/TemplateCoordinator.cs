using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace BVNetwork.InstantArticles
{
    [ServiceConfiguration(typeof(IViewTemplateModelRegistrator))]
    public class TemplateCoordinator : IViewTemplateModelRegistrator
    {
        public const string BlockFolder = "~/modules/BVNetwork.InstantArticles/Views/Shared/Blocks/";
   
        /// <summary>
        /// Registers renderers/templates which are specific for this module
        /// </summary>
        public void Register(TemplateModelCollection viewTemplateModelRegistrator)
        {
            viewTemplateModelRegistrator.Add(typeof(IInstantArticleImageBlock), new TemplateModel
            {
                Name = "IInstantArticleImageBlock",
                Tags = new[] { "InstantArticle" },
                AvailableWithoutTag = false,
                Inherit = true,
                Path = BlockPath("InstantArticleImageBlock.cshtml")
            });

            viewTemplateModelRegistrator.Add(typeof(IInstantArticleImageFile), new TemplateModel
            {
                Name = "IInstantArticleImageFile",
                Tags = new[] { "InstantArticle" },
                AvailableWithoutTag = false,
                Inherit = true,
                Path = BlockPath("InstantArticleImageFile.cshtml")
            });
        }

        private static string BlockPath(string fileName)
        {
            return string.Format("{0}{1}", BlockFolder, fileName);
        }
    }
}