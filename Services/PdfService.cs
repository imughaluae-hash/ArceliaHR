using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ArceliaHR.Models;
using ArceliaHR.Database.Repositories;

namespace ArceliaHR.Services
{
    public class PdfService
    {
        private readonly SettingsRepository _settingsRepo = new();

        static PdfService()
        {
            // QuestPDF Community License requirement
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private void ComposeHeader(IContainer container, string title)
        {
            var settings = _settingsRepo.GetSettings();

            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(settings.CompanyName ?? "ArceliaHR").FontSize(24).ExtraBold().FontColor(Colors.Blue.Medium);
                    col.Item().Text(title).FontSize(14).SemiBold().FontColor(Colors.Grey.Medium);
                });

                if (settings.CompanyLogo != null && settings.CompanyLogo.Length > 0)
                {
                    row.ConstantItem(80).AlignRight().Image(settings.CompanyLogo).FitArea();
                }
                else
                {
                    row.RelativeItem().AlignRight().Column(col =>
                    {
                        col.Item().Text($"Date: {DateTime.Now:dd/MM/yyyy}");
                    });
                }
            });
        }

        public byte[] GenerateStatementPdf(EmployeeModel employee, List<EmployeeStatementForm.StatementRow> rows)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header().Element(c => ComposeHeader(c, "Employee Statement of Account"));

                    page.Content().PaddingVertical(10).Column(x =>
                    {
                        x.Item().PaddingBottom(5).Text(t =>
                        {
                            t.Span("Employee: ").SemiBold();
                            t.Span(employee.Name);
                            t.Span(" | ID: ").SemiBold();
                            t.Span(employee.Id?.ToString() ?? "N/A");
                        });

                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30); // SN
                                columns.ConstantColumn(80); // Date
                                columns.RelativeColumn();   // Description
                                columns.ConstantColumn(80); // Due
                                columns.ConstantColumn(80); // Paid
                                columns.ConstantColumn(100); // Balance
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("SN");
                                header.Cell().Element(CellStyle).Text("Date");
                                header.Cell().Element(CellStyle).Text("Description");
                                header.Cell().Element(CellStyle).AlignRight().Text("Due");
                                header.Cell().Element(CellStyle).AlignRight().Text("Paid");
                                header.Cell().Element(CellStyle).AlignRight().Text("Balance");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                }
                            });

                            foreach (var row in rows)
                            {
                                table.Cell().Element(RowStyle).Text(row.SN.ToString());
                                table.Cell().Element(RowStyle).Text(row.Date.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(row.Description);
                                table.Cell().Element(RowStyle).AlignRight().Text(row.DueAmount);
                                table.Cell().Element(RowStyle).AlignRight().Text(row.Paid);
                                table.Cell().Element(RowStyle).AlignRight().Text(row.Balance);

                                static IContainer RowStyle(IContainer container)
                                {
                                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten3).PaddingVertical(3);
                                }
                            }
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            });

            return document.GeneratePdf();
        }

        public byte[] GenerateProfilePdf(EmployeeModel employee)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header().Element(c => ComposeHeader(c, "Employee Personal Profile"));

                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        // Profile Top Section with Photo
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(infoCol =>
                            {
                                infoCol.Item().Text(employee.Name).FontSize(22).Bold().FontColor(Colors.Blue.Darken2);
                                infoCol.Item().Text(employee.Work ?? "Designation Not Set").FontSize(14).Italic().FontColor(Colors.Grey.Darken1);
                                infoCol.Item().PaddingTop(5).Text(t => { t.Span("Department: ").SemiBold(); t.Span(employee.Department ?? "N/A"); });
                                infoCol.Item().Text(t => { t.Span("Employee Status: ").SemiBold(); t.Span(employee.Status ?? "Active").FontColor(employee.Status == "Active" ? Colors.Green.Medium : Colors.Red.Medium); });
                            });

                            if (employee.Picture != null && employee.Picture.Length > 0)
                            {
                                row.ConstantItem(100).Height(120).Border(1).BorderColor(Colors.Grey.Lighten2).Image(employee.Picture).FitArea();
                            }
                        });

                        col.Item().PaddingTop(20).BorderBottom(1).BorderColor(Colors.Blue.Lighten4).PaddingBottom(5).Text("Personal Information").FontSize(14).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().PaddingTop(10).Column(c =>
                        {
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Father's Name: ").Bold(); x.Span(employee.FatherName ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Date of Birth: ").Bold(); x.Span(employee.DateOfBirth?.ToString("dd/MM/yyyy") ?? "N/A"); }); });
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Nationality: ").Bold(); x.Span(employee.Nationality ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Religion: ").Bold(); x.Span(employee.Religion ?? "N/A"); }); });
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Gender: ").Bold(); x.Span(employee.Gender ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Marital Status: ").Bold(); x.Span(employee.MaritalStatus ?? "N/A"); }); });
                        });

                        col.Item().PaddingTop(20).BorderBottom(1).BorderColor(Colors.Blue.Lighten4).PaddingBottom(5).Text("Contact Details").FontSize(14).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().PaddingTop(10).Column(c =>
                        {
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Mobile: ").Bold(); x.Span(employee.Mobile ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Basic Salary: ").Bold(); x.Span(employee.BasicSalary?.ToString("N2") ?? "0.00"); }); });
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("ICE Contact: ").Bold(); x.Span(employee.ICEContact ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Relation: ").Bold(); x.Span(employee.Relation ?? "N/A"); }); });
                        });

                        col.Item().PaddingTop(20).BorderBottom(1).BorderColor(Colors.Blue.Lighten4).PaddingBottom(5).Text("Legal Documentation").FontSize(14).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().PaddingTop(10).Column(c =>
                        {
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Passport No: ").Bold(); x.Span(employee.PassportNumber ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("Emirates ID: ").Bold(); x.Span(employee.IDNumber ?? "N/A"); }); });
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Passport Issue: ").Bold(); x.Span(employee.PassportIssueDate?.ToString("dd/MM/yyyy") ?? "N/A"); }); r.RelativeItem().Text(x => { x.Span("ID Expiry: ").Bold(); x.Span(employee.IDExpiryDate?.ToString("dd/MM/yyyy") ?? "N/A"); }); });
                            c.Item().Row(r => { r.RelativeItem().Text(x => { x.Span("Passport Expiry: ").Bold(); x.Span(employee.PassportExpiryDate?.ToString("dd/MM/yyyy") ?? "N/A"); }); r.RelativeItem(); });
                        });
                    });

                    page.Footer().PaddingTop(20).Column(f =>
                    {
                        f.Item().BorderTop(1).BorderColor(Colors.Grey.Lighten2).PaddingTop(5).Row(row =>
                        {
                            row.RelativeItem().Text("Generated by ArceliaHR Software").FontSize(9).FontColor(Colors.Grey.Medium);
                            row.RelativeItem().AlignRight().Text(DateTime.Now.ToString("f")).FontSize(9).FontColor(Colors.Grey.Medium);
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}
