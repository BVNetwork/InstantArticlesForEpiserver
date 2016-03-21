using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;

namespace BVNetwork.InstantArticles
{
    public interface IInstantArticleService
    {
        IEnumerable<PageData> GetAllInstantArticlePages();
    }
}
