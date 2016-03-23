using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace BVNetwork.InstantArticles.Models.Pages
{
    [ContentType(DisplayName = "Instant Article RSS Page", GUID = "4d135fa2-e22d-45b1-9ad3-a1e67c57232e", Description = "Facebook instant article RSS feed")]
    public class RssPage : PageData
    {

        [Display(Name = "RSS feed title", Description = "The title of the RSS feed. For example your organization name.", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual string RssTitle { get; set; }

        [Display(Name = "RSS feed description", Description = "A short description of the RSS feed", GroupName = SystemTabNames.Content, Order = 30)]
        [UIHint(UIHint.Textarea)]
        public virtual string RssDescription { get; set; }

        [Display(Name = "Article style", Description = "The default style to be used for articles in this feed", GroupName = SystemTabNames.Content, Order = 40)]
        public virtual string ArticleStyle { get; set; }



    }
}