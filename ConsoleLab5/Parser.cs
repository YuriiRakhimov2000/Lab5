using System;
using System.Collections.Generic;
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
        public List<String> BookPagesUrls;

        public string GetBookDownloadLinkPdf(string bookUrl)
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
          
            HtmlAgilityPack.HtmlDocument doc = null;
            string item = null;
            
                doc = web.Load(bookUrl); //From 30 links, usually only 10 load properly
                 item = doc.DocumentNode.SelectSingleNode(".//span[contains(@class,'download-links')]")
                    .Descendants("a").FirstOrDefault().Attributes["href"].Value;
                 
            

            return item;
        }
        public void GetAllBookFromPageNumber(int numberOfPage)
        {

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

            HtmlAgilityPack.HtmlDocument doc = null;

            doc = web.Load($"http://www.allitebooks.org/page/{numberOfPage}");

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes(".//article"))
            {

                string item = node.SelectSingleNode(".//*")
                    .Descendants("a").FirstOrDefault().Attributes["href"].Value;
                BookPagesUrls.Add(item);
            }

        }
        public void GetAllBookPagesUrls()
       {
          
            ParallelLoopResult par =  Parallel.For(1, 5,GetAllBookFromPageNumber);
          
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
            string path = "D:\\F1\\F2\\";
            Client.DownloadFile(bookFile, string.Concat(path, bookName));

        }


    }
}
