using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace pro_Models.Helper
{
    public static class Helper
    {
        public static string CdnUri { get; set; }
        static Helper()
        {
            CdnUri  = @"https://VocMasterPullZone.b-cdn.net/";
        }

        #region Pro
        static string Reformat(string txt)
        {
            txt = txt.Replace("<font face=\"comic sans ms\">", "");
            txt = txt.Replace("</font>", "");
            txt = txt.Replace("<i>", "");
            txt = txt.Replace("</i>", "");
            txt = txt.Replace("- ", " ");
            txt = txt.Replace("?", "");
            txt = txt.Replace("!", "");
            txt = txt.Replace(", ", " ");
            txt = txt.Replace(".", "");
            txt = txt.Replace("<i>", "");
            txt = txt.Replace("</i>", "");
            txt = txt.Replace("-", "");
            txt = txt.Replace(" '", " ");
            txt = txt.Replace("(laughter)", "");

            return txt;
        }
        #endregion

        public static async Task<List<Subtitle>> ProduceSubtitlesFromText(string movieId)
        {
            string Text = "";
            decimal start = -1; decimal end = 0;
            string txt = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    Text = client.DownloadString($"https://video.google.com/timedtext?lang=en&v={movieId}");
                }
                catch (Exception ex)
                {

                }
            }

            Text = Text.ToLower();
            Text = Text.Replace("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", "");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Text);

            XmlNodeList xnList = xml.SelectNodes("/transcript/text");

            List<Subtitle> Subtitles = new List<Subtitle>();
            foreach (XmlNode xn in xnList)
            {
                if (string.IsNullOrEmpty(xn.InnerText) || xn.Attributes["start"] == null || xn.Attributes["dur"] == null) continue;

                xn.InnerText = Reformat(xn.InnerText); if (txt == "") start = Convert.ToDecimal(xn.Attributes["start"].Value);
                if (txt != "") txt += " ";
                txt += xn.InnerText;
                if (txt.Length < 50) continue;

                end = Convert.ToDecimal(xn.Attributes["start"].Value) + Convert.ToDecimal(xn.Attributes["dur"].Value); 

                Subtitles.Add(new pro_Models.Models.Subtitle
                {
                    Text = txt,
                    StartTime = start,
                    EndtTime = end
                });
                txt = "";
            }

            return Subtitles;
        }
    }
}
