import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../_models/user';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  // Pass the user from a parent component. in this case member-list component
  @Input() user: User;

  constructor() { }

  ngOnInit() {
  }

}
