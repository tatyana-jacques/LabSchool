using AutoMapper;
using LabSchoolAPI.DTO;
using LabSchoolAPI.Models;


namespace LabSchoolAPI
{
    public class LabProfile: Profile
    {
        public LabProfile() {

            CreateMap<Aluno, AlunoDTO>()
            .ForMember(dest => dest.DataNascimento, act => act.MapFrom(act => act.DataNascimento.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.SituacaoMatricula, act => act.MapFrom(act => act.SituacaoMatricula.ToString()));

            CreateMap<AlunoDTO, Aluno>();
        
        }
            
    }
}
