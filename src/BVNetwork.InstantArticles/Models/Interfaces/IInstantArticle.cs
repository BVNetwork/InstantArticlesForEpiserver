using System;
using System.Collections.Generic;
using EPiServer.Core;

namespace BVNetwork.InstantArticles.Models.Interfaces
{
    public interface IInstantArticle
    {
        string Title { get; }

        string Subtitle { get; }

      //  string Description { get; }

        IList<ContentReference> Authors { get; }

        string Kicker { get; }

       ContentReference Image { get; }

        string ImageText { get; }

        XhtmlString MainBody { get; }

        bool ExcludeFromFacebook { get; }

        PageReference PageLink { get; }

        Guid ContentGuid { get; }

        DateTime StartPublish { get; }
        DateTime Changed { get; }

    }
}
