import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.scss']
})
export class NavmenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    //TODO Wyświetlanie plików tylko dla zalogowanych
  }

}
