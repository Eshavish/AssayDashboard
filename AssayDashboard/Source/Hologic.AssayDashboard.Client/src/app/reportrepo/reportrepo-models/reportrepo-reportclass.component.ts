/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
export class Report {
    FileName: string;
    AssayNameID: number;
    ReportTypeID: number;
    MajorVersion: number;
    MinorVersion: number;
    ServicePackNumber: number;
    BuildNumber: number;
    LastModifiedOn: Date;
    LastModifiedBy: string;

    constructor(filename, assayname, reportname, majorversion, minorversion, servicepacknumber, buildnumber) {
        this.FileName = filename;
        this.AssayNameID = assayname;
        this.ReportTypeID = reportname;
        this.MajorVersion = majorversion;
        this.MinorVersion = minorversion;
        this.ServicePackNumber = servicepacknumber;
        this.BuildNumber = buildnumber;
        this.LastModifiedOn = new Date(Date.now());
        this.LastModifiedBy = 'admin';
    }
}