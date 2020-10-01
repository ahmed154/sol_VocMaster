using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using pro_Models.Models;
using pro_Models.ViewModels;
using pro_Server.Helpers;
using pro_Server.Pages.Voc;
using pro_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace pro_Server.Pages
{
    public class MainBase : ComponentBase
    {
        #region Declarations
        [Inject] public IUserVocService UserVocService { get; set; }
        [Inject] public IVocMasterService VocMasterService { get; set; }
        [Inject] public IImageService ImageService { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public IMyService MyService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public VocMasterVM VocMasterVM { get; set; } = new VocMasterVM();
        public VocMasterVM VocMasterVMUpdate { get; set; } = new VocMasterVM();
        public string UserName { get; set; }

        public bool bol_New { get; set; }
        public bool bol_Study { get; set; }
        public bool bol_Test { get; set; }
        public bool bol_Results { get; set; }

        public bool bol_AddImage { get; set; }
        #endregion
        protected override async Task OnInitializedAsync()
        {
            //UserName = await JSRuntime.GetFromLocalStorage("Email");

            //if (!string.IsNullOrWhiteSpace(UserName))
            //{
            //    await GetVocMasterVM();
            //}
            
        }

        #region Pro
        public async Task GetVocMasterVM()
        {
            VocMasterVM = await VocMasterService.GetVocMasterVM(new UserNameVM { UserName = UserName });
            VocMasterVM.Results = VocMasterVM.Results.OrderBy(x => x.NextReviewTime).ToList();
        }
        public async Task OffSwiches()
        {
            bol_New = false;
            bol_Study = false;
            bol_Test = false;
            bol_Results = false;

            await UpdateVocMasterVM();
        }
        public async Task UpdateVocMasterVM()
        {
            if (VocMasterVMUpdate.News.Count == 0 && VocMasterVMUpdate.Studys.Count == 0 && VocMasterVMUpdate.Tests.Count == 0) return;
            
            VocMasterVMUpdate.UserInfo.UserName = UserName;
            await VocMasterService.UpdateVocMasterVM(VocMasterVMUpdate);
            VocMasterVMUpdate.News.Clear(); VocMasterVMUpdate.Studys.Clear(); VocMasterVMUpdate.Tests.Clear();
        }
        #endregion

        #region New
        #region Pro

        #endregion
        public async Task PickNew()
        {
            await OffSwiches(); bol_New = true;
        }
        public async Task PickNewYes()
        {
            VocMasterVMUpdate.UserInfo.LastVocId = VocMasterVM.News[0].Voc.Id;
            VocMasterVM.News.RemoveAt(0);

            if (VocMasterVM.News.Count == 0)
            {
                if (VocMasterVM.Studys.Count < 3)
                {
                    //await GetPickNewVMs();
                }
                await UpdateVocMasterVM();
            }
        }
        public async Task PickNewNo()
        {
            VocMasterVM.News[0].UserVoc.VocId = VocMasterVM.News[0].Voc.Id;
            VocMasterVM.News[0].UserVoc.Voc = VocMasterVM.News[0].Voc;
            VocMasterVM.News[0].UserVoc.NextReviewTime = DateTime.UtcNow;

            VocMasterVM.Studys.Add(VocMasterVM.News[0]);
            VocMasterVM.Results.Add(VocMasterVM.News[0].UserVoc);

            VocMasterVMUpdate.UserInfo.LastVocId = VocMasterVM.News[0].Voc.Id;
            VocMasterVMUpdate.News.Add(VocMasterVM.News[0]);

            VocMasterVM.News.RemoveAt(0);

            if (VocMasterVM.News.Count == 0)
            {
                await UpdateVocMasterVM();
                if (VocMasterVM.Studys.Count < 3)
                {
                    await GetVocMasterVM();
                }
            }
        }
        #endregion

        #region Study
        #region Declarations
        public List<GetHtmlHelper> GetHtmlHelper { get; set; } = new List<GetHtmlHelper>();

        public List<string> stringList { get; set; } = new List<string>();
        public List<Dif> Difs { get; set; } = new List<Dif>();
        public pro_Models.ViewModels.ImageVM ImageVM { get; set; } = new pro_Models.ViewModels.ImageVM();
        public List<string> Synonyms { get; set; } = new List<string>();
        public List<string> Translation { get; set; } = new List<string>();
        public string phon { get; set; }

        List<Definition> definitions = new List<Definition>();

        public class Dic
        {
            public string DicName { get; set; }
            public string URL { get; set; }
            public string Element { get; set; }
        }
        public class Meaning
        {
            public string Dic { get; set; }
            public string Word { get; set; }
            public int Rank { get; set; } = 1;
            public string URL { get; set; }
            public string AudioFileUrl { get; set; }
        }
        public int Pre_Clicked_Id;
        public int Clicked_Id;
        #endregion

        #region Pro
        public async Task FillGetHtmlHelper(VocCardVM vocCardVM)
        {
            GetHtmlHelper.Clear();
            GetHtmlHelper.Add(new GetHtmlHelper()
            {
                Id = 0,
                VocText = vocCardVM.Voc.Text,
                Uri = "https://www.oxfordlearnersdictionaries.com/definition/english/",
                Key = "sound audio_play_button pron-us icon-audio\" data-src-mp3=\"",
                KeyEnd = "data-src-ogg",
                SmallKey = "class=\"phon\">",
                SmallKeyEnd = "</span>",
                SiteName = "Oxford",
                Target = "Pronouciation"
            });

            GetHtmlHelper.Add(new GetHtmlHelper()
            {
                Id = 1,
                VocText = vocCardVM.Voc.Text,
                Uri = "https://dictionary.cambridge.org/dictionary/english/",
                Key = "def ddef_d db\">",
                KeyEnd = "</div>",
                SmallKey = ">",
                SmallKeyEnd = "<",
                SiteName = "Cambridg",
                Target = "Difinition"
            });
            GetHtmlHelper.Add(new GetHtmlHelper()
            {
                Id = 2,
                VocText = vocCardVM.Voc.Text,
                Uri = "https://dictionary.cambridge.org/dictionary/english/",
                Key = "examp dexamp\">",
                KeyEnd = "</span></div>",
                SmallKey = ">",
                SmallKeyEnd = "<",
                SiteName = "Cambridg",
                Target = "Examples"
            });
            GetHtmlHelper.Add(new GetHtmlHelper()
            {
                Id = 3,
                VocText = vocCardVM.Voc.Text,
                Uri = "https://www.thesaurus.com/browse/",
                Key = "class=\"css-18rr30y etbu2a31\">",
                KeyEnd = "</a>",
                SiteName = "Thesurus",
                Target = "Synonyms"
            });
            GetHtmlHelper.Add(new GetHtmlHelper()
            {
                Id = 4,
                VocText = vocCardVM.Voc.Text,
                Uri = "https://dictionary.cambridge.org/dictionary/english-arabic/",
                Key = "<span class=\"trans dtrans dtrans-se \" lang=\"ar\">",
                KeyEnd = "</span>",
                SiteName = "Cambridg",
                Target = "ArabicTranslation"
            });
        }
        public async Task GetAudioUri()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    GetHtmlHelper[0].PageContent = client.DownloadString($"{GetHtmlHelper[0].Uri}{GetHtmlHelper[0].VocText}");
                }
                catch (Exception)
                {
                }
            }

            if (GetHtmlHelper[0].PageContent != null && GetHtmlHelper[0].PageContent.Contains(GetHtmlHelper[0].Key))
            {
                GetHtmlHelper[0].StartIndex = GetHtmlHelper[0].PageContent.IndexOf(GetHtmlHelper[0].Key) + GetHtmlHelper[0].Key.Length;
                GetHtmlHelper[0].Text = GetHtmlHelper[0].PageContent.Substring(GetHtmlHelper[0].StartIndex);

                GetHtmlHelper[0].EndIndex = GetHtmlHelper[0].Text.IndexOf(GetHtmlHelper[0].KeyEnd);
                GetHtmlHelper[0].PageContent = GetHtmlHelper[0].Text;

                GetHtmlHelper[0].Text = GetHtmlHelper[0].Text.Substring(0, GetHtmlHelper[0].EndIndex);
                GetHtmlHelper[0].Text = GetHtmlHelper[0].Text.Replace("\"", "").Trim();
            }
            if (GetHtmlHelper[0].PageContent != null && GetHtmlHelper[0].PageContent.Contains(GetHtmlHelper[0].SmallKey))
            {
                GetHtmlHelper[0].StartIndex = GetHtmlHelper[0].PageContent.IndexOf(GetHtmlHelper[0].SmallKey) + GetHtmlHelper[0].SmallKey.Length;
                phon = GetHtmlHelper[0].PageContent.Substring(GetHtmlHelper[0].StartIndex);

                GetHtmlHelper[0].EndIndex = phon.IndexOf(GetHtmlHelper[0].SmallKeyEnd);
                phon = phon.Substring(0, GetHtmlHelper[0].EndIndex);
            }
        }
        public async Task<Dif> GetCambText(string pageContent, string bigKeyStart, string bigKeyEnd, string smallKeyStart, string smallKeyEnd)
        {
            int contentPageStart, contentPageEnd, start, end;
            string txt = pageContent;

            contentPageStart = pageContent.IndexOf(bigKeyStart) + bigKeyStart.Length;
            contentPageEnd = (pageContent.Substring(contentPageStart).Contains(bigKeyEnd)) ? pageContent.Substring(contentPageStart).IndexOf(bigKeyEnd) : pageContent.Length - contentPageStart;

            txt = txt.Substring(contentPageStart, contentPageEnd);

            stringList.Clear();
            if (txt.Contains(smallKeyEnd))
            {
                stringList.Add(txt.Substring(0, txt.IndexOf(smallKeyEnd)));
                txt = txt.Substring(txt.IndexOf(smallKeyEnd));
            }
            else
            {

            }


            while (txt.Contains(smallKeyStart))
            {
                start = txt.IndexOf(smallKeyStart) + smallKeyStart.Length;
                end = (txt.Substring(start).Contains(smallKeyEnd)) ? txt.Substring(start).IndexOf(smallKeyEnd) : txt.Length - start;

                stringList.Add(txt.Substring(start, end));

                txt = txt.Substring(start + end);
            }

            txt = "";
            foreach (var item in stringList)
            {
                txt += item + " ";
            }

            pageContent = pageContent.Substring(contentPageStart + contentPageEnd);
            return new Dif { Text = txt, PageContent = pageContent };

        }
        public async Task GetSynonyms()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    GetHtmlHelper[3].PageContent = client.DownloadString($"{GetHtmlHelper[3].Uri}{GetHtmlHelper[3].VocText}");

                }
                catch (Exception)
                {

                }
            }

            Synonyms.Clear();
            while (GetHtmlHelper[3].PageContent != null && GetHtmlHelper[3].PageContent.Contains(GetHtmlHelper[3].Key))
            {
                GetHtmlHelper[3].Text = "";

                GetHtmlHelper[3].StartIndex = GetHtmlHelper[3].PageContent.IndexOf(GetHtmlHelper[3].Key) + GetHtmlHelper[3].Key.Length;
                GetHtmlHelper[3].Text = GetHtmlHelper[3].PageContent.Substring(GetHtmlHelper[3].StartIndex);

                GetHtmlHelper[3].EndIndex = GetHtmlHelper[3].Text.IndexOf(GetHtmlHelper[3].KeyEnd);

                GetHtmlHelper[3].PageContent = GetHtmlHelper[3].Text;

                GetHtmlHelper[3].Text = GetHtmlHelper[3].Text.Substring(0, GetHtmlHelper[3].EndIndex);

                Synonyms.Add(GetHtmlHelper[3].Text.Trim());
            }
        }
        public async Task GetTranslation()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    GetHtmlHelper[4].PageContent = client.DownloadString($"{GetHtmlHelper[4].Uri}{GetHtmlHelper[4].VocText}");

                }
                catch (Exception)
                {
                }
            }

            Translation.Clear();
            while (GetHtmlHelper[4].PageContent != null && GetHtmlHelper[4].PageContent.Contains(GetHtmlHelper[4].Key))
            {
                GetHtmlHelper[4].Text = "";

                GetHtmlHelper[4].StartIndex = GetHtmlHelper[4].PageContent.IndexOf(GetHtmlHelper[4].Key) + GetHtmlHelper[4].Key.Length;
                GetHtmlHelper[4].Text = GetHtmlHelper[4].PageContent.Substring(GetHtmlHelper[4].StartIndex);

                GetHtmlHelper[4].EndIndex = GetHtmlHelper[4].Text.IndexOf(GetHtmlHelper[4].KeyEnd);

                GetHtmlHelper[4].PageContent = GetHtmlHelper[4].Text;

                GetHtmlHelper[4].Text = GetHtmlHelper[4].Text.Substring(0, GetHtmlHelper[4].EndIndex);

                Translation.Add(GetHtmlHelper[4].Text.Trim());
            }
        }
        string RemoveTashkeel(string str)
        {
            str = str.Replace("\u064b", "");
            str = str.Replace("\u064f", "");
            str = str.Replace("\u064c", "");
            str = str.Replace("\u0652", "");
            str = str.Replace("\u064d", "");
            str = str.Replace("\u0650", "");
            str = str.Replace("\u0651", "");
            str = str.Replace("\u064e", "");

            return str;
        }
        string Reformat(string txt)
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

            return txt;
        }
        public async Task GetFromCambridgeDic(string word)
        {
            //var webGet = new HtmlWeb();
            //var doc = new HtmlDocument();
            string strPage;
            List<Dic> DicsList = new List<Dic>();

            List<Meaning> DicMeanList = new List<Meaning>();

            int StartIndex, EndIndex = 0;
            string txt;
            bool Exsist = false;

            DicsList.Add(new Dic { DicName = "Cambridge", URL = "https://dictionary.cambridge.org/dictionary/english-arabic/", Element = "<span class=\"trans dtrans dtrans-se \" lang=\"ar\">" });
            //DicsList.Add(new Dic { DicName = "Reverso", URL = "https://dictionary.reverso.net/english-arabic/", Element = "<span id=\"translationName\">" });
            DicsList.Add(new Dic { DicName = "Glosbe", URL = "https://en.glosbe.com/en/ar/", Element = "<strong class=\"nobold phr\">" });
            DicsList.Add(new Dic { DicName = "dict", URL = "https://www.dict.com/arabic-english/", Element = "<span class='lex_ful_rr'>" });
            //DicsList.Add(new Dic { DicName = "bab.la", URL = "https://en.bab.la/dictionary/english-arabic/", Element = "href='/dictionary/arabic-english/" });



            foreach (var item in DicsList)
            {
                item.URL = item.URL + word;
                using (WebClient client = new WebClient())
                {
                    strPage = client.DownloadString(item.URL);
                }

                while (strPage.Contains(item.Element))
                {
                    StartIndex = strPage.IndexOf(item.Element);
                    strPage = strPage.Substring(StartIndex + item.Element.Length).Trim();

                    if (item.DicName == "bab.la")
                    {
                        EndIndex = strPage.IndexOf("'");
                    }
                    else
                    {
                        EndIndex = strPage.IndexOf("<");
                    }

                    txt = strPage.Substring(0, EndIndex);
                    txt = RemoveTashkeel(txt).Trim();


                    Exsist = false;
                    foreach (var obj in DicMeanList)
                    {
                        if (obj.Word == txt && obj.Dic == item.DicName)
                        {
                            Exsist = true;
                        }
                    }

                    if (Exsist == false && txt != "")
                    {
                        DicMeanList.Add(new Meaning { Dic = item.DicName, Word = txt, URL = item.URL });
                    }
                }
            }

            int x = 0;
            for (int i = 0; i < DicMeanList.Count; i++)
            {
                for (x = i + 1; x < DicMeanList.Count; x++)
                {
                    if (DicMeanList[i].Word == DicMeanList[x].Word)
                    {
                        DicMeanList[i].Rank++;
                    }
                }
            }

            DicMeanList = DicMeanList.OrderByDescending(r => r.Rank).ToList();

            Translation.Clear();
            foreach (var dic in DicMeanList)
            {
                if (!Translation.Contains(dic.Word)) Translation.Add(dic.Word);
            }
        }
        public async Task FillDataBaseVocCard()
        {
            VocCardVM vocCardVM = new VocCardVM();

            List<Synonym> synonyms = new List<Synonym>();
            List<Translate> translates = new List<Translate>();

            foreach (var newVocCard in VocMasterVM.News)
            {
                await CamDifHandlerForDatabase(newVocCard);

                synonyms.Clear();
                foreach (var syn in Synonyms)
                {
                    synonyms.Add(new Synonym { Text = syn, VocId = newVocCard.Voc.Id });
                }
                translates.Clear();
                foreach (var tran in Translation)
                {
                    translates.Add(new Translate { Text = tran, VocId = newVocCard.Voc.Id });
                }

                vocCardVM.Voc = newVocCard.Voc;
                vocCardVM.VocAudios = new List<VocAudio> { new VocAudio { Uri = GetHtmlHelper[0].Text, Phon = phon, VocId = newVocCard.Voc.Id } };
                vocCardVM.Definitions = definitions;
                vocCardVM.Synonyms = synonyms;
                vocCardVM.Translates = translates;

                await UserVocService.AddVocCardVM(vocCardVM);
            }
        }
        public async Task CamDifHandlerForDatabase(VocCardVM vocCardVM)
        {
            await FillGetHtmlHelper(vocCardVM);
            await GetAudioUri();
            await GetSynonyms();
            //await GetTranslation();
            await GetFromCambridgeDic(vocCardVM.Voc.Text);

            //int contentPageStart, contentPageEnd, start, end;
            string pageContent, txt;
            using (WebClient client = new WebClient()) pageContent = client.DownloadString($"{GetHtmlHelper[1].Uri}{GetHtmlHelper[1].VocText}");

            txt = pageContent;
            Difs.Clear();
            definitions.Clear();
            while (pageContent.Contains(GetHtmlHelper[1].Key))
            {
                Dif dif = new Dif();
                var x = await GetCambText(pageContent, GetHtmlHelper[1].Key, GetHtmlHelper[1].KeyEnd, GetHtmlHelper[1].SmallKey, GetHtmlHelper[1].SmallKeyEnd);
                dif.Text = x.Text;

                //if(string.IsNullOrEmpty(dif.Text)) definitions.Add(new Definition { Text = dif.Text, VocId = Voc.Id});

                pageContent = x.PageContent;

                while (pageContent.Contains(GetHtmlHelper[2].Key) && pageContent.IndexOf(GetHtmlHelper[2].Key) < pageContent.IndexOf(GetHtmlHelper[1].Key))
                {
                    var y = await GetCambText(pageContent, GetHtmlHelper[2].Key, GetHtmlHelper[2].KeyEnd, GetHtmlHelper[2].SmallKey, GetHtmlHelper[2].SmallKeyEnd);
                    dif.Examples.Add(y.Text);

                    //if (definitions.Count > 0) definitions[definitions.Count - 1].Examples.Add(new Example { Text = y.Text, VocId = Voc.Id});

                    pageContent = y.PageContent;
                }
                Difs.Add(dif);
                if (!string.IsNullOrEmpty(dif.Text))
                {
                    List<Example> examples = new List<Example>();
                    foreach (var ex in dif.Examples)
                    {
                        examples.Add(new Example { Text = ex.Trim(), VocId = vocCardVM.Voc.Id });
                    }
                    definitions.Add(new Definition { Text = dif.Text.Trim(), Examples = examples, VocId = vocCardVM.Voc.Id });
                }
            }

        }

        public TimeSpan TimeLeft;
        public TimeSpan Diffrence;
        string displayText = "";

        bool show = false;
        void Start()
        {
            Task.Delay(1000);
            displayText = "Start Time";
            show = true;
            Timer();
        }

        async Task Timer()
        {
            while (TimeLeft > new TimeSpan())
            {
                await Task.Delay(1000);
                TimeLeft = TimeLeft.Subtract(new TimeSpan(0, 0, 1));
                StateHasChanged();
            }

            await AfterTime();
            StateHasChanged();
        }

        Task AfterTime()
        {
            displayText = "Now";
            TimeLeft = new TimeSpan(0, 0, 0, 0);
            return Task.CompletedTask;
        }
        #endregion


        public async Task Study()
        {
            await OffSwiches();
            bol_Study = true;

            if(VocMasterVM.Studys.Count > 0)
            {
                TimeLeft = new TimeSpan(); await Task.Delay(1200);
                Diffrence = VocMasterVM.Studys[0].UserVoc.NextReviewTime - DateTime.UtcNow;
                TimeLeft = new TimeSpan(Diffrence.Days, Diffrence.Hours, Diffrence.Minutes, Diffrence.Seconds);
                Start();
            }
        }
        public async Task PlaySound()
        {
            await JSRuntime.InvokeVoidAsync("playSound");
        }
        protected async Task StudyNexy()
        {
            VocMasterVM.Studys[0].UserVoc.Study = true;
            VocMasterVMUpdate.Studys.Add(VocMasterVM.Studys[0]);
            VocMasterVM.Results.FirstOrDefault(x => x.Voc.Id == VocMasterVM.Studys[0].Voc.Id).Study = true;

            if (VocMasterVM.Studys[0].UserVoc.Repetition == 0) VocMasterVM.Tests.Add(VocMasterVM.Studys[0]);

            VocMasterVM.Studys.RemoveAt(0);
            if (VocMasterVM.Studys.Count == 0)
            {
                await UpdateVocMasterVM();
            }
            else
            {
                //TimeLeft = new TimeSpan(); await Task.Delay(1200);
                //Diffrence = VocMasterVM.Studys[0].UserVoc.NextReviewTime - DateTime.UtcNow;
                //TimeLeft = new TimeSpan(Diffrence.Days, Diffrence.Hours, Diffrence.Minutes, Diffrence.Seconds);
                //Start();
            }
        }
        public async Task SaveImage(EditContext formContext)
        {
            if (!formContext.Validate()) return;
            ImageVM.Exception = null;
            ImageVM.Image.VocId = VocMasterVM.Studys[0].Voc.Id;
            ImageVM.Image.Voc = VocMasterVM.Studys[0].Voc;
            ImageVM createdImageVM = await ImageService.CreateImage(ImageVM);

            if (string.IsNullOrEmpty(createdImageVM.Exception))
            {
                await JSRuntime.Notfication($"Image {ImageVM.Image.Comment} has been created successfully!", IJSRuntimeExtensionMethods.SweetAlertMessageType.success, 3000);

                VocMasterVM.Studys[0].Images.Add(new pro_Models.Models.Image { Uri = ImageVM.Image.Uri, Comment = ImageVM.Image.Comment });
                ImageVM = new ImageVM();
            }
            else
            {
                await JSRuntime.Notfication(createdImageVM.Exception, IJSRuntimeExtensionMethods.SweetAlertMessageType.error, 10000);
            }

            bol_AddImage = false;
        }
        public async Task LikeImage(pro_Models.Models.Image image)
        {
            if (image.Liked)
            {
                image.Like--; image.Liked = false;
            }
            else
            {
                if (image.Disliked) image.Dislike--; image.Disliked = false;
                image.Like++;
                image.Liked = true;
            }
        }
        public async Task DislikeImage(pro_Models.Models.Image image)
        {
            if (image.Disliked)
            {
                image.Dislike--; image.Disliked = false;
            }
            else
            {
                if (image.Liked) image.Like--; image.Liked = false;
                image.Dislike++;
                image.Disliked = true;
            }
        }
        public void PlayVid(int id)
        {
            Pre_Clicked_Id = Clicked_Id;
            Clicked_Id = id;
        }
        #endregion

        #region Test
        public async Task Test()
        {
            await OffSwiches();
            bol_Test = true;
        }
        public async Task Forgot()
        {
            #region Prepare
            VocMasterVM.Tests[0].UserVoc.Repetition++;
            switch (VocMasterVM.Tests[0].UserVoc.Level)
            {
                case 1:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(1);
                    break;
                case 2:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(3);
                    break;
                case 3:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(10);
                    break;
                default:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = new DateTime(9999, 12, 31, 23, 59, 59);
                    break;
            }
            VocMasterVM.Tests[0].UserVoc.Study = false;
            #endregion

            VocMasterVMUpdate.Tests.Add(VocMasterVM.Tests[0]);
            VocMasterVM.Studys.Add(VocMasterVM.Tests[0]);

            var deleteResult = VocMasterVM.Results.FirstOrDefault(x => x.Voc.Id == VocMasterVM.Tests[0].Voc.Id);
            VocMasterVM.Results.Remove(deleteResult);
            VocMasterVM.Results.Add(VocMasterVM.Tests[0].UserVoc);

            VocMasterVM.Tests.RemoveAt(0);

            if (VocMasterVM.Tests.Count == 0) await UpdateVocMasterVM();
        }
        public async Task Remember()
        {
            #region Prepare
            VocMasterVM.Tests[0].UserVoc.Repetition++;
            VocMasterVM.Tests[0].UserVoc.Success++;
            if (VocMasterVM.Tests[0].UserVoc.LevelCounter < 2)
            {
                VocMasterVM.Tests[0].UserVoc.LevelCounter++;
            }
            else
            {
                VocMasterVM.Tests[0].UserVoc.LevelCounter = 0;
                VocMasterVM.Tests[0].UserVoc.Level++;
            }
            switch (VocMasterVM.Tests[0].UserVoc.Level)
            {
                case 1:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(1);
                    break;
                case 2:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(3);
                    break;
                case 3:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = VocMasterVM.Tests[0].UserVoc.NextReviewTime.AddDays(10);
                    break;
                default:
                    VocMasterVM.Tests[0].UserVoc.NextReviewTime = new DateTime(9999, 12, 31, 23, 59, 59);
                    break;
            }

            #endregion

            VocMasterVMUpdate.Tests.Add(VocMasterVM.Tests[0]);

            var deleteResult = VocMasterVM.Results.FirstOrDefault(x => x.Voc.Id == VocMasterVM.Tests[0].Voc.Id);
            VocMasterVM.Results.Remove(deleteResult);
            VocMasterVM.Results.Add(VocMasterVM.Tests[0].UserVoc);

            VocMasterVM.Tests.RemoveAt(0);

            if (VocMasterVM.Tests.Count == 0) await UpdateVocMasterVM();
        }
        #endregion

        #region Results
        #region Pro

        #endregion
        public async Task Results()
        {
            OffSwiches(); bol_Results = true;
        }
        #endregion
    }
}
