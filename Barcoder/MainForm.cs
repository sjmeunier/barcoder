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
            var formatItems = new[] {
                new { Value = Enums.Barcodes.EAN13, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.EAN13) },
                new { Value = Enums.Barcodes.EAN8, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.EAN8) },
                new { Value = Enums.Barcodes.UPC2, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.UPC2) },
                new { Value = Enums.Barcodes.UPC5, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.UPC5) },
                new { Value = Enums.Barcodes.UPCA, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.UPCA) },
                new { Value = Enums.Barcodes.UPCE, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.UPCE) },
                new { Value = Enums.Barcodes.Standard2Of5, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.Standard2Of5) },
                new { Value = Enums.Barcodes.Interleaved2Of5, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.Interleaved2Of5) },
                new { Value = Enums.Barcodes.Postnet, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.Interleaved2Of5) },
                new { Value = Enums.Barcodes.MSI, Text = Extensions.GetDescription<Enums.Barcodes>(Enums.Barcodes.MSI) }
            };

            cboFormat.DisplayMember = "Text";
            cboFormat.ValueMember = "Value";
            cboFormat.DataSource = formatItems;

            var moduloItems = new[] {
                new { Value = Enums.Modulo.None, Text = Extensions.GetDescription<Enums.Modulo>(Enums.Modulo.None) },
                new { Value = Enums.Modulo.Modulo10, Text = Extensions.GetDescription<Enums.Modulo>(Enums.Modulo.Modulo10) },
                new { Value = Enums.Modulo.Modulo11, Text = Extensions.GetDescription<Enums.Modulo>(Enums.Modulo.Modulo11) },
                new { Value = Enums.Modulo.Modulo1011, Text = Extensions.GetDescription<Enums.Modulo>(Enums.Modulo.Modulo1011) },
                new { Value = Enums.Modulo.Modulo1110, Text = Extensions.GetDescription<Enums.Modulo>(Enums.Modulo.Modulo1110) }
            };

            cboModulo.DisplayMember = "Text";
            cboModulo.ValueMember = "Value";
            cboModulo.DataSource = moduloItems;

            var weightingItems = new[] {
                new { Value = Enums.MSIWeighting.IBM, Text = Extensions.GetDescription<Enums.MSIWeighting>(Enums.MSIWeighting.IBM) },
                new { Value = Enums.MSIWeighting.NCR, Text = Extensions.GetDescription<Enums.MSIWeighting>(Enums.MSIWeighting.NCR) }
            };

            cboWeightType.DisplayMember = "Text";
            cboWeightType.ValueMember = "Value";
            cboWeightType.DataSource = weightingItems;
        }

        private void cmdEncode_Click(object sender, EventArgs e)
        {
            cmdSave.Enabled = false;

            try
            {
                Enums.Barcodes barcodeType = (Enums.Barcodes)cboFormat.SelectedValue;

                IBarcode barcoder = null;
                switch (barcodeType)
                {
                    case Enums.Barcodes.EAN13:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.EAN13);
                        break;
                    case Enums.Barcodes.EAN8:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.EAN8);
                        break;
                    case Enums.Barcodes.UPC2:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.UPC2);
                        break;
                    case Enums.Barcodes.UPC5:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.UPC5);
                        break;
                    case Enums.Barcodes.UPCA:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.UPCA);
                        break;
                    case Enums.Barcodes.UPCE:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.UPCE);
                        break;
                    case Enums.Barcodes.Interleaved2Of5:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.Interleaved2Of5);
                        break;
                    case Enums.Barcodes.Standard2Of5:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.Standard2Of5);
                        break;
                    case Enums.Barcodes.Postnet:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.Postnet);
                        break;
                    case Enums.Barcodes.MSI:
                        barcoder = BarcodeBuilder.CreateBarcode(Enums.Barcodes.MSI);
                        break;
                }

                if (barcoder == null)
                {
                    MessageBox.Show("Invalid barcode type specified", "Error", MessageBoxButtons.OK);
                    return;
                }

                if (barcodeType == Enums.Barcodes.MSI)
                    picOutput.Image = ((BarcodeMSI)barcoder).EncodeToBitmap(txtMessage.Text.Trim(), (Enums.Modulo)cboModulo.SelectedValue, (Enums.MSIWeighting)cboWeightType.SelectedValue);
                else
                    picOutput.Image = barcoder.EncodeToBitmap(txtMessage.Text.Trim());
                cmdSave.Enabled = true;
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
            if ((Enums.Barcodes)cboFormat.SelectedValue == Enums.Barcodes.MSI)
            {
                cboModulo.Visible = true;
                if (((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo11) || ((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo1011) || ((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo1110))
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
            if (((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo11) || ((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo1011) || ((Enums.Modulo)cboModulo.SelectedValue == Enums.Modulo.Modulo1110))
            {
                cboWeightType.Visible = true;
            }
            else
            {
                cboWeightType.Visible = false;
            }
        }

        private void cboWeightType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
