using Application.Features.Clientes.Commands.CreateClienteCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Comands
            CreateMap<CreateClienteCommand, Cliente>();

            #endregion
        }
    }
}
