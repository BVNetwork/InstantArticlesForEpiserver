using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Business.Interfaces
{
    public interface IInstantArticleImageCreator<T>
    {
        IInstantArticleImage CreateInstantArticleImage(BlockData block);
    }
}
