using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Photo
    {
        public int Photo_Id { get; set; }
        public string Photo_Name { get; set; }
        public string Photo_Src { get; set; }
        public DateTime Photo_Time { get; set; }
        public int album_id { get; set; }
        public string Photo_Describe { get; set; }
        public int Photo_Like { get; set; }
        public int Photo_Label { get; set; }
    }
} 