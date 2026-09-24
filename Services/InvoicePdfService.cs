using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WhatsappAutomation.Models;

namespace WhatsappAutomation.Services
{
    public class InvoicePdfService
    {
        private static string GetAppDirectory()
        {
            try
            {
                string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrEmpty(loc))
                    return Path.GetDirectoryName(loc);
            }
            catch { }
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GenerateInvoicePdf(SalesRecord record, List<SalesItem> items, AppSettings settings)
        {
            string outputDir = Path.Combine(GetAppDirectory(), "invoices");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            string fileName = $"Invoice_{record.VoucherNo}_{record.SI}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string filePath = Path.Combine(outputDir, fileName);

            if (settings.PaperSize == "Thermal3Inch")
            {
                GenerateThermal3InchPdf(record, items, settings, filePath);
            }
            else
            {
                GenerateA4Pdf(record, items, settings, filePath);
            }

            return filePath;
        }

        private static void GenerateA4Pdf(SalesRecord record, List<SalesItem> items, AppSettings settings, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var doc = new Document(PageSize.A4, 36f, 36f, 36f, 36f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Fonts
                    Font fTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.BLACK);
                    Font fSubTitle = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.DARK_GRAY);
                    Font fSectionHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE);
                    Font fCell = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, BaseColor.BLACK);
                    Font fCellBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, BaseColor.BLACK);
                    Font fTotal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.BLACK);

                    // 1. Header Section
                    if (!string.IsNullOrWhiteSpace(settings.Header1_ShopName))
                    {
                        var pHeader1 = new Paragraph(settings.Header1_ShopName.Trim(), fTitle) { Alignment = Element.ALIGN_CENTER };
                        doc.Add(pHeader1);
                    }
                    if (!string.IsNullOrWhiteSpace(settings.Header2_Address))
                    {
                        var pHeader2 = new Paragraph(settings.Header2_Address.Trim(), fSubTitle) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 2f };
                        doc.Add(pHeader2);
                    }
                    if (!string.IsNullOrWhiteSpace(settings.Header3_Contact))
                    {
                        var pHeader3 = new Paragraph(settings.Header3_Contact.Trim(), fSubTitle) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 2f, SpacingAfter = 10f };
                        doc.Add(pHeader3);
                    }

                    // Divider Line
                    var line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1)));
                    line.SpacingAfter = 10f;
                    doc.Add(line);

                    // 2. Bill & Customer Details Table
                    PdfPTable metaTable = new PdfPTable(2) { WidthPercentage = 100 };
                    metaTable.SetWidths(new float[] { 50f, 50f });

                    PdfPCell leftCell = new PdfPCell
                    {
                        Border = Rectangle.NO_BORDER,
                        PaddingBottom = 8f
                    };
                    leftCell.AddElement(new Paragraph($"Bill / Invoice No: {record.VoucherNo}", fCellBold));
                    leftCell.AddElement(new Paragraph($"Date: {(record.SoldDate.HasValue ? record.SoldDate.Value.ToString("dd-MMM-yyyy hh:mm tt") : DateTime.Now.ToString("dd-MMM-yyyy"))}", fCell));
                    metaTable.AddCell(leftCell);

                    PdfPCell rightCell = new PdfPCell
                    {
                        Border = Rectangle.NO_BORDER,
                        PaddingBottom = 8f,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    rightCell.AddElement(new Paragraph($"Customer: {(string.IsNullOrWhiteSpace(record.CustomerName) ? "Cash Customer" : record.CustomerName)}", fCellBold));
                    if (!string.IsNullOrWhiteSpace(record.CustomerMobile))
                    {
                        rightCell.AddElement(new Paragraph($"Mobile: {record.CustomerMobile}", fCell));
                    }
                    metaTable.AddCell(rightCell);
                    doc.Add(metaTable);

                    // 3. Items Table
                    PdfPTable table = new PdfPTable(6) { WidthPercentage = 100, SpacingBefore = 8f, SpacingAfter = 10f };
                    table.SetWidths(new float[] { 6f, 44f, 12f, 13f, 10f, 15f });

                    BaseColor headerBg = new BaseColor(44, 62, 80); // Dark Slate

                    void AddHeaderCell(string text, int align)
                    {
                        PdfPCell c = new PdfPCell(new Phrase(text, fSectionHeader))
                        {
                            BackgroundColor = headerBg,
                            HorizontalAlignment = align,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            Padding = 5f,
                            BorderColor = BaseColor.LIGHT_GRAY
                        };
                        table.AddCell(c);
                    }

                    AddHeaderCell("#", Element.ALIGN_CENTER);
                    AddHeaderCell("Item Description", Element.ALIGN_LEFT);
                    AddHeaderCell("Qty", Element.ALIGN_RIGHT);
                    AddHeaderCell("Rate", Element.ALIGN_RIGHT);
                    AddHeaderCell("Disc", Element.ALIGN_RIGHT);
                    AddHeaderCell("Amount", Element.ALIGN_RIGHT);

                    int index = 1;
                    double totalItemAmount = 0;
                    if (items != null && items.Count > 0)
                    {
                        foreach (var it in items)
                        {
                            BaseColor rowBg = (index % 2 == 0) ? new BaseColor(245, 247, 250) : BaseColor.WHITE;

                            PdfPCell cNum = new PdfPCell(new Phrase(index.ToString(), fCell)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };
                            PdfPCell cDesc = new PdfPCell(new Phrase(it.ItemName ?? string.Empty, fCell)) { HorizontalAlignment = Element.ALIGN_LEFT, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };
                            PdfPCell cQty = new PdfPCell(new Phrase(it.Qty.ToString("0.##"), fCell)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };
                            PdfPCell cRate = new PdfPCell(new Phrase(it.Rate.ToString("N2"), fCell)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };
                            PdfPCell cDisc = new PdfPCell(new Phrase(it.Disc > 0 ? it.Disc.ToString("N2") : "-", fCell)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };
                            PdfPCell cAmt = new PdfPCell(new Phrase(it.NetAmount.ToString("N2"), fCell)) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = rowBg, Padding = 4f, BorderColor = BaseColor.LIGHT_GRAY };

                            table.AddCell(cNum);
                            table.AddCell(cDesc);
                            table.AddCell(cQty);
                            table.AddCell(cRate);
                            table.AddCell(cDisc);
                            table.AddCell(cAmt);

                            totalItemAmount += it.NetAmount;
                            index++;
                        }
                    }
                    else
                    {
                        PdfPCell emptyCell = new PdfPCell(new Phrase("Sales summary item", fCell))
                        {
                            Colspan = 6,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 10f
                        };
                        table.AddCell(emptyCell);
                    }

                    doc.Add(table);

                    // 4. Totals Table
                    PdfPTable totalTable = new PdfPTable(2) { WidthPercentage = 45, HorizontalAlignment = Element.ALIGN_RIGHT, SpacingAfter = 20f };
                    totalTable.SetWidths(new float[] { 50f, 50f });

                    void AddTotalRow(string label, string val, Font font, bool isBorder = false)
                    {
                        PdfPCell lCell = new PdfPCell(new Phrase(label, font))
                        {
                            Border = isBorder ? Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER : Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Padding = 4f
                        };
                        PdfPCell rCell = new PdfPCell(new Phrase(val, font))
                        {
                            Border = isBorder ? Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER : Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            Padding = 4f
                        };
                        totalTable.AddCell(lCell);
                        totalTable.AddCell(rCell);
                    }

                    double grand = record.GrandTotal > 0 ? record.GrandTotal : totalItemAmount;
                    AddTotalRow("Grand Total:", grand.ToString("N2"), fTotal, true);
                    doc.Add(totalTable);

                    // 5. Footer
                    var pFooter = new Paragraph("Thank you for your business! Please visit again.", fSubTitle)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 15f
                    };
                    doc.Add(pFooter);

                    doc.Close();
                }
            }
        }

        private static void GenerateThermal3InchPdf(SalesRecord record, List<SalesItem> items, AppSettings settings, string filePath)
        {
            // 80mm width = ~226.77 points
            float width = 226.77f;
            int count = (items != null && items.Count > 0) ? items.Count : 1;
            bool hasShopHeader = !string.IsNullOrWhiteSpace(settings.Header1_ShopName);
            float calcHeight = Math.Max(300f, (hasShopHeader ? 50f : 0f) + 185f + (count * 16f));

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var doc = new Document(new Rectangle(width, calcHeight), 6f, 6f, 6f, 6f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // Fonts matching the thermal bill
                    Font fShopName = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9.5f, BaseColor.BLACK);
                    Font fShopSub = FontFactory.GetFont(FontFactory.HELVETICA, 7f, BaseColor.BLACK);
                    Font fBillMeta = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f, BaseColor.BLACK);
                    Font fBillMetaBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8.5f, BaseColor.BLACK);
                    Font fTh = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7.5f, BaseColor.BLACK);
                    Font fRow = FontFactory.GetFont(FontFactory.HELVETICA, 7.2f, BaseColor.BLACK);
                    Font fDivider = FontFactory.GetFont(FontFactory.HELVETICA, 6.5f, BaseColor.BLACK);
                    Font fTotalsLabel = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);
                    Font fTotalsVal = FontFactory.GetFont(FontFactory.HELVETICA, 8f, BaseColor.BLACK);
                    Font fGrandLabel = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9.5f, BaseColor.BLACK);
                    Font fGrandVal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9.5f, BaseColor.BLACK);
                    Font fWordsTitle = FontFactory.GetFont(FontFactory.HELVETICA, 7.5f, BaseColor.BLACK);
                    Font fWordsVal = FontFactory.GetFont(FontFactory.HELVETICA, 7.5f, BaseColor.BLACK);

                    // 1. Optional Shop Header (if configured)
                    if (hasShopHeader)
                    {
                        doc.Add(new Paragraph(settings.Header1_ShopName.Trim(), fShopName) { Alignment = Element.ALIGN_CENTER });
                        if (!string.IsNullOrWhiteSpace(settings.Header2_Address))
                        {
                            doc.Add(new Paragraph(settings.Header2_Address.Trim(), fShopSub) { Alignment = Element.ALIGN_CENTER });
                        }
                        if (!string.IsNullOrWhiteSpace(settings.Header3_Contact))
                        {
                            doc.Add(new Paragraph(settings.Header3_Contact.Trim(), fShopSub) { Alignment = Element.ALIGN_CENTER });
                        }
                        doc.Add(new Paragraph(new string('-', 56), fDivider) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 1f, SpacingAfter = 1f });
                    }

                    // 2. Bill Info Header:
                    // Bill No: 6609        Date : 22-09-2026
                    // To : SAFVAN
                    PdfPTable metaTable = new PdfPTable(2) { WidthPercentage = 100 };
                    metaTable.SetWidths(new float[] { 50f, 50f });

                    string billNoText = $"Bill No:  {record.VoucherNo}";
                    string dateText = $"Date :  {(record.SoldDate.HasValue ? record.SoldDate.Value.ToString("dd-MM-yyyy") : DateTime.Today.ToString("dd-MM-yyyy"))}";
                    string customerText = $"To  :  {(string.IsNullOrWhiteSpace(record.CustomerName) ? "CASH" : record.CustomerName.Trim())}";

                    PdfPCell cBillNo = new PdfPCell(new Phrase(billNoText, fBillMetaBold)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 1f };
                    PdfPCell cDate = new PdfPCell(new Phrase(dateText, fBillMeta)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1f };
                    PdfPCell cCust = new PdfPCell(new Phrase(customerText, fBillMetaBold)) { Border = Rectangle.NO_BORDER, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT, Padding = 1f };

                    metaTable.AddCell(cBillNo);
                    metaTable.AddCell(cDate);
                    metaTable.AddCell(cCust);
                    doc.Add(metaTable);

                    // Solid separator line above table header
                    doc.Add(new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.75f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1))) { SpacingBefore = 2f, SpacingAfter = 1f });

                    // 3. Items Table
                    // Columns: SI, Item Name, Qty, Rate, Amount
                    PdfPTable itemTable = new PdfPTable(5) { WidthPercentage = 100 };
                    itemTable.SetWidths(new float[] { 7f, 48f, 11f, 17f, 17f });

                    void AddTh(string text, int align)
                    {
                        PdfPCell c = new PdfPCell(new Phrase(text, fTh))
                        {
                            Border = Rectangle.BOTTOM_BORDER,
                            BorderWidthBottom = 0.75f,
                            BorderColorBottom = BaseColor.BLACK,
                            HorizontalAlignment = align,
                            PaddingTop = 1f,
                            PaddingBottom = 2f
                        };
                        itemTable.AddCell(c);
                    }

                    AddTh("SI", Element.ALIGN_LEFT);
                    AddTh("Item Name", Element.ALIGN_LEFT);
                    AddTh("Qty", Element.ALIGN_RIGHT);
                    AddTh("Rate", Element.ALIGN_RIGHT);
                    AddTh("Amount", Element.ALIGN_RIGHT);

                    double calcGross = 0;
                    if (items != null && items.Count > 0)
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            var it = items[i];
                            calcGross += it.NetAmount;

                            PdfPCell cSi = new PdfPCell(new Phrase((i + 1).ToString(), fRow)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 1.5f, PaddingBottom = 1.5f };
                            PdfPCell cName = new PdfPCell(new Phrase(it.ItemName ?? string.Empty, fRow)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, PaddingTop = 1.5f, PaddingBottom = 1.5f };
                            PdfPCell cQty = new PdfPCell(new Phrase(it.Qty.ToString("0.##"), fRow)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 1.5f, PaddingBottom = 1.5f };
                            PdfPCell cRate = new PdfPCell(new Phrase(it.Rate.ToString("0.00"), fRow)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 1.5f, PaddingBottom = 1.5f };
                            PdfPCell cAmt = new PdfPCell(new Phrase(it.NetAmount.ToString("0.00"), fRow)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 1.5f, PaddingBottom = 1.5f };

                            itemTable.AddCell(cSi);
                            itemTable.AddCell(cName);
                            itemTable.AddCell(cQty);
                            itemTable.AddCell(cRate);
                            itemTable.AddCell(cAmt);
                        }
                    }
                    else
                    {
                        PdfPCell cEmpty = new PdfPCell(new Phrase("Sales summary item", fRow)) { Border = Rectangle.NO_BORDER, Colspan = 5, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4f };
                        itemTable.AddCell(cEmpty);
                    }

                    doc.Add(itemTable);

                    // 4. Double divider line: ==================================================
                    doc.Add(new Paragraph(new string('=', 52), fDivider) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 2f, SpacingAfter = 1f });

                    // 5. Totals Table
                    double gross = record.NetTotal > 0 ? record.NetTotal : (calcGross > 0 ? calcGross : record.GrandTotal);
                    double discount = record.Discount;
                    double grand = record.GrandTotal > 0 ? record.GrandTotal : (gross - discount);

                    PdfPTable totTable = new PdfPTable(2) { WidthPercentage = 100 };
                    totTable.SetWidths(new float[] { 62f, 38f });

                    void AddTotRow(string label, string val, Font fontLabel, Font fontVal)
                    {
                        PdfPCell cLbl = new PdfPCell(new Phrase(label, fontLabel)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1f };
                        PdfPCell cVal = new PdfPCell(new Phrase(val, fontVal)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 1f };
                        totTable.AddCell(cLbl);
                        totTable.AddCell(cVal);
                    }

                    AddTotRow("Gross Amount", gross.ToString("0.00"), fTotalsLabel, fTotalsVal);
                    AddTotRow("Discount", discount.ToString("0.00"), fTotalsLabel, fTotalsVal);
                    AddTotRow("GRAND Total :", grand.ToString("0.00"), fGrandLabel, fGrandVal);

                    doc.Add(totTable);

                    // 6. Dashed separator line: --------------------------------------------------
                    doc.Add(new Paragraph(new string('-', 56), fDivider) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 2f, SpacingAfter = 2f });

                    // 7. Amount in words payable :
                    doc.Add(new Paragraph("Amount in words payable :", fWordsTitle) { SpacingBefore = 1f, SpacingAfter = 1f });
                    doc.Add(new Paragraph(ConvertAmountToWords(grand), fWordsVal) { SpacingAfter = 4f });

                    doc.Close();
                }
            }
        }

        public static string ConvertAmountToWords(double amount)
        {
            long wholePart = (long)Math.Floor(amount);
            long decimalPart = (long)Math.Round((amount - wholePart) * 100);

            if (wholePart == 0 && decimalPart == 0)
                return "Zero Rupees Only";

            string words = ConvertNumberToWordsIndian(wholePart);
            if (!string.IsNullOrWhiteSpace(words))
            {
                words += " Rupees";
            }

            if (decimalPart > 0)
            {
                string paisaWords = ConvertNumberToWordsIndian(decimalPart);
                if (!string.IsNullOrWhiteSpace(words))
                    words += " and " + paisaWords + " Paisa";
                else
                    words = paisaWords + " Paisa";
            }

            words += " Only";
            return words.Trim();
        }

        private static string ConvertNumberToWordsIndian(long number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + ConvertNumberToWordsIndian(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += ConvertNumberToWordsIndian(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += ConvertNumberToWordsIndian(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWordsIndian(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWordsIndian(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
