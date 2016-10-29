using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoderLib
{
    public static class BarcodeBuilder
    {
        public static IBarcode CreateBarcode(Enums.Barcodes barcodeType)
        {
            IBarcode barcode;
            switch (barcodeType)
            {
                case Enums.Barcodes.EAN13:
                    barcode = new BarcodeEAN13();
                    break;
                case Enums.Barcodes.EAN8:
                    barcode = new BarcodeEAN8();
                    break;
                case Enums.Barcodes.Interleaved2Of5:
                    barcode = new BarcodeInter2of5();
                    break;
                case Enums.Barcodes.MSI:
                    barcode = new BarcodeMSI();
                    break;
                case Enums.Barcodes.Postnet:
                    barcode = new BarcodePostnet();
                    break;
                case Enums.Barcodes.Standard2Of5:
                    barcode = new BarcodeStandard2of5();
                    break;
                case Enums.Barcodes.UPC2:
                    barcode = new BarcodeUPC2();
                    break;
                case Enums.Barcodes.UPC5:
                    barcode = new BarcodeUPC5();
                    break;
                case Enums.Barcodes.UPCA:
                    barcode = new BarcodeUPCA();
                    break;
                default:
                    barcode = new BarcodeUPCE();
                    break;
            }

            return barcode;
        }
    }
}
