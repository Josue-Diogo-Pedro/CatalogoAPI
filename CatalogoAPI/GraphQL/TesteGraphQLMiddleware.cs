using CatalogoAPI.Repository;

namespace CatalogoAPI.GraphQL;

//É incluido no pipeline do request para processar
//a requisição http usando a instância do nosso repositório
public class TesteGraphQLMiddleware
{
    //instancia para processar o request http
    private readonly RequestDelegate _next;

    //Instancia do UnitOfWork
    private readonly UnitOfWork _context;


}
