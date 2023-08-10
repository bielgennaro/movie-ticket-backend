using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MovieTicketApi.Models.Enums;

public enum TicketStatus
{
    Pending = 0,
    Paid = 1,
    Cancelled = 2
}

public class TicketStatusConverter : EnumToStringConverter<TicketStatus>
{
    public TicketStatusConverter(ConverterMappingHints mappingHints = null)
        : base(mappingHints)
    {
    }
}