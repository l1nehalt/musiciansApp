using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansApp.Model
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        public List<Album> Albums { get; set; }

        public List<Song> Singles { get; set; }
    }
}
