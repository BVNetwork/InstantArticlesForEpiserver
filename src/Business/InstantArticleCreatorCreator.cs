using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BVNetwork.InstantArticles.Business.Interfaces;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business
{
    public class InstantArticleCreatorCreator : IInstantArticleCreator<PageData>
    {
        public IInstantArticle CreateInstantArticle(PageData page)
        {
            var instantArticle = new InstantArticle(page);
            return instantArticle;
        }
    }
}