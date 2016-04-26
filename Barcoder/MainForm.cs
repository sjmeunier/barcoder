using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BarcoderLib;

namespace Barcoder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox1 = new AboutBox();
            aboutBox1.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void cmdEncode_Click(object sender, EventArgs e)
        {
            cmdSave.Enabled = false;

            try
            {
                switch (cboFormat.Text.Trim())
                {
                    case "EAN-8":
                        BarcodeEAN8 encoderEAN8 = new BarcodeEAN8();
                        picOutput.Image = encoderEAN8.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "EAN-13":
                        BarcodeEAN13 encoderEAN13 = new BarcodeEAN13();
                        picOutput.Image = encoderEAN13.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "Interleaved 2 of 5":
                        BarcodeInter2of5 encoderInter2of5 = new BarcodeInter2of5();
                        picOutput.Image = encoderInter2of5.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "MSI":
                        BarcodeMSI encoderMSI = new BarcodeMSI();
                        picOutput.Image = encoderMSI.Encode(txtMessage.Text.Trim(), cboModulo.Text.Trim(), cboWeightType.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "Standard 2 of 5":
                        BarcodeStandard2of5 encoderStandard2of5 = new BarcodeStandard2of5();
                        picOutput.Image = encoderStandard2of5.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "Postnet":
                        BarcodePostnet encoderPostnet = new BarcodePostnet();
                        picOutput.Image = encoderPostnet.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "UPC-A":
                        BarcodeUPCA encoderUPCA = new BarcodeUPCA();
                        picOutput.Image = encoderUPCA.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "UPC-E":
                        BarcodeUPCE encoderUPCE = new BarcodeUPCE();
                        picOutput.Image = encoderUPCE.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "UPC-2":
                        BarcodeUPC2 encoderUPC2 = new BarcodeUPC2();
                        picOutput.Image = encoderUPC2.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    case "UPC-5":
                        BarcodeUPC5 encoderUPC5 = new BarcodeUPC5();
                        picOutput.Image = encoderUPC5.Encode(txtMessage.Text.Trim());
                        cmdSave.Enabled = true;
                        break;
                    default:
                        MessageBox.Show("Incorrect barcode type specified", "Error", MessageBoxButtons.OK);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        picOutput.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        picOutput.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        picOutput.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormat.SelectedItem.ToString().Trim() == "MSI")
            {
                cboModulo.Visible = true;
                if ((cboModulo.SelectedItem.ToString() == "Modulo 11") || (cboModulo.SelectedItem.ToString() == "Modulo 1011") || (cboModulo.SelectedItem.ToString() == "Modulo 1110"))
                {
                    cboWeightType.Visible = true;
                }
                else
                {
                    cboWeightType.Visible = false;
                }
            }
            else
            {
                cboModulo.Visible = false;
                cboWeightType.Visible = false;
            }
        }

        private void cboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboModulo.SelectedItem.ToString() == "Modulo 11") || (cboModulo.SelectedItem.ToString() == "Modulo 1011") || (cboModulo.SelectedItem.ToString() == "Modulo 1110"))
            {
                cboWeightType.Visible = true;
            }
            else
            {
                cboWeightType.Visible = false;
            }
        }
    }
}
