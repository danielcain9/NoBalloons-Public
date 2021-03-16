using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using No_Balloons.Models;

namespace No_Balloons.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Article()
        {
            Model1 model1 = new Model1();


            String titleValue = Request.QueryString["title"];        //Get url
            if (titleValue == null)
            {
                return RedirectToAction("Index", "Home");  // If the url has no parameters, redirect to home page.
            }

            Regex regex = new Regex(@"%");
            titleValue = regex.Replace(titleValue, " ");     // Replace all '%' with a space

            regex = new Regex(@".cshtml");
            titleValue = regex.Replace(titleValue, "");         //Remove .cshtml from the link, if it exists.

            List<Article> articleList = new List<Article>();
            int i = 0;

            // Add first 11 articles to list
            foreach (Article obj in model1.Articles)
            {
                if (i < 11)
                {
                    articleList.Add(obj);
                }
                else
                {
                    break;
                }
                i++;
            }

            Article article;
            if (titleValue.Length > 0)
            {
                
                article = model1.Articles.Find(titleValue);
                if (article != null) // If the url matches an article.
                {                    
                    ViewBag.Articles = articleList;  // add the 11 articles to a viewbag.
                    return View(article);            // return the matching article to the view.
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Horoscope()
        {
            Model1 model1 = new Model1();
            
            return View(model1.Horoscopes); // Return set of horoscopes from database to view.
        }
    }
}