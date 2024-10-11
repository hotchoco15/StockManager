using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq.Expressions;
using System.Collections;


namespace StockManager
{
    public partial class SearchForm : Form
    {
        string connStr = "Server=localhost\\SQLEXPRESS;Database=DB1;Trusted_Connection=True";
        SqlConnection conn;
        string sql = "";
        SqlCommand cmd;
        string keyword = "";


        public SearchForm()
        {
            InitializeComponent();
        }



        private void SearchForm_Load(object sender, EventArgs e)
        {
            // 폼 로드 시 기존내역 초기화 
            BtnAdd.Enabled = false;
            lbl1.Text = string.Empty;
            lbl2.Text = string.Empty;
            lbl3.Text = string.Empty;
            SearchBox.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Refresh();


            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                sql = "SELECT symbol, description FROM items";


                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable tbl = ds.Tables[0];

                List<string> list = new List<string>();
                List<string> list1 = new List<string>();

                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    DataRow dataRow = tbl.Rows[i];
                    list.Add(dataRow["description"].ToString());    // 리스트에 회사명 추가
                    list1.Add(dataRow["symbol"].ToString());        // 리스트에 회사코드 추가

                }

                if (list.Count > 0 && list[0] != "")
                {
                    lbl1.Text = list[0];
                    hiddlelbl1.Text = list1[0];                     // hidden라벨에 회사코드 추가함 
                }
                if (list.Count > 1 && list[1] != "")
                {
                    lbl2.Text = list[1];
                    hiddlelbl2.Text = list1[1];
                }
                if (list.Count > 2 && list[2] != "")
                {
                    lbl3.Text = list[2];
                    hiddlelbl3.Text = list1[2];
                }

                // 회사코드, 회사명 컬럼 추가
                dataGridView.Columns.Add("Value", "회사코드");
                dataGridView.Columns.Add("Description", "회사명");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {          
            string stock1 = hiddlelbl1.Text.Trim();
            string stock2 = hiddlelbl2.Text.Trim();
            string stock3 = hiddlelbl3.Text.Trim();

            if (stock1 == "" && stock2 == "" && stock3 == "")
            {
                MessageBox.Show("관심종목을 추가해주세요.");
                return;
            }

            conn.Close();
            this.Hide();
            DetailForm subForm = new DetailForm(stock1, stock2, stock3);
            subForm.ShowDialog();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            conn.Close();
            this.Close();
        }


