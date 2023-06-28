using MiniHubApi.Application.Enums;
using System.Runtime.Serialization;

namespace MiniHubApi.Application.Messages
{
    public class Mensagem
    {
        public Mensagem() : this(string.Empty, TipoMensagem.NenhumErro) { }

        public Mensagem(string conteudo, TipoMensagem tipoMensagem) : this(string.Empty, conteudo, tipoMensagem)
        {
        }
        public Mensagem(string codigo, string conteudo, TipoMensagem tipoMensagem)
        {
            Codigo = codigo;
            Conteudo = conteudo;
            TipoMensagem = tipoMensagem;
        }

        [DataMember]
        public string Codigo { get; }
        [DataMember]
        public string Conteudo { get; }
        [DataMember]
        public TipoMensagem TipoMensagem { get; }

        public override bool Equals(object? obj)
        {
            var mensagem = obj as Mensagem;

            return mensagem != null &&
                Codigo == mensagem.Codigo &&
                Conteudo == mensagem.Conteudo &&
                TipoMensagem == mensagem.TipoMensagem;
        }
    }
}