import { trigger, state, style, transition, animate, group } from '@angular/animations';

export function routerTransition() {
    return trigger('handleTransition', [
      state('void', style({
        position: 'fixed',
        top: '5em',
        left: 0,
        right: 0,
        bottom: 0,
        width: '100%',
        overflow: 'scroll',
        height: '100%',
    })),
      transition(':enter', [
        style({ opacity: 0 }),
        animate('.5s', style({ opacity: 1 }))
    ]),
  ]);
}