import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'nav-menu',
  templateUrl: './navmenu.component.html',
  styleUrls: ['./navmenu.component.scss']
})



export class NavmenuComponent implements OnInit {

 public files: FileController[];
 public cacheFiles: FileController[];
 public summaries: any[];

  ngOnInit() {
  }

}
interface FileController {
  name: string;
}
