﻿#region

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#endregion

namespace MovieTicketApi.Models.Enums;

public enum TicketStatus
{
    Pending = 0,
    Paid = 1,
    Cancelled = 2
}

public class TicketStatusConverter : EnumToStringConverter<TicketStatus>
{
}