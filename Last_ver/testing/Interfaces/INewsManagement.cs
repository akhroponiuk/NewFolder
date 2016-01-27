using testing.Models.NewsManagement;
using System;

namespace testing.Interfaces
{
    interface INewsManagement
    {
        void ReadJson();
        void WriteJson();
        void ChangeNews(News newsToChange);
        void DeleteNews(Guid id);
        void AddNews(News newsToAdd);

    }
}
