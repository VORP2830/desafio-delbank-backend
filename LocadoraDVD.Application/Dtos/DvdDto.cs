namespace LocadoraDVD.Application.Dtos;
public class DvdDto : BaseEntityDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Copies { get; set; }
    public bool Available { get; set; }
    public int DirectorId { get; set; }
}