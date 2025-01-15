using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto;

public sealed class ClientDto : BaseDto
{
    [Required(ErrorMessage = "Nome não pode ser vazio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 50 caracteres")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Apelido não pode ser vazio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Apelido deve ter entre 2 e 50 caracteres")]
    public string FictitiousName { get; set; } = null!;
    [Required(ErrorMessage = "Obrigatório preenchimento do endereço")]
    public AddressDto Address { get; set; } = null!;
    [Required(ErrorMessage = "Obrigatório o preechimento do segmento")]
    public ESegment Segment { get; set; }

    public bool Validate()
    {
        if(string.IsNullOrWhiteSpace(Name)) AddNotifications("Nome não pode ser vazio");
        if(string.IsNullOrWhiteSpace(FictitiousName)) AddNotifications("Apelido não pode ser vazio");

        return Notifications.Count <= 0;
    }
}
