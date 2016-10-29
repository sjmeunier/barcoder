using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeUPCA : IBarcode
    {
        private string _leftGaurd = "101";
        private string gCentreGuard = "01010";
        private string _rightGaurd = "101";
        private string[] gLH = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] gRH = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private int[] _weighting = { 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
        private string _longBars = "11111111110000000000000000000000000000000000011111000000000000000000000000000000000001111111111";

  

        public Bitmap EncodeToBitmap(string message)
        {
            string encodedMessage;
            string fullMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            fullMessage = message + CalcParity(message).ToString().Trim();
            encodedMessage = Encode(fullMessage);

            PrintBarcode(g, encodedMessage, fullMessage, 250, 100);

            return barcodeImage;
        }

        public string EncodeToString(string message)
        {
            Validate(message);
            message += CalcParity(message).ToString().Trim();
            return Encode(message);
        }

        private void Validate(string message)
        {

            Regex reNum = new Regex(@"^\d+$");
            if (reNum.Match(message).Success == false)
            {
                throw new Exception("Encode string must be numeric");
            }

            if (message.Length != 11)
            {
                throw new Exception("Encode string must be 11 digits long");
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
            int barGuardHeight = 7;

            for (int i = 0; i < encodedMessage.Length; i++)
            {
                if (encodedMessage[i] == '1')
                {
                    if (_longBars[i] == '1')
                    {
                        g.FillRectangle(blackBrush, xPos, yTop, 1, barHeight + barGuardHeight);
                    }
                    else
                    {
                        g.FillRectangle(blackBrush, xPos, yTop, 1, barHeight);
                    }
                }
                xPos += 1;
            }
            
            xPos = 20;
            yTop += barHeight - 2;
            g.DrawString(message[0].ToString().Trim(), textFont, blackBrush, xPos - 10, yTop);

            xPos += 8;
            for (int i = 1; i < 6; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
            xPos += 4;

            for (int i = 6; i <11; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
            xPos += 11;
            g.DrawString(message[11].ToString().Trim(), textFont, blackBrush, xPos, yTop);

        }

        private string Encode(string message)
        {
            int i;
            string encodedString = _leftGaurd;

            for (i = 0; i < 6; i++)
            {
                encodedString += gLH[Convert.ToInt32(message[i].ToString())];
            }
            encodedString += gCentreGuard;

            for (i = 6; i < 12; i++)
            {
                encodedString += gRH[Convert.ToInt32(message[i].ToString())];
            }
            encodedString += _rightGaurd;

            return encodedString;
        }

        private int CalcParity(string message)
        {
            int sum = 0;
            int parity = 0;

            for (int i = 0; i < 11; i++)
            {
                sum += Convert.ToInt32(message[i].ToString()) * _weighting[i];
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
