// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------

using Hologic.AssayDashboard.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hologic.AssayDashboard.Database.Repositories
{
    public class CurveFilesRepository : IDisposable
    {
        public class VersionPair
        {
            public string[] AssayVersion { get; set; }
            public string[] SoftwareVersion { get; set; }
        }

        private readonly AssayDashboardContext _dataContext;

        public CurveFilesRepository()
        {
                _dataContext = new AssayDashboardContext();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public IEnumerable<CurveFile> GetAll()
        {
            return _dataContext.CurveFile.ToList();
        }

        public int GetNumberOfKnown()
        {
            return _dataContext.CurveFile.Where(cf => cf.IsGolden).ToList().Count;
        }

        public int GetNumberOfCurve()
        {
            return _dataContext.CurveFile.ToList().Count;
        }

        public int GetCountOfCurve(long typeId)
        {
            return _dataContext.CurveFile.Where(cf => cf.AssayDBID == typeId).ToList().Count;
        }

        public IEnumerable<CurveFile> GetAllGolden()
        {
            return _dataContext.CurveFile.Where(cf => cf.IsGolden).ToList();
        }

        public IEnumerable<CurveFile> GetCurveWithTag (string Tag)
        {
            return _dataContext.CurveFile.Where(cf => cf.Tag == Tag).ToList();
        }

        public IEnumerable<CurveFile> GetCurveWithTypeId(long TypeId)
        {
            return _dataContext.CurveFile.Where(cf => cf.AssayDBID == TypeId).ToList();
        }

        public IEnumerable<long> GetUniqueTypeID()
        {
            return _dataContext.CurveFile.Select(cf => cf.AssayDBID).Distinct().ToList();
        }
        public IEnumerable<CurveFile> GetSpecifiedFile (Boolean IsGolden, string Tag, string AssayVersion, string SoftwareVersion, long ID)
        {
            IEnumerable<CurveFile> result = Enumerable.Empty<CurveFile>();
            if (IsGolden == true)
            {
                //when assay is specified
                if (ID != -1)
                {
                    if (AssayVersion == "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID && cf.IsGolden == IsGolden
                        && cf.Tag.Contains(Tag)).ToList();
                    }
                    else if (AssayVersion == "" && SoftwareVersion != "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID && cf.IsGolden == IsGolden
                        && cf.Tag.Contains(Tag) && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                    else if (AssayVersion != "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID && cf.IsGolden == IsGolden
                       && cf.Tag.Contains(Tag) && cf.AssayVersion == AssayVersion).ToList();
                    }
                    else
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID && cf.IsGolden == IsGolden
                      && cf.Tag.Contains(Tag) && cf.AssayVersion == AssayVersion && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                }
                //Assay is specified
                else
                {
                    if (AssayVersion == "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.IsGolden == IsGolden && cf.Tag.Contains(Tag)).ToList();
                    }
                    else if (AssayVersion == "" && SoftwareVersion != "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.IsGolden == IsGolden && cf.Tag.Contains(Tag) && 
                        cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                    else if (AssayVersion != "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.IsGolden == IsGolden && cf.Tag.Contains(Tag) &&
                        cf.AssayVersion == AssayVersion).ToList();
                    }
                    else
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.IsGolden == IsGolden && cf.Tag.Contains(Tag) &&
                        cf.AssayVersion == AssayVersion && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                }
            }
            else
            {
                if (ID != -1)
                {
                    if (AssayVersion == "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID 
                        && cf.Tag.Contains(Tag)).ToList();
                    }
                    else if (AssayVersion == "" && SoftwareVersion != "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID
                        && cf.Tag.Contains(Tag) && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                    else if (AssayVersion != "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID 
                       && cf.Tag.Contains(Tag) && cf.AssayVersion == AssayVersion).ToList();
                    }
                    else
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.AssayDBID == ID&& cf.Tag.Contains(Tag)
                        && cf.AssayVersion == AssayVersion && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                }
                else
                {
                    if (AssayVersion == "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.Tag.Contains(Tag)).ToList();
                    }
                    else if (AssayVersion == "" && SoftwareVersion != "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.Tag.Contains(Tag) &&
                        cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                    else if (AssayVersion != "" && SoftwareVersion == "")
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.Tag.Contains(Tag) &&
                        cf.AssayVersion == AssayVersion).ToList();
                    }
                    else
                    {
                        result = _dataContext.CurveFile.Where(cf => cf.Tag.Contains(Tag) &&
                        cf.AssayVersion == AssayVersion && cf.SoftwareVersion == SoftwareVersion).ToList();
                    }
                }
            }
            if (result == null)
            {
                return null;
            }
            return result;
        }
        public void AddCurveFile(CurveFile[] curveObjects)
        {
            foreach(var curveObject in curveObjects) {
                if (curveObject != null)
                {
                    _dataContext.CurveFile.Add(curveObject);
                    _dataContext.SaveChanges();
                }
            }
        }

        public VersionPair GetVersion()
        {
            VersionPair result = new VersionPair {
                AssayVersion = _dataContext.CurveFile.Select(cf => cf.AssayVersion).Distinct().ToList().ToArray(),
                SoftwareVersion = _dataContext.CurveFile.Select(cf => cf.SoftwareVersion).Distinct().ToList().ToArray()
            };
            string delim = "";

            result.AssayVersion = result.AssayVersion.Where(val => val != delim).ToArray();
            result.SoftwareVersion = result.SoftwareVersion.Where(val => val != delim).ToArray();


            return result;
        }


    }
}
