using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;


namespace BarcoderLib
{
    public class BarcodeStandard2of5
    {
        private string gLeftGuard = "1111011110110";
        private string gRightGuard = "1111011011110";
        private string[] gCoding = { "11011011111101111110110", "11111101101101101111110", "11011111101101101111110", 
                                     "11111101111110110110110", "11011011111101101111110", "11111101101111110110110",
                                     "11011111101111110110110", "11011011011111101111110", "11111101101101111110110", "11011111101101111110110" };
        public Bitmap Encode(string message)
        {
            string encodedMessage;
            string fullMessage;

            Bitmap barcodeImage = new Bitmap(400, 100);
            Graphics g = Graphics.FromImage(barcodeImage);

            Validate(message);

            fullMessage = message + CalcParity(message).ToString().Trim();
            encodedMessage = EncodeBarcode(fullMessage);

            PrintBarcode(g, encodedMessage, fullMessage, 350, 100);

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
            Font textFont = new Font(FontFamily.GenericMonospace, 12, FontStyle.Regular);
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
                if ((i % 2) == 0)
                {
                    sum += Convert.ToInt32(message[i].ToString()) * 3;
                }else
                {
                    sum += Convert.ToInt32(message[i].ToString()) * 1;
                }
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
