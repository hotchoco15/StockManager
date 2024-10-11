namespace StockManager
{
    partial class SearchForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnDetail = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnSch = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.BtnDel1 = new System.Windows.Forms.Button();
            this.BtnDel2 = new System.Windows.Forms.Button();
            this.BtnDel3 = new System.Windows.Forms.Button();
            this.hiddlelbl1 = new System.Windows.Forms.Label();
            this.hiddlelbl2 = new System.Windows.Forms.Label();
            this.hiddlelbl3 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.BtnLstAdd = new System.Windows.Forms.Button();
            this.BtnLstView = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnDetail
            // 
            this.BtnDetail.Location = new System.Drawing.Point(6, 512);
            this.BtnDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnDetail.Name = "BtnDetail";
            this.BtnDetail.Size = new System.Drawing.Size(69, 22);
            this.BtnDetail.TabIndex = 0;
            this.BtnDetail.Text = "조회";
            this.BtnDetail.UseVisualStyleBackColor = true;
            this.BtnDetail.Click += new System.EventHandler(this.BtnDetail_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(78, 512);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(69, 22);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "취소";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "관심종목(3개)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "관심종목검색";
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(105, 202);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(184, 21);
            this.SearchBox.TabIndex = 5;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Enabled = false;
            this.BtnAdd.Location = new System.Drawing.Point(317, 511);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(69, 22);
            this.BtnAdd.TabIndex = 6;
            this.BtnAdd.Text = "추가";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSch
            // 
            this.BtnSch.Location = new System.Drawing.Point(295, 202);
            this.BtnSch.Name = "BtnSch";
            this.BtnSch.Size = new System.Drawing.Size(69, 22);
            this.BtnSch.TabIndex = 8;
            this.BtnSch.Text = "검색";
            this.BtnSch.UseVisualStyleBackColor = true;
            this.BtnSch.Click += new System.EventHandler(this.BtnSch_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(106, 229);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(214, 239);
            this.dataGridView.TabIndex = 9;
            // 
            // lbl1
            // 
            this.lbl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl1.Location = new System.Drawing.Point(108, 29);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(200, 18);
            this.lbl1.TabIndex = 10;
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl2
            // 
            this.lbl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl2.Location = new System.Drawing.Point(108, 54);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(200, 18);
            this.lbl2.TabIndex = 11;
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl3
            // 
            this.lbl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl3.Location = new System.Drawing.Point(108, 78);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(200, 18);
            this.lbl3.TabIndex = 12;
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // BtnDel1
            // 
            this.BtnDel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel1.Font = new System.Drawing.Font("바탕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDel1.Location = new System.Drawing.Point(312, 28);
            this.BtnDel1.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDel1.Name = "BtnDel1";
            this.BtnDel1.Size = new System.Drawing.Size(19, 19);
            this.BtnDel1.TabIndex = 13;
            this.BtnDel1.Text = "X";
            this.BtnDel1.UseVisualStyleBackColor = true;
            this.BtnDel1.Click += new System.EventHandler(this.BtnDel1_Click);
            // 
            // BtnDel2
            // 
            this.BtnDel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel2.Font = new System.Drawing.Font("바탕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDel2.Location = new System.Drawing.Point(312, 53);
            this.BtnDel2.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDel2.Name = "BtnDel2";
            this.BtnDel2.Size = new System.Drawing.Size(19, 19);
            this.BtnDel2.TabIndex = 14;
            this.BtnDel2.Text = "X";
            this.BtnDel2.UseVisualStyleBackColor = true;
            this.BtnDel2.Click += new System.EventHandler(this.BtnDel2_Click);
            // 
            // BtnDel3
            // 
            this.BtnDel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel3.Font = new System.Drawing.Font("바탕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDel3.Location = new System.Drawing.Point(312, 77);
            this.BtnDel3.Margin = new System.Windows.Forms.Padding(0);
            this.BtnDel3.Name = "BtnDel3";
            this.BtnDel3.Size = new System.Drawing.Size(19, 19);
            this.BtnDel3.TabIndex = 15;
            this.BtnDel3.Text = "X";
            this.BtnDel3.UseVisualStyleBackColor = true;
            this.BtnDel3.Click += new System.EventHandler(this.BtnDel3_Click);
            // 
            // hiddlelbl1
            // 
            this.hiddlelbl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hiddlelbl1.Location = new System.Drawing.Point(346, 29);
            this.hiddlelbl1.Name = "hiddlelbl1";
            this.hiddlelbl1.Size = new System.Drawing.Size(41, 18);
            this.hiddlelbl1.TabIndex = 16;
            this.hiddlelbl1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.hiddlelbl1.Visible = false;
            // 
            // hiddlelbl2
            // 
            this.hiddlelbl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hiddlelbl2.Location = new System.Drawing.Point(346, 54);
            this.hiddlelbl2.Name = "hiddlelbl2";
            this.hiddlelbl2.Size = new System.Drawing.Size(41, 18);
            this.hiddlelbl2.TabIndex = 17;
            this.hiddlelbl2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.hiddlelbl2.Visible = false;
            // 
            // hiddlelbl3
            // 
            this.hiddlelbl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hiddlelbl3.Location = new System.Drawing.Point(346, 79);
            this.hiddlelbl3.Name = "hiddlelbl3";
            this.hiddlelbl3.Size = new System.Drawing.Size(41, 18);
            this.hiddlelbl3.TabIndex = 18;
            this.hiddlelbl3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.hiddlelbl3.Visible = false;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(7, 20);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(96, 16);
            this.checkBox.TabIndex = 19;
            this.checkBox.Text = "모의투자종목";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // BtnLstAdd
            // 
            this.BtnLstAdd.Location = new System.Drawing.Point(135, 16);
            this.BtnLstAdd.Name = "BtnLstAdd";
            this.BtnLstAdd.Size = new System.Drawing.Size(70, 23);
            this.BtnLstAdd.TabIndex = 20;
            this.BtnLstAdd.Text = "종목추가";
            this.BtnLstAdd.UseVisualStyleBackColor = true;
            this.BtnLstAdd.Click += new System.EventHandler(this.BtnLstAdd_Click);
            // 
            // BtnLstView
            // 
            this.BtnLstView.Location = new System.Drawing.Point(207, 16);
            this.BtnLstView.Name = "BtnLstView";
            this.BtnLstView.Size = new System.Drawing.Size(70, 23);
            this.BtnLstView.TabIndex = 21;
            this.BtnLstView.Text = "종목확인";
            this.BtnLstView.UseVisualStyleBackColor = true;
            this.BtnLstView.Click += new System.EventHandler(this.BtnLstView_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.BtnLstView);
            this.groupBox.Controls.Add(this.BtnLstAdd);
            this.groupBox.Controls.Add(this.checkBox);
            this.groupBox.Location = new System.Drawing.Point(104, 105);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(281, 52);
            this.groupBox.TabIndex = 22;
            this.groupBox.TabStop = false;
            // 
            // SearchForm
            // 
            this.AcceptButton = this.BtnSch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(392, 541);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.hiddlelbl3);
            this.Controls.Add(this.hiddlelbl2);
            this.Controls.Add(this.hiddlelbl1);
            this.Controls.Add(this.BtnDel3);
            this.Controls.Add(this.BtnDel2);
            this.Controls.Add(this.BtnDel1);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.BtnSch);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SearchForm";
            this.Text = "종목검색";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnDetail;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSch;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Button BtnDel1;
        private System.Windows.Forms.Button BtnDel2;
        private System.Windows.Forms.Button BtnDel3;
        private System.Windows.Forms.Label hiddlelbl1;
        private System.Windows.Forms.Label hiddlelbl2;
        private System.Windows.Forms.Label hiddlelbl3;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Button BtnLstAdd;
        private System.Windows.Forms.Button BtnLstView;
        private System.Windows.Forms.GroupBox groupBox;
    }
}

