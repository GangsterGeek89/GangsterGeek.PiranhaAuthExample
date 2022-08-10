using Piranha.AttributeBuilder;
using Piranha.Models;

namespace Site.Models
{
    [PostType(Title = "Standard post")]
    public class StandardPost  : Post<StandardPost>
    {
    }
}