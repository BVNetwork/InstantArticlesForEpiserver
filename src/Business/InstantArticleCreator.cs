using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BVNetwork.InstantArticles.Business.Interfaces;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business
{
    public abstract class InstantArticleCreatorBase : IInstantArticleCreator<PageData>
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