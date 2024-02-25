using AutoMapper;
using CatalogoAPI.Context;
using CatalogoAPI.DTOs.Mappings;
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
}
