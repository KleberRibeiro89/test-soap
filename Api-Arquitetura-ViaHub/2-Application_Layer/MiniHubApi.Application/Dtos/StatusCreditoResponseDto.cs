using MiniHubApi.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniHubApi.Application.Dtos
{
    public class StatusCreditoResponseDto : BaseResponse
    {
        public string Mensagem { get; set; }
    }
}
