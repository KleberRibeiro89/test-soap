using MiniHubApi.Application.Dtos;

namespace MiniHubApi.Application.Interfaces
{
    public interface ICreditoServices
    {
        Task<List<StatusCreditoResponseDto>> ObterStatusCredito(StatusCreditoResquestDto dto);
    }
}
