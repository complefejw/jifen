
using System.Windows.Forms;

namespace 消防积分获取
{

    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            timer1 = new Timer(components);
            checkBox1 = new CheckBox();
            listBox1 = new ListBox();
            button1 = new Button();
            button3 = new Button();
            checkBox2 = new CheckBox();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            alipay = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            系统ID = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox3 = new ComboBox();
            textBox4 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(36, 40);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(114, 21);
            label1.TabIndex = 0;
            label1.Text = "Authorization";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(156, 5);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(728, 78);
            textBox1.TabIndex = 2;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new System.Drawing.Point(901, 5);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(68, 38);
            button2.TabIndex = 7;
            button2.Text = "增加配置";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // timer1
            // 
            timer1.Interval = 10000;
            timer1.Tick += timer1_Tick;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox1.AutoSize = true;
            checkBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBox1.ForeColor = System.Drawing.Color.Red;
            checkBox1.Location = new System.Drawing.Point(329, 450);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(125, 25);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "是否正常播放";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Visible = false;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new System.Drawing.Point(487, 428);
            listBox1.Name = "listBox1";
            listBox1.ScrollAlwaysVisible = true;
            listBox1.Size = new System.Drawing.Size(482, 140);
            listBox1.TabIndex = 16;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button1.Location = new System.Drawing.Point(22, 428);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(298, 140);
            button1.TabIndex = 15;
            button1.Text = "开始执行";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.ForeColor = System.Drawing.Color.Red;
            button3.Location = new System.Drawing.Point(901, 49);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(68, 38);
            button3.TabIndex = 18;
            button3.Text = "禁用配置";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // checkBox2
            // 
            checkBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox2.AutoSize = true;
            checkBox2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBox2.ForeColor = System.Drawing.Color.Red;
            checkBox2.Location = new System.Drawing.Point(329, 428);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(125, 25);
            checkBox2.TabIndex = 19;
            checkBox2.Text = "仅执行选中项";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, alipay, Column6, Column7, 系统ID, Column8 });
            dataGridView1.Location = new System.Drawing.Point(22, 93);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(947, 329);
            dataGridView1.TabIndex = 21;
            // 
            // Column1
            // 
            Column1.HeaderText = "用户ID";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "姓名";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 80;
            // 
            // Column3
            // 
            Column3.HeaderText = "增加时间";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 130;
            // 
            // Column4
            // 
            Column4.HeaderText = "过期时间";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 130;
            // 
            // alipay
            // 
            alipay.HeaderText = "alipay";
            alipay.Name = "alipay";
            alipay.ReadOnly = true;
            alipay.Width = 70;
            // 
            // Column6
            // 
            Column6.HeaderText = "Authorization";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.HeaderText = "今日初始分";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 110;
            // 
            // 系统ID
            // 
            系统ID.HeaderText = "系统ID";
            系统ID.Name = "系统ID";
            系统ID.ReadOnly = true;
            系统ID.Width = 70;
            // 
            // Column8
            // 
            Column8.HeaderText = "今日新增分";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // Column9
            // 
            Column9.Name = "Column9";
            // 
            // Column10
            // 
            Column10.Name = "Column10";
            // 
            // Column11
            // 
            Column11.Name = "Column11";
            // 
            // Column12
            // 
            Column12.Name = "Column12";
            // 
            // Column13
            // 
            Column13.Name = "Column13";
            // 
            // Column14
            // 
            Column14.Name = "Column14";
            // 
            // Column15
            // 
            Column15.Name = "Column15";
            // 
            // Column16
            // 
            Column16.Name = "Column16";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(329, 478);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(90, 21);
            label4.TabIndex = 22;
            label4.Text = "当前时间：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(329, 499);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(90, 21);
            label3.TabIndex = 23;
            label3.Text = "定时执行：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(382, 525);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(26, 21);
            label5.TabIndex = 25;
            label5.Text = "时";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(460, 525);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(26, 21);
            label6.TabIndex = 27;
            label6.Text = "分";
            // 
            // textBox3
            // 
            textBox3.FormattingEnabled = true;
            textBox3.Items.AddRange(new object[] { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" });
            textBox3.Location = new System.Drawing.Point(333, 523);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(43, 25);
            textBox3.TabIndex = 28;
            textBox3.Text = "05";
            // 
            // textBox4
            // 
            textBox4.FormattingEnabled = true;
            textBox4.Items.AddRange(new object[] { "00", "15", "30", "45", "50" });
            textBox4.Location = new System.Drawing.Point(411, 523);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(43, 25);
            textBox4.TabIndex = 29;
            textBox4.Text = "30";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(987, 580);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(dataGridView1);
            Controls.Add(checkBox2);
            Controls.Add(button3);
            Controls.Add(checkBox1);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "一个看不得大家重复劳动的人帮大家完成消防任务.我不想被网警请去喝茶,请大家合理使用,禁止大面积传播";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        //private DoubleBufferDataGridView dataGridView1;
        private CheckBox checkBox2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private Label label4;
        private Label label3;
        private Label label5;
        private Label label6;
        private ComboBox textBox3;
        private ComboBox textBox4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn alipay;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn 系统ID;
        private DataGridViewTextBoxColumn Column8;
    }
}

