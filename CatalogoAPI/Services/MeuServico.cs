namespace CatalogoAPI.Services;

public class MeuServico : IMeuServico
{
    public string Saudacao(string nome)
    {
        return $"Bem-vindo {nome}\n\n{DateTime.Now}";
    }
}
