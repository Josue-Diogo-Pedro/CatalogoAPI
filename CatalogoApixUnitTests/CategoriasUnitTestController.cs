using AutoMapper;
using CatalogoAPI.Context;
using CatalogoAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApixUnitTests;

public class CategoriasUnitTestController
{
    private IMapper mapper;
    private IUnitOfWork repository;

    public static DbContextOptions<AppDbContext> dbContextOptions;

    private static string connectString = "Data Source=DESKTOP-DSNQA59\\SQLEXPRESS02;Initial Catalog=CatalogoAPI;Integrated Security=True; Trust Server Certificate=true;MultipleActiveResultSets=true";
}
