/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import {TestOrder} from './TestOrder';
import { Item } from "./Item";

export class Run {
  AssayName: string;
  AssayType: string;
  AssayVersion: string;
  BackCtrlSetId: string;
  CalSetId: string;
  FrontCtrlSetId: string;
  MasterLotBarcode: string;
  MasterLotExpiration: string;
  OtherData: any;
  PcrCartridgeLot: string;
  RunId: string;
  SerialNumber: string;
  SiteName: string;
  SystemSWVersion: string;
  TestOrders: any;
  FileName: string;

  equals(run: Run): boolean {
    if (run.FileName === this.FileName) 
      return true;
    return false;
  }
}