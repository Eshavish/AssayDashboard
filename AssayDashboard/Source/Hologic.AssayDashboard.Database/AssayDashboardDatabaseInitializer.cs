using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Hologic.AssayDashboard.Database
{
    public class AssayDashboardDatabaseInitializer : CreateDatabaseIfNotExists<AssayDashboardContext>
    {
        protected override void Seed(AssayDashboardContext context)
        {
            base.Seed(context);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "AC2 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =21,
                AssayShortName = "CT/GC",
            }
            );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "AC2 250",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1,
                AssayShortName = "CT/GC",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "AdV/hMPV/RV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =102,
                AssayShortName = "AdV/hMPV/RV",
            }
         );

            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "Babesia",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =50,
                AssayShortName = "Babesia",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "BV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId = 46,
                AssayShortName = "BV",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "C. diff",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =108,
                AssayShortName = "C. diff",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "CT 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =22,
                AssayShortName = "CT",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "CT 250",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =5,
                AssayShortName = "CT",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "CV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId = 48,
                AssayShortName = "CV",
            }
         );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "DENV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =36,
                AssayShortName = "DENV",
            }
       );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "dHBV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =97,
                AssayShortName = "dHBV",
            }
       );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "dHCV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =98,
                AssayShortName = "dHCV",
            }

      );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "dHIV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =99,
                AssayShortName = "dHIV",
            }

      );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "DNA60",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1507,
                AssayShortName = "DNA60",
            }

      );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "Flu A/B/RSV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =101,
                AssayShortName = "Flu A/B/RSV",
            }
      );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "GBS",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =106,
                AssayShortName = "GBS",
            }
     );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "GC 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =23,
                AssayShortName = "GC",
            }
     );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "GC 250",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =6,
                AssayShortName = "GC",
            }
     );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "GT HPV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =20,
                AssayShortName = "GT HPV",
            }
   );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "HEV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =39,
                AssayShortName = "HEV",
            }
   );

            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "HPV 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =24,
                AssayShortName = "HPV",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "HPV 250",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =14,
                AssayShortName = "HPV",
            }
);
               context.AssayType.Add(new Models.AssayType
            {
                AssayName = "HSV 1&2",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =40,
                AssayShortName = "HSV 1&2",
            }
   );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT RT TMA-1",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =90,
                AssayShortName = "LDT RT TMA-1",
            }
 );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT RT TMA-2",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =91,
                AssayShortName = "LDT RT TMA-2",
            }

 );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT RT TMA-3",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =92,
                AssayShortName = "LDT RT TMA-3",
            }

 );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT TMA-1",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =95,
                AssayShortName = "LDT TMA-1",
            }

 );
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT TMA-2",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =88,
                AssayShortName = "LDT TMA-2",
            }

);
             context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT TMA-3",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =89,
                AssayShortName = "LDT TMA-3",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "LDT1",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1001,
                AssayShortName = "LDT1",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "M gen",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =43,
                AssayShortName = "M gen",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "MRSA",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =107,
                AssayShortName = "MRSA",
            }     
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "OADNA55",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1505,
                AssayShortName = "OADNA55",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "OADNA58",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1506,
                AssayShortName = "OADNA58",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "OADNA62",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1508,
                AssayShortName = "OADNA62",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "OADNA65",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1509,
                AssayShortName = "OADNA65",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "Paraflu",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =103,
                AssayShortName = "Paraflu",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "Parvo/HAV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =29,
                AssayShortName = "Parvo/HAV",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "PSA 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =31,
                AssayShortName = "PSA",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "QCDNAIC",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1501,
                AssayShortName = "QCDNAIC",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "QCFluPPR",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1503,
                AssayShortName = "QCFluPPR",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "QCRNAIC",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1502,
                AssayShortName = "QCRNAIC",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "qHBV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =34,
                AssayShortName = "qHBV",
            }
);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "qHCV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =32,
                AssayShortName = "qHCV",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "qHIV-1",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =28,
                AssayShortName = "qHIV-1",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "RNA60",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1510,
                AssayShortName = "RNA60",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "RNA60-10uL",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =1512,
                AssayShortName = "RNA60-10uL",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "TRICH 100",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =9,
                AssayShortName = "TRICH",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "TRICH 250",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =10,
                AssayShortName = "TRICH",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "Ultrio Elite",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =25,
                AssayShortName = "Ultrio Elite",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "WNV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =4,
                AssayShortName = "WNV",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "ZIKV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =52,
                AssayShortName = "ZIKV",
            }

);
            context.AssayType.Add(new Models.AssayType
            {
                AssayName = "ZIKV",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
                TypeId =51,
                AssayShortName = "ZIKV",
            }

);
            //ReportType TABLE
            context.ReportType.Add(new Models.ReportType
            {
                ReportName = "ResultReport",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
            }

);
            context.ReportType.Add(new Models.ReportType
            {
                ReportName = "QCReport",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
            }

);
            context.ReportType.Add(new Models.ReportType
            {
                ReportName = "SampleCurveReport",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
            }

);
            context.ReportType.Add(new Models.ReportType
            {
                ReportName = "ResultWorklistReport",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
            }

);
            context.ReportType.Add(new Models.ReportType
            {
                ReportName = "PrevalenceReport",
                LastModifiedOn = DateTime.Now,
                LastModifiedBy = "admin",
            }

            );
        }
    }
}