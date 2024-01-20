using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CatalogoAPI.Validations;

namespace CatalogoAPI.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    //[BindNever]
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}