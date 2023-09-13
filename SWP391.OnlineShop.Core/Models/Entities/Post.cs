using SWP391.OnlineShop.Core.Models.BaseEntities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Post : BaseEntity<int>
{
    public Post()
    {
        PostTags = new HashSet<PostTag>();
    }

    public string Title { get; set; }
    public string Featured { get; set; }
    public string Brief { get; set; }
    public string Description { get; set; }
    public string Thumbnail { get; set; }
    public string Author { get; set; }
    public int? CategoryId { get; set; }

    public Category Category { get; set; }

    public ICollection<PostTag> PostTags { get; set; }
}