
/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Control } from "app/assayviewrepo/assayviewrepo-models/control";
import { Calibrator } from "app/assayviewrepo/assayviewrepo-models/calibrator";
import { Reagent } from "app/assayviewrepo/assayviewrepo-models/reagent";

export class ReagentTypeDescription {
    controls: Control[];
    calibrators: Calibrator[];
    reagents: Reagent[];

   /* constructor() {
        this.controls = [];
        this.calibrators = [];
        this.reagents = [];
    }*/
}