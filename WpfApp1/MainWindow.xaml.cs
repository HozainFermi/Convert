using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        static float refm;
        static float key;
        static float ticket;
        static float LC;
        static float CurCalc;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(100);
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ItemName = Findbar.Text;
            ItemName.Replace(" ", "%20");
            ItemName = $"https://next.backpack.tf/items/{ItemName}";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(ItemName);
            

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
            chromeOptions.AddArgument("--window-position=-32000,-32000");

            IWebDriver driver = new ChromeDriver(service, chromeOptions);
            driver.Navigate().GoToUrl("https://next.backpack.tf/stats?item=Refined%20Metal&quality=Unique");
            var refcost = driver.FindElement(By.XPath("//span[@tabindex='0']")).GetAttribute("textContent");
            Thread.Sleep(1000);
            driver.Quit();

            IWebDriver driver2 = new ChromeDriver(chromeOptions);
            driver2.Navigate().GoToUrl("https://next.backpack.tf/stats?item=Mann%20Co.%20Supply%20Crate%20Key&quality=Unique");
            Thread.Sleep(1000);
            var keycost = driver2.FindElement(By.XPath("//span[@tabindex='0']")).GetAttribute("textContent");
            driver2.Quit();

            IWebDriver driver3 = new ChromeDriver(chromeOptions);
            driver3.Navigate().GoToUrl("https://next.backpack.tf/stats?item=Tour%20of%20Duty%20Ticket&quality=Unique");
            Thread.Sleep(1000);
            var ticketcost = driver3.FindElement(By.XPath("//span[@tabindex='0']")).GetAttribute("textContent");
            driver3.Quit();

            refcost = refcost.Remove(0, 26).Remove(12, 24);
            keycost = keycost.Remove(0, 26).Remove(15, 24);
            ticketcost = ticketcost.Remove(0, 26).Remove(12, 24);

            InfoBar.Text = $"Ref-{refcost} Key-{keycost} Ticket-{ticketcost}";
            refm = Convert.ToSingle( refcost.Remove(0, 1).Remove(4, 7).Replace(".", ","));
            key = Convert.ToSingle(keycost.Remove(5, 10).Replace(".", ","));
            ticket = Convert.ToSingle(ticketcost.Remove(5, 7).Replace(".", ","));



             
//25.88–26 ref
//


        }


        private void USDField_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            float ost;
            float num;

            try
            {
                if (USDField.Text == "")
                {
                    LocalCurField.Text ="";
                    KeyField.Text = "";
                    RefField.Text = "";
                    RecField.Text = "";
                    ScrapField.Text = "";
                }


                else
                {
                    CurCalc = Convert.ToSingle(USDField.Text) * LC; //LocalCur
                    LocalCurField.Text = Convert.ToString(CurCalc); // Set LocalCur

                    CurCalc = Convert.ToSingle(USDField.Text) / refm; // Number of refs
                    if ((CurCalc % key) == 0)
                    {
                        number = (int)(CurCalc / key); // number of keys
                        KeyField.Text = number.ToString(); // set keys

                    }
                        if ((CurCalc % key) > 0)
                        {
                        number = (int)(CurCalc / key); // number of keys
                        KeyField.Text = number.ToString(); // set keys

                        ost = CurCalc % key; 
                            number = (int)ost; //number of refs
                            RefField.Text = number.ToString(); //set int ost refs
                            ost = ost - number; // число после запятой остаток
                            if (ost % 0.33 == 0) 
                            {
                                RecField.Text = "1";
                            }
                            if (ost % 0.33 > 0)
                            {
                                CurCalc = (float)(ost / 0.33); 
                                number = (int)CurCalc; // number of rec
                                RecField.Text = number.ToString(); //set rec
                                num = (float)(ost - 0.33 * number); //сколько осталось скрапов флоут
                                number = (int)(num / 0.11); // number of scraps
                                ScrapField.Text = number.ToString(); //set scraps
                            }





                        }
                    
                  
                }
            }
            catch (FormatException ex)
            {
               

                LocalCurField.Text =ex.Message;
                KeyField.Text = ex.Message;
                RefField.Text = ex.Message;
                RecField.Text = ex.Message;
                ScrapField.Text = ex.Message;

            }

        }

        private void LocalCurField_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            float ost;
            float num;

            try
            {
                if (LocalCurField.Text == "")
                {
                    USDField.Text = "";
                    KeyField.Text = "";
                    RefField.Text = "";
                    RecField.Text = "";
                    ScrapField.Text = "";
                }


                else
                {
                    CurCalc = Convert.ToSingle(LocalCurField.Text) / LC; //USD
                   USDField.Text = Convert.ToString(CurCalc); // Set USD

                    CurCalc = Convert.ToSingle(USDField.Text) / refm; // Number of refs
                    if ((CurCalc % key) == 0)
                    {
                        number = (int)(CurCalc / key); // number of keys
                        KeyField.Text = number.ToString(); // set keys

                    }
                    if ((CurCalc % key) > 0)
                    {
                        number = (int)(CurCalc / key); // number of keys
                        KeyField.Text = number.ToString(); // set keys

                        ost = CurCalc % key;
                        number = (int)ost; //number of refs
                        RefField.Text = number.ToString(); //set int ost refs
                        ost = ost - number; // число после запятой остаток
                        if (ost % 0.33 == 0)
                        {
                            RecField.Text = "1";
                        }
                        if (ost % 0.33 > 0)
                        {
                            CurCalc = (float)(ost / 0.33);
                            number = (int)CurCalc; // number of rec
                            RecField.Text = number.ToString(); //set rec
                            num = (float)(ost - 0.33 * number); //сколько осталось скрапов флоут
                            number = (int)(num / 0.11); // number of scraps
                            ScrapField.Text = number.ToString(); //set scraps
                        }





                    }


                }
            }
            catch (FormatException ex)
            {
                LocalCurField.Text = ex.Message;
                KeyField.Text = ex.Message;
                RefField.Text = ex.Message;
                RecField.Text = ex.Message;
                ScrapField.Text = ex.Message;

            }


        }

        private void KeyField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void RefField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RecField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ScrapField_TextChanged(object sender, TextChangedEventArgs e)
        {
          

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //col-md-2 col-xs-9 _right mono-num

            string url = "https://www.banki.ru/products/currency/usd/";
            var httpclient = new HttpClient();

            var html = httpclient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var LocalCur = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='Text__sc-j452t5-0 bCCQWi']").InnerText;
            LocalCur = LocalCur.Remove(5, 2);
            LC = Convert.ToSingle(LocalCur);


        }

       
    }
}
