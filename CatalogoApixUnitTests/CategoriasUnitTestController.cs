using AutoMapper;
using CatalogoAPI.Context;
using CatalogoAPI.Controllers;
using CatalogoAPI.DTOs;
using CatalogoAPI.DTOs.Mappings;
using CatalogoAPI.Pagination;
using CatalogoAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApixUnitTests;

public class CategoriasUnitTestController
{
    private IMapper mapper;
    private IUnitOfWork repository;

    public static DbContextOptions<AppDbContext> dbContextOptions;

    private static string connectString = "Data Source=DESKTOP-DSNQA59\\SQLEXPRESS02;Initial Catalog=CatalogoAPI;Integrated Security=True; Trust Server Certificate=true;MultipleActiveResultSets=true";

    //Construtor estático é o primeiro a iniciar quando uma classe é instanciada
    static CategoriasUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectString)
            .Options;
    }

    public CategoriasUnitTestController()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile(new MappingProfile()));
        mapper = config.CreateMapper();

        var context = new AppDbContext(dbContextOptions);

        var repository = new UnitOfWork(context);
    }

    //=======================================================================
    // testes unitários
    // Inicio dos testes : método GET
    //[Fact]
    //public async void GetCategorias_Return_OkResult()
    //{
    //    //Arrange  
    //    var controller = new CategoriasController(mapper, repository);
    //    var param = new CategoriasParameters();
    //    param.PageNumber = 1;
    //    param.PageSize = 5;

    //    //Act  
    //    var data = await controller.Get(param);

    //    //Assert  
    //    Assert.IsType<List<CategoriaDTO>>(data);
    //}

    [Fact]
    public async void GetCategoriaById_Return_OkResultv2()
    {
        //Arrange  
        var controller = new CategoriasController(mapper, repository);
        var catId = 2;

        //Act  
        var data = controller.GetById(catId);

        //Assert  
        Assert.IsType<CategoriaDTO>(data);
    }
}
