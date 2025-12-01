namespace Student_Management_System
{
    partial class frmAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttendance));
            this.cboCourseCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAllPresent = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblCourseName = new System.Windows.Forms.Label();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCourseCode
            // 
            this.cboCourseCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourseCode.FormattingEnabled = true;
            this.cboCourseCode.Location = new System.Drawing.Point(245, 224);
            this.cboCourseCode.Name = "cboCourseCode";
            this.cboCourseCode.Size = new System.Drawing.Size(138, 24);
            this.cboCourseCode.TabIndex = 91;
            this.cboCourseCode.SelectedIndexChanged += new System.EventHandler(this.cboCourseCode_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 22);
            this.label1.TabIndex = 90;
            this.label1.Text = "Select Course Code";
            // 
            // cboCourse
            // 
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.FormattingEnabled = true;
            this.cboCourse.Location = new System.Drawing.Point(245, 184);
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Size = new System.Drawing.Size(138, 24);
            this.cboCourse.TabIndex = 89;
            this.cboCourse.SelectedIndexChanged += new System.EventHandler(this.cboCourse_SelectedIndexChanged_1);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(245, 265);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(138, 22);
            this.dtpDate.TabIndex = 88;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnBack.Location = new System.Drawing.Point(199, 377);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(96, 28);
            this.btnBack.TabIndex = 87;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.btnClear.Location = new System.Drawing.Point(38, 377);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 28);
            this.btnClear.TabIndex = 86;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Peru;
            this.btnSave.Location = new System.Drawing.Point(624, 56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(164, 35);
            this.btnSave.TabIndex = 85;
            this.btnSave.Text = "Save Attendance";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAllPresent
            // 
            this.btnAllPresent.BackColor = System.Drawing.Color.Salmon;
            this.btnAllPresent.Location = new System.Drawing.Point(403, 56);
            this.btnAllPresent.Name = "btnAllPresent";
            this.btnAllPresent.Size = new System.Drawing.Size(164, 35);
            this.btnAllPresent.TabIndex = 84;
            this.btnAllPresent.Text = "Mark All Present";
            this.btnAllPresent.UseVisualStyleBackColor = false;
            this.btnAllPresent.Click += new System.EventHandler(this.btnAllPresent_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-8, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.Location = new System.Drawing.Point(23, 265);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(103, 22);
            this.lblCourse.TabIndex = 82;
            this.lblCourse.Text = "Select Date";
            // 
            // lblCourseName
            // 
            this.lblCourseName.AutoSize = true;
            this.lblCourseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourseName.Location = new System.Drawing.Point(23, 186);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(123, 22);
            this.lblCourseName.TabIndex = 81;
            this.lblCourseName.Text = "Select Course";
            // 
            // dgvAttendance
            // 
            this.dgvAttendance.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendance.Location = new System.Drawing.Point(403, 103);
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.RowHeadersWidth = 51;
            this.dgvAttendance.RowTemplate.Height = 24;
            this.dgvAttendance.Size = new System.Drawing.Size(385, 313);
            this.dgvAttendance.TabIndex = 80;
            this.dgvAttendance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttendance_CellContentClick);
            // 
            // frmAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cboCourseCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCourse);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAllPresent);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.lblCourseName);
            this.Controls.Add(this.dgvAttendance);
            this.Name = "frmAttendance";
            this.Text = "frmAttendance";
            this.Load += new System.EventHandler(this.frmAttendance_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCourseCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAllPresent;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label lblCourseName;
        private System.Windows.Forms.DataGridView dgvAttendance;
    }
}