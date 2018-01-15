/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import {Analyte} from './Analyte';
import { Flag } from './Flag';

export class TestOrder {
    Analytes: Analyte[] = [];
    CompletionTime: string = "";
    InstrumentFlags: Flag[] = [];
    MeasurementTime: string = "";
    MtuId: string = "";
    Operator: string = "";
    OrderType: string = "";
    OtherData: any[] = []
    ParentTestOrderId: string = "";
    PipettingTime: string = "";
    PositionInMtu: string = "";
    RackAndPosition: string = "";
    Result: any[] =[];
    SampleDisplayName: string ="";
    SampleId: string ="";
    SampleType: string = "";
    TestOrderGuid: string = "";
    TestOrderId: string = "";
    WorklistIdSuffix: string ="";
}