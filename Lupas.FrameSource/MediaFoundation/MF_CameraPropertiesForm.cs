// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// MF_CameraPropertiesForm.cs : 30.8.2020
// MIT license

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lupas.FrameSource.MediaFoundation
{
    public partial class MF_CameraPropertiesForm : Form
    {
        public event EventHandler OnTransformChanged;
        public event EventHandler OnSourceReaderChanged;
        public event PropertyBarChangedEventHandler OnPropertyChanged;
        private readonly List<AMCamProperty> props;

        public void SetReaderCheckBoxes(bool adv, bool dxdeice)
        {
            manageRadioButtonsHandlers(false);
            checkBoxAdvancedProcessing.Checked = adv;
            checkBoxDXDevice.Checked = dxdeice;
            manageRadioButtonsHandlers(true);
        }

        public (bool adv, bool dxdev) GetReaderCheckBoxes()
        {
            return (checkBoxAdvancedProcessing.Checked, checkBoxDXDevice.Checked);
        }

        public int SelectedMediaType
        {
            get { return comboBoxMediaTypes.SelectedIndex; }
            set { if (comboBoxMediaTypes.Items.Count > value) comboBoxMediaTypes.SelectedIndex = value; }
        }

        public MF_CameraPropertiesForm(string deviceName, IEnumerable<string> types, List<AMCamProperty> props)
        {
            InitializeComponent();

            comboBoxMediaTypes.Items.AddRange(types.ToArray());

            this.Text += $" [{deviceName}]";

            this.props = props;
            PopulateProperties();

            manageRadioButtonsHandlers(true);
        }

        public MediaTransform Transform {
            get{
                if (radioButtonNV12.Checked) return MediaTransform.ForceNV12;
                if (radioButtonRGBA.Checked) return MediaTransform.ForceRGB32;
                return MediaTransform.NoTransform;
            }
            set
            {
                manageRadioButtonsHandlers(false);
                radioButtonRaw.Checked = value == MediaTransform.NoTransform;
                radioButtonNV12.Checked = value == MediaTransform.ForceNV12;
                radioButtonRGBA.Checked = value == MediaTransform.ForceRGB32;
                manageRadioButtonsHandlers(true);
            }
        }

        private void manageRadioButtonsHandlers(bool set)
        {
            if (set)
            {
                radioButtonRaw.CheckedChanged += CommandTransform_Changed;
                radioButtonNV12.CheckedChanged += CommandTransform_Changed;
                radioButtonRGBA.CheckedChanged += CommandTransform_Changed;
                checkBoxAdvancedProcessing.CheckedChanged += CommandSourceReader_Changed;
                checkBoxDXDevice.CheckedChanged += CommandSourceReader_Changed;
            }
            else
            {
                radioButtonRaw.CheckedChanged -= CommandTransform_Changed;
                radioButtonNV12.CheckedChanged -= CommandTransform_Changed;
                radioButtonRGBA.CheckedChanged -= CommandTransform_Changed;
                checkBoxAdvancedProcessing.CheckedChanged -= CommandSourceReader_Changed;
                checkBoxDXDevice.CheckedChanged -= CommandSourceReader_Changed;
            }
        }

        private void CommandTransform_Changed(object sender, EventArgs e)
        {
            OnTransformChanged?.Invoke(this, new EventArgs());
        }
        private void CommandSourceReader_Changed(object sender, EventArgs e)
        {
            OnSourceReaderChanged?.Invoke(this, new EventArgs());
        }

        private void PopulateProperties()
        {
            foreach (var p in props)
            {
                var val = p.Value;
                var ctl = new PropertyBarControl(p.PropertyName, val, p.Flag, p.Min, p.Max, p.Delta, p.PossibleFlags == 0);
                ctl.Tag = p;
                ctl.OnPropertyChanged += Ctl_OnPropertyChanged;
                
                if (p is AMVideoProcAmpProperty) flowLayoutPanelVidProc.Controls.Add(ctl);
                if (p is AMCameraControlProperty) flowLayoutPanelCamControl.Controls.Add(ctl);
            }
        }

        private void Ctl_OnPropertyChanged(object sender, PropertyBarEventArgs e) => OnPropertyChanged?.Invoke(this, e);


        private void comboBoxMediaTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTransformChanged?.Invoke(this, new EventArgs());
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            var ctls1 = from PropertyBarControl c in flowLayoutPanelVidProc.Controls select c;
            var ctls2 = from PropertyBarControl c in flowLayoutPanelCamControl.Controls select c;

            foreach (var c in ctls1.Union(ctls2))
                c.SetDevaultValue(props.Find(p => c.Tag == p).DefaultValue);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MF_CameraPropertiesForm_MouseLeave(object sender, EventArgs e)
        {
        }
    }

    public delegate void PropertyBarChangedEventHandler(object sender, PropertyBarEventArgs e);
    public class PropertyBarEventArgs : EventArgs
    {
        public readonly object prop;
        public readonly int val;
        public readonly int flag;
        public PropertyBarEventArgs(object property, int newValue, int newFlag)
        {
            prop = property;
            val = newValue;
            flag = newFlag;
        }
    }
}
