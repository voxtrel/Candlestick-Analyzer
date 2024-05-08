namespace Project1
{
    partial class Form_StockViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button_pickStock = new System.Windows.Forms.Button();
            this.openFileDialog_TickerChooser = new System.Windows.Forms.OpenFileDialog();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label_Start = new System.Windows.Forms.Label();
            this.label_End = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.chart_candlesticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox_Pattern = new System.Windows.Forms.ComboBox();
            this.label_Pattern = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).BeginInit();
            this.SuspendLayout();
            // 
            // button_pickStock
            // 
            this.button_pickStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_pickStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_pickStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_pickStock.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_pickStock.Location = new System.Drawing.Point(90, 14);
            this.button_pickStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_pickStock.Name = "button_pickStock";
            this.button_pickStock.Size = new System.Drawing.Size(257, 40);
            this.button_pickStock.TabIndex = 0;
            this.button_pickStock.Text = "Pick a Stock";
            this.button_pickStock.UseVisualStyleBackColor = false;
            this.button_pickStock.Click += new System.EventHandler(this.button_pickStock_Click);
            // 
            // openFileDialog_TickerChooser
            // 
            this.openFileDialog_TickerChooser.Filter = "Stock Files|*.csv| Daily Files|*-Day.csv| Weekly Files|*-Week.csv| Monthly Files|" +
    "*-Month.csv| AAPL Files|AAPL-*| IBM Files|IBM-*| MSFT Files|MSFT-*| GOOG Files|G" +
    "OOG-*";
            this.openFileDialog_TickerChooser.Multiselect = true;
            this.openFileDialog_TickerChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_TickerChooser_FileOk);
            // 
            // candlestickBindingSource
            // 
            this.candlestickBindingSource.DataSource = typeof(Project1.Candlestick);
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.Location = new System.Drawing.Point(640, 24);
            this.dateTimePicker_start.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePicker_start.MaxDate = new System.DateTime(2050, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_start.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(233, 22);
            this.dateTimePicker_start.TabIndex = 2;
            this.dateTimePicker_start.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Location = new System.Drawing.Point(640, 61);
            this.dateTimePicker_end.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePicker_end.MaxDate = new System.DateTime(2050, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_end.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(233, 22);
            this.dateTimePicker_end.TabIndex = 3;
            this.dateTimePicker_end.Value = new System.DateTime(2024, 1, 17, 0, 0, 0, 0);
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_Start.Location = new System.Drawing.Point(527, 23);
            this.label_Start.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(93, 23);
            this.label_Start.TabIndex = 4;
            this.label_Start.Text = "Start Date";
            // 
            // label_End
            // 
            this.label_End.AutoSize = true;
            this.label_End.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_End.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_End.Location = new System.Drawing.Point(527, 60);
            this.label_End.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_End.Name = "label_End";
            this.label_End.Size = new System.Drawing.Size(87, 23);
            this.label_End.TabIndex = 5;
            this.label_End.Text = "End Date";
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_update.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.Location = new System.Drawing.Point(90, 61);
            this.button_update.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(257, 40);
            this.button_update.TabIndex = 7;
            this.button_update.Text = "Update Data";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // chart_candlesticks
            // 
            this.chart_candlesticks.BackColor = System.Drawing.Color.Cornsilk;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gold;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gold;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.Name = "ChartArea_OCHL";
            chartArea2.AlignWithChartArea = "ChartArea_OCHL";
            chartArea2.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gold;
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gold;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.Name = "ChartArea_volume";
            this.chart_candlesticks.ChartAreas.Add(chartArea1);
            this.chart_candlesticks.ChartAreas.Add(chartArea2);
            this.chart_candlesticks.DataSource = this.candlestickBindingSource;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_candlesticks.Legends.Add(legend1);
            this.chart_candlesticks.Location = new System.Drawing.Point(13, 125);
            this.chart_candlesticks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chart_candlesticks.Name = "chart_candlesticks";
            series1.ChartArea = "ChartArea_OCHL";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series_OCHL";
            series1.XValueMember = "date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueMembers = "high, low, open, close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_volume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series_volume";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueMembers = "volume";
            this.chart_candlesticks.Series.Add(series1);
            this.chart_candlesticks.Series.Add(series2);
            this.chart_candlesticks.Size = new System.Drawing.Size(891, 365);
            this.chart_candlesticks.TabIndex = 8;
            this.chart_candlesticks.Text = "chart1";
            // 
            // comboBox_Pattern
            // 
            this.comboBox_Pattern.FormattingEnabled = true;
            this.comboBox_Pattern.Location = new System.Drawing.Point(730, 99);
            this.comboBox_Pattern.Name = "comboBox_Pattern";
            this.comboBox_Pattern.Size = new System.Drawing.Size(143, 23);
            this.comboBox_Pattern.TabIndex = 9;
            this.comboBox_Pattern.Text = "Select a Pattern";
            this.comboBox_Pattern.SelectedIndexChanged += new System.EventHandler(this.comboBox_Pattern_SelectedIndexChanged);
            // 
            // label_Pattern
            // 
            this.label_Pattern.AutoSize = true;
            this.label_Pattern.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.label_Pattern.ForeColor = System.Drawing.Color.Orange;
            this.label_Pattern.Location = new System.Drawing.Point(527, 99);
            this.label_Pattern.Name = "label_Pattern";
            this.label_Pattern.Size = new System.Drawing.Size(138, 23);
            this.label_Pattern.TabIndex = 10;
            this.label_Pattern.Text = "Select a Pattern";
            // 
            // Form_StockViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(928, 580);
            this.Controls.Add(this.label_Pattern);
            this.Controls.Add(this.comboBox_Pattern);
            this.Controls.Add(this.chart_candlesticks);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.label_End);
            this.Controls.Add(this.label_Start);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.dateTimePicker_start);
            this.Controls.Add(this.button_pickStock);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form_StockViewer";
            this.Text = "Form_StockViewer";
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_pickStock;
        private System.Windows.Forms.OpenFileDialog openFileDialog_TickerChooser;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.Label label_End;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlesticks;
        private System.Windows.Forms.ComboBox comboBox_Pattern;
        private System.Windows.Forms.Label label_Pattern;
    }
}

