import { Component, Input } from '@angular/core';

@Component({
  selector: 'collapsable-container',
  templateUrl: './collapsable-container.component.html',
  styleUrls: ['./collapsable-container.component.css']
})
export class CollapsableContainerComponent {
  @Input()
  public title: string;
}
