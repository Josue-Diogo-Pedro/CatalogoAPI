using CatalogoAPI.Repository;
using GraphQL;
using GraphQL.Types;
using GraphQL.NewtonsoftJson;

namespace CatalogoAPI.GraphQL;

//É incluido no pipeline do request para processar
//a requisição http usando a instância do nosso repositório
public class TesteGraphQLMiddleware
{
    //instancia para processar o request http
    //private readonly RequestDelegate _next;

    ////Instancia do UnitOfWork
    //private readonly UnitOfWork _context;

    //public TesteGraphQLMiddleware(RequestDelegate next, UnitOfWork context)
    //{
    //    _next = next;
    //    _context = context;
    //}

    //public async Task Invoke(HttpContext httpContext)
    //{
    //    //Verifica se o caminho do request é /graphql
    //    if (httpContext.Request.Path.StartsWithSegments("/graphql"))
    //    {
    //        //Tenta ler o corpo do request usando o StreamReader
    //        using(var stream = new StreamReader(httpContext.Request.Body))
    //        {
    //            var query = await stream.ReadToEndAsync();
    //            if (!string.IsNullOrWhiteSpace(query))
    //            {
    //                //Um object schema é criado com a propriedade Query
    //                //Definida com uma instância do nosso contexto(repositorio)
    //                var schema = new Schema
    //                {
    //                    Query = new CategoriaQuery(_context)
    //                };

    //                //Criamos um DocumentExecuter que
    //                //Executa consulta contra o schema e o resultado
    //                //é escrito no response como JSON via WirteResult
    //                var result = await new DocumentExecuter().ExecuteAsync(options =>
    //                {
    //                    options.Schema = schema;
    //                    options.Query = query;
    //                });

    //                await WriteResult(httpContext, result);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        await _next(httpContext);
    //    }
    //}


    //private async Task WriteResult(HttpContext httpContext, ExecutionResult result)
    //{
    //    var json = new DocumentWriter
    //}

}
