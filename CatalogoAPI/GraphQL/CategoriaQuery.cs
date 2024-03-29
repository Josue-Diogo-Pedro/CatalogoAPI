﻿using CatalogoAPI.Repository;
using GraphQL;
using GraphQL.Types;

namespace CatalogoAPI.GraphQL;

//Mapeamos os campos para uma dada consulta
//para uma chamada do repositorio (CategoriasRepository)
public class CategoriaQuery : ObjectGraphType
{
	//Recebe a instância do nosso UnitOfWork que contém
	//as instâncias dos repositorios
	public CategoriaQuery(IUnitOfWork _context)
	{
		//Nosso método vai retornar um objecto categoria
		Field<CategoriaType>("categoria")
			.Arguments(new QueryArguments(
				new QueryArgument<IntGraphType>() { Name = "id" }))
			.ResolveAsync(async context =>
			{
				var id = context.GetArgument<int>("id");
				return await _context.CategoriaRepository.GetById(c => c.CategoriaId == id);
			});


		//Nosso método vai retornar uma lista do objecto categoria
		//aqui resolve vai mapear a requisição do cliente com os dados
		//da consulta Get definida na CategoriaRepository
		Field<ListGraphType<CategoriaType>>("categorias")
			.Resolve(context =>
			{
				return _context.CategoriaRepository.Get();
			});


    }
}
