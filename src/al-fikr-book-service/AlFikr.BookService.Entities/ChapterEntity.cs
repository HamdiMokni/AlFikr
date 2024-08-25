using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities
{
    public class ChapterEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdDocument { get; set; }
        public int ChapterNumber { get; set; }
        public string Authors { get; set; }
        public int StartPage { get; set; }
        public int StartPageOuv { get; set; }
        public int EndPage { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
    }
}
