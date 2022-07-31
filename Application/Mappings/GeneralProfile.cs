using Application.DTOs;
using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Cliente, ClienteDto>();            

            #endregion


            #region Comands
            CreateMap<CreateClienteCommand, Cliente>();
            CreateMap<UpdateClienteCommand, Cliente>();

            #endregion
        }
    }
}
