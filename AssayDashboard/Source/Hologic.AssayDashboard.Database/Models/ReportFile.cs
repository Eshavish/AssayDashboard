using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.Models
{
    public class ReportFile
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string FileName { get; set; }
        public long AssayTypeId { get; set; }
        public long ReportTypeId { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int ServicePackNumber { get; set; }
        public int BuildNumber { get; set; }
        public Byte[] FileContent { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        [Required]
        public string HashValue { get; set; }
     

        //Navigation properties
        public virtual AssayType AssayType { get; set; }
        public virtual ReportType ReportType { get; set; }
    }
}
