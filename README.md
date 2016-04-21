# Facebook Instant Articles module for EPiServer #
An open source module for making you pages avalible as Instant Articles on Facebook from your EPiServer CMS or Commerce site.

## Features ##
 * Creates an RSS feed that Facebook can consume making your Episerver pages avalible as Instant Articles in the Facebook mobile app.

## Installation ##
The installation is done through Visual Studio installing the nuget created from this project. 
Let your article page type implement IInstantArticlePage and add required properties to you article page type.

Example of the required mothod CreateInstantArticle(...):
```C#
        public IInstantArticle CreateInstantArticle(InstantArticleRssPage rssPage)
        {
            var instantArticle = this.CreateInstantArticleBase();
            instantArticle.Body = MainBody;
            instantArticle.Authors = GetInstantArticleAuthors();
            instantArticle.ExcludeFromFacebook = ExcludeFromFacebook;
            instantArticle.Image = Image;
            instantArticle.ImageCaption = ImageCaption;
            instantArticle.Kicker = Kicker;
            instantArticle.Subtitle = MetaDescription;
            instantArticle.Title = PageName;
            instantArticle.ArticleStyle = rssPage.DefaultArticleStyle;
            return instantArticle;
        }

        private IList<IInstantArticleAuthor> GetInstantArticleAuthors()
        {
            //Using Episerver Relations to retrive authors for this article https://github.com/BVNetwork/Relations
            var authorsPageData = EPiCode.Relations.Helpers.PageHelper.GetPagesRelated(this.ContentLink.ToPageReference(), "ArticleWriter").Cast<IInstantArticleAuthorContent>();
            var instantArticleAuthors = new List<IInstantArticleAuthor>();
            foreach (var author in authorsPageData)
            {
                instantArticleAuthors.Add(author.CreateInstantArticleAuthor());
            }
            return instantArticleAuthors;
        }
```

For images to be shown corectly inside the article (MainBody) you have to options, use:
1. Use a normal image => Inherit from ImageData and implement IInstantArticleImageFile.
2. Create an "ImageBlock" => Inherit from BlockData and implement IInstantArticleImageBlock.


Example using normal image

```C#
    public class ImageFile : ImageData, IInstantArticleImageFile
    {
        [Display(
          Name = "Image caption",
          Description = "Descriptive text for your image. May also include attribution to the originator or creator of this image.",
          Order = 10)]
        [CultureSpecific]
        public virtual string ImageCaption { get; set; }

        [Display(
          Name = "Alt text",
          Description = "Sets Alt text of the image.",
          Order = 20)]
        [CultureSpecific]
        public virtual string AltText { get; set; }

        public IInstantArticleImage CreateInstantArticleImage()
        {
            return new InstantArticleImage()
            {
                Image = this.ContentLink,
                ImageCaption = ImageCaption
            };
        }
```

Example using "ImageBlock"
```C#
    public class ImageBlock : BlockData, IInstantArticleImageBlock
    {
        [Display(
          Name = "Image",
          Description = "Reference to the image",
          Order = 10)] 
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [Display(
              Name = "Image text",
              Description = "Descriptive text for your image. May also include attribution to the originator or creator of this image.",
            Order = 20)]
        [CultureSpecific]
        public virtual string ImageCaption { get; set; }

        public IInstantArticleImage CreateInstantArticleImage()
        {
            return new InstantArticleImage()
            {
                Image = Image,
                ImageCaption = ImageCaption
            };
        }
    }
```

Create an implementation of IInstantArticleService and set it up with IOC.
Example:
```C#
 public class InstantArticleService : IInstantArticleService
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        private IContentRepository _contentRepository;

        public IEnumerable<IInstantArticlePage> GetAllInstantArticlePages()
        {
            var articles = SearchClient.Instance.Search<PageData>()
                .Filter(x => x.MatchTypeHierarchy(typeof(IInstantArticlePage)))
                .Take(1000)
                .FilterForVisitor()
                .Filter(x => ((IInstantArticlePage)x).ExcludeFromFacebook.Match(false))
                .OrderByDescending(x => x.StartPublish)
                .GetContentResult();

              //  UpdateModifydate(articles);

            return articles.Cast<IInstantArticlePage>();
        }

        private void UpdateModifydate(IContentResult<PageData> contentResult)
        {
            foreach (var item in contentResult.Items)
            {
                var page = _contentRepository.Get<PageData>(item.ContentLink).CreateWritableClone();
                page["PageChangedOnPublish"] = true;
                page.Changed = DateTime.Now;
                _contentRepository.Save(page, SaveAction.Publish, AccessLevel.NoAccess);
            }
        }

        public InstantArticleRssPage GetInstantArticleRssPage()
        {
            var InstantArticleRssPageRef = _contentRepository.Get<StartPage>(ContentReference.StartPage).InstantArticleRssPage;

            try
            {
                return _contentRepository.Get<InstantArticleRssPage>(InstantArticleRssPageRef);
            }
            catch (Exception exception)
            {
                logger.Error("Please specify Instant Article RSS page on start page under Site settins", exception);
                throw;
            }
        }
        }
    }
```

