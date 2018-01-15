using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.Models
{
    public class ReportType
    {
      /*  public ReportType()
        {
            this.ReportFile = new HashSet<ReportFile>();
        }*/

        [Key]
        [Required]
        public long Id { get; set; }
        public string ReportName { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }

       // public virtual ICollection<ReportFile> ReportFile { get; set; }
    }
}
