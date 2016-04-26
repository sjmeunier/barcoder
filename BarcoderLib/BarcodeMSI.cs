using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BarcoderLib
{
    public class BarcodeMSI
    {
        private string gLeftGuard = "110";
        private string gRightGuard = "1001";
        private string[] gCoding = {  "100100100100", "100100100110", "100100110100", "100100110110", "100110100100", 
                                      "100110100110", "100110110100", "100110110110", "110100100100", "110100100110"};


        public Bitmap Encode(string message, string modulo, string weightType)
        {
            string encodedMessage;
            string fullMessage;

            Bitmap barcodeImage = new Bitmap(250, 100);
            Graphics g = Graphics.FromImage(barcodeImage);


            Validate(message);
            fullMessage = message;
            switch (modulo)
            {
                case "Modulo 10":
                    fullMessage = message + CalcParity10(message).ToString().Trim();
                    break;
                case "Modulo 11":
                    fullMessage = message + CalcParity11(message, weightType).ToString().Trim();
                    break;
                case "Modulo 1011":
                    fullMessage = message + CalcParity10(message).ToString().Trim();
                    fullMessage = fullMessage + CalcParity11(fullMessage, weightType).ToString().Trim();
                    break;
                case "Modulo 1110":
                    fullMessage = message + CalcParity11(message, weightType).ToString().Trim();
                    fullMessage = fullMessage + CalcParity10(fullMessage).ToString().Trim();
                    break;
            }
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
                encodedString += gCoding[Convert.ToInt32(message[i].ToString())];
            }
            encodedString += gRightGuard;

            return encodedString;
        }

        private int CalcParity10(string message)
        {
            long sum = 0;
            int parity = 0;
            string shortStringA = "";
            string shortStringB = "";

            for (int i = message.Length - 1; i >= 0; i--)
            {
                if (((message.Length - 1 - i) % 2) == 1)
                {
                    shortStringA += message[i].ToString().Trim();
                }
                else
                {
                    shortStringB += message[i].ToString().Trim();
                }
            }
            string tmp = "";
            for (int i = shortStringB.Length - 1; i >= 0; i--)
            {
                tmp += shortStringB[i];
            }
            shortStringB = tmp;

            tmp = "";
            for (int i = shortStringA.Length - 1; i >= 0; i--)
            {
                tmp += shortStringA[i];
            }
            shortStringA = tmp;

            long mult = 2 * Convert.ToInt64(shortStringA);
            
            for (int i = 0; i < shortStringB.Length; i++)
            {
                sum += Convert.ToInt32(shortStringB[i]);
            }
        
            sum += mult;

            parity = Convert.ToInt32(10 - (sum % 10));
            if (parity == 10)
            {
                parity = 0;
            }
            return parity;


         }

        private int CalcParity11(string message, string weightType)
        {
            int weightMax = 0;
            int sum = 0;
            int parity;

            if(weightType == "IBM")
            {
                weightMax = 7;
            }
            else{
                weightMax = 9;
            }
            int weight = 2;
                    
            for (int i = message.Length - 1; i >= 0; i--)
            {
                sum += Convert.ToInt32(message[i].ToString()) * weight;
                weight++;
                if (weight > weightMax)
                {
                    weight = 2;
                }
            }

            parity = 11 - (sum % 11);
            if (parity == 11)
            {
                parity = 0;
            }
            return parity;
        }
    }
}
