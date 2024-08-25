using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.FrontendUI.Entities;

public class AdvancedSearchEntity
{
    public List<AdvancedSearchItem> AdvancedSearchEntities { get; set; }
}

public class AdvancedSearchItem
{
    public string? Field { get; set; }
    public string? Expression { get; set; }
    public string? Operator { get; set; }
}
