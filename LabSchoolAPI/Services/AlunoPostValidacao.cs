using Azure.Core;
using LabSchoolAPI.Controllers;
using LabSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace LabSchoolAPI.Services
{
    public static class AlunoPostValidacao
    {


        public static string ValidacaoALuno(Aluno aluno) {

            string mensagem = string.Empty;

            if ((aluno.CPF.ToString()).Length < 10 || (aluno.CPF.ToString()).Length >11)
            {
                mensagem = "O CPF deve conter 11 números ou 10 números caso inicie com 0.";
            }

            if (aluno.Nome.Length < 5)
            {
                mensagem = "O nome deve conter ao menos 5 caracteres.";

            }
            if (aluno.Telefone.Length < 10)
            {
                mensagem = "O telefone deve conter DDD.";
            }


            return mensagem; 
        }

                
       
               
    }
}





              
                
