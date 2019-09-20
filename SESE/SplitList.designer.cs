namespace Krkadoni.SESE
{
    partial class SplitList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.Version = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcessTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.menuTasks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ProcessTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSatellites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeselectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnZIP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDownload = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpload = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridTasks = new System.Windows.Forms.DataGridView();
            this.gridSatellites = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbTaskName = new System.Windows.Forms.Label();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lbTaskDirectory = new System.Windows.Forms.Label();
            this.txtTaskDirectory = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtZipFileName = new System.Windows.Forms.TextBox();
            this.lbTaskFileName = new System.Windows.Forms.Label();
            this.ceZIP = new System.Windows.Forms.CheckBox();
            this.ceStream = new System.Windows.Forms.CheckBox();
            this.ceDVBC = new System.Windows.Forms.CheckBox();
            this.ceDVBT = new System.Windows.Forms.CheckBox();
            this.bgwOpen = new System.ComponentModel.BackgroundWorker();
            this.vmXmlSatelliteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuTasks.SuspendLayout();
            this.menuSatellites.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSatellites)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vmXmlSatelliteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.ProgressBar,
            this.Version});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            resources.ApplyResources(this.Status, "Status");
            this.Status.Spring = true;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ProgressBar.Name = "ProgressBar";
            resources.ApplyResources(this.ProgressBar, "ProgressBar");
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // Version
            // 
            this.Version.Name = "Version";
            this.Version.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            resources.ApplyResources(this.Version, "Version");
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddTask,
            this.toolStripSeparator1,
            this.btnRemoveTask,
            this.toolStripSeparator2,
            this.btnProcessTask,
            this.toolStripSeparator3,
            this.btnSaveTask,
            this.toolStripSeparator4,
            this.btnSettings,
            this.toolStripSeparator5,
            this.btnAbout});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnAddTask
            // 
            this.btnAddTask.Image = global::Krkadoni.SESE.Properties.Resources.icon_add_item;
            resources.ApplyResources(this.btnAddTask, "btnAddTask");
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnRemoveTask
            // 
            this.btnRemoveTask.Image = global::Krkadoni.SESE.Properties.Resources.icon_delete;
            resources.ApplyResources(this.btnRemoveTask, "btnRemoveTask");
            this.btnRemoveTask.Name = "btnRemoveTask";
            this.btnRemoveTask.Click += new System.EventHandler(this.btnRemoveTask_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnProcessTask
            // 
            this.btnProcessTask.Image = global::Krkadoni.SESE.Properties.Resources.icon_computer_ok;
            resources.ApplyResources(this.btnProcessTask, "btnProcessTask");
            this.btnProcessTask.Name = "btnProcessTask";
            this.btnProcessTask.Click += new System.EventHandler(this.btnProcessTask_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // btnSaveTask
            // 
            this.btnSaveTask.Image = global::Krkadoni.SESE.Properties.Resources.icon_save;
            resources.ApplyResources(this.btnSaveTask, "btnSaveTask");
            this.btnSaveTask.Name = "btnSaveTask";
            this.btnSaveTask.Click += new System.EventHandler(this.btnSaveTask_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::Krkadoni.SESE.Properties.Resources.icon_gear;
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::Krkadoni.SESE.Properties.Resources.icon_questionmark;
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // menuTasks
            // 
            this.menuTasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveTaskToolStripMenuItem,
            this.toolStripSeparator6,
            this.ProcessTaskToolStripMenuItem,
            this.ProcessAllTasksToolStripMenuItem});
            this.menuTasks.Name = "menuTasks";
            resources.ApplyResources(this.menuTasks, "menuTasks");
            // 
            // RemoveTaskToolStripMenuItem
            // 
            this.RemoveTaskToolStripMenuItem.Image = global::Krkadoni.SESE.Properties.Resources.icon_delete_small;
            this.RemoveTaskToolStripMenuItem.Name = "RemoveTaskToolStripMenuItem";
            resources.ApplyResources(this.RemoveTaskToolStripMenuItem, "RemoveTaskToolStripMenuItem");
            this.RemoveTaskToolStripMenuItem.Click += new System.EventHandler(this.RemoveTaskToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ProcessTaskToolStripMenuItem
            // 
            this.ProcessTaskToolStripMenuItem.Name = "ProcessTaskToolStripMenuItem";
            resources.ApplyResources(this.ProcessTaskToolStripMenuItem, "ProcessTaskToolStripMenuItem");
            this.ProcessTaskToolStripMenuItem.Click += new System.EventHandler(this.ProcessTaskToolStripMenuItem_Click);
            // 
            // ProcessAllTasksToolStripMenuItem
            // 
            this.ProcessAllTasksToolStripMenuItem.Image = global::Krkadoni.SESE.Properties.Resources.icon_computer_ok_small;
            this.ProcessAllTasksToolStripMenuItem.Name = "ProcessAllTasksToolStripMenuItem";
            resources.ApplyResources(this.ProcessAllTasksToolStripMenuItem, "ProcessAllTasksToolStripMenuItem");
            this.ProcessAllTasksToolStripMenuItem.Click += new System.EventHandler(this.ProcessAllTasksToolStripMenuItem_Click);
            // 
            // menuSatellites
            // 
            this.menuSatellites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectToolStripMenuItem,
            this.DeselectToolStripMenuItem1,
            this.toolStripSeparator7,
            this.SelectAllToolStripMenuItem,
            this.DeselectToolStripMenuItem});
            this.menuSatellites.Name = "menuSatellites";
            resources.ApplyResources(this.menuSatellites, "menuSatellites");
            // 
            // SelectToolStripMenuItem
            // 
            this.SelectToolStripMenuItem.Name = "SelectToolStripMenuItem";
            resources.ApplyResources(this.SelectToolStripMenuItem, "SelectToolStripMenuItem");
            this.SelectToolStripMenuItem.Click += new System.EventHandler(this.SelectToolStripMenuItem_Click);
            // 
            // DeselectToolStripMenuItem1
            // 
            this.DeselectToolStripMenuItem1.Name = "DeselectToolStripMenuItem1";
            resources.ApplyResources(this.DeselectToolStripMenuItem1, "DeselectToolStripMenuItem1");
            this.DeselectToolStripMenuItem1.Click += new System.EventHandler(this.DeselectToolStripMenuItem1_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // SelectAllToolStripMenuItem
            // 
            this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
            resources.ApplyResources(this.SelectAllToolStripMenuItem, "SelectAllToolStripMenuItem");
            this.SelectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // DeselectToolStripMenuItem
            // 
            this.DeselectToolStripMenuItem.Name = "DeselectToolStripMenuItem";
            resources.ApplyResources(this.DeselectToolStripMenuItem, "DeselectToolStripMenuItem");
            this.DeselectToolStripMenuItem.Click += new System.EventHandler(this.DeselectToolStripMenuItem_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AllowMerge = false;
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZIP,
            this.toolStripSeparator8,
            this.btnOpen,
            this.toolStripSeparator9,
            this.btnDownload,
            this.toolStripSeparator10,
            this.btnUpload,
            this.toolStripSeparator11});
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Name = "toolStrip2";
            // 
            // btnZIP
            // 
            this.btnZIP.Image = global::Krkadoni.SESE.Properties.Resources.icon_zip;
            resources.ApplyResources(this.btnZIP, "btnZIP");
            this.btnZIP.Name = "btnZIP";
            this.btnZIP.Click += new System.EventHandler(this.btnZIP_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::Krkadoni.SESE.Properties.Resources.icon_open;
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // btnDownload
            // 
            this.btnDownload.Image = global::Krkadoni.SESE.Properties.Resources.icon_down;
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.Name = "btnDownload";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            // 
            // btnUpload
            // 
            this.btnUpload.Image = global::Krkadoni.SESE.Properties.Resources.icon_up;
            resources.ApplyResources(this.btnUpload, "btnUpload");
            this.btnUpload.Name = "btnUpload";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.gridTasks, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridSatellites, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // gridTasks
            // 
            this.gridTasks.AllowUserToAddRows = false;
            this.gridTasks.AllowUserToDeleteRows = false;
            this.gridTasks.AllowUserToOrderColumns = true;
            this.gridTasks.AllowUserToResizeColumns = false;
            this.gridTasks.AllowUserToResizeRows = false;
            this.gridTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTasks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTasks.ContextMenuStrip = this.menuTasks;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTasks.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.gridTasks, "gridTasks");
            this.gridTasks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTasks.Name = "gridTasks";
            this.gridTasks.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTasks.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTasks.RowHeadersVisible = false;
            this.gridTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // gridSatellites
            // 
            this.gridSatellites.AllowUserToAddRows = false;
            this.gridSatellites.AllowUserToDeleteRows = false;
            this.gridSatellites.AllowUserToResizeColumns = false;
            this.gridSatellites.AllowUserToResizeRows = false;
            this.gridSatellites.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSatellites.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridSatellites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSatellites.ContextMenuStrip = this.menuSatellites;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSatellites.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.gridSatellites, "gridSatellites");
            this.gridSatellites.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridSatellites.Name = "gridSatellites";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSatellites.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridSatellites.RowHeadersVisible = false;
            this.gridSatellites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.lbTaskName, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtTaskName, 1, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // lbTaskName
            // 
            resources.ApplyResources(this.lbTaskName, "lbTaskName");
            this.lbTaskName.Name = "lbTaskName";
            // 
            // txtTaskName
            // 
            resources.ApplyResources(this.txtTaskName, "txtTaskName");
            this.txtTaskName.Name = "txtTaskName";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.btnFolder, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbTaskDirectory, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtTaskDirectory, 1, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // btnFolder
            // 
            resources.ApplyResources(this.btnFolder, "btnFolder");
            this.btnFolder.Image = global::Krkadoni.SESE.Properties.Resources.open2;
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lbTaskDirectory
            // 
            resources.ApplyResources(this.lbTaskDirectory, "lbTaskDirectory");
            this.lbTaskDirectory.Name = "lbTaskDirectory";
            // 
            // txtTaskDirectory
            // 
            resources.ApplyResources(this.txtTaskDirectory, "txtTaskDirectory");
            this.txtTaskDirectory.Name = "txtTaskDirectory";
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.txtZipFileName, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.lbTaskFileName, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.ceZIP, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.ceStream, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.ceDVBC, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.ceDVBT, 0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // txtZipFileName
            // 
            resources.ApplyResources(this.txtZipFileName, "txtZipFileName");
            this.txtZipFileName.Name = "txtZipFileName";
            // 
            // lbTaskFileName
            // 
            resources.ApplyResources(this.lbTaskFileName, "lbTaskFileName");
            this.lbTaskFileName.Name = "lbTaskFileName";
            // 
            // ceZIP
            // 
            resources.ApplyResources(this.ceZIP, "ceZIP");
            this.ceZIP.Checked = true;
            this.ceZIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ceZIP.Name = "ceZIP";
            this.ceZIP.UseVisualStyleBackColor = true;
            this.ceZIP.CheckedChanged += new System.EventHandler(this.ceZIP_CheckedChanged);
            // 
            // ceStream
            // 
            resources.ApplyResources(this.ceStream, "ceStream");
            this.ceStream.Checked = true;
            this.ceStream.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ceStream.Name = "ceStream";
            this.ceStream.UseVisualStyleBackColor = true;
            // 
            // ceDVBC
            // 
            resources.ApplyResources(this.ceDVBC, "ceDVBC");
            this.ceDVBC.Checked = true;
            this.ceDVBC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ceDVBC.Name = "ceDVBC";
            this.ceDVBC.UseVisualStyleBackColor = true;
            // 
            // ceDVBT
            // 
            resources.ApplyResources(this.ceDVBT, "ceDVBT");
            this.ceDVBT.Checked = true;
            this.ceDVBT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ceDVBT.Name = "ceDVBT";
            this.ceDVBT.UseVisualStyleBackColor = true;
            // 
            // bgwOpen
            // 
            this.bgwOpen.WorkerReportsProgress = true;
            this.bgwOpen.WorkerSupportsCancellation = true;
            // 
            // vmXmlSatelliteBindingSource
            // 
            this.vmXmlSatelliteBindingSource.DataSource = typeof(Krkadoni.SESE.VmXmlSatellite);
            // 
            // SplitList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "SplitList";
            this.Load += new System.EventHandler(this.FrmSplitList_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuTasks.ResumeLayout(false);
            this.menuSatellites.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSatellites)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vmXmlSatelliteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRemoveTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnProcessTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSaveTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnAbout;
        internal System.Windows.Forms.ContextMenuStrip menuTasks;
        internal System.Windows.Forms.ToolStripMenuItem RemoveTaskToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripMenuItem ProcessTaskToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ProcessAllTasksToolStripMenuItem;
        internal System.Windows.Forms.ContextMenuStrip menuSatellites;
        internal System.Windows.Forms.ToolStripMenuItem SelectToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DeselectToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DeselectToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnZIP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripDropDownButton btnDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripDropDownButton btnUpload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView gridTasks;
        private System.Windows.Forms.DataGridView gridSatellites;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbTaskDirectory;
        private System.Windows.Forms.TextBox txtTaskDirectory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbTaskName;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox ceZIP;
        private System.Windows.Forms.Label lbTaskFileName;
        private System.Windows.Forms.TextBox txtZipFileName;
        private System.Windows.Forms.ToolStripStatusLabel Version;
        private System.ComponentModel.BackgroundWorker bgwOpen;
        private System.Windows.Forms.CheckBox ceStream;
        private System.Windows.Forms.CheckBox ceDVBC;
        private System.Windows.Forms.CheckBox ceDVBT;
        private System.Windows.Forms.BindingSource vmXmlSatelliteBindingSource;
    }
}