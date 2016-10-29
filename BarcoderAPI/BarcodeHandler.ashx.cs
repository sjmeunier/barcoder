using BarcoderLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace BarcoderAPI
{
    /// <summary>
    /// Summary description for BarcodeHandler
    /// </summary>
    public class BarcodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Enums.Barcodes barcodeType = (Enums.Barcodes)int.Parse(context.Request.QueryString["barcodeType"]);
            string message = context.Request.QueryString["message"];
            IBarcode barcoder = BarcodeBuilder.CreateBarcode(barcodeType);

            Bitmap bitmap;
            if (barcodeType == Enums.Barcodes.MSI)
            {
                Enums.Modulo modulo = (Enums.Modulo)int.Parse(context.Request.QueryString["modulo"]);
                Enums.MSIWeighting weighting = (Enums.MSIWeighting)int.Parse(context.Request.QueryString["msiWeighting"]);
                bitmap = ((BarcodeMSI)barcoder).EncodeToBitmap(message, modulo, weighting);
            } else
            {
                bitmap = barcoder.EncodeToBitmap(message);
            }

            MemoryStream mem = new MemoryStream();
            bitmap.Save(mem, ImageFormat.Png);

            byte[] buffer = mem.ToArray();

            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(buffer);
            context.Response.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}