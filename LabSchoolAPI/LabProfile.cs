using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Enums;
using LabSchoolAPI.Models;


namespace LabSchoolAPI
{
    public class LabProfile : Profile
    {
        public LabProfile()
        {

            CreateMap<Aluno, AlunoDTO>()
            .ForMember(dest => dest.DataNascimento, act => act.MapFrom(act => act.DataNascimento.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Situacao, act => act.MapFrom(act => act.Situacao.ToString()));
           

            CreateMap<AlunoDTOPost, Aluno>()
             .ForMember(dest => dest.Situacao, act => act.MapFrom(act => Enum.Parse (typeof (EnumSituacaoMatricula), act.Situacao)));

        }
    }
}
