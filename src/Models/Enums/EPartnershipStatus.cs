using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Models.Enums;

public enum EPartnershipStatus
{
    CREATED = 0,
    PENDING = 1,
    ACCEPTED = 2,
    CANCELED = 3,
    OVERDUE = 4,
    COMPLETED = 5,
    ERROR = 99
}
