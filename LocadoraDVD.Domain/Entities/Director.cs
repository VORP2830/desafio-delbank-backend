namespace LocadoraDVD.Domain.Entities;

public class Director : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public IEnumerable<Dvd> Dvds { get; set; }
    public string GetFullName()
        {
            return $"{Name} {Surname}";
        }
}
