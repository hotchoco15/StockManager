using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace StockManager
{
    public partial class DetailForm : Form
    {
        Dictionary<string, string> companies = new Dictionary<string, string>();
        string ticker1; 
        string ticker2;
        string ticker3;


        public DetailForm(string stock1, string stock2, string stock3)
        {
            InitializeComponent();
            ticker1 = stock1;
            ticker2 = stock2;
            ticker3 = stock3;

        }

        private async void DetailForm_Load(object sender, EventArgs e)
        {
            DataGridView.Columns.Add("CompanyName", "회사명");
            DataGridView.Columns.Add("LastSalePrice", "현재가");
            DataGridView.Columns.Add("NetChange", "전일대비");
            DataGridView.Columns.Add("PercentageChange", "전일대비(%)");


            string[] tickers = { ticker1, ticker2, ticker3 };

            for (int i = 0; i < tickers.Length; i++)
            {
                // 관심종목이 3개 미만일 경우 관심종목 수만큼 진행
                if (tickers[i] == "") { return; }

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://yahoo-finance15.p.rapidapi.com/api/v1/markets/quote?ticker={tickers[i]}&type=STOCKS"),
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
                        var body = await response.Content.ReadAsStringAsync();          // API 결과값을 string으로 변환하여 body에 담음
                        //MessageBox.Show(body);
                        DetailResponse response1 = JsonSerializer.Deserialize<DetailResponse>(body);    // 결과의 요소를 꺼내기 위해 역직렬화함

                        if (response1 != null) 
                        { 
                            if (response1.Body != null)
                            {
                                var primaryData = response1.Body.PrimaryData;

                                string symbol = response1.Body.Symbol;          // 회사코드
                                string company = response1.Body.CompanyName;    // 회사명

                                companies.Add(company, symbol);  

                                // DateGridView에 회사명, 현재가, 전일대비, 전일대비(%)를 추가함 
                                int rowindex = DataGridView.Rows.Add(response1.Body.CompanyName, primaryData.LastSalePrice, (string.IsNullOrEmpty(primaryData.NetChange) ? "0" : primaryData.NetChange), (string.IsNullOrEmpty(primaryData.PercentageChange) ? "0" : primaryData.PercentageChange));

                                if (primaryData.DeltaIndicator == "up")             // 가격이 상승하면 텍스트를 빨간색으로 변경
                                {
                                    DataGridView.Rows[rowindex].DefaultCellStyle.ForeColor = Color.Red;
                                }
                                else if (primaryData.DeltaIndicator == "down")      // 가격이 하락하면 텍스트를 파란색으로 변경
                                {
                                    DataGridView.Rows[rowindex].DefaultCellStyle.ForeColor = Color.Blue;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error :" + ex.Message);
                    }
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string companyname = DataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            string ticker = companies[companyname];     // companies의 key가 companyname 일때 value를 ticker에 담음 
            Chart subForm = new Chart(ticker);          
            subForm.ShowDialog();
        }
    }

    public class DetailResponse
    {
        [JsonPropertyName("meta")]
        public DetailMeta Meta { get; set; }
        [JsonPropertyName("body")]
        public DetailBody Body { get; set; }
       
    }

    public class DetailMeta
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class DetailBody
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("stockType")]
        public string StockType { get; set; }
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("primaryData")]
        public PrimaryData PrimaryData { get; set; }

        
        [JsonPropertyName("marketStatus")]
        public string MarketStatus { get; set; }
        [JsonPropertyName("assetClass")]
        public string AssetClass { get; set; }
        [JsonPropertyName("keyStats")]
        public KeyStats KeyStats { get; set; }
    }

    public class PrimaryData
    {
        [JsonPropertyName("lastSalePrice")]
        public string LastSalePrice { get; set; }

        [JsonPropertyName("netChange")]
        public string NetChange { get; set; }
        [JsonPropertyName("percentageChange")]
        public string PercentageChange { get; set; }
        [JsonPropertyName("deltaIndicator")]
        public string DeltaIndicator { get; set; }
        [JsonPropertyName("lastTradeTimestamp")]
        public string LastTradeTimestamp { get; set; }
        [JsonPropertyName("isRealTime")]
        public bool IsRealTime { get; set; }
        [JsonPropertyName("bidPrice")]
        public string BidPrice { get; set; }
        [JsonPropertyName("askPrice")]
        public string AskPrice { get; set; }
        [JsonPropertyName("bidSize")]
        public string BidSize { get; set; }
        [JsonPropertyName("askSize")]
        public string AskSize { get; set; }
        [JsonPropertyName("volume")]
        public string Volume { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class KeyStats
    {
        [JsonPropertyName("fiftyTwoWeekHighLow")]
        public FiftyTwoWeekHighLow FiftyTwoWeekHighLow { get; set; }
    }

    public class FiftyTwoWeekHighLow
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
