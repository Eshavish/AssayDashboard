// import { TestBed, inject } from '@angular/core/testing';
// import { ParserService } from './parser.service';
// import { RouterModule } from "@angular/router";
// import { MockBackend } from "@angular/http/testing";
// import { ResponseOptions, HttpModule } from "@angular/http";
// import { APP_BASE_HREF } from "@angular/common";
// import { ToastrService } from "ngx-toastr";

// describe('ParserService', () => {
//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       providers: [ParserService, {provide: APP_BASE_HREF, useValue: '/'}],
//       imports:[HttpModule, RouterModule.forRoot([])]
//     });
//   });

//   it('should an assay object on subscribe',
//     inject([ParserService, MockBackend], (service: ParserService, MockBackend) => {
//       const mockResponse = {
//         data: [
//           { id: 0, name: 'Video 0' },
//           { id: 1, name: 'Video 1' },
//           { id: 2, name: 'Video 2' },
//           { id: 3, name: 'Video 3' },
//         ]
//       };

//       const resultSet = [{}];
//       const response = new Response(new ResponseOptions({body:resultSet, status:200}));
//       MockBackend.connections.subscribe(connection => connection.mockRespond(response));
//      // service.sendData("text", "fileName").subscribe(res => expect(res).toEqual(1));
//     }));


//  /* it('should be created', inject([ParserService], (service: ParserService) => {
//     expect(service).toBeTruthy();
//   }));*/
// });
