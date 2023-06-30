using MiniHubApi.Application.Dtos;
using MiniHubApi.Application.Services;

namespace MiniHubApi.Application.Interfaces
{
    public interface ICreditoServices
    {
        Task<List<StatusCreditoResponseDto>> ObterStatusCredito(StatusCreditoResquestDto dto);

        Endereco? GetRetornandoUmStringXML();

        Endereco? GetRetornandoUmObjeto();
    }
}
