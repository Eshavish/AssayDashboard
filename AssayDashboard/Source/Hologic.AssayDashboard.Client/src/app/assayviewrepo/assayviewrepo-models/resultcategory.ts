/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { ResultEnumeration } from "app/assayviewrepo/assayviewrepo-models/resultenumeration";

export class ResultCategory {
    categoryName:string;
    index:string;
    sentToLIS: string;
    sentToUI: string;
    sentToReport:string;
    lisHold: string;
    inSpecimenTallies:string;
    resultEnum: ResultEnumeration;
    active: boolean;

    constructor() {
        this.categoryName = undefined;
        this.index = undefined;
        this.sentToLIS = undefined;
        this.sentToUI = undefined;
        this.lisHold = undefined;
        this.sentToReport = undefined;
        this.inSpecimenTallies = undefined;
        this.active = false;
    }
}