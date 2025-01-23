﻿using API.Models.Dto;
using API.Models.Enums;
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

    public async Task TryCreateIfValid(PartnershipCommandDto dto)
    {
        if (dto.Validate())
            await PostAsync(dto);
        else
        {
            ApiView.SetValues(dto.Notifications.ToList(), HttpStatusCode.BadRequest, false);
        }
    }

    private async Task PostAsync(PartnershipCommandDto dto)
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

    public async Task SearchPartnershipAsync(uint clientId)
    {
        try
        {
            IEnumerable<PartnershipQueryDto> partnership = await _repository.SearchPartnershipAsync(clientId);
            ApiView.SetData(partnership);
        }
        catch(Exception ex)
        {
            ApiView.SetCode(HttpStatusCode.InternalServerError);
            _logger.LogError("Falha no processo #8fb3e966. Erro: {erro}", ex.Message);
        }
    }

    public async Task GetByStatusAsync(uint ClientNomieesId, EPartnershipStatus status)
    {
        try
        {
            IEnumerable<PartnershipQueryDto> partnership = await _repository.GetByStatus(ClientNomieesId, status);
            ApiView.SetData(partnership);
        }
        catch (Exception ex)
        {
            ApiView.SetCode(HttpStatusCode.InternalServerError);
            _logger.LogError("Falha no processo #4ed9bf29. Erro: {erro}", ex.Message);
        }
    }
}
