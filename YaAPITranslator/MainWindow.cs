using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace YaAPITranslator
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = input_text.Text;
            string key = "trnsl.1.1.20160618T185757Z.4ae7f57c17c0ab51.32931f3ae5bf01cb5d337d52b7c6496403858eb4";
            string lang = "uk-en";

            string url = String.Format(
                "https://translate.yandex.net/api/v1.5/tr.json/translate?lang={0}&key={1}&text={2}",
                lang, key, text    
            );


            WebRequest req = WebRequest.Create(url);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream dataStream = res.GetResponseStream();
            string data = "";

            using(StreamReader sr = new StreamReader(dataStream))
            {
                data = sr.ReadToEnd();
                sr.Close();
            }


            JavaScriptSerializer js = new JavaScriptSerializer();
            data = js.Deserialize<TranslateResponse>(data).text[0];

            output_text.Text = data;
        }
    }

    struct TranslateResponse
    {
        public string lang;
        public List<string> text;
        public int code;
    }
}
