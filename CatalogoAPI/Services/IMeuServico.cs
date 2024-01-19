using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Services;

public interface IMeuServico
{
    string Saudacao(string nome);
}
