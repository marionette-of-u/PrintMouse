namespace PrintMouse
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxBmpFilePath = new System.Windows.Forms.TextBox();
            this.buttonReferenceFile = new System.Windows.Forms.Button();
            this.buttonStartClick = new System.Windows.Forms.Button();
            this.RestTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxBmpFilePath
            // 
            this.textBoxBmpFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxBmpFilePath.Location = new System.Drawing.Point(12, 12);
            this.textBoxBmpFilePath.Name = "textBoxBmpFilePath";
            this.textBoxBmpFilePath.Size = new System.Drawing.Size(260, 19);
            this.textBoxBmpFilePath.TabIndex = 0;
            // 
            // buttonReferenceFile
            // 
            this.buttonReferenceFile.Location = new System.Drawing.Point(12, 37);
            this.buttonReferenceFile.Name = "buttonReferenceFile";
            this.buttonReferenceFile.Size = new System.Drawing.Size(75, 23);
            this.buttonReferenceFile.TabIndex = 1;
            this.buttonReferenceFile.Text = "参照";
            this.buttonReferenceFile.UseVisualStyleBackColor = true;
            this.buttonReferenceFile.Click += new System.EventHandler(this.buttonReferenceFile_Click);
            // 
            // buttonStartClick
            // 
            this.buttonStartClick.Location = new System.Drawing.Point(94, 37);
            this.buttonStartClick.Name = "buttonStartClick";
            this.buttonStartClick.Size = new System.Drawing.Size(130, 23);
            this.buttonStartClick.TabIndex = 2;
            this.buttonStartClick.Text = "待機... (Qキーで描画)";
            this.buttonStartClick.UseVisualStyleBackColor = true;
            this.buttonStartClick.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // RestTime
            // 
            this.RestTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RestTime.Location = new System.Drawing.Point(279, 12);
            this.RestTime.Name = "RestTime";
            this.RestTime.Size = new System.Drawing.Size(117, 19);
            this.RestTime.TabIndex = 3;
            this.RestTime.Text = "1";
            this.RestTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 77);
            this.Controls.Add(this.RestTime);
            this.Controls.Add(this.buttonStartClick);
            this.Controls.Add(this.buttonReferenceFile);
            this.Controls.Add(this.textBoxBmpFilePath);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PrintMouse";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBmpFilePath;
        private System.Windows.Forms.Button buttonReferenceFile;
        private System.Windows.Forms.Button buttonStartClick;
        private System.Windows.Forms.TextBox RestTime;
    }
}

