using No_Balloons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace No_Balloons.Controllers
{
    public class DateComparator : Comparer<No_Balloons.Models.Article>
    {
        public override int Compare(Article x, Article y)
        {
            if(x.ArticleDate < y.ArticleDate)
            {
                return 1;
            }
            else if(x.ArticleDate == y.ArticleDate)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}