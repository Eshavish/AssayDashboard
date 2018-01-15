/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { Injectable } from '@angular/core';
import { Combined } from '../curverepo-models/Combined.component';

@Injectable()
export class CurverepoStorageService {

  view_IsKnown: string;
  view_Assay: string;
  view_Tag: string;
  view_assayVersion: string;
  view_softwareVersion: string;
  
  cbFileStatus: boolean[] = [];
  selectAll: boolean;

  view_containers: Combined[] = [];

  curverepo_graph: string;
  curverepo_graphAssay: string;
  curverepo_fileName: string;
  curverepo_fileType: string;

  upload_containers: string;

  assays: any[] = [];
  
  constructor() { }

}
