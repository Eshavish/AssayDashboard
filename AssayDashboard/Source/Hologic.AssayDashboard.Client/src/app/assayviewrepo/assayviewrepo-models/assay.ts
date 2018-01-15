/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import { DataReduction } from "app/assayviewrepo/assayviewrepo-models/datareduction";
import { Reagent } from "app/assayviewrepo/assayviewrepo-models/reagent";
import { Control } from "app/assayviewrepo/assayviewrepo-models/control";
import { Calibrator } from "app/assayviewrepo/assayviewrepo-models/calibrator";
import { Analyte } from "app/assayviewrepo/assayviewrepo-models/analyte";
import { ReagentTypeDescription } from "app/assayviewrepo/assayviewrepo-models/reagentTypeDescription";
import { AssayMessage } from "app/assayviewrepo/assayviewrepo-models/assaymessage";

export class Assay {
  assayName: string;
  version: string;
  assayStatus: string;
  assayTypeID: string;
  thermalProfileReference: string;
  frontEndReference: string;
  resource: string;
  objectCrc: number;
  isFrontEnd: string;
  isLocked: string;
  calculatedCrc: number;
  analytes: string;
  analyteList: Analyte[];
  reagentTypeDescription: ReagentTypeDescription;
  fileName: string;
  type: string;
  dataReduction: DataReduction;
  xmlText: string = ''; 
  processSteps: any[];
  assayMessages: AssayMessage[];
  date: number;

  equals(assay: Assay): boolean {
    if (assay.fileName === this.fileName) 
      return true;
    return false;
  }

  getType(): void {
    if (this.isLocked === 'true' && this.frontEndReference === undefined && this.isFrontEnd === undefined)
      this.type = "TMA";
    if (this.isLocked === 'true' && this.frontEndReference !== undefined && String(this.assayTypeID).length <= 3)
      this.type = "PCR";
    if (this.isLocked === 'true' && this.isFrontEnd === 'true')
      this.type = "Front-end";
    if (String(this.assayTypeID).length > 3 && this.isLocked === 'false')
      this.type = "Open Access";
  }
}