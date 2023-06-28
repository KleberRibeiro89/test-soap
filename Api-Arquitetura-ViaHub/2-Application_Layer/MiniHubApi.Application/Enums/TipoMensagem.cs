using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniHubApi.Application.Enums
{
    public enum TipoMensagem
    {
        [EnumMember(Value = "Nenhum Erro")]
        NenhumErro,
        [EnumMember(Value = "Error de Applicacao")]
        ErroApplicacao,
        [EnumMember(Value = "Error de Negocio")]
        ErroNegocio,
        [EnumMember(Value = "Error de Validacao")]
        ErroValidacao,
        [EnumMember(Value = "Aplicacao")]
        Aplicacao,
        [EnumMember(Value = "Negocio")]
        Negocio,
        [EnumMember(Value = "Validacao")]
        Validacao
    }
}
