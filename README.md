# Facebook Instant Articles module for EPiServer #
An open source module for making you pages avalible as Instant Articles on Facebook from your EPiServer CMS or Commerce site.

## Features ##
 * Creates an RSS feed that Facebook can consume making your Episerver pages avalible as Instant Articles in the Facebook mobile app.

## Installation ##
The installation is done through Visual Studio installing the nuget created from this project. 

Let your article page type implement IInstantArticlePage



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


