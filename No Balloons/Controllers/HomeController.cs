using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using No_Balloons.Models;
using System.Text.RegularExpressions;
using System.Net;
using System.Web.Script.Serialization;


namespace No_Balloons.Controllers
{
    public class HomeController : Controller
    {        
        private List<No_Balloons.Models.Article> articles;
        
        private Model1 model1;
        private List<Article> articlesMatching;
        private List<Article> curPage;
        private int pages;
        private int page;

        /* Creates a list of articles, taken from the database.
         * The list is sorted in descending order by date and the first 10 are returned to the view.
         * Weather details are retrieved by calling WeatherDetail. The weather details are passed to the view by a viewbag.
         
         */
        public ActionResult Index()
        {       
            articles = new List<No_Balloons.Models.Article>();
            List<No_Balloons.Models.Article> articlesToSort = new List<Article>();
            model1 = new Model1();          

            foreach (Article article in model1.Articles)
            {
                articlesToSort.Add(article);
            }

            articlesToSort.Sort(new DateComparator());
            
            bool addedMain = false;
            for (int i = 0; i < articlesToSort.Count; i++)
            {
                if (addedMain == false && articlesToSort[i].Main == "1")
                {
                    articles.Add(articlesToSort[i]);
                    addedMain = true;
                    articlesToSort.RemoveAt(i);
                    i = 0;
                }
            }
            int size = 10;
            if(articlesToSort.Count < 10)
            {
                size = articlesToSort.Count;
            }
            for (int i = 0; i < size; i++)
            {
                articles.Add(articlesToSort[i]);
            }
            
            ViewBag.Weather = WeatherDetail();
            return View(articles);
        }

        public ActionResult Weather()
        { 
            return View();
        }

        // Passes all the articles from the database to the partial view search.
        public ActionResult Search()
        {
            articles = new List<Article>() ;
            foreach (Article article in model1.Articles)
            {
                articles.Add(article);
            }
            return View(articles);
        }
        

     
        [HttpPost]
        public ActionResult SearchArticles()
        {           
            model1 = new Model1();
            string title = Request.Form["title"];       //Gets the user input. Will be refered as keyword.
            if(title != null)
            {
                articlesMatching = new List<Article>();
                Regex regex = new Regex(@title, RegexOptions.IgnoreCase);

                foreach (Article obj in model1.Articles)
                {
                    if (regex.IsMatch(obj.Title))          //Checks by using regular expressions if the article's title contains the user's keyword
                    {
                        articlesMatching.Add(obj);         //Since matching, add to the list of search results.
                    }
                }
            }
            
           
            int count = articlesMatching.Count();
            const int PageSize = 10; 
           
            var data = articlesMatching.Skip(page * PageSize).Take(PageSize).ToList();        // Used to display 10 articles at a time.

           
            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            ViewBag.keyword = title;
            return View("SearchResults", data);
           
        }

             
        // See the method above's comments.
        public ActionResult SearchResults(string keyword, int page = 0)
        {
            articlesMatching = new List<Article>();
            Regex regex = new Regex(@keyword, RegexOptions.IgnoreCase);
            model1 = new Model1();
            foreach (Article obj in model1.Articles)
            {
                if (regex.IsMatch(obj.Title))
                {
                    articlesMatching.Add(obj);
                }
            }          
            
            const int PageSize = 10; 
            
            int count = articlesMatching.Count();

            var data = articlesMatching.Skip(page * PageSize).Take(PageSize).ToList();

            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            ViewBag.keyword = keyword;
            ViewBag.Page = page;
            return View(data);
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }      

        public ActionResult Article()
        {
            model1 = new Models.Model1();

            String titleValue = Request.QueryString["title"];        //Get the url passed.

            Regex regex = new Regex(@"%");
            titleValue = regex.Replace(titleValue, " ");            //Replace any '%' symbols with a space.

            regex = new Regex(@".cshtml");
            titleValue = regex.Replace(titleValue, "");             //Remove .cshtml from the link, if it exists.

            Article article;
            if (titleValue.Length > 0)
            {               
                article = model1.Articles.Find(titleValue);        //Search for the article and asign it to article.
                return View(article);                              // Return the article to the view.
            } 
            return View();
        }

        /* Used to get data about the users ip address
         * Makes use of a free API from ipstack.com*/
        public string Location()
        {
            string ipAddress = getIpAddress();
            string yourApiKEy = ""; // Place your ipstack.com api key here.
            string url = string.Format("http://api.ipstack.com/{0}?access_key={1}&format=1&fields=main",ipAddress,yourApiKEy);

            using (WebClient client = new WebClient())
            {
                string json;
                try
                {
                    json = client.DownloadString(url);     // Try to get the ip address data
                }
                catch (WebException e)
                {
                    return "Cape Town";                    // If attempt fails, return default Cape Town.
                }

                RootLocationObject rootLocationObject= (new JavaScriptSerializer()).Deserialize<RootLocationObject>(json);  // Turn json into custom model object.
                ResultLocationModel rslt = new ResultLocationModel();
               
                string city = rootLocationObject.city;

                if (city != null && city.Length > 0)
                {
                    return city;
                }
                else return "Cape Town";

            }             
        }

        // Get the user's ip address.
        public string getIpAddress()
        {         
                string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return ipAddress;
        }

        /* Used to get weather data based on the user's city.
        *  Makes use of a free API from openweathermap.org*/
        public No_Balloons.Models.ResultViewModel WeatherDetail()
        {
            string City = Location();
            
            //Place your API KEY from OPENWEATHERMAP.ORG here
            string appId = "";
           
            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);
            string json;
            using (WebClient client = new WebClient())
            {     
                try
                {
                    json = client.DownloadString(url);  // Try to get the weather details from openweathermap.org
                }
                catch (WebException e)  // If no details for user's city, use default Cape Town.
                {
                    City = "Cape Town";
                    url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);
                    json = client.DownloadString(url);
                }

                //Converting to OBJECT from JSON string.  
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                // Custome view model
                ResultViewModel resultViewModel = new ResultViewModel();

                resultViewModel.Country = weatherInfo.sys.country;
                resultViewModel.City = weatherInfo.name;
                resultViewModel.Lat = Convert.ToString(weatherInfo.coord.lat);
                resultViewModel.Lon = Convert.ToString(weatherInfo.coord.lon);
                resultViewModel.Description = weatherInfo.weather[0].description;
                resultViewModel.Humidity = Convert.ToString(weatherInfo.main.humidity);
                resultViewModel.Temp = Convert.ToString(weatherInfo.main.temp);
                resultViewModel.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                resultViewModel.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                resultViewModel.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                resultViewModel.WeatherIcon = "http://openweathermap.org/img/w/"+weatherInfo.weather[0].icon+".png";
            
                return resultViewModel;
            }

        }
      
    }
}