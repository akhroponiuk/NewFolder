using System.Linq;
using System;
using System.Web.Mvc;
using testing.Models.NewsManagement;
using testing.Constants;
using testing.Models.ViewToModel;
using System.Collections.Generic;

namespace testing.Controllers
{
    public class NewsController : Controller
    {
        private NewsManagement newsManagement = new NewsManagement();

        [HttpGet]
        public ActionResult Index(string sortOrder, int page=1)
        {
            PageInfo pageInfo = new PageInfo(PageConstants.itemsPerPage, page);
            NewsIndexModelView nivm = new NewsIndexModelView();
            nivm.PageInfo = pageInfo;



            List<News> sortedNews = new List<News>();
            if (User.IsInRole(Constants.UserRoles.AdminRoleName))
            {
                sortedNews = newsManagement.news.ToList<News>();
            }
            else
            {
                foreach (var item in newsManagement.news)
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
            nivm.News = sortedNews.Skip((page - 1) * PageConstants.itemsPerPage).Take(PageConstants.itemsPerPage).ToList<News>(); ;
            nivm.PageInfo.TotalItems = sortedNews.Count;

            return View(nivm);
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName,UserRoles.EditorRoleName)]
        public ActionResult Article(Guid id)
        {
            foreach (News item in newsManagement.news)
                if (item.Id == id)
                    return View(item);
            return Redirect("/error");
        }

        [HttpPost]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        [ValidateInput(false)]
        public ActionResult Article(News changedArticle)
        {
            changedArticle.AuthorsID = User.Identity.Name;
            newsManagement.ChangeNews(changedArticle);
            return Redirect("/");
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        public ActionResult Del(Guid id)
        {
            newsManagement.DeleteNews(id);
            return Redirect("/");
        }

        [HttpGet]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        public ActionResult Add(Guid id)
        {
            News newArticle = new News();
            newArticle.Id = id;

            return View(newArticle);
        }

        [HttpPost]
        [AuthorizeRoles(UserRoles.AdminRoleName, UserRoles.EditorRoleName)]
        [ValidateInput(false)]
        public ActionResult Add(News newArticle)
        {
            newArticle.AuthorsID = User.Identity.Name;
            newsManagement.AddNews(newArticle);
            return Redirect("/");
        }
    }
}