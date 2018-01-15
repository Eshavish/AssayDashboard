/*
// ----------------------------------------------------------------
// Copyright (c) 2017 Hologic, Inc.  All Rights Reserved.
// ----------------------------------------------------------------
*/
import { FluidVessel } from "app/assayviewrepo/assayviewrepo-models/fluidvessel";

export class Reagent {
    reagentName:string;
    reagentTypeID:string;
    onBoardStability;
    openKitStability;
    isOnboard:string;
    fluidVessel: FluidVessel;

    constructor() {
        this.reagentName = undefined;
        this.reagentTypeID = undefined;
        this.onBoardStability = undefined;
        this.openKitStability = undefined;
        this.isOnboard = undefined;
    }

    public convertTime() {
        let days: number = +this.openKitStability / 1440;
        let hours: number = +this.openKitStability % 1440 * 24;

        if(hours > 0) {
            this.openKitStability = days + ' days, ' + ('0' + hours).slice(-2) + ' hours';
        }
        else {
            this.openKitStability = days + ' days'; 
        }
        this.onBoardStability = ('0' + +this.onBoardStability / 60).slice(-2) + ' hours';
    }
}