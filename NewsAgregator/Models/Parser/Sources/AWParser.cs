﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;

namespace NewsAgregator.Models.Parser.Sources
{
    /// <summary>
    /// Парсер новостей с сайта "Авиация России".
    /// </summary>
    public class AWParser : IParser<List<News>>
    {
        public string BaseUrl { get; set; } = "https://aviation21.ru/category/novosti-aviacii/";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            IEnumerable<IElement> items = document.QuerySelectorAll("article"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                // Заполнение полей новости.
                News news = new News() 
                {
                    Title = item.QuerySelector("h2").TextContent,
                    ImageSrc = item.QuerySelector("img").GetAttribute("src"),
                    Text = item.QuerySelector("p").TextContent,
                    NewsURL = item.QuerySelector("a").GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("time").GetAttribute("datetime"))
                };
                newsList.Add(news);
            }
            return newsList;
        }
    }
}