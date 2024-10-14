using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Miljoboven.Models.POCO
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int ErrandId { get; set; }
    }
}
