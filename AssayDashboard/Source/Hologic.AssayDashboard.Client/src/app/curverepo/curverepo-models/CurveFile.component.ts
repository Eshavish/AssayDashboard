/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

export class CurveFile{
    ID: number;
    Tag: string;
    IsGolden: boolean;
    FullFileName: string;
    CreationDate: Date;
    AssayDBID: number;
    ModifiedBy: string;
    Data: any;
    FormatCreationDate: string;
    Assay: string;
    AssayVersion: string;
    SoftwareVersion: string;

    //default testing constructor
    constructor(tag, goldenTag, fileName){
        this.ID= 1;
        this.Tag = tag;
        this.IsGolden = goldenTag;
        this.CreationDate = new Date(Date.now());
        this.FullFileName = fileName;
        this.AssayDBID = 0;
        this.ModifiedBy = "admin";
        this.Data = null;
        this.AssayVersion = "";
        this.SoftwareVersion = "";
    }
}

