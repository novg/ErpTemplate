using Application.Interfaces;
using Domain.Models;
using NPOI.SS.UserModel;

namespace Infrastructure.Services;

public class XlsxReader : IFileReader
{
    public Task<Order> Read(Stream stream)
    {
        IWorkbook workbook = WorkbookFactory.Create(stream);
        ISheet sheet = workbook.GetSheetAt(0);
        Order order = new Order();

        for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)
        {
            IRow row = sheet.GetRow(rowNum);

            if (row == null || row.Cells.Count < 2)
            {
                continue;
            }

            row.Cells.ElementAt(0).SetCellType(CellType.String);
            row.Cells.ElementAt(1).SetCellType(CellType.String);

            string bookIdValue = row.Cells.ElementAt(0).StringCellValue.Trim();
            string bookCountValue = row.Cells.ElementAt(1).StringCellValue.Trim();

            if (!int.TryParse(bookIdValue, out int bookId)
                || !int.TryParse(bookCountValue, out int bookCount))
            {
                continue;
            }

            order.Books.Add(new Book
            {
                Id = (int)bookId,
                Count = (int)bookCount
            });
        }

        return Task.Run(() => order);
    }
}