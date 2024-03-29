﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Models;
[Table("Stock")]
public class Stock
{
    public int Id { get; set; }
    
    public string Symbol { get; set; } = string.Empty;

    public string CompanyName {get;set;} = string.Empty;
    [Column(TypeName ="decimal(18,2)")]
    public decimal LastDiv {get;set;}
    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap {get;set;}

    public List<Comment> comments{get;set;} = new List<Comment>();
    public List<Portfolio> portfolios { get; set; } = new List<Portfolio>();


}
