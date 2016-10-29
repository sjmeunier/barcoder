using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoderLib
{
    public interface IBarcode
    {
        Bitmap EncodeToBitmap(string message);
        string EncodeToString(string message);
    }
}
