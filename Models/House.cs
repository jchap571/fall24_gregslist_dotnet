using System.ComponentModel.DataAnnotations;

namespace gregslist_csharp.Models;

public class House
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  [Range(1, 1000000)]
  public uint Sqft { get; set; }

  [Range(1, 1000000)]
  public uint Bedrooms { get; set; }

  [Range(1, 1000000)]
  public uint Bathrooms { get; set; }

  [MaxLength(500)]
  public string ImgUrl { get; set; }

  [MaxLength(500)]
  public string Description { get; set; }

  [Range(0, 1000000)]
  public int Price { get; set; }

  public string CreatorId { get; set; }
  public Account Creator { get; set; }

}