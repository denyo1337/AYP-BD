using Domain.Common;

namespace Domain.Models
{
    public class Role : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
