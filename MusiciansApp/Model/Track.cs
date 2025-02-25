using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansApp.Model
{
    public class Track
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("musician")]
        public string Musician { get; set; }

        [Column("track_path")]
        public string FilePath { get; set; }
    }
}
