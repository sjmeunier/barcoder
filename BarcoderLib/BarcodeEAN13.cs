﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeEAN13 : IBarcode
    {
        private string _leftGaurd = "101";
        private string gCentreGuard = "01010";
        private string _rightGaurd = "101";
        private string[] gLHOdd = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011"};
        private string[] gLHEven = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111"};
        private string[] gRH = {"1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100"};
        private string[] gParity = {"111111", "110100", "110010", "110001", "101100", "100110", "100011", "101010", "101001", "100101" };
        private int[] _weighting = { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
        private string _longBars = "11100000000000000000000000000000000000000000011111000000000000000000000000000000000000000000111";

        public Bitmap EncodeToBitmap(string message)
        {
            Validate(message);
            message += CalcParity(message).ToString().Trim();
            string encodedMessage = Encode(message);

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);
            PrintBarcode(g, encodedMessage, message, 250, 100);
            
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

            if (message.Length != 12)
            {
                throw new Exception("Encode string must be 12 digits long");
            }
        }

        private void PrintBarcode(Graphics g, string encodedMessage, string message, int width, int height)
        {
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Font textFont = new Font(FontFamily.GenericMonospace, 9, FontStyle.Regular);
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
            yTop += barHeight - 3;
            g.DrawString(message[0].ToString().Trim(), textFont, blackBrush, xPos - 14, yTop);

            xPos += 2;
            for (int i = 1; i < 7; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
            xPos += 3;
            
            for (int i = 7; i < message.Length; i++)
            {
                g.DrawString(message[i].ToString().Trim(), textFont, blackBrush, xPos, yTop);
                xPos += 7;
            }
             
        }

        public string Encode(string message)
        {
            int i;
            char parity;

            string encodedString = _leftGaurd;
            int parityCode = Convert.ToInt32(message[0].ToString());

            for (i = 1; i < 7; i++)
            {
                parity = gParity[parityCode][i-1];
                if (parity == '1')
                {
                    encodedString += gLHOdd[Convert.ToInt32(message[i].ToString())];
                }
                else
                {
                    encodedString += gLHEven[Convert.ToInt32(message[i].ToString())];
                }
            }
            encodedString += gCentreGuard;

            for(i = 7; i < 13; i++)
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

            for(int i = 0; i < 12; i++)
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
