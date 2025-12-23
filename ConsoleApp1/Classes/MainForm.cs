using System;
using System.Windows.Forms;

namespace ConsoleApp1.Classes
{
    public class MainForm : Form
    {
        public string SelectedFilePath { get; private set; } = string.Empty;
        public string SelectedFormatKey { get; private set; } = "2"; // "1"=Rockwell, "2"=Omron

        private TextBox txtPath;
        private Button btnBrowse;
        private ComboBox cmbFormat;
        private Button btnOk;
        private Button btnCancel;

        public MainForm(string defaultPath = "")
        {
            InitializeComponents();
            if (!string.IsNullOrEmpty(defaultPath)) txtPath.Text = defaultPath;
        }

        private void InitializeComponents()
        {
            Text = "Alarm Export - Input";
            Width = 600;
            Height = 180;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblPath = new Label() { Text = "Excel file path:", Left = 10, Top = 20, Width = 100 };
            txtPath = new TextBox() { Left = 120, Top = 18, Width = 360 };
            btnBrowse = new Button() { Text = "Browse...", Left = 490, Top = 16, Width = 80 };
            btnBrowse.Click += (s, e) =>
            {
                using var dlg = new OpenFileDialog();
                dlg.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (dlg.ShowDialog() == DialogResult.OK) txtPath.Text = dlg.FileName;
            };

            var lblFormat = new Label() { Text = "Input format:", Left = 10, Top = 60, Width = 100 };
            cmbFormat = new ComboBox() { Left = 120, Top = 58, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbFormat.Items.Add(new ComboBoxItem("Rockwell", "1"));
            cmbFormat.Items.Add(new ComboBoxItem("Omron", "2"));
            cmbFormat.SelectedIndex = 1; // default Omron

            btnOk = new Button() { Text = "OK", Left = 360, Top = 100, Width = 95, DialogResult = DialogResult.OK };
            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    MessageBox.Show("Please specify a file path.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None; // prevent dialog from closing
                    return;
                }
                SelectedFilePath = txtPath.Text.Trim();
                var sel = cmbFormat.SelectedItem as ComboBoxItem;
                SelectedFormatKey = sel?.Key ?? "2";
                this.Close();
            };

            btnCancel = new Button() { Text = "Cancel", Left = 470, Top = 100, Width = 95, DialogResult = DialogResult.Cancel };

            Controls.AddRange(new Control[] { lblPath, txtPath, btnBrowse, lblFormat, cmbFormat, btnOk, btnCancel });
        }

        private class ComboBoxItem
        {
            public string Text { get; }
            public string Key { get; }
            public ComboBoxItem(string text, string key) { Text = text; Key = key; }
            public override string ToString() => Text;
        }
    }
}
