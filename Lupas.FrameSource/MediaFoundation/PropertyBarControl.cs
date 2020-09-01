// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// PropertyBarControl.cs : 30.8.2020
// MIT license

using System;
using System.Windows.Forms;

namespace Lupas.FrameSource.MediaFoundation
{
    public partial class PropertyBarControl : UserControl
    {
        public event PropertyBarChangedEventHandler OnPropertyChanged;

        public PropertyBarControl()
        {
            InitializeComponent();
        }

        public PropertyBarControl(string propertyName, int value, int flags, int min, int max, int delta, bool enableFlags)
        {
            InitializeComponent();
            labelName.Text = propertyName;
            trackBarValues.Minimum = min;
            trackBarValues.Maximum = max;
            trackBarValues.Value = value;
            trackBarValues.SmallChange = delta;
            checkBoxAuto.Enabled = enableFlags;
            if (enableFlags) checkBoxAuto.Checked = flags == 1 ? false : true;
        }

        internal void SetDevaultValue(int defaultValue)
        {
            trackBarValues.Value = defaultValue;
        }

        private void trackBarValues_ValueChanged(object sender, EventArgs e)
        {
            var flag = checkBoxAuto.Checked ? 1 : 2;
            OnPropertyChanged?.Invoke(this, new PropertyBarEventArgs(this.Tag, trackBarValues.Value, flag));
        }
    }
}
