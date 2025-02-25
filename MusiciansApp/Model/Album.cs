using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansApp.Model
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("artist_id")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public List<Song> Songs { get; set; }
    }
}
