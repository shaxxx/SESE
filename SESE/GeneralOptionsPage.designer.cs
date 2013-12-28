namespace Krkadoni.SESE
{
    partial class GeneralOptionsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionsPage));
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.ceDonated = new System.Windows.Forms.CheckBox();
            this.lbLanguage = new System.Windows.Forms.Label();
            this.tableLanguage = new System.Windows.Forms.TableLayoutPanel();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.lbAuthorName = new System.Windows.Forms.Label();
            this.ceUpdates = new System.Windows.Forms.CheckBox();
            this.tableLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLanguage
            // 
            resources.ApplyResources(this.cbLanguage, "cbLanguage");
            this.cbLanguage.DisplayMember = "Name";
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.ValueMember = "Name";
            // 
            // ceDonated
            // 
            resources.ApplyResources(this.ceDonated, "ceDonated");
            this.ceDonated.Name = "ceDonated";
            this.ceDonated.Tag = "";
            this.ceDonated.UseVisualStyleBackColor = true;
            // 
            // lbLanguage
            // 
            resources.ApplyResources(this.lbLanguage, "lbLanguage");
            this.lbLanguage.Name = "lbLanguage";
            // 
            // tableLanguage
            // 
            resources.ApplyResources(this.tableLanguage, "tableLanguage");
            this.tableLanguage.Controls.Add(this.lbLanguage, 0, 0);
            this.tableLanguage.Controls.Add(this.cbLanguage, 1, 0);
            this.tableLanguage.Controls.Add(this.lbAuthor, 0, 1);
            this.tableLanguage.Controls.Add(this.lbAuthorName, 1, 1);
            this.tableLanguage.Name = "tableLanguage";
            // 
            // lbAuthor
            // 
            resources.ApplyResources(this.lbAuthor, "lbAuthor");
            this.lbAuthor.Name = "lbAuthor";
            // 
            // lbAuthorName
            // 
            resources.ApplyResources(this.lbAuthorName, "lbAuthorName");
            this.lbAuthorName.Name = "lbAuthorName";
            // 
            // ceUpdates
            // 
            resources.ApplyResources(this.ceUpdates, "ceUpdates");
            this.ceUpdates.Name = "ceUpdates";
            this.ceUpdates.Tag = "";
            this.ceUpdates.UseVisualStyleBackColor = true;
            // 
            // GeneralOptionsPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLanguage);
            this.Controls.Add(this.ceUpdates);
            this.Controls.Add(this.ceDonated);
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Name = "GeneralOptionsPage";
            this.tableLanguage.ResumeLayout(false);
            this.tableLanguage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.CheckBox ceDonated;
        private System.Windows.Forms.Label lbLanguage;
        private System.Windows.Forms.TableLayoutPanel tableLanguage;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.Label lbAuthorName;
        private System.Windows.Forms.CheckBox ceUpdates;

    }
}
