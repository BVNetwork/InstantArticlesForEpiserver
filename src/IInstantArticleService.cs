using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BVNetwork.InstantArticles.Models.Pages;
using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleService
    {
     //   IEnumerable<IInstantArticle> GetAllInstantArticles();
        IEnumerable<IInstantArticlePage> GetAllInstantArticlePages();
    }
}
