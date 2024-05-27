using LocadoraDVD.Domain.Enums;

namespace LocadoraDVD.Domain.Entities;

public class Dvd : BaseEntity
{
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public int Copies { get; set; }
    public bool Available { get; set; }
    public int DirectorId { get; set; }
    public Director Director { get; set; }
}
