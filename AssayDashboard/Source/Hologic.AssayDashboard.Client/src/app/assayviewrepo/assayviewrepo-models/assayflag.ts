/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
export class AssayFlag {
    flagName: string;
    resultBit: String;
    flagId: String;
    flagSeverity: String;
    flagLegend: String;

    constructor() {
        this.flagName = undefined;
        this.resultBit = undefined;
        this.flagId = undefined;
        this.flagSeverity = undefined;
        this.flagLegend = undefined;
    }
}