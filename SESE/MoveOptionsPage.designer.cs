namespace Krkadoni.SESE
{
    sealed partial class MoveOptionsPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOptionsPage));
            this.lbPositions = new System.Windows.Forms.ListBox();
            this.txtOriginalPosition = new Krkadoni.SESE.PositionTextBox();
            this.lbOriginalPosition = new System.Windows.Forms.Label();
            this.txtDestination = new Krkadoni.SESE.PositionTextBox();
            this.lbDestination = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelPosition = new System.Windows.Forms.Panel();
            this.panelPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPositions
            // 
            this.lbPositions.DisplayMember = "Display";
            this.lbPositions.FormattingEnabled = true;
            resources.ApplyResources(this.lbPositions, "lbPositions");
            this.lbPositions.Name = "lbPositions";
            this.lbPositions.ValueMember = "Display";
            // 
            // txtOriginalPosition
            // 
            resources.ApplyResources(this.txtOriginalPosition, "txtOriginalPosition");
            this.txtOriginalPosition.Name = "txtOriginalPosition";
            // 
            // lbOriginalPosition
            // 
            resources.ApplyResources(this.lbOriginalPosition, "lbOriginalPosition");
            this.lbOriginalPosition.Name = "lbOriginalPosition";
            // 
            // txtDestination
            // 
            resources.ApplyResources(this.txtDestination, "txtDestination");
            this.txtDestination.Name = "txtDestination";
            // 
            // lbDestination
            // 
            resources.ApplyResources(this.lbDestination, "lbDestination");
            this.lbDestination.Name = "lbDestination";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.CausesValidation = false;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelPosition
            // 
            this.panelPosition.Controls.Add(this.lbDestination);
            this.panelPosition.Controls.Add(this.txtOriginalPosition);
            this.panelPosition.Controls.Add(this.txtDestination);
            this.panelPosition.Controls.Add(this.lbOriginalPosition);
            resources.ApplyResources(this.panelPosition, "panelPosition");
            this.panelPosition.Name = "panelPosition";
            // 
            // MoveOptionsPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPosition);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbPositions);
            this.Image = global::Krkadoni.SESE.Properties.Resources.move;
            this.Name = "MoveOptionsPage";
            this.panelPosition.ResumeLayout(false);
            this.panelPosition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbPositions;
        private PositionTextBox txtOriginalPosition;
        private System.Windows.Forms.Label lbOriginalPosition;
        private PositionTextBox txtDestination;
        private System.Windows.Forms.Label lbDestination;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelPosition;
    }
}
