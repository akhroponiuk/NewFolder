using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using testing.Constants;

namespace testing.Models.NewsManagement
{
    
    class NewsManagement
    {
        public static void WriteJson(News[] newsContext)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(News[]));
            using (FileStream fs = new FileStream(Paths.JsonPath, FileMode.Create))
            {

                jsonFormatter.WriteObject(fs, newsContext);

                fs.Close();

            }
        }
        public static News[] ReadJson()
        {

            News[] readContext;

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(News[]));

            using (FileStream fs = new FileStream(Paths.JsonPath, FileMode.OpenOrCreate))

            {
                readContext = (News[])jsonFormatter.ReadObject(fs);

                fs.Close();

            }

            return readContext;
        }
        public static News[] ChangeNews(News newsToChange, News[] prevNews)
        {

            newsToChange.Date = DateTime.Now;

            for (int i = 0; i < prevNews.Length; i++)
            {
                if (prevNews[i].ID == newsToChange.ID)

                    prevNews[i] = newsToChange;
            }

            WriteJson(prevNews);

            return prevNews;

        }
        public static void DeleteNews(News[] news, Guid id)
        {

            News[] newContext = new News[news.Length - 1];

            int index = 0;

            foreach (News element in news)
            {

                if (element.ID == id)
                {
                    id = Guid.Empty;
                    continue;
                } 
                else
                {
                    newContext[index] = element;

                    index++;
                }
            }

            WriteJson(newContext);
        }
        public static News[] AddNews(News newsToAdd, News[] prevNews)
        {

            newsToAdd.Date = DateTime.Now;

            News[] newNews = new News[prevNews.Length + 1];

            int i = 0;

            foreach (News p in prevNews)
            {

                newNews[i] = prevNews[i];

                i++;

            }

            newNews[prevNews.Length] = newsToAdd;

            WriteJson(newNews);

            return prevNews;

        }
    }
}
