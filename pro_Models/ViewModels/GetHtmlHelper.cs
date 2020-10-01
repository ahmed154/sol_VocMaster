using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace pro_Models.ViewModels
{
    public class GetHtmlHelper
    {
        public int Id { get; set; }
        public string VocText { get; set; }
        public string SiteName { get; set; }
        public string Uri { get; set; }
        public string PageContent { get; set; }
        public string Target { get; set; }
        public string Key { get; set; }
        public string KeyEnd { get; set; }
        public string SmallKey { get; set; }
        public string SmallKeyEnd { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Text { get; set; }

        public string GetHtml(string contentPage=null)
        {
            if(contentPage == null)
            {
                using (WebClient client = new WebClient())
                {
                    PageContent = client.DownloadString($"{Uri}{VocText}");
                }
            }
            else
            {
                PageContent = contentPage;
            }

            if (PageContent.Contains(Key))
            {
                StartIndex = PageContent.IndexOf(Key);
                StartIndex += Key.Length;
                Text = PageContent.Substring(StartIndex);

                EndIndex = Text.IndexOf(KeyEnd);
                Text = Text.Substring(0, EndIndex);
                Text = Text.Replace("\"", "").Trim();
            }
            return Text;
        }
    }
}
