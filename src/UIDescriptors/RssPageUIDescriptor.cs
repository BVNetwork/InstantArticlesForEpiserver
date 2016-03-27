using System;
using BVNetwork.InstantArticles.Models.Pages;
using EPiServer.Shell;

namespace BVNetwork.InstantArticles.UIDescriptors
{
   

        /// <summary>
        /// Describes how the UI should appear for <see cref="RssPage"/> content.
        /// </summary>
        [UIDescriptorRegistration]
        public class RssPageUIDescriptor : UIDescriptor<RssPage>
        {
            public RssPageUIDescriptor() : base("InstantArticleRss-icon")
            {
                DefaultView = CmsViewNames.AllPropertiesView;
            }
        }
}