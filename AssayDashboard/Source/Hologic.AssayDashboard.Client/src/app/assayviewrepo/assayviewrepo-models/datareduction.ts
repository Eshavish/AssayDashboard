/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { ResultCategory } from "app/assayviewrepo/assayviewrepo-models/resultcategory";
import { AssayDilution } from "app/assayviewrepo/assayviewrepo-models/assaydilution";
import { Setting } from "app/assayviewrepo/assayviewrepo-models/assaysetting";
import { AssayFlag } from "app/assayviewrepo/assayviewrepo-models/assayflag";

export class DataReduction {
    reductionAssembly: string;
    resultCategories: ResultCategory[];
    assayFlags: AssayFlag[];
    assaySettings: Setting[];
    assayDilutions: AssayDilution[];

    constructor() {
        this.resultCategories = undefined;
    }
}
