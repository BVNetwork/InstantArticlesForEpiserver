using System;
using BVNetwork.InstantArticles.Models.Pages;
using EPiServer.Shell;

namespace BVNetwork.InstantArticles.UIDescriptors
{


    /// <summary>
    /// Describes how the UI should appear for <see cref="InstantArticleRssPage"/> content.
    /// </summary>
    [UIDescriptorRegistration]
        public class InstantArticleRssPageUIDescriptor : UIDescriptor<InstantArticleRssPage>
        {
            public InstantArticleRssPageUIDescriptor() : base("InstantArticleRss-icon")
            {
                DefaultView = CmsViewNames.AllPropertiesView;
            }
        }
}