using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodePostnet
    {
        private string gLeftGuard = "1";
        private string gRightGuard = "1";
        private string[] gCoding = { "11000", "00011", "00101", "00111", "01001", "01010", "01100", "10001", "10010", "10100" };

        public Bitmap Encode(string message)
        {
            string encodedMessage;
            string fullMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            fullMessage = message + CalcParity(message).ToString().Trim();
            encodedMessage = EncodeBarcode(fullMessage);

            PrintBarcode(g, encodedMessage, fullMessage, 250, 100);

            return barcodeImage;
        }
        private void Validate(string message)
        {

            Regex reNum = new Regex(@"^\d+$");
            if (reNum.Match(message).Success == false)
            {
                throw new Exception("Encode string must be numeric");
            }

            if ((message.Length != 5) && (message.Length != 9) && (message.Length != 11))
            {
                throw new Exception("Encode string must be 5, 9 or 11 digits long");
            }
        }

        private void PrintBarcode(Graphics g, string encodedMessage, string message, int width, int height)
        {
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Font textFont = new Font(FontFamily.GenericMonospace, 8, FontStyle.Regular);
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
            yTop += barHeight -2;

            for (int i = 0; i < message.Length; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 5;
            }

        }

        private string EncodeBarcode(string message)
        {
            int i;
            string encodedString = gLeftGuard;

            for (i = 0; i < message.Length; i++)
            {
                encodedString += gCoding[Convert.ToInt32(message[i].ToString())];
            }

            encodedString += gRightGuard;

            return encodedString;
        }

        private int CalcParity(string message)
        {
            int sum = 0;
            int parity = 0;

            for (int i = 0; i < message.Length; i++)
            {
                sum += Convert.ToInt32(message[i].ToString());
            }

            parity = 10 - (sum % 10);
            if (parity == 10)
            {
                parity = 0;
            }
            return parity;
        }
    }
}
