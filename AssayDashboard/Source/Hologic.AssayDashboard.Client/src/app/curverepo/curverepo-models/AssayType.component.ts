/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/

export class AssayType{
    id: any;
    name: string;
    checked: boolean = false;

    constructor(id: any, name: any){
        this.id = id;
        this.name = name;
        this.checked = false;
    }
}