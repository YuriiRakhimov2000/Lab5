using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
namespace ConsoleLab5
{
    class Parser
    {
        private const int NumberOfPages = 864;
        public Parser()
        {
            BookPagesUrls = new List<string>();
        }
        public List<string> BookPagesUrls;

        public string GetBookDownloadLinkPdf(string bookUrl)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
          
            HtmlAgilityPack.HtmlDocument doc = null;
            string item = null;
            try
            {
                doc = web.Load(bookUrl); 
                 item = doc.DocumentNode.SelectSingleNode(".//span[contains(@class,'download-links')]")
                    .Descendants("a").FirstOrDefault().Attributes["href"].Value;
                 
            }
            catch (WebException)
            {
                Console.WriteLine("A WebException has been caught.");
            }
         
            
            return item;
        }
        public void GetAllBookFromPageNumber(int numberOfPage)
        {

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = null;

            try
            {
                doc = web.Load($"http://www.allitebooks.org/page/{numberOfPage}");
            }
            catch (WebException)
            {
                Console.WriteLine("A WebException has been caught.");
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes(".//article"))
            {

                string item = node.SelectSingleNode(".//*")
                    .Descendants("a").FirstOrDefault().Attributes["href"].Value;
                BookPagesUrls.Add(item);
            }

        }
        public void GetAllBookPagesUrls()
        {

            //get books href from 1 page to 5 page (40 books)
            ParallelLoopResult par = Parallel.For(1, 5, GetAllBookFromPageNumber);

            if (par.IsCompleted)
            {
                Console.WriteLine("Completed");
            }
        }

     


        

       public void DownloadAll()
        {

            WebClient Client = new WebClient();

            string bookFile = GetBookDownloadLinkPdf("http://www.allitebooks.org/firewalls-dont-stop-dragons/");
            string bookName = bookFile.Substring(37);
            string path = @"D:\F1\F2\";
             
            Client.DownloadFile(bookFile, Path.Combine(path,bookName));

        }


    }
}
