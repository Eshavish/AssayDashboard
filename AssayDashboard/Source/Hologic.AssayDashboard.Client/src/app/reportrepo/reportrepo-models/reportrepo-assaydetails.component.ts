/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
export class AssayDetails {
    AssayName: string;
    TypeId: number;
    Id: number;

    constructor(assaynameselected, typeidselected, idnumberofassay) {
        this.AssayName = assaynameselected;
        this.TypeId = typeidselected;
        this.Id = idnumberofassay;
    }
}