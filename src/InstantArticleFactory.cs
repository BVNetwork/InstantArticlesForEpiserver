using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business
{
    public class InstantArticleFactory : IInstantArticleFactory
    {
        public IInstantArticle CreateInstantArticle(PageData page)
        {
            var instantArticle = new InstantArticle()
            {
                Title = page.PageName,
                PageLink = page.PageLink,
                ContentGuid = page.ContentGuid,
                Changed = page.Changed
            };
            return instantArticle;
        }
    }
}