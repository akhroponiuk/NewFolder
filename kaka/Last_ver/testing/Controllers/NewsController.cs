using System.Linq;
using System;
using System.Web.Mvc;
using System.Web;
using testing.Models;
using testing.Models.NewsManagement;
using testing.Constants;
using testing.Models.ViewToModel;
using System.Collections.Generic;

namespace testing.Controllers
{
    public class NewsController : Controller
    {
        private News[] news = NewsManagement.ReadJson();

        [HttpGet]
        public ActionResult Index(string sortOrder, int page=1)
        {
            int pageSize = 3;
            List<News> phonesPerPage = news.Skip((page - 1) * pageSize).Take(pageSize).ToList<News>();
            PageInfo pageInfo = new PageInfo(pageSize, page);
            NewsIndexModelView nivm = new NewsIndexModelView(phonesPerPage, pageInfo);



            List<News> sortedNews = new List<News>();
            if (User.IsInRole(Constants.UserRoles.AdminRoleName))
            {
                sortedNews = news.ToList<News>();
            }
            else
            {
                foreach (var item in news)
                {
                    if (item.IsVisible || User.Identity.Name == item.AuthorsID)
                    {
                        sortedNews.Add(item);
                    }
                }
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sortedNews = sortedNews.OrderByDescending(s => s.AuthorsID).ToList();
                    break;
                case "Date":
                    sortedNews = sortedNews.OrderBy(s => s.Date).ToList();
                    break;
                case "date_desc":
                    sortedNews = sortedNews.OrderByDescending(s => s.Date).ToList();
                    break;
                default:
                    sortedNews = sortedNews.OrderBy(s => s.AuthorsID).ToList();
                    break;
            }

            ArticleSorting sortingParam = new ArticleSorting();
            sortingParam.CurrentSort = sortOrder;
            sortingParam.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            sortingParam.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            nivm.ArticleSort = sortingParam;
            nivm.News = sortedNews.Skip((page - 1) * pageSize).Take(pageSize).ToList<News>();
            nivm.PageInfo.TotalItems = sortedNews.Count;
            return View(nivm);
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName,UserRoles.EditorRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Article(Guid id)
        {
            foreach (News item in news)
                if (item.ID == id)
                    return View(item);
            return Redirect("/error");
        }

        [HttpPost]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        [ValidateInput(false)]
        public ActionResult Article(News changedArticle)
        {
            changedArticle.AuthorsID = User.Identity.Name;
            NewsManagement.ChangeNews(changedArticle, news);
            return Redirect("/");
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        public ActionResult Del(Guid id)
        {
            NewsManagement.DeleteNews(news, id);
            return Redirect("/");
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Guid id)
        {
            News newArticle = new News();
            newArticle.ID = Guid.NewGuid();

            return View(newArticle);
        }

        [HttpPost]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        [ValidateInput(false)]
        public ActionResult Add(News newArticle)
        {
            newArticle.AuthorsID = User.Identity.Name;
            NewsManagement.AddNews(newArticle, news);
            return Redirect("/");
        }
    }
}