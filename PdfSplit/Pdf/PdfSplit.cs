using Microsoft.AspNetCore.Components.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfSplit.Pdf;

public class PdfSplit
{
    public async Task<List<(string, MemoryStream)>> Split(IBrowserFile pdfPath, int pagesPerSplit)
    {
        List<(string, MemoryStream)> result = new(); 

        var fileName = pdfPath.Name;
        Stream openReadStream = pdfPath.OpenReadStream(100_000_000);
        try
        {
            using var memoryStream = new MemoryStream();
            await openReadStream.CopyToAsync(memoryStream);
            using PdfDocument document = PdfReader.Open(memoryStream, PdfDocumentOpenMode.Import);
            int totalPageCount = document.PageCount;
            int splitCount = (totalPageCount + pagesPerSplit - 1) / pagesPerSplit;

            for (int i = 0; i < splitCount; i++)
            {
                int startPage = i * pagesPerSplit;
                int endPage = Math.Min(startPage + pagesPerSplit, totalPageCount);

                PdfDocument newPdf = new PdfDocument();
                for (int j = startPage; j < endPage; j++)
                {
                    newPdf.AddPage(document.Pages[j]);
                }

                var name = $"{fileName}_{i * pagesPerSplit}_{(i + 1) * pagesPerSplit}.pdf";
                // newPdf.Save($"E:\\{fileName}_{i * pagesPerSplit}_{(i + 1) * pagesPerSplit}.pdf");
                using var outStream = new MemoryStream();
                newPdf.Save(outStream);
                result.Add((name, outStream));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return result;
    }
}