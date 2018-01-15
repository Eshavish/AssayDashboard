/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

import {CurveFile} from './CurveFile.component';

export class Combined{
    curveFile: CurveFile;
    AssayName: string;
    dataSet: DataSet;
    FileType: string;
} 

class DataSet{
    channels: Channel[] = [];
}

class Channel{
    channel: string;
    dict: { [sampleID: string] : DataPoint}
}

class DataPoint{
    xAxis: number[] = [];
    yAxis: number[] = [];
}