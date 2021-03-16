using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace No_Balloons.Models
{
    public class SearchResultModel
    {
        public List<Article> articleList{get;set;}

        public void ArticleSet(params Article[] articles)
        {
            articleList = new List<Article>();
            foreach (var article in articles)
            {
                articleList.Add(article);
            }
        }

    }
}