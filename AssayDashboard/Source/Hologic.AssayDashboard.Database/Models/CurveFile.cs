// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hologic.AssayDashboard.Database.Models
{
    public class CurveFile
    {
     
        [Key]
        [Required]
        public long ID { get; set; }
        public string Tag { get; set; }
        [Required]
        public bool IsGolden { get; set; }
        [Required]
        public string FullFileName { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        //foreign Key
        public long AssayDBID { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        [Required]
        public byte[] Data { get; set; }

        public string AssayVersion { get; set; }

        public string SoftwareVersion { get; set; }
        //Navigation properties
        public virtual AssayType AssayType { get; set; }
    }
}
