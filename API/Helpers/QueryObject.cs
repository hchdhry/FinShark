namespace API.Helpers{

public class QueryObject
{
    public string ? CompanyName {get; set;}

    public string ? Symbol {get; set;}

    public string ? SortBy { get; set; }

    public bool IsDescending { get; set; } = false;

}
}
