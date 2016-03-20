using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business
{
    /// <summary>
    /// Extension methods for content
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        /// Shorthand for DataFactory.Instance.Get
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="contentLink"></param>
        /// <returns></returns>
        public static IContent Get<TContent>(this ContentReference contentLink) where TContent : IContent
        {
            return DataFactory.Instance.Get<TContent>(contentLink);
        }
    }
}
