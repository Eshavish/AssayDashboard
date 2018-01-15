using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.Models
{
    public class AssayType
    {
       /* public AssayType()
        {
            this.CurveFile = new HashSet<CurveFile>();
            this.ReportFile = new HashSet<ReportFile>();
        }*/
        [Key]
        [Required]
        public long ID { get; set; }
        [Required]
        public string AssayName { get; set; }
        [Required]
        public string AssayShortName { get; set; }
        public DateTime LastModifiedOn { get; set; }
        [Required]
        public string LastModifiedBy { get; set; }
        [Required]
        public long TypeId { get; set; }
        public virtual ICollection<CurveFile> CurveFile { get; set; }
       // public virtual ICollection<ReportFile> ReportFile { get; set; }
    }
}
