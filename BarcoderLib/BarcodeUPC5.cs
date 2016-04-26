using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeUPC5 
    {
        private string gLeftGuard = "1011";
        private string gCentreGuard = "01";
        private string[] gOdd = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] gEven = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private string[] gParity = {"00111", "01011", "01101", "01110", "10011", "11001", "11100", "10101", "10110", "11010"};

        private int[] gWeighting = { 3, 9, 3, 9, 3};
    
        public Bitmap Encode(string message)
        {
            string encodedMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            encodedMessage = EncodeBarcode(message);

            PrintBarcode(g, encodedMessage, message, 250, 100);

            return barcodeImage;
        }
        private void Validate(string message)
        {

            Regex reNum = new Regex(@"^\d+$");
            if (reNum.Match(message).Success == false)
            {
                throw new Exception("Encode string must be numeric");
            }

            if (message.Length != 5)
            {
                throw new Exception("Encode string must be 5 digits long");
            }
        }

        private void PrintBarcode(Graphics g, string encodedMessage, string message, int width, int height)
        {
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Font textFont = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            g.FillRectangle(whiteBrush, 0, 0, width, height);

            int xPos = 20;
            int yTop = 20;
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
            yTop -= 17;
            for (int i = 0; i < message.Length; i++)
            {
                g.DrawString(message[i].ToString(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
        }

        private string EncodeBarcode(string message)
        {
            int i;
            int parityCode = CalcParity(message);
            char parity;
            string encodedString = gLeftGuard;

            for (i = 0; i < 5; i++)
            {
                parity = gParity[parityCode][i];
                if (parity == '1')
                {
                    encodedString += gOdd[Convert.ToInt32(message[i].ToString())];
                }
                else{
                    encodedString += gEven[Convert.ToInt32(message[i].ToString())];
                }
                if (i < 4)
                {
                    encodedString += gCentreGuard;
                }
            }

            return encodedString;
        }

        private int CalcParity(string message)
        {
            int sum = 0;
            int parity = 0;

            for (int i = 0; i < 5; i++)
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

    }
}
