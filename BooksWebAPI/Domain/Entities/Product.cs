using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebAPI.Domain.Entities;

public partial class Product
{
    // Primary key
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Isbn { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Author { get; set; } = null!;

    [Range(0.01, double.MaxValue)]
    public double ListPrice { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Price { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Price50 { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Price100 { get; set; }

    // Foreign key
    [Required]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(250)]
    public string ImageUrl { get; set; } = null!;

    // Navigation property
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = null!;

    // Optional concurrency token
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
