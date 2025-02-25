using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusiciansApp.Model
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Column("artist_id")]
        public int ArtistId { get; set; }

        [Column("album_id")]
        public int? AlbumId { get; set; } = null;

        [Column("title")]
        public string Title { get; set; }

        [Column("file_path")]
        public string FilePath {  get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("is_single")]
        public bool IsSingle { get; set; }

        public Album Album { get; set; }
        public Artist Artist { get; set; }

    }
}
