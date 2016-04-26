using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeUPCE
    {
        private string gLeftGuard = "101";
        private string gRightGuard = "010101";
        private string[] gOdd = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] gEven = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private int[] gWeighting = { 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
        private string gLongBars = "111000000000000000000000000000000000000000000111111";
        private string[] gParity0 = { "000111", "001011", "001101", "001110", "010011", "011001", "011100", "010101", "010110", "011010"};
        private string[] gParity1 = { "111000", "110100", "110010", "110001", "101100", "100110", "100011", "101010", "101001", "100101"};

        public Bitmap Encode(string message)
        {
            string encodedMessage;
            string fullMessage;
            string shortMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            fullMessage = message + CalcParity(message).ToString().Trim();
            shortMessage = ConvertToShort(fullMessage);
            encodedMessage = EncodeBarcode(shortMessage, fullMessage);

            PrintBarcode(g, encodedMessage, shortMessage, fullMessage, 250, 100);

            return barcodeImage;
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

        private void PrintBarcode(Graphics g, string encodedMessage, string message, string fullMessage, int width, int height)
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
                    if (gLongBars[i] == '1')
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
            g.DrawString(fullMessage[0].ToString().Trim(), textFont, blackBrush, xPos - 10, yTop);

            xPos += 1;
            for (int i = 0; i < 6; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
            xPos += 7;
            g.DrawString(message[6].ToString().Trim(), textFont, blackBrush, xPos, yTop);

        }

        private string EncodeBarcode(string message, string fullMessage)
        {
            int i;
            string encodedString = gLeftGuard;
            int parityCode = Convert.ToInt32(fullMessage[11].ToString());
            int paritySet = Convert.ToInt32(fullMessage[0].ToString());
            char parity;

            for (i = 0; i < 6; i++)
            {
                if (paritySet == 0)
                {
                    parity = gParity0[parityCode][i];
                }
                else
                {
                    parity = gParity0[parityCode][i];
                }
                if (parity == '1')
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
    
        private int CalcParity(string message)
        {
            int sum = 0;
            int parity = 0;

            for (int i = 0; i < message.Length; i++)
            {
                sum += Convert.ToInt32(message[i].ToString()) * gWeighting[i];
            }

            parity = 10 - (sum % 10);
            if (parity == 10)
            {
                parity = 0;
            }
            return parity;
        }

        private string ConvertToShort(string message)
        {
            string tmp = "";
            string manufacturer = message.Substring(1, 5);
            string product = message.Substring(6, 5);
            string digits = "";
            if ((manufacturer.Substring(2) == "000") || (manufacturer.Substring(2) == "100") || (manufacturer.Substring(2) == "200"))
            {
                digits = manufacturer.Substring(0, 2) + product.Substring(2, 3) + manufacturer.Substring(2, 1);
            }
            else if (manufacturer.Substring(3) == "00")
            {
                digits = manufacturer.Substring(0, 3) + product.Substring(3) + "3";
            }
            else if (manufacturer.Substring(4) == "0")
            {
                digits = manufacturer.Substring(0, 4) + product.Substring(4) + "4";

            }
            else
            {
                digits = manufacturer.Substring(0, 5) + product.Substring(4);
            }
            tmp = digits + message.Substring(11, 1);
            return tmp;
        }
            
    }
}
