using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities
{
    public class VolumeGroupEntity
    {
        public int Id { get; set; }
        public int IdDocument { get; set; }
        public int NumVolume { get; set; }
        public int IdGroupRefs { get; set; }

    }
}
