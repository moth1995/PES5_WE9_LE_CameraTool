using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace PES5_WE9_LE_CameraTool
{
    public partial class frmMain : Form
    {
        private string executablePath;
        private Configuration config = new Configuration();
        private byte[] stadRoofValueOn = BitConverter.GetBytes((float)-1.0);
        private byte[] stadRoofValueOff = new byte[sizeof(float)] { 0x50, 0x77, 0xD6, 0xBD };

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(nudCameraZoom, "Minimum value allowed by the game is 200.0");
            toolTip.SetToolTip(lblCameraZoom, "This value will apply zoom for cameras type: \n- Normal (Close, Medium, Long)\n- Wide\n- Broadcasting 1\n- Zoom");
            toolTip.SetToolTip(chkFixStadClipping, "With this option we fix the clipping when we use lower zoom values");
            toolTip.SetToolTip(chkAddStadRoof, "With this option the stadium roof will be rendered during the matches");
            LoadConfigFiles();
            lblCurrentExecutable.Text = $"Current executable: {executablePath}";
        }

        private void LoadConfigFiles()
        {
            string configFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");
            lblCurrentConfig.Text = $"Current Configuration: {config.name}";

            if (!Directory.Exists(configFolder))
            {
                MessageBox.Show("The configs folder does not exist. Default configuration will be used.", $"{Text} Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] files = Directory.GetFiles(configFolder, "*.json");

            if (files == null || files.Length == 0)
            {
                MessageBox.Show("The configs folder does not contains any configuration file. Default configuration will be used.", $"{Text} Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                ToolStripMenuItem menuItem = new ToolStripMenuItem(fileName);
                menuItem.Click += (sender, e) => LoadAndProcessConfigFile(file);
                configToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }

        private void LoadAndProcessConfigFile(string filePath)
        {
            config = LoadConfiguration(filePath);
            config.name = Path.GetFileNameWithoutExtension(filePath);
            lblCurrentConfig.Text = $"Current Configuration: {config.name}";
        }

        private Configuration LoadConfiguration(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                var serializer = new DataContractJsonSerializer(typeof(Configuration));
                return (Configuration)serializer.ReadObject(fs);
            }
        }

        private void LoadExecutableValues()
        {
            if (string.IsNullOrEmpty(executablePath)) return;

            float zoomValue;
            float stadRoofValue;
            byte[] clippingValueBytes = new byte[4];
            Clipping clipping = config.clippingList[0];

            if (!Convert.ToBoolean(config.cameraZoomOffset) || !Convert.ToBoolean(config.stadRoofOffset1) || !Convert.ToBoolean(clipping.offset))
            {
                MessageBox.Show($"Offsets cannot be zero, error when reading configuration", $"{Text} Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(executablePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    
                    zoomValue = BinaryPatcher.ReadFloatWithShift(fs, br, config.cameraZoomOffset, config.cameraZoomShift);

                    stadRoofValue = BinaryPatcher.ReadFloatWithShift(fs, br, config.stadRoofOffset1, config.stadRoofShift1);

                    fs.Seek(clipping.offset, SeekOrigin.Begin);
                    clippingValueBytes = br.ReadBytes(clippingValueBytes.Length);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has ocurred while reading values from executable {ex}", $"{Text} Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nudCameraZoom.Value = (decimal)zoomValue;

            chkAddStadRoof.Checked = stadRoofValueOn.SequenceEqual(BitConverter.GetBytes(stadRoofValue));

            chkFixStadClipping.Checked = clipping.newValue.SequenceEqual(clippingValueBytes);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = $"{Text} Executable Browser";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "All Files (*.*)|*.*";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            executablePath = ofd.FileName;
            FileInfo fileInfo = new FileInfo(executablePath);
            if (config.executableSize != fileInfo.Length) 
            {
                MessageBox.Show("Error! The executable size doesn't match with the one given on the configuration", $"{Text} Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadExecutableValues();
            lblCurrentExecutable.Text = $"Current executable: {Path.GetFileName(executablePath)}";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(executablePath))
            {
                MessageBox.Show("Please first select your executable", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            float newZoom = (float)nudCameraZoom.Value;

            byte[] newZoomValueBytes = BitConverter.GetBytes(newZoom);

            try
            {
                using (FileStream fs = new FileStream(executablePath, FileMode.OpenOrCreate, FileAccess.Write))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    BinaryPatcher.WriteFloatWithShift(fs, bw, config.cameraZoomOffset, newZoomValueBytes, config.cameraZoomShift);
                    BinaryPatcher.WriteFloatWithShift(fs, bw, config.cameraZoomOutOffset1, newZoomValueBytes, config.cameraZoomOutShift1);
                    BinaryPatcher.WriteFloatWithShift(fs, bw, config.cameraZoomOutOffset2, newZoomValueBytes, config.cameraZoomOutShift2);

                    byte[] stadRoofNewValue = chkAddStadRoof.Checked ? stadRoofValueOn : stadRoofValueOff;

                    BinaryPatcher.WriteFloatWithShift(fs, bw, config.stadRoofOffset1, stadRoofNewValue, config.stadRoofShift1);
                    BinaryPatcher.WriteFloatWithShift(fs, bw, config.stadRoofOffset2, stadRoofNewValue, config.stadRoofShift2);

                    foreach (Clipping clipping in config.clippingList)
                    {
                        if (!Convert.ToBoolean(clipping.offset)) continue;
                        fs.Seek(clipping.offset, SeekOrigin.Begin);
                        bw.Write(chkFixStadClipping.Checked ? clipping.newValue : clipping.orgValue);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has ocurred while trying to save the changes {ex}", $"{Text} Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Changes saved!", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string about = $@"{Text} is a tool developed by PES 5 Indie Team.

The main proporse is to allow PS2 & PSP users to change the zoom of the camera in the same way we do on PC versions.
Enjoy it!

Copyright (c) PES Indie Team 2024";
            MessageBox.Show($"{about}", $"{Text}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
