namespace PES5_WE9_LE_CameraTool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.chkFixStadClipping = new System.Windows.Forms.CheckBox();
            this.chkAddStadRoof = new System.Windows.Forms.CheckBox();
            this.lblCameraZoom = new System.Windows.Forms.Label();
            this.nudCameraZoom = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurrentConfig = new System.Windows.Forms.Label();
            this.lblCurrentExecutable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCameraZoom)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkFixStadClipping
            // 
            this.chkFixStadClipping.AutoSize = true;
            this.chkFixStadClipping.Location = new System.Drawing.Point(31, 80);
            this.chkFixStadClipping.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkFixStadClipping.Name = "chkFixStadClipping";
            this.chkFixStadClipping.Size = new System.Drawing.Size(146, 20);
            this.chkFixStadClipping.TabIndex = 0;
            this.chkFixStadClipping.Text = "Fix Stadium Clipping";
            this.chkFixStadClipping.UseVisualStyleBackColor = true;
            // 
            // chkAddStadRoof
            // 
            this.chkAddStadRoof.AutoSize = true;
            this.chkAddStadRoof.Location = new System.Drawing.Point(31, 118);
            this.chkAddStadRoof.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAddStadRoof.Name = "chkAddStadRoof";
            this.chkAddStadRoof.Size = new System.Drawing.Size(131, 20);
            this.chkAddStadRoof.TabIndex = 1;
            this.chkAddStadRoof.Text = "Add Stadium Roof";
            this.chkAddStadRoof.UseVisualStyleBackColor = true;
            // 
            // lblCameraZoom
            // 
            this.lblCameraZoom.AutoSize = true;
            this.lblCameraZoom.Location = new System.Drawing.Point(30, 45);
            this.lblCameraZoom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCameraZoom.Name = "lblCameraZoom";
            this.lblCameraZoom.Size = new System.Drawing.Size(88, 16);
            this.lblCameraZoom.TabIndex = 4;
            this.lblCameraZoom.Text = "Camera Zoom";
            // 
            // nudCameraZoom
            // 
            this.nudCameraZoom.DecimalPlaces = 2;
            this.nudCameraZoom.Location = new System.Drawing.Point(195, 45);
            this.nudCameraZoom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudCameraZoom.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudCameraZoom.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudCameraZoom.Name = "nudCameraZoom";
            this.nudCameraZoom.Size = new System.Drawing.Size(99, 22);
            this.nudCameraZoom.TabIndex = 5;
            this.nudCameraZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCameraZoom.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(365, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // lblCurrentConfig
            // 
            this.lblCurrentConfig.AutoSize = true;
            this.lblCurrentConfig.Location = new System.Drawing.Point(31, 188);
            this.lblCurrentConfig.Name = "lblCurrentConfig";
            this.lblCurrentConfig.Size = new System.Drawing.Size(0, 16);
            this.lblCurrentConfig.TabIndex = 7;
            // 
            // lblCurrentExecutable
            // 
            this.lblCurrentExecutable.AutoSize = true;
            this.lblCurrentExecutable.Location = new System.Drawing.Point(33, 216);
            this.lblCurrentExecutable.Name = "lblCurrentExecutable";
            this.lblCurrentExecutable.Size = new System.Drawing.Size(0, 16);
            this.lblCurrentExecutable.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 184);
            this.Controls.Add(this.lblCurrentExecutable);
            this.Controls.Add(this.lblCurrentConfig);
            this.Controls.Add(this.nudCameraZoom);
            this.Controls.Add(this.lblCameraZoom);
            this.Controls.Add(this.chkAddStadRoof);
            this.Controls.Add(this.chkFixStadClipping);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PES5/WE9/LE Camera Zoom Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCameraZoom)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFixStadClipping;
        private System.Windows.Forms.CheckBox chkAddStadRoof;
        private System.Windows.Forms.Label lblCameraZoom;
        private System.Windows.Forms.NumericUpDown nudCameraZoom;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.Label lblCurrentConfig;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Label lblCurrentExecutable;
    }
}

