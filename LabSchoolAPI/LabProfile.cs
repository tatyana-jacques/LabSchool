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

            CreateMap<Aluno, AlunoDTOResposta>()
               .ForMember(dest => dest.DataNascimento, act => act.MapFrom(act => act.DataNascimento.ToString("yyyy-MM-dd")))
               .ForMember(dest => dest.Situacao, act => act.MapFrom(act => act.Situacao.ToString()));

            CreateMap<AlunoDTOPostRequisicao, Aluno>()
               .ForMember(dest => dest.Situacao, act => act.MapFrom(act => Enum.Parse(typeof(EnumSituacaoMatricula), act.Situacao.ToUpper())));

            CreateMap<Professor, ProfessorDTOResposta>()
               .ForMember(dest => dest.DataNascimento, act => act.MapFrom(act => act.DataNascimento.ToString("yyyy-MM-dd")))
               .ForMember(dest => dest.Formacao, act => act.MapFrom(act => act.Formacao.ToString()))
               .ForMember(dest => dest.Experiencia, act => act.MapFrom(act => act.Experiencia.ToString()))
               .ForMember(dest => dest.Estado, act => act.MapFrom(act => act.Estado.ToString()));

            CreateMap<ProfessorDTORequisicao, Professor>()
                .ForMember(dest => dest.Formacao, act => act.MapFrom(act => Enum.Parse(typeof(EnumFormacaoAcademica), act.Formacao.ToUpper())))
                .ForMember(dest => dest.Experiencia, act => act.MapFrom(act => Enum.Parse(typeof(EnumExperiencia), act.Experiencia.ToUpper())))
                .ForMember(dest => dest.Estado, act => act.MapFrom(act => Enum.Parse(typeof(EnumEstado), act.Estado.ToUpper())));

            CreateMap<Pedagogo, PedagogoDTOResposta>()
                .ForMember(dest => dest.DataNascimento, act => act.MapFrom(act => act.DataNascimento.ToString("yyyy-MM-dd")));

            CreateMap<PedagogoDTORequisicao, Pedagogo>();
            
          
            



        }
    }
}
