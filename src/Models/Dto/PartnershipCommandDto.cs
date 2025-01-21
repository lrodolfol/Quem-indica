namespace API.Models.Dto;

public record PartnershipCommandDto(uint ClientNomieesId, uint ClientReferrerId);
public record PartnershipQueryDto(uint ClientReferrerId, string Name,  string FictitiousName, string Segment);
