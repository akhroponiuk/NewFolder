using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.Models.NewsManagement;

namespace testing.Models.ViewToModel
{
    public class ArticleSorting
    {
        public string CurrentSort { get; set; }
        public string NameSortParm { get; set; }
        public string DateSortParm { get; set; }

        public ArticleSorting() { }

        public ArticleSorting(string currentSort, string date, string name)
        {
            CurrentSort = currentSort;
            NameSortParm = name;
            DateSortParm = date;
        }

    }
    public class PageInfo
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
        public PageInfo(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
    public class NewsIndexModelView
    {
        public ArticleSorting ArticleSort { get; set; }
        public List<News> News { get; set; }
        public PageInfo PageInfo { get; set; }
        public NewsIndexModelView(IEnumerable<News> news, PageInfo pageInfo)
        {
            News = news.ToList<News>();
            PageInfo = pageInfo;
        }
    }
}
