using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager
{
    public partial class SimulationForm : Form
    {
        string connStr = "Server=localhost\\SQLEXPRESS;Database=DB1;Trusted_Connection=True";
        SqlConnection conn;
        string sql = "";
        SqlCommand cmd;

        public SimulationForm()
        {
            InitializeComponent();
        }

        private async void SimulationForm_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(connStr);
            conn.Open();
            sql = "SELECT symbol, description, createtime, stockprice FROM lists";      // 회사코드, 회사명, 매수일, 매수가를 DB에서 가져옴

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable tbl = ds.Tables[0];   

            tbl.Columns.Add("LastSalePrice");           // Datatable에 현재가 컬럼추가함 
            tbl.Columns.Add("PercentageChange");        // Datatable에 손익률 컬럼추가함

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                DataRow dataRow = tbl.Rows[i];
                string ticker = dataRow["symbol"].ToString();       
                //string createtime = dataRow["createtime"].ToString();
                

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://yahoo-finance15.p.rapidapi.com/api/v1/markets/quote?ticker={ticker}&type=STOCKS"),
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
                        var body = await response.Content.ReadAsStringAsync();
                        SimulationResponse response1 = JsonSerializer.Deserialize<SimulationResponse>(body);
                        var primaryData = response1.Body.SimulationPrimaryData;
                        string saleprice = primaryData.LastSalePrice;
                        
                        dataRow["LastSalePrice"] = saleprice;               // 현재가를 Datatable에 저장
                        
                        double stockprice = Double.Parse(dataRow["stockprice"].ToString().Replace("$", ""));    
                        double change = Math.Round((((Double.Parse(saleprice.Replace("$", ""))) - stockprice) * 100 / stockprice), 2);  // 소수점 2자리까지 저장
                        string text1 = "+";
                        string text2 = "%";

                        if (change > 0)
                        {
                            dataRow["PercentageChange"] = text1 + (change.ToString()) + text2;
                        }
                        else
                        { 
                            dataRow["PercentageChange"] = (change.ToString()) + text2;    // 손익률을 Datatable에 저장
                        }

                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show("Error :" + ex.Message);
                    }
                }  
            }
            tbl.Columns[0].ColumnName = "회사코드";
            tbl.Columns[1].ColumnName = "회사명";
            tbl.Columns[2].ColumnName = "매수일";
            tbl.Columns[3].ColumnName = "매수가";
            tbl.Columns[4].ColumnName = "현재가";
            tbl.Columns[5].ColumnName = "손익률(%)";

            DataGridView.DataSource = tbl;

        }

        // 리스트 초기화 
        private void BtnDel_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            cmd.Connection = conn;

            sql = "Delete from lists";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            MessageBox.Show("모든 종목이 삭제되었습니다.");
            this.Close();
           
        }
    }

    public class SimulationResponse
    {
        [JsonPropertyName("meta")]
        public SimulationMeta Meta { get; set; }
        [JsonPropertyName("body")]
        public SimulationBody Body { get; set; }

    }

    public class SimulationMeta 
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class SimulationBody
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
        public PrimaryData SimulationPrimaryData { get; set; }


        [JsonPropertyName("marketStatus")]
        public string MarketStatus { get; set; }
        [JsonPropertyName("assetClass")]
        public string AssetClass { get; set; }
        [JsonPropertyName("keyStats")]
        public KeyStats KeyStats { get; set; }
    }

    public class SimulationPrimaryData
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
}