        private async void BtnSch_Click(object sender, EventArgs e)
        {
            // 기존 검색결과 초기화
            dataGridView.Rows.Clear();
            dataGridView.Refresh();


            // 3개 라벨에 값이 전부 있으면 
            if (lbl1.Text != "" && lbl2.Text != "" && lbl3.Text != "")
            {
                MessageBox.Show("관심종목은 3개를 초과할 수 없습니다.");
            }
            else
            {
                keyword = SearchBox.Text;

                if (keyword == "")
                {
                    MessageBox.Show("키워드를 입력해주세요.");
                }

                else if (keyword != "")
                {
                    if (!keyword.All(Char.IsLetter))        // 입력값이 문자가 아니면 
                    {
                        MessageBox.Show("유효한 값을 입력해주세요.");
                        return;
                    }

                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://yahoo-finance15.p.rapidapi.com/api/v1/markets/search?search={keyword}"),
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
                            var body = await response.Content.ReadAsStringAsync();                // API 결과값을 string으로 변환하여 body에 담음
                            Response response1 = JsonSerializer.Deserialize<Response>(body);      // 결과의 요소를 꺼내기 위해 역직렬화함

                            if (response1.Body != null)
                            {
                                foreach (var item in response1.Body)
                                {
                                    dataGridView.Rows.Add(item.Symbol, item.Name);          // DateGridView에 회사코드, 회사명 추가함 
                                }

                                BtnAdd.Enabled = true;                                      // 추가 버튼 활성화함 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)        // DataGridView에 선택된 row가 있으면 
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                string item = row.Cells[0].Value.ToString();
                string description = row.Cells[1].Value.ToString();

                if (IsDuplicated(description))
                {
                    MessageBox.Show("중복되지 않은 종목을 입력해주세요.");
                    return;
                }

                conn = new SqlConnection(connStr);
                conn.Open();

                cmd = new SqlCommand();
                cmd.Connection = conn;

                sql = "INSERT INTO items VALUES ((SELECT ISNULL(MAX(uid) + 1, 1) FROM items), '" + item + "','" + description + "')";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                MessageBox.Show(description + "가 관심종목으로 추가되었습니다.");


                SearchForm_Load(sender, e);         // 변경 후 다시 폼로딩 

            }
        }

        // 중복값 체크 
        private bool IsDuplicated(string description)
        {
            if (description == lbl1.Text || description == lbl2.Text)
            {
                return true;
            }
            return false;
        }

        private void BtnDel1_Click(object sender, EventArgs e)
        {
            if (hiddlelbl1.Text == "")
            {
                MessageBox.Show("삭제할 관심종목이 없습니다.");
                return;
            }

            cmd = new SqlCommand();
            cmd.Connection = conn;

            sql = "Delete from items where description = '" + lbl1.Text + "'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("종목삭제가 완료되었습니다.");

            SearchForm_Load(sender, e);
        }

        private void BtnDel2_Click(object sender, EventArgs e)
        {
            if (hiddlelbl2.Text == "")
            {
                MessageBox.Show("삭제할 관심종목이 없습니다.");
                return;
            }

            cmd = new SqlCommand();
            cmd.Connection = conn;

            sql = "Delete from items where description = '" + lbl2.Text + "'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("종목삭제가 완료되었습니다.");

            SearchForm_Load(sender, e);
        }

        private void BtnDel3_Click(object sender, EventArgs e)
        {
            if (hiddlelbl3.Text == "")
            {
                MessageBox.Show("삭제할 관심종목이 없습니다.");
                return;
            }

            cmd = new SqlCommand();
            cmd.Connection = conn;

            sql = "Delete from items where description = '" + lbl3.Text + "'";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("종목삭제가 완료되었습니다.");

            SearchForm_Load(sender, e);
        }

        private async void BtnLstAdd_Click(object sender, EventArgs e)
        {
            if (!checkBox.Checked)
            {
                MessageBox.Show("모의투자종목을 체크해주세요");
            }
            else
            {
                string ticker1 = hiddlelbl1.Text;
                string ticker2 = hiddlelbl2.Text;
                string ticker3 = hiddlelbl3.Text;

                string[] tickers = { ticker1, ticker2, ticker3 };


                try
                { 
                    for (int i = 0; i < tickers.Length; i++)
                    {
                        // 종목이 3개 미만일 경우 
                        if (tickers[i] == "") 
                        {
                            MessageBox.Show("종목추가가 완료되었습니다.");
                            return; 
                        }

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

                            response.EnsureSuccessStatusCode();
                            var body = await response.Content.ReadAsStringAsync();

                            SearchResponse response1 = JsonSerializer.Deserialize<SearchResponse>(body);

                            if (response1.Body != null)
                            {
                                var primaryData = response1.Body.SprimaryData;

                                string symbol = response1.Body.Symbol;
                                string companyName = response1.Body.CompanyName;
                                string salePrice = primaryData.LastSalePrice;

                                cmd = new SqlCommand();
                                cmd.Connection = conn;

                                sql = "INSERT INTO lists VALUES((SELECT ISNULL(MAX(uid) + 1, 1) FROM lists), '" + symbol + "','" + companyName + "', (SELECT CONVERT(date, GETDATE())), '" + salePrice + "')";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                if (i == tickers.Length - 1)     // 종목이 3개 추가되었을 때 
                                {
                                    MessageBox.Show("종목추가가 완료되었습니다.");     
                                }
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

        private void BtnLstView_Click(object sender, EventArgs e)
        {
            if (hiddlelbl1.Text == "" && hiddlelbl2.Text == "" && hiddlelbl3.Text == "")
            {
                MessageBox.Show("관심종목을 추가해주세요.");
                return;
            }
            conn.Close();
            this.Hide();
            SimulationForm subForm = new SimulationForm();
            subForm.ShowDialog();
        }
    }
    public class SearchResponse
    { 
        [JsonPropertyName("meta")]
        public SearchMeta Meta { get; set; }
        [JsonPropertyName("body")]
        public SearchBody Body { get; set; }

    }

    public class SearchMeta
        {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class SearchBody
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("primaryData")]
        public PrimaryData SprimaryData { get; set; }
        

    }

    public class SprimaryData
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


    public class Response
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
        [JsonPropertyName("body")]
        public List<Body> Body { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("exch")]
        public string Exch { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("exchDisp")]
        public string ExchDisp { get; set; }
        [JsonPropertyName("typeDisp")]
        public string TypeDisp { get; set; }
    }

}
