using System.ComponentModel;

namespace BarcoderLib
{
    public class Enums
    {
        public enum Barcodes
        {
            [Description("EAN-13")]
            EAN13,
            [Description("EAN-8")]
            EAN8,
            [Description("Interleaved 2 of 5")]
            Interleaved2Of5,
            [Description("Standard 2 of 5")]
            Standard2Of5,
            [Description("UPC-2")]
            UPC2,
            [Description("UPC-5")]
            UPC5,
            [Description("UPC-A")]
            UPCA,
            [Description("UPC-E")]
            UPCE,
            [Description("Postnet")]
            Postnet,
            [Description("MSI")]
            MSI
        }

        public enum Modulo
        {
            [Description("None")]
            None,
            [Description("Modulo 10")]
            Modulo10,
            [Description("Modulo 11")]
            Modulo11,
            [Description("Modulo 1011")]
            Modulo1011,
            [Description("Modulo 1110")]
            Modulo1110
        }

        public enum MSIWeighting
        {
            [Description("IBM")]
            IBM,
            [Description("NCR")]
            NCR
        }
    }
}
