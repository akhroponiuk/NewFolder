using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using testing.Constants;
using testing.Interfaces;

namespace testing.Models.NewsManagement
{
    
    class NewsManagement: INewsManagement
    {
        public List<News> news;
        public void WriteJson()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<News>));
            using (FileStream fs = new FileStream(Paths.JsonPath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, news);
                fs.Close();
            }
        }
        public void ReadJson()
        {

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<News>));

            using (FileStream fs = new FileStream(Paths.JsonPath, FileMode.OpenOrCreate))
            {
                news = (List<News>)jsonFormatter.ReadObject(fs);
                fs.Close();
            }

        }
        public void ChangeNews(News newsToChange)
        {

            newsToChange.Date = DateTime.Now;
            
            for(int i=0; i < news.Count; i++)
            {
                if (news[i].Id == newsToChange.Id)
                    news[i] = newsToChange;
            }

            WriteJson();


        }
        public void DeleteNews(Guid id)
        {

            News[] newContext = new News[news.Count - 1];

            int index = 0;

            foreach (News item in news)
            {

                if (item.Id == id)
                {
                    id = Guid.Empty;
                    continue;
                } 
                else
                {
                    newContext[index] = item;
                    index++;
                }
            }
            news = newContext.ToList<News>();
            WriteJson();
        }
        public void AddNews(News newsToAdd)
        {

            newsToAdd.Date = DateTime.Now;

            news.Add(newsToAdd);

            WriteJson();


        }

        public NewsManagement()
        {
            ReadJson();
        }
    }
}
