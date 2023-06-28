using MiniHubApi.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniHubApi.Application.Messages
{
    public abstract class BaseResponse
    {
        private bool valido = true;

        public Guid Protocolo { get; set; }

        public List<Mensagem> Mensagens { get; set; } = new List<Mensagem>();

        public bool Valido { get { return !Mensagens.Any(); } set { Valido = value; } }

        public void AdicionarMensagemErro(TipoMensagem tipoMensagem, string conteudo)
        {
            Mensagens.Add(new Mensagem(conteudo, tipoMensagem));
        }

        public void AdicionarMensagemErro(List<Mensagem> mensagens)
        {
            mensagens.ForEach(m => Mensagens.Add(m));
        }

        public void AdicionarMensagemErro(string codigo, string conteudo, TipoMensagem tipoMensagem)
        {
            Mensagens.Add(new Mensagem(codigo, conteudo, tipoMensagem));
        }

        public void AdicionarMensagemErro(Mensagem mensagem)
        {
            Mensagens.Add(mensagem);
        }
    }
}
