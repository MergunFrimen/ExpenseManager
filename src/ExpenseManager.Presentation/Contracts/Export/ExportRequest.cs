namespace ExpenseManager.Presentation.Contracts.Export;

public record ExportRequest(
    string StartDate,
    string EndDate
    );