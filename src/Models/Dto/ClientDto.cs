using API.Models.Enums;

namespace API.Models.Dto;

public class ClientDto
{
    public string Name { get; set; } = null!;
    public string FictitiousName { get; set; } = null!;
    public AddressDto Address { get; set; }
    public ESegment Segment { get; set; }
}
