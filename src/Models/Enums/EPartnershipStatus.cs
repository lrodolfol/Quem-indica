using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Models.Enums;

public enum EPartnershipStatus
{
    PENDING = 0,
    CANCELED = 1,
    OVERDUE = 4,
    IGNORED = 5,
    COMPLETED = 6,
    ERROR = 99
}
