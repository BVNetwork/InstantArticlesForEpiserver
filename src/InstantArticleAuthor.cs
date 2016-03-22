using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BVNetwork.InstantArticles
{
    public class InstantArticleAuthor : IInstantArticleAuthor
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string FacebookUsername { get; set; }
    }
}