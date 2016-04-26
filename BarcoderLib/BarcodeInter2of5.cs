using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeInter2of5
    {
        private string gLeftGuard = "1010";
        private string gRightGuard = "01101";
        private string[] gOdd = { "1011001", "1101011", "1001011", "1100101", "1011011", "1101101", "1001101", "1010011", "1101001", "1001001" };
        private string[] gEven = { "0100110", "0010100", "0110100", "0011010", "0100100", "0010010", "0110010", "0101100", "0010110", "0110110" };

        public Bitmap Encode(string message)
        {
            string encodedMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            encodedMessage = EncodeBarcode(message);

            PrintBarcode(g, encodedMessage, message, 350, 100);

            return barcodeImage;
        }
        private void Validate(string message)
        {

            Regex reNum = new Regex(@"^\d+$");
            if (reNum.Match(message).Success == false)
            {
                throw new Exception("Encode string must be numeric");
            }

        }

        private void PrintBarcode(Graphics g, string encodedMessage, string message, int width, int height)
        {
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Font textFont = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            g.FillRectangle(whiteBrush, 0, 0, width, height);

            int xPos = 20;
            int yTop = 10;
            int barHeight = 50;

            for (int i = 0; i < encodedMessage.Length; i++)
            {
                if (encodedMessage[i] == '1')
                {
                    g.FillRectangle(blackBrush, xPos, yTop, 1, barHeight);
                }
                xPos += 1;
            }

            xPos = 20;
            yTop += barHeight - 2;
            for (int i = 0; i < message.Length; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
        }

        private string EncodeBarcode(string message)
        {
            int i;
            string encodedString = gLeftGuard;

            for (i = 0; i < message.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    encodedString += gOdd[Convert.ToInt32(message[i].ToString())];
                }
                else
                {
                    encodedString += gEven[Convert.ToInt32(message[i].ToString())];
                }
            }

            encodedString += gRightGuard;

            return encodedString;
        }

     }
}
