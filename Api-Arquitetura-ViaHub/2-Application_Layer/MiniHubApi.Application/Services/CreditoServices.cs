using Microsoft.VisualBasic;
using MiniHubApi.Application.Dtos;
using MiniHubApi.Application.Interfaces;
using MiniHubApi.Infra.Soap;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MiniHubApi.Application.Services
{
    public class CreditoServices : ICreditoServices
    {
        public readonly ISoapService _soapService;
        public CreditoServices(ISoapService soapService)
        {
            _soapService = soapService;
        }


        public async Task<List<StatusCreditoResponseDto>> ObterStatusCredito(StatusCreditoResquestDto dto)
        {
            _soapService.Url = "https://viacep.com.br/ws/01001000/xml/";
            var result = await _soapService.GetSoapAsync(string.Empty);

            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(byteArray);

            var endereco = new XmlSerializer(typeof(Endereco)).Deserialize(stream);

            List<StatusCreditoResponseDto>? response = null;
            response = new List<StatusCreditoResponseDto>();
            response.Add(new StatusCreditoResponseDto
            {
                Mensagem = "Occorreu sucesso na chamada!"
            });

            return response;
        }

        public Endereco? GetRetornandoUmStringXML()
        {
            _soapService.Url = "https://viacep.com.br/ws/01001000/xml/";
            var result = _soapService.GetSoapAsync(string.Empty).Result;
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(byteArray);

            var endereco = new XmlSerializer(typeof(Endereco)).Deserialize(stream) as Endereco;



            return endereco;
        }

        public Endereco? GetRetornandoUmObjeto()
        {
            _soapService.Url = "https://viacep.com.br/ws/01001000/xml/";
            var result = _soapService.GetSoapAsync<Endereco>(null).Result;

            return result;
        }
    }

    [Serializable()]
    [XmlRoot("xmlcep")]
    public class Endereco
    {
        [XmlElement("cep")]
        public string Cep { get; set; }

        [XmlElement("logradouro")]
        public string Logradouro { get; set; }

        [XmlElement("complemento")]
        public string Complemento { get; set; }

        [XmlElement("bairro")]
        public string Bairro { get; set; }

        [XmlElement("localidade")]
        public string Localidade { get; set; }

        [XmlElement("uf")]
        public string Uf { get; set; }


    }
}
