namespace Krkadoni.SESE
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.panelIcons = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelPage = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panelIcons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelIcons
            // 
            resources.ApplyResources(this.panelIcons, "panelIcons");
            this.panelIcons.Controls.Add(this.listView);
            this.panelIcons.Name = "panelIcons";
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "run.png");
            // 
            // panelPage
            // 
            resources.ApplyResources(this.panelPage, "panelPage");
            this.panelPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPage.Name = "panelPage";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // OptionsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.CausesValidation = false;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelPage);
            this.Controls.Add(this.panelIcons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsDialog_FormClosed);
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.panelIcons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIcons;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Panel panelPage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnApply;
    }
}