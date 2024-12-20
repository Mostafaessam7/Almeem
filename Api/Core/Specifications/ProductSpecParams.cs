﻿namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 30;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int? CategoryId { get; set; }
    public List<string>? CategoryNames { get; set; } = [];
    public string? Sort { get; set; }
    public bool? IsNewArrival { get; set; }
    public bool? IsActive { get; set; }
    private string? _search;
    public string? Search
    {
        get => _search;
        set => _search = value?.ToLower();
    }
}