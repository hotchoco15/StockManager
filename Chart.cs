using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace StockManager
{
    public partial class Chart : Form
    {
        string symbol;

        public Chart(string ticker)
        {
            InitializeComponent();
            symbol = ticker;
           
        }

        private async void Chart_Load(object sender, EventArgs e)
        {
            List<ChartBody> lists = new List<ChartBody>();
            Dictionary<DateTime, double> dlists = new Dictionary<DateTime, double>();

            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://yahoo-finance15.p.rapidapi.com/api/v1/markets/stock/history?symbol={symbol}&interval=1d&diffandsplits=false"),
                Headers =
                {
                    { "x-rapidapi-key", "" },
                    { "x-rapidapi-host", "yahoo-finance15.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();              // API 결과값을 string으로 변환하여 body에 담음
                    ChartResponse response1 = JsonSerializer.Deserialize<ChartResponse>(body);  // 결과의 요소를 꺼내기 위해 역직렬화함


                    if (response1.Meta != null)
                    {
                        double fiftyTwoWeekHigh = response1.Meta.FiftyTwoWeekHigh;
                        double fiftyTwoWeekLow = response1.Meta.FiftyTwoWeekLow;

                    }

                    if (response1.Body != null)
                    {
                        foreach (var element in response1.Body)
                        {
                            string key = element.Key;
                            ChartBody value = element.Value;
                            string date = value.Date;
                            double close = value.Close;                         // 주식 가격(마감)
                            DateTime date1 = DateTime.Today.AddMonths(-3);      // 3개월 전 날짜

                            string dateformat = "dd-MM-yyyy";

                            try
                            {
                                // 날짜 비교를 위해 타입을 DateTime으로 변경
                                DateTime date3 = DateTime.ParseExact(date, dateformat, CultureInfo.InvariantCulture);

                                if (date3 >= date1)             // API 결과 중에 3개월 전 날짜보다 큰 날짜이면 
                                {
                                    //lists.Add(value);
                                    dlists.Add(date3, close);   // 날짜와 주식 가격을 담음
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error :" + ex.Message);
                            }
                        }

                        //MessageBox.Show(dlists.ToString());

                        stockChart.ChartAreas[0].AxisY.Maximum = dlists.Values.Max() + 10.0;    // 차트가 그려질 때 위로 여백을 줌
                        stockChart.ChartAreas[0].AxisY.Minimum = dlists.Values.Min() - 10.0;    // 차트가 그려질 때 아래로 여백을 줌 


                        foreach (KeyValuePair<DateTime, double> kvp in dlists)
                        {
                            stockChart.Series[0].Points.AddXY(kvp.Key, kvp.Value);
                        }
                        stockChart.Series[0].ToolTip = "#VALX, #VALY";      // 그래프에 마우스를 올렸을 때 해당 날짜와 가격이 나옴

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error :" + ex.Message);
                }
            }
        }
    }

    public class ChartResponse
    {
        [JsonPropertyName("meta")]
        public ChartMeta Meta { get; set; }
        [JsonPropertyName("body")]
        public Dictionary<string, ChartBody> Body { get; set; }
    }

    public class ChartMeta
    {
        [JsonPropertyName("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }
        [JsonPropertyName("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }
    }

    public class ChartBody
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("close")]
        public double Close { get; set; }
    }

}
