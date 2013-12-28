// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Krkadoni.SESE.Properties;

namespace Krkadoni.SESE
{
    public partial class MoveOptionsPage : OptionsPage
    {

        private readonly TransformType _transformType;
        private readonly ErrorProvider _erp;
        private readonly BindingSource _bsPositions;

        public enum TransformType
        {
            Move = 1,
            Copy = 2
        }

        public MoveOptionsPage(TransformType transformType)
        {
            InitializeComponent();
            _transformType = transformType;
            switch (transformType)
            {
                case TransformType.Move:
                    base.Title = Resources.MoveOptionsPage_MoveOptionsPage_Move;
                    base.Image = Properties.Resources.move;
                    _bsPositions = new BindingSource(AppSettings.DefInstance.MoveList, "");
                    break;
                case TransformType.Copy:
                    base.Title = Resources.MoveOptionsPage_MoveOptionsPage_Copy;
                    base.Image = Properties.Resources.copy;
                    _bsPositions = new BindingSource(AppSettings.DefInstance.CopyList, "");
                    break;
            }

            txtOriginalPosition.DataBindings.Add(new Binding("Text", _bsPositions, "OriginalPosition", true, DataSourceUpdateMode.OnPropertyChanged));
            txtDestination.DataBindings.Add(new Binding("Text", _bsPositions, "Destination", true, DataSourceUpdateMode.OnPropertyChanged));
            _bsPositions.CurrentChanged += bsPositions_CurrentChanged;
            _erp = new ErrorProvider(this) { ContainerControl = this };
            lbPositions.DataSource = _bsPositions;
            panelPosition.Validating += panelPosition_Validating;
            txtOriginalPosition.Validating += txtOriginalPosition_Validating;
            txtDestination.Validating += txtDestination_Validating;
            txtOriginalPosition.DataBindings[0].Parse += MoveOptionsPage_Parse;
            txtDestination.DataBindings[0].Parse += MoveOptionsPage_Parse;
            btnDelete.Enabled = (lbPositions.SelectedItem != null);
           // panelPosition.Enabled = (lbPositions.SelectedItem != null);
        }

        void MoveOptionsPage_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
                e.Value = 0;
        }

        void txtDestination_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsPositions.Current != null)
            {
                var err = ((PositionTransform)_bsPositions.Current)["Destination"];
                _erp.SetError(txtDestination, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtDestination, string.Empty);
            }
        }

        void txtOriginalPosition_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsPositions.Current != null)
            {
                var err = ((PositionTransform)_bsPositions.Current)["OriginalPosition"];
                _erp.SetError(txtOriginalPosition, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtDestination, string.Empty);
            }
        }

        void panelPosition_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsPositions.Current != null)
            {
                e.Cancel = ((PositionTransform)_bsPositions.Current).Error.Length > 0;
            }
        }

        void bsPositions_CurrentChanged(object sender, System.EventArgs e)
        {
            var currentProfile = (PositionTransform)lbPositions.SelectedItem;
            btnDelete.Enabled = (lbPositions.SelectedItem != null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newTransform = new PositionTransform();
            newTransform.OriginalPosition = 0;
            newTransform.Destination = 0;
            switch (_transformType)
            {
                case TransformType.Copy:
                    AppSettings.DefInstance.CopyList.Add(newTransform);
                    break;
                case TransformType.Move:
                    AppSettings.DefInstance.MoveList.Add(newTransform);
                    break;
            }
            lbPositions.SelectedItem = newTransform;
            btnDelete.Enabled = (lbPositions.SelectedItem != null);
            var err = ((PositionTransform)_bsPositions.Current)["OriginalPosition"];
            _erp.SetError(txtOriginalPosition, err);
            txtOriginalPosition.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            switch (_transformType)
            {
                case TransformType.Copy:
                    AppSettings.DefInstance.CopyList.Remove((PositionTransform)lbPositions.SelectedItem);
                    break;
                case TransformType.Move:
                    AppSettings.DefInstance.MoveList.Remove((PositionTransform)lbPositions.SelectedItem);
                    break;
            }
            btnDelete.Enabled = (lbPositions.SelectedItem != null);
        }

    }
}
