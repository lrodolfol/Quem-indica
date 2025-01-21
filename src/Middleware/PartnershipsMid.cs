using API.Models.Dto;
using API.Models.ReturnView;
using API.Repository.Abstraction;
using System.Net;

namespace API.Middleware;

public class PartnershipsMid
{
    private readonly IPartnershipRepository _repository;
    private readonly ILogger<PartnershipsMid> _logger;

    public ApiView ApiView { get; private set; }

    public PartnershipsMid(IPartnershipRepository repository, ILogger<PartnershipsMid> logger)
    {
        _repository = repository;
        _logger = logger;
        ApiView = new ApiView();
    }

    public async Task Post(PartnershipCommandDto dto)
    {
        try
        {
            await _repository.PostAsync(dto);
            ApiView.SetCode(HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            _logger.LogError("Falha no processo #bc23d48e. Erro: {erro}", ex.Message);
            ApiView.SetCode(HttpStatusCode.InternalServerError);
        }
    }

    public async Task GetAsync(uint clientId)
    {
        try
        {
            IEnumerable<PartnershipQueryDto> partnership = await _repository.SearchPartnershipByClient(clientId);
            ApiView.SetData(partnership);
        }
        catch(Exception ex)
        {
            ApiView.SetCode(HttpStatusCode.InternalServerError);
            _logger.LogError("Falha no processo #8fb3e966. Erro: {erro}", ex.Message);
        }
    }
}
