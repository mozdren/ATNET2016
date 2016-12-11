
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SharedLibs
{
    public class PdfDocumentFields
    {
        public const int ORDER_ITEMS_AT_FIRST_PAGE_COUNT = 23;
        public const int ORDER_ITEMS_AT_OTHER_PAGE_COUNT = 25;
        public const int INVOICE_ITEMS_AT_FIRST_PAGE_COUNT = 12;
        public const int INVOICE_ITEMS_AT_OTHER_PAGE_COUNT = 25;
        public const int DELIVERY_NOTE_ITEMS_AT_FIRST_PAGE_COUNT = 11;
        public const int DELIVERY_NOTE_ITEMS_AT_OTHER_PAGE_COUNT = 15;
        public const int ORDER_COLUMN_COUNT = 4;
        public const int INVOICE_HEADER_COLUMN_COUNT = 5;
        public const int INVOICE_CUSTOMER_COLUMN_COUNT = 2;
        public const int INVOICE_INFO_COLUMN_COUNT = 2;
        public const int DELIVERY_NOTE_INFO_COLUMN_COUNT = 3;
        public const int DELIVERY_NOTE_HEADER_COLUMN_COUNT = 6;
        public static float[] orderColumnWidths = new float[ORDER_COLUMN_COUNT] { 330.0f, 100.0f, 80.0f, 80.0f };
        public static float[] invoiceColumnWidths = new float[INVOICE_HEADER_COLUMN_COUNT] { 80.0f, 230.0f, 100.0f, 80.0f, 100.0f };
        public static float[] deliveryNoteInfoTableColumnWidths = new float[DELIVERY_NOTE_INFO_COLUMN_COUNT] { 40.0f, 40.0f, 20.0f };
        public static float[] deliveryNoteHeaderTableColumnWidths = new float[DELIVERY_NOTE_HEADER_COLUMN_COUNT] { 40.0f, 80.0f, 300.0f, 100.0f, 80.0f, 100.0f };
        public static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string directoryPath = currentDirectory + @"\Files";
        public const string COMPANY_TITLE = "NÁŠ ESHOP VŠB s.r.o. ";
        public const string COMPANY_ADDRESS = "Pod mostem 55, 123 99 Kotěhůlky";
        public const string COMPANY_IDENT_NUMBERS = "IČ: 12345678   DIČ: CZ13245678";
        public const string REGISTERED_WIDTH_INSTITUTION_PART_I = "zapsána v obchodním rejstříku vedené městským soudem v Kotěhůlkách";
        public const string REGISTERED_WIDTH_INSTITUTION_PART_II = "oddíl Z, vložka 98765";
        public static int generalItemsPerPageCount = 0;
        public static int itemsPerPageCount = 0;
        public static int pageNumber = 1;
        public static int itemNumber = 0;
        public static int sumaItemNumber = 1;
        public static double sumaPrice = 0;
        public static double deliveryPrice = 100.0;

        public static string arrialUni = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
        public static iTextSharp.text.pdf.draw.VerticalPositionMark horizontalSpace = new iTextSharp.text.pdf.draw.VerticalPositionMark();
        public static iTextSharp.text.pdf.draw.LineSeparator lineSeparator =
            new iTextSharp.text.pdf.draw.LineSeparator(1.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 0.0f);
        public static iTextSharp.text.pdf.draw.LineSeparator sumaLineSeparator =
            new iTextSharp.text.pdf.draw.LineSeparator(1.0f, 40.0f, BaseColor.BLACK, Element.ALIGN_RIGHT, 0.0f);
        public static iTextSharp.text.pdf.draw.LineSeparator thinLineSeparator =
            new iTextSharp.text.pdf.draw.LineSeparator(0.5f, 100.0f, BaseColor.BLACK, Element.ALIGN_CENTER, 0.0f);

        public static Font middleBoldFont = FontFactory.GetFont(arrialUni, BaseFont.CP1250, 14.0f, Font.BOLD);
        public static Font bigBoldFont = FontFactory.GetFont(arrialUni, BaseFont.CP1250, 20.0f, Font.BOLD);
        public static Font smallNormalFont = FontFactory.GetFont(arrialUni, BaseFont.CP1250, 10.0f, Font.NORMAL);
        public static Font middleNormalFont = FontFactory.GetFont(arrialUni, BaseFont.CP1250, 14.0f, Font.NORMAL);
    }
}
